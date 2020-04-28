using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Walk,
        Die
    }

    public State state = State.Walk;

    private Animator animator;
    private EnemyMove enemyMove;
    private WaitForSeconds ws;

    public bool isDie = false;

    private readonly int hashDie = Animator.StringToHash("Die");

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
        ws = new WaitForSeconds(0.1f);
    }

    void OnEnable()
    {
        StartCoroutine(Action());
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;

            switch (state)
            {
                case State.Walk:
                    break;
                case State.Die:
                    isDie = true;
                    enemyMove.Stop();
                    animator.SetTrigger(hashDie);
                    break;
            }
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
        isDie = false;
    }
}
