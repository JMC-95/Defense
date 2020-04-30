using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CanonMissileSpawn : MonoBehaviour
{
    [SerializeField] public GameObject gunBarrel = null;             //포신 프리팹
    [SerializeField] public Transform firePos = null;                //발사 위치

    private List<GameObject> collEnemys = new List<GameObject>();    //사거리 내에 들어온(충돌한) 객체를 담을 리스트
    [SerializeField] private float fireTimeMin = 0f;                 //발사 주기(최소)
    [SerializeField] private float fireTimeMax = 3.0f;               //발사 주기(최대)    //3초마다 쏘겠다

    [SerializeField] private float delayTimeMin = 0f;                 //발사 주기(최소)
    [SerializeField] private float delayTimeMax = 0.2f;               //발사 주기(최대)
    private int Firecount = 0;

    public float theta = 45f;   //각도
    public float gravity;       //중력값
    public float v0;

    void Start()    //초기화
    {
        gravity = 50.0f;
        theta = 45f;
    }

    float Radian(float degree)
    {
        return degree * Mathf.PI / 180.0f;
    }

    void Update()
    {
        //발사주기 갱신
        fireTimeMin += Time.deltaTime;

        //충돌한 객체가 한놈이라도 있을 경우
        if (collEnemys.Count > 0)   
        {
            //첫번째로 충돌한 객체를 타겟으로 넣는다
            GameObject target = collEnemys[0];                
            if (target != null)
            {
                //타겟을 향해 포신이 회전한다 (바라본다)
                gunBarrel.transform.LookAt(target.transform.position);        

                //발사주기가 발사주기 최대치에 도달했으면
                if (fireTimeMin > fireTimeMax)
                {
                    //발사 딜레이주기 갱신
                    delayTimeMin += Time.deltaTime;
                   
                    if (delayTimeMin >= delayTimeMax)
                    {
                        //미사일 생성
                        var aBolt = BulletManager.instance.GetCanonMissile();  
                        var cannon = aBolt.GetComponent<CanonMissile>();
                        aBolt.transform.position = firePos.position;

                        Vector3 velocity = new Vector3(target.transform.position.x - cannon.transform.position.x, 0.0f, target.transform.position.z - cannon.transform.position.z);
                        velocity = Vector3.Normalize(velocity);
                        velocity.y = Mathf.Tan(Radian(theta));
                        velocity = Vector3.Normalize(velocity);
                        var dist = Vector3.Distance(cannon.transform.position, target.transform.position);
                        v0 = Mathf.Sqrt(gravity * dist / Mathf.Sin(Radian(2 * theta)));
                        aBolt.GetComponent<CanonMissile>().SetVelocity(velocity * v0);

                        aBolt.gameObject.SetActive(true);

                        //이펙트 생성
                        var fireEffect = EffectManager.instance.GetCanonFire();  
                        fireEffect.transform.position = this.transform.position + new Vector3(0, 5, 0);
                        fireEffect.SetActive(true);

                        Firecount += 1;
                        delayTimeMin = 0.0f;
                        if (Firecount == 3)
                        {
                            Firecount = 0;
                            fireTimeMin = 0.0f;
                        }
                    }
                }
            }
            for(int i = 0;i < collEnemys.Count; ++i)
            {
                if(!collEnemys[i].activeInHierarchy)
                {
                    collEnemys.Remove(collEnemys[i]);
                }
            }
        }
    }

    //사거리내에 들어온 enemy태그가 붙은 객체를 리스트에 추가
    private void OnTriggerEnter(Collider collision)         
    {
        if (collision.tag == "ENEMY")
        {
            collEnemys.Add(collision.gameObject);
        }
    }

    //사거리를 빠져나간 녀석은 리스트에서 지운다
    private void OnTriggerExit(Collider collision)          
    {
        foreach (GameObject enemy in collEnemys)
        {
            if (enemy == collision.gameObject)
            {
                collEnemys.Remove(enemy);
                break;
            }
        }
    }
}