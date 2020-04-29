using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Create Info")]
    private float createTime = 1.5f;    //몬스터 생성 시간

    private int enemyNum;
    public int currEnemy;
    public int maxEnemy;
    public int genCount;                //한 왜이브에 생성된 오브젝트의 수
    public int genCountLimit;           //한 웨이브에 생성될수 있는 오브젝트의 수

    string[] enemyStrList;

    [Header("Object Pool")]
    //public GameObject[] enemyPrefabs;
    private int maxPool = 10;           //오브젝트 풀내 오브젝트의 수

    private List<GameObject> enemyPool = new List<GameObject>();
    GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Get();

        if (gameManager.enemyPrefabs[0].name == "Orc")
            enemyNum = 0;
        if (gameManager.enemyPrefabs[1].name == "Golem")
            enemyNum = 1;

        CreatePooling();
    }

    void Start()
    {
        currEnemy = 0;
        maxEnemy = 60;
        genCount = 0;
        genCountLimit = 10;
    }

    public IEnumerator CreateEnemy()
    {
        while (!GameManager.instance.isGameOver && !GameManager.instance.isWaveEnd)
        {
            int enemyCount = currEnemy;

            if (enemyCount < maxEnemy && genCount < genCountLimit)
            {
                yield return new WaitForSeconds(createTime);

                if (GetEnemy() != null)
                {
                    var enemy = GetEnemy();

                    if (enemy.name == "Orc" || enemy.name == "Golem")
                    {
                        var enemyDamage = enemy.GetComponent<EnemyDamage>();
                        enemyDamage.SetHpBar();
                    }
                    SetToUnit(enemy);
                    currEnemy += 1;
                    genCount += 1;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    public void SetToUnit(GameObject unitObj)
    {
        unitObj.transform.position = transform.position;
        unitObj.transform.rotation = transform.rotation;
        unitObj.SetActive(true);
        unitObj.GetComponent<EnemyMove>().Init(enemyNum);
    }

    public GameObject GetEnemy()
    {
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (enemyPool[i].activeSelf == false)
            {
                return enemyPool[i];
            }
        }

        return null;
    }

    public void CreatePooling()
    {
        var waveCount = gameManager.waveCount;
        GameObject objectPools = new GameObject("ObjectPools");

        if (gameObject.name == "MiddleSpawner")
        {
            if (waveCount == 0)
            {
                for (int i = 0; i < maxPool; i++)
                {
                    var obj = Instantiate<GameObject>(gameManager.enemyPrefabs[0], objectPools.transform);

                    obj.name = "Orc";
                    obj.SetActive(false);
                    enemyPool.Add(obj);
                }
            }
        }

        if (gameObject.name == "LeftSpawner")
        {
            if (waveCount == 1)
            {
                for (int i = 0; i < maxPool; i++)
                {
                    var obj = Instantiate<GameObject>(gameManager.enemyPrefabs[1], objectPools.transform);

                    obj.name = "Golem";
                    obj.SetActive(false);
                    enemyPool.Add(obj);
                }
            }
        }

        if (gameObject.name == "RightSpawner")
        {
            if (waveCount == 0)
            {
                for (int i = 0; i < maxPool; i++)
                {
                    var obj = Instantiate<GameObject>(gameManager.enemyPrefabs[0], objectPools.transform);

                    obj.name = "Orc";
                    obj.SetActive(false);
                    enemyPool.Add(obj);
                }
            }
        }

        //GameObject objectPools = new GameObject("ObjectPools");

        //var gameManager = GameManager.Get();

        //if (gameManager.waveCount == 0)
        //    enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy/Orc") as GameObject;
        //else if (gameManager.waveCount == 1)
        //    enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy/Golem") as GameObject;


        //for (int i = 0; i < maxPool; i++)
        //{
        //    var obj = Instantiate<GameObject>(enemyPrefab, objectPools.transform);

        //    if (enemyPrefab.name == "Orc") obj.name = "Orc";
        //    else if (enemyPrefab.name == "Golem") obj.name = "Golem";

        //    obj.SetActive(false);
        //    enemyPool.Add(obj);
        //}
        //else if (enemyPrefab.name == "Mage")
        //{
        //    for (int i = 0; i < maxPool; i++)
        //    {
        //        var obj = Instantiate<GameObject>(enemyPrefab, objectPools.transform);

        //        obj.name = "Mage";
        //        obj.SetActive(false);
        //        enemyPool.Add(obj);
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < maxPool; i++)
        //    {
        //        var obj = Instantiate<GameObject>(enemyPrefab, objectPools.transform);

        //        obj.name = "Swordman";
        //        obj.SetActive(false);
        //        enemyPool.Add(obj);
        //    }
        //}
    }
}
