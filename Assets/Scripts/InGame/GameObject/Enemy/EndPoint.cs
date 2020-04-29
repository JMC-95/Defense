using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private const string enemyTag = "ENEMY";

    public GameObject[] enemyObj;
    public EnemySpawner[] enemySpawnerScript;

    private void Start()
    {
        var middle = GameObject.Find("MiddleSpawner");
        var left = GameObject.Find("LeftSpawner");
        var right = GameObject.Find("RightSpawner");

        enemyObj = new GameObject[3] { middle, left, right };
        enemySpawnerScript = new EnemySpawner[3];

        for (int i = 0; i < enemyObj.Length; ++i)
        {
            enemySpawnerScript[i] = enemyObj[i].GetComponent<EnemySpawner>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag && other.name == "Orc")
        {
            enemySpawnerScript[0].currEnemy -= 1;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == enemyTag && other.name == "Golem")
        {
            enemySpawnerScript[1].currEnemy -= 1;
            other.gameObject.SetActive(false);
        }
    }
}
