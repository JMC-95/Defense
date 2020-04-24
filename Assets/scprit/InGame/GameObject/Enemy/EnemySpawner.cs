using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Create Info")]
    private float createTime = 1.5f;    //몬스터 생성 시간

    public int enemyNum;
    public int currEnemy;
    public int maxEnemy;
    public int genCount;                //한 왜이브에 생성된 오브젝트의 수 
    public int genCountLimit;           //한 웨이브에 생성될수 있는 오브젝트의 수

    [Header("Object Pool")]
    public GameObject enemyPrefab;

    private int maxPool = 10;           //오브젝트 풀내 오브젝트의 수

    private List<GameObject> enemyPool = new List<GameObject>();

    void Awake()
    {
        CreatePooling();
    }

    void Start()
    {
        currEnemy = 0;
        maxEnemy = 60;
        genCount = 0;
        genCountLimit = 5;

        if (enemyPrefab.name == "Archer") enemyNum = 0;
        else if (enemyPrefab.name == "Mage") enemyNum = 1;
        else enemyNum = 2;

        //StartCoroutine(this.CreateEnemy());
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
        GameObject objectPools = new GameObject("ObjectPools");

        if (enemyPrefab.name == "Archer")
        {
            for (int i = 0; i < maxPool; i++)
            {
                var obj = Instantiate<GameObject>(enemyPrefab, objectPools.transform);

                obj.name = "Archer";
                obj.SetActive(false);
                enemyPool.Add(obj);
            }
        }
        else if (enemyPrefab.name == "Mage")
        {
            for (int i = 0; i < maxPool; i++)
            {
                var obj = Instantiate<GameObject>(enemyPrefab, objectPools.transform);

                obj.name = "Mage";
                obj.SetActive(false);
                enemyPool.Add(obj);
            }
        }
        else
        {
            for (int i = 0; i < maxPool; i++)
            {
                var obj = Instantiate<GameObject>(enemyPrefab, objectPools.transform);

                obj.name = "Swordman";
                obj.SetActive(false);
                enemyPool.Add(obj);
            }
        }
    }
}
