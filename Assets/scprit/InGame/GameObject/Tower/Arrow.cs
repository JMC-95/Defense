﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
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
        //trail.Clear();
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

            transform.position += transform.up * m_currentSpeed * Time.deltaTime;               //Y축(머리)으로 가속하여 날아간다
            targetPosition = (m_target.transform.position - transform.position).normalized;     //표적 위치 - 미사일 위치 => 방향과 거리 산출       normarlize로 방향만 남김
            transform.up = Vector3.Lerp(transform.up, targetPosition, 0.5f);                   //Y축(머리)을 해당 방향으로 설정
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("ENEMY"))     //Enemy 태그가 붙은 객체와 충돌했을 때
        {

            //Destroy(collision.gameObject);              //충돌된 객체 삭제
            //Destroy(gameObject);                        //자신을 삭제
            this.gameObject.SetActive(false);
        }
    }
}
