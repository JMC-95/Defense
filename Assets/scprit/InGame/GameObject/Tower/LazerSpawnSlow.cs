using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSpawnSlow : MonoBehaviour
{
    public GameObject Raybody; //레이캐스팅을 쏘는 위치
    public GameObject ScaleDistance; //거리에 따른 스케일 변화를 위한 오브젝트 대상
    public GameObject RayResult; //충돌하는 위치에 출력할 결과
    private List<GameObject> collEnemys = new List<GameObject>();    //사거리 내에 들어온(충돌한) 객체를 담을 리스트
    public bool hitEffect;
    public int layerMask;
    public int damage = 1;
    public float damageTime;
    public bool giveDamage;

    void Start()
    {
        layerMask = (-1) - (1 << LayerMask.NameToLayer("Tower"));
        hitEffect = false;
        damageTime = 0f;
    }

    void Update()
    {
        damageTime += Time.deltaTime;
        if(damageTime >= 1.0f)
        {
            damageTime = 0f;
            giveDamage = true;
        }

        if (hitEffect == true)
        {
            RayResult.SetActive(true);
        }
        if (hitEffect == false)
        {
            RayResult.SetActive(false);
        }

        if (collEnemys.Count > 0)   //충돌한 객체가 한놈이라도 있을 경우
        {
            hitEffect = true;
            GameObject target = collEnemys[0];          //첫번째로 충돌한 객체를 타겟으로 넣는다           
            if (target != null)
            {
                //레이캐스팅 결과정보를 hit라는 이름으로 정한다.
                RaycastHit hit;

                //레이캐스트 쏘는 위치, 방향, 결과값, 최대인식거리
                Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out hit, 200, layerMask );

                //거리에 따른 레이저 스케일 변화
                ScaleDistance.transform.localScale = new Vector3(1, hit.distance + 5, 1);

                //대상을 향해 쏘도록
                ScaleDistance.transform.rotation = Quaternion.LookRotation(hit.normal);

                //레이캐스트가 닿은 곳에 오브젝트를 옮긴다(피격이펙트).
                RayResult.transform.position = hit.point;

                //해당하는 오브젝트의 회전값을 닿은 면적의 노멀방향와 일치시킨다.
                RayResult.transform.rotation = Quaternion.LookRotation(hit.normal);


                var enemyDamage = target.GetComponent<EnemyDamage>();
                {
                    if (giveDamage == true)
                    {
                        enemyDamage.hp -= damage;
                        giveDamage = false;
                    }
                    enemyDamage.hpBarImage.fillAmount = enemyDamage.hp / (float)enemyDamage.initHp;

                    if (enemyDamage.hp <= 0.0f)
                    {
                        Destroy(enemyDamage.hpBar);
                        target.GetComponent<EnemyAI>().state = EnemyAI.State.Die;
                    }
                }                
            }

            if (target.GetComponent<EnemyDamage>().hp <= 0.0f)
            {
                    collEnemys.Remove(target);
                    hitEffect = false;
                    ScaleDistance.transform.localScale = new Vector3(1, 0, 1);      //레이저 길이 초기화                
            }
        }

        if(collEnemys.Count <= 0)
        {
            hitEffect = false;
            ScaleDistance.transform.localScale = new Vector3(1, 0, 1);      //레이저 길이 초기화
        }
    }

    private void OnTriggerEnter(Collider collision)         //사거리내에 들어온 enemy태그가 붙은 객체를 리스트에 추가
    {
        if (collision.tag == "ENEMY")
            collEnemys.Add(collision.gameObject);
    }

    private void OnTriggerExit(Collider collision)          //사거리를 빠져나간 녀석은 리스트에서 지운다
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