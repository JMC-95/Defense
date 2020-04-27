using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance = null;

    [Header("Object pool")]

    [SerializeField] public GameObject arrowPrefab;
    [SerializeField] public GameObject canonPrefab;

    public int maxPool = 10;
    public List<GameObject> arrowPool = new List<GameObject>();
    public List<GameObject> canonPool = new List<GameObject>();

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        CreatePooling();
    }

    public GameObject GetArrow()
    {
        for (int i = 0; i < arrowPool.Count; i ++)
        {
            if(arrowPool[i].activeSelf == false)
            {
                return arrowPool[i];
            }
        }
        return null;
    }

    public GameObject GetCanon()
    {
        for (int i = 0; i < canonPool.Count; i++)
        {
            if (canonPool[i].activeSelf == false)
            {
                return canonPool[i];
            }
        }
        return null;
    }

        public void CreatePooling()
    {
        GameObject objectPools = new GameObject("ObjectPools");

        for(int i = 0; i < maxPool; i ++)
        {
            var arr = Instantiate<GameObject>(arrowPrefab, objectPools.transform);
            arr.name = "Arrow_" + i.ToString("00");
            arr.SetActive(false);
            arrowPool.Add(arr);

            var can = Instantiate<GameObject>(canonPrefab, objectPools.transform);
            can.name = "Canon_" + i.ToString("00");
            can.SetActive(false);
            canonPool.Add(can);
        }
    }
}
