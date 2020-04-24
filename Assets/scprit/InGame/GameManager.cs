using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject[] enemyObj;
    public EnemySpawner[] enemySpawner;

    private float pastTime = 0.0f;
    private float waveDelay = 3.0f;

    public int waveCount;
    public bool isWaveEnd = false;
    public bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        var archer = GameObject.Find("ArcherSpawner");
        var mage = GameObject.Find("MageSpawner");
        var swordman = GameObject.Find("SwordmanSpawner");

        enemyObj = new GameObject[3] { archer, mage, swordman };
        enemySpawner = new EnemySpawner[3];

        for (int i = 0; i < enemyObj.Length; ++i)
        {
            enemySpawner[i] = enemyObj[i].GetComponent<EnemySpawner>();
        }
        waveCount = 0;

        StartCoroutine(enemySpawner[1].CreateEnemy());
    }

    void Update()
    {
        if (!isWaveEnd)
        {
            if (enemySpawner[0].genCount >= enemySpawner[0].genCountLimit)
            {
                if (GameObject.FindGameObjectsWithTag("ENEMY").Length == 0)
                {
                    isWaveEnd = true;
                }
            }
        }
        else
        {
            pastTime += Time.deltaTime;

            if (pastTime > waveDelay)
            {
                waveCount += 1;
                enemySpawner[0].genCount = 0;
                isWaveEnd = false;
                pastTime = 0.0f;
            }
        }
    }
}
