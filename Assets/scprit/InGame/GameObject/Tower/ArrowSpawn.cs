using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawn : MonoBehaviour
{
    [SerializeField] public GameObject bullet = null;                //총알 프리팹
    [SerializeField] public GameObject shooter = null;               //사수 프리팹
    [SerializeField] public Transform firePos = null;                //발사 위치

    private List<GameObject> collEnemys = new List<GameObject>();    //사거리 내에 들어온(충돌한) 객체를 담을 리스트
    [SerializeField] private float fireTimeMin = 0f;                 //발사 주기(최소)
    [SerializeField] private float fireTimeMax = 1.0f;               //발사 주기(최대)    //1초마다 쏘겠다

    void Update()
    {
        fireTimeMin += Time.deltaTime;  //발사주기 갱신
       
        if (collEnemys.Count > 0)   //충돌한 객체가 한놈이라도 있을 경우
        {
            GameObject target = collEnemys[0];          //첫번째로 충돌한 객체를 타겟으로 넣는다           
            if (target != null)
            {                
                shooter.transform.LookAt(target.transform.position);        //타겟을 향해 사수가 회전한다 (바라본다)

                if (fireTimeMin > fireTimeMax)
                {
                    fireTimeMin = 0.0f;
         
                    var arrow = BulletManager.instance.GetBullet();  //미사일 생성
                    if (arrow != null)
                    {
                        arrow.transform.position = firePos.position;                    //미사일 생성 포지션
                        arrow.transform.rotation = bullet.transform.rotation;           //미사일 생성 회전값
                        arrow.GetComponent<Arrow>().m_target = target;                  //미사일에게 타겟 전달
                        //arrow.GetComponent<Rigidbody>().velocity = Vector3.up * 5f;     //미사일에게 위로 뜨게하는 힘을 가함
                        arrow.SetActive(true);
                    }  
                }
            }
            if (target == null)     //타겟이 없으면 리스트의 첫번째에 담은 녀석을 지운다
            {
                collEnemys.Remove(target);
            }
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