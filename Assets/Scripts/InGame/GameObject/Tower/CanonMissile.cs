using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonMissile : MonoBehaviour
{
    public Rigidbody canonRigidBody;
    public float gravity;
    public Vector3 velocity;
    public int damage = 30;

    public void SetVelocity(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    private void Start()
    {
        canonRigidBody = GetComponent<Rigidbody>();
        gravity = 50.0f;
    }

    void Update()
    {
        velocity = new Vector3(velocity.x, velocity.y - gravity * Time.deltaTime, velocity.z);
        canonRigidBody.transform.position += velocity * Time.deltaTime;
        transform.up = velocity;                          //Y축(머리)을 해당 방향으로 설정
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "GROUND")
        {
            //포탄 삭제
            this.gameObject.SetActive(false);

            //이펙트 생성
            var hitEffect = EffectManager.instance.GetCanonHit(); 
            hitEffect.transform.position = this.transform.position;
            hitEffect.SetActive(true);

            //중앙 데미지
            Collider[] hitsCol = Physics.OverlapSphere(transform.position, 10.0f);
            //범위 데미지
            Collider[] hitsSplashCol = Physics.OverlapSphere(transform.position, 20.0f);

            //원에 충돌한 녀석을 탐지한다
            foreach (Collider hit in hitsCol)
            {
                if (hit.gameObject.tag == "ENEMY")
                {
                    var enemyDamage = hit.GetComponent<EnemyDamage>();
                    {
                        enemyDamage.CurHp -= damage;

                        enemyDamage.hpBarImage.fillAmount = enemyDamage.CurHp / (float)enemyDamage.InitHp;

                        if (enemyDamage.CurHp <= 0.0f)
                        {
                            Destroy(enemyDamage.hpBar);
                            hit.GetComponent<EnemyAI>().state = EnemyAI.State.Die;
                        }
                    }
                }
            }

            foreach (Collider hit in hitsSplashCol)
            {
                if (hit.gameObject.tag == "ENEMY")
                {
                    var enemyDamage = hit.GetComponent<EnemyDamage>();
                    {
                        enemyDamage.CurHp -= damage;

                        enemyDamage.hpBarImage.fillAmount = enemyDamage.CurHp / (float)enemyDamage.InitHp;

                        if (enemyDamage.CurHp <= 0.0f)
                        {
                            Destroy(enemyDamage.hpBar);
                            hit.GetComponent<EnemyAI>().state = EnemyAI.State.Die;
                        }
                    }
                }
            }
        }
    }
}