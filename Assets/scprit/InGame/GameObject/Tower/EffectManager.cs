using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance = null;

    [Header("Object pool")]

    [SerializeField] public GameObject canonEffectPrefab;

    public int maxPool = 10;
    public List<GameObject> canonEffectPool = new List<GameObject>();

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

        CreatePooling();
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < canonEffectPool.Count; i++)
        {
            if (canonEffectPool[i].activeSelf == false)
            {
                return canonEffectPool[i];
            }
        }
        return null;
    }

    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("ObjectPools");

        for (int i = 0; i < maxPool; i++)
        {
            var obj = Instantiate<GameObject>(canonEffectPrefab, objectPools.transform);
            obj.name = "Effect_" + i.ToString("00");

            obj.SetActive(false);

            canonEffectPool.Add(obj);
        }
    }
}