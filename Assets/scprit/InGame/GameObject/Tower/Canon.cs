using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject m_target = null;                      //타겟
    public Vector3 targetPosition = Vector3.zero;           //타겟의 위치

    void Update()
    {
        transform.up = GetComponent<Rigidbody>().velocity;                          //Y축(머리)을 해당 방향으로 설정
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("ENEMY"))     //Enemy 태그가 붙은 객체와 충돌했을 때
        {

            //Destroy(collision.gameObject);              //충돌된 객체 삭제
            Destroy(gameObject);                        //자신을 삭제
        }
    }
}
