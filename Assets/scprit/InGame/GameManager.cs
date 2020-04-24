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

    public int enemyType;
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
        enemyType = 0;

        StartCoroutine(enemySpawner[enemyType].CreateEnemy());
    }

    void Update()
    {
        if (!isWaveEnd)
        {
            if (enemySpawner[enemyType].genCount >= enemySpawner[enemyType].genCountLimit)
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
                isWaveEnd = false;

                enemySpawner[enemyType].genCount = 0;
                pastTime = 0.0f;
                waveCount += 1;
                enemyType += 1;

                if (enemyType > 2) enemyType = 0;
                StartCoroutine(enemySpawner[enemyType].CreateEnemy());
            }
        }
    }
}
