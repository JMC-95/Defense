using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSpawnSlow : MonoBehaviour
{
    public GameObject FireEffect;
    public GameObject RayResult; //충돌하는 위치에 출력할 결과
    private List<GameObject> collEnemys = new List<GameObject>();    //사거리 내에 들어온(충돌한) 객체를 담을 리스트    

    public float damage = 0;

    void Update()
    {
        if (collEnemys.Count > 0)   //충돌한 객체가 한놈이라도 있을 경우
        {
            RayResult.SetActive(true);
            FireEffect.SetActive(true);
            GameObject target = collEnemys[0];          //첫번째로 충돌한 객체를 타겟으로 넣는다
            var enemyMove = target.GetComponent<EnemyMove>();

            if (target.GetComponent<EnemyDamage>().CurHp > 0.0f)
            {
                //레이캐스트가 닿은 곳에 오브젝트를 옮긴다(피격이펙트).
                RayResult.transform.position = target.transform.position;

                var enemyDamage = target.GetComponent<EnemyDamage>();
                

                enemyDamage.CurHp -= damage * Time.deltaTime;
                enemyDamage.hpBarImage.fillAmount = enemyDamage.CurHp / (float)enemyDamage.InitHp;
                enemyMove.SetSlow(1);

                if (enemyDamage.CurHp <= 0.0f)
                {
                    Destroy(enemyDamage.hpBar);
                    target.GetComponent<EnemyAI>().state = EnemyAI.State.Die;
                    collEnemys.Remove(target);
                    enemyMove.ResetSlow();
                }
            }
            if (target.GetComponent<EnemyDamage>().CurHp <= 0.0f)
            {
                enemyMove.ResetSlow();
                collEnemys.Remove(target);
                RayResult.SetActive(false);
                FireEffect.SetActive(false);
            }
        }
        if (collEnemys.Count <= 0)
        {
            RayResult.SetActive(false);
            FireEffect.SetActive(false);
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
                enemy.GetComponent<EnemyMove>().ResetSlow();
                collEnemys.Remove(enemy);
                break;
            }
        }
    }
}