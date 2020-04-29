using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 25;
    public GameObject m_target = null;                      //타겟
    public Vector3 targetPosition = Vector3.zero;           //타겟의 위치

    [SerializeField] float m_speed = 0f;                    //최고 속도
    [SerializeField] float m_currentSpeed = 0f;             //현재 속도

    private bool activeArrow = false;
    private Transform tr;
    private Rigidbody rb;
    private TrailRenderer trail;

    void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
    }

    void OnEnable()
    {
        activeArrow = true;
    }

    void OnDisable()
    {
        activeArrow = false;
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rb.Sleep();
    }

    void Update()
    {
        if (activeArrow == true)
        {
            if (m_currentSpeed <= m_speed)                      //현재 속도가 최고 속도 이하일 경우 
                m_currentSpeed += m_speed * Time.deltaTime;     //현재 속도를 증가시킨다

            //transform.position += transform.up * m_currentSpeed * Time.deltaTime;               //Y축(머리)으로 가속하여 날아간다
            transform.position += transform.up * m_speed * Time.deltaTime;               //Y축(머리)으로 가속하여 날아간다
            targetPosition = (m_target.transform.GetChild(0).transform.position - transform.position).normalized;     //표적 위치 - 미사일 위치 => 방향과 거리 산출       normarlize로 방향만 남김
            transform.up = Vector3.Lerp(transform.up, targetPosition, 0.5f);                   //Y축(머리)을 해당 방향으로 설정            

            if (m_target.GetComponent<EnemyDamage>().CurHp <= 0 || !m_target.activeInHierarchy)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "ENEMY")     //Enemy 태그가 붙은 객체와 충돌했을 때
        {
            var enemyDamage = coll.gameObject.GetComponent<EnemyDamage>();
            this.gameObject.SetActive(false);
            enemyDamage.CurHp -= damage;
            enemyDamage.hpBarImage.fillAmount = enemyDamage.CurHp / (float)enemyDamage.InitHp;
            if (enemyDamage.CurHp <= 0)
            {
                enemyDamage.Die();
            }
        }
    }
}
