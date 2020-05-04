using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawnTwoShooter : MonoBehaviour
{
    [SerializeField] public GameObject shooterRight = null;               //사수 프리팹
    [SerializeField] public GameObject shooterLeft = null;               //사수 프리팹
    [SerializeField] public Transform firePosRight = null;                //발사 위치
    [SerializeField] public Transform firePosLeft = null;                //발사 위치

    private List<GameObject> collEnemys = new List<GameObject>();    //사거리 내에 들어온(충돌한) 객체를 담을 리스트
    [SerializeField] private float fireTimeMin = 0f;                 //발사 주기(최소)
    [SerializeField] private float fireTimeMax = 1.0f;               //발사 주기(최대)    //1초마다 쏘겠다
    [SerializeField] public int damage;                              //데미지
    private Vector3 targetPositionRight;
    private Vector3 targetPositionLeft;

    void Update()
    {
        fireTimeMin += Time.deltaTime;  //발사주기 갱신
       
        if (collEnemys.Count > 0)   //충돌한 객체가 한놈이라도 있을 경우
        {
            GameObject target = collEnemys[0];          //첫번째로 충돌한 객체를 타겟으로 넣는다 
            if (target != null)
            {
                targetPositionRight = new Vector3(target.transform.position.x, shooterRight.transform.position.y, target.transform.position.z);
                targetPositionLeft = new Vector3(target.transform.position.x, shooterLeft.transform.position.y, target.transform.position.z);
                shooterLeft.transform.LookAt(targetPositionLeft);        //타겟을 향해 사수가 회전한다 (바라본다)
                shooterRight.transform.LookAt(targetPositionRight);        //타겟을 향해 사수가 회전한다 (바라본다)

                if (fireTimeMin > fireTimeMax)
                {
                    fireTimeMin = 0.0f;

                    SoundManager.Instance.PlaySound(Type.Audio.ArrowShot);

                    var arrowRight = BulletManager.instance.GetArrow();  //미사일 생성
                    if (arrowRight != null)
                    {
                        arrowRight.transform.position = firePosRight.position;                    //미사일 생성 포지션
                        arrowRight.transform.rotation = target.transform.rotation;           //미사일 생성 회전값
                        arrowRight.GetComponent<Arrow>().m_target = target;                  //미사일에게 타겟 전달
                        arrowRight.GetComponent<Arrow>().damage = damage;                  
                        arrowRight.SetActive(true);
                    }

                    var arrowLeft = BulletManager.instance.GetArrow();  //미사일 생성
                    if (arrowLeft != null)
                    {
                        arrowLeft.transform.position = firePosLeft.position;                    //미사일 생성 포지션
                        arrowLeft.transform.rotation = target.transform.rotation;           //미사일 생성 회전값
                        arrowLeft.GetComponent<Arrow>().m_target = target;                  //미사일에게 타겟 전달
                        arrowLeft.GetComponent<Arrow>().damage = damage;                  
                        arrowLeft.SetActive(true);
                    }
                }
            }
            for (int i = 0; i < collEnemys.Count; ++i)
            {
                if (!collEnemys[i].activeInHierarchy)
                {
                    collEnemys.Remove(collEnemys[i]);
                }
            }
        }

        if (collEnemys.Count <= 0)   //충돌한 객체가 하나도 없으면 사수 바라보기를 정면으로
        {           
            shooterRight.transform.rotation = Quaternion.identity;
            shooterLeft.transform.rotation = Quaternion.identity;
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