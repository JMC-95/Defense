using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Enemy Create Info")]
    private float createTime = 1.5f;    //몬스터 생성 시간

    public int currEnemy;
    public int maxEnemy;
    public int genCount;                //한 왜이브에 생성된 오브젝트의 수
    public int genCountLimit;           //한 웨이브에 생성될수 있는 오브젝트의 수

    string[] enemyStrList;

    [Header("Object Pool")]
    private int maxPool = 50;           //오브젝트 풀내 오브젝트의 수
    public Dictionary<string, List<GameObject>> enemyPools;

    List<GenInfomation> curWaveEnemyList = new List<GenInfomation>();

    void Awake()
    {
        gameManager = GameManager.Get();
        gameManager.SetGenInfomation();
        CreatePooling();
    }

    void Start()
    {
        currEnemy = 0;
        maxEnemy = 60;
        genCount = 0;
    }

    public void ResetGenInfo()
    {
        genCount = 0;
        genCountLimit = 0;
        curWaveEnemyList.Clear();

        curWaveEnemyList = gameManager.GenInfoMation[GameManager.instance.curRound][GameManager.instance.curWave];

        foreach (var genInfo in curWaveEnemyList)
        {
            genCountLimit += genInfo.GenCount;
        }
    }

    public IEnumerator CreateEnemy()
    {
        while (!GameManager.instance.isGameOver && !GameManager.instance.isWaveEnd)
        {
            if (genCount < genCountLimit)
            {
                yield return new WaitForSeconds(createTime);

                for (int i = 0; i < curWaveEnemyList.Count; ++i)
                {
                    var enemy = GetEnemy(curWaveEnemyList[i].EnemyName);
                    var enemyDamage = enemy.GetComponent<EnemyDamage>();

                    enemyDamage.isDie = false;
                    enemyDamage.SetHpBar(curWaveEnemyList[i].Hp);
                    enemy.GetComponent<EnemyAI>().state = EnemyAI.State.Walk;

                    SetToUnit(enemy, curWaveEnemyList[i].Line, curWaveEnemyList[i].Speed, curWaveEnemyList[i].Gold);
                    currEnemy += 1;
                    genCount += 1;
                }
            }
            else
            {
                if(GameManager.instance.curWave == 5)
                {
                    GameManager.instance.roundEnd = true;
                }
                yield return null;
            }
        }
    }

    public void SetToUnit(GameObject unitObj, int line, int speed, int gold)
    {
        unitObj.transform.position = transform.GetChild(line).position;
        unitObj.transform.rotation = transform.GetChild(line).rotation;
        unitObj.SetActive(true);
        unitObj.GetComponent<EnemyMove>().Init(line, speed, gold);
    }

    public GameObject GetEnemy(string enemyName)
    {
        var list = enemyPools[enemyName];

        for (int i = 0; i < list.Count; ++i)
        {
            if (!list[i].activeInHierarchy)
            {
                return list[i];
            }
        }

        return null;
    }

    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("ObjectPools");

        enemyPools = new Dictionary<string, List<GameObject>>();
        gameManager.LoadEnemyPrefabs();

        for (int i = 0; i < Type.Enemy.Max; ++i)
        {
            var enemyName = Type.Enemy.ToString(i);

            List<GameObject> enemyPool = new List<GameObject>();

            for (int j = 0; j < maxPool; ++j)
            {
                var enemy = Instantiate<GameObject>(gameManager.enemyPrefabs[i], objectPools.transform) as GameObject;

                enemyPool.Add(enemy);
                enemy.SetActive(false);
            }

            enemyPools.Add(enemyName, enemyPool);
        }
    }
}
