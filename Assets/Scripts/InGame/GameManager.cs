using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject[] enemyObj;
    public EnemySpawner[] enemySpawner;
    public UiManager uiManagerScript;

    private float pastTime = 0.0f;
    private float waveDelay = 3.0f;
    public int Gold = 500;

    public int enemyType;
    public int waveCount;
    public int curRound;
    public int roundMax;

    public bool isWaveEnd = false;
    public bool isGameOver = false;

    
    static public GameManager Get()
    {
        if (!instance)
        {
            return instance = new GameManager();
        }

        return instance;
    }

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
        enemyType = 0;
        waveCount = 0;
        curRound = 0;
        roundMax = 6;

        var archer = GameObject.Find("ArcherSpawner");
        var mage = GameObject.Find("MageSpawner");
        var swordman = GameObject.Find("SwordmanSpawner");
        var uiManager = GameObject.Find("UiManager");

        uiManagerScript = uiManager.GetComponent<UiManager>();
        enemyObj = new GameObject[3] { archer, mage, swordman };
        enemySpawner = new EnemySpawner[3];

        for (int i = 0; i < enemyObj.Length; ++i)
        {
            enemySpawner[i] = enemyObj[i].GetComponent<EnemySpawner>();
        }

        StartCoroutine(enemySpawner[enemyType].CreateEnemy());
    }

    public void UseGold(int cost)
    {
        Gold -= cost;
        uiManagerScript.UpdateGoldText();
    }


    void Update()
    {
        if (!isGameOver && !isWaveEnd)
        {
            if (enemyType < 3)
            {
                if (enemySpawner[enemyType].genCount >= enemySpawner[enemyType].genCountLimit)
                {
                    if (GameObject.FindGameObjectsWithTag("ENEMY").Length == 0)
                    {
                        isWaveEnd = true;
                    }
                }
            }
            else if (enemyType == 3)
            {
                if (enemySpawner[0].genCount >= enemySpawner[0].genCountLimit &&
                    enemySpawner[1].genCount >= enemySpawner[1].genCountLimit)
                {
                    if (GameObject.FindGameObjectsWithTag("ENEMY").Length == 0)
                    {
                        isWaveEnd = true;
                    }
                }
            }
            else if (enemyType == 4)
            {
                if (enemySpawner[0].genCount >= enemySpawner[1].genCountLimit &&
                    enemySpawner[1].genCount >= enemySpawner[2].genCountLimit)
                {
                    if (GameObject.FindGameObjectsWithTag("ENEMY").Length == 0)
                    {
                        isWaveEnd = true;
                    }
                }
            }
            else if (enemyType == 5)
            {
                if (enemySpawner[0].genCount >= enemySpawner[0].genCountLimit &&
                    enemySpawner[1].genCount >= enemySpawner[2].genCountLimit)
                {
                    if (GameObject.FindGameObjectsWithTag("ENEMY").Length == 0)
                    {
                        isWaveEnd = true;
                    }
                }
            }
            else if (enemyType == 6)
            {
                if (enemySpawner[0].genCount >= enemySpawner[0].genCountLimit &&
                    enemySpawner[1].genCount >= enemySpawner[1].genCountLimit &&
                    enemySpawner[1].genCount >= enemySpawner[2].genCountLimit)
                {
                    if (GameObject.FindGameObjectsWithTag("ENEMY").Length == 0)
                    {
                        isGameOver = true;
                        Debug.Log("Game End!!");
                    }
                }
            }
        }
        else
        {
            pastTime += Time.deltaTime;

            if (pastTime > waveDelay)
            {
                if (curRound < roundMax)
                {
                    pastTime = 0.0f;
                    enemyType += 1;
                    waveCount += 1;
                    curRound += 1;

                    if (enemyType < 3)
                    {
                        isWaveEnd = false;
                        enemySpawner[enemyType].genCount = 0;
                        StartCoroutine(enemySpawner[enemyType].CreateEnemy());
                    }
                    else if (enemyType == 3)
                    {
                        isWaveEnd = false;
                        enemySpawner[0].genCount = 0;
                        enemySpawner[1].genCount = 0;
                        StartCoroutine(enemySpawner[0].CreateEnemy());
                        StartCoroutine(enemySpawner[1].CreateEnemy());
                    }
                    else if (enemyType == 4)
                    {
                        isWaveEnd = false;
                        enemySpawner[1].genCount = 0;
                        enemySpawner[2].genCount = 0;
                        StartCoroutine(enemySpawner[1].CreateEnemy());
                        StartCoroutine(enemySpawner[2].CreateEnemy());
                    }
                    else if (enemyType == 5)
                    {
                        isWaveEnd = false;
                        enemySpawner[0].genCount = 0;
                        enemySpawner[2].genCount = 0;
                        StartCoroutine(enemySpawner[0].CreateEnemy());
                        StartCoroutine(enemySpawner[2].CreateEnemy());
                    }
                    else if (enemyType == 6)
                    {
                        isWaveEnd = false;
                        enemySpawner[0].genCount = 0;
                        enemySpawner[1].genCount = 0;
                        enemySpawner[2].genCount = 0;
                        StartCoroutine(enemySpawner[0].CreateEnemy());
                        StartCoroutine(enemySpawner[1].CreateEnemy());
                        StartCoroutine(enemySpawner[2].CreateEnemy());
                    }
                }
            }
        }
    }
}
