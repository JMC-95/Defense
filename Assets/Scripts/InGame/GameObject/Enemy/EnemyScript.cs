using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int[] eachTurnHpMax;

    void Awake()
    {
        eachTurnHpMax = new int[10];

        for (int i = 0; i < GameManager.Get().roundMax; ++i)
        {
            eachTurnHpMax[i] = 100 + i * 10;
        }
    }

    void Update()
    {
        
    }
}
