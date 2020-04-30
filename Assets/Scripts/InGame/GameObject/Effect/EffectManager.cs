using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance = null;

    [Header("Object pool")]

    [SerializeField] public GameObject canonHitEffectPrefab;
    [SerializeField] public GameObject canonFireEffectPrefab;

    public int maxPool = 10;
    public List<GameObject> canonHitEffectPool = new List<GameObject>();
    public List<GameObject> canonFireEffectPool = new List<GameObject>();

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

    public GameObject GetCanonHit()
    {
        for (int i = 0; i < canonHitEffectPool.Count; i++)
        {
            if (canonHitEffectPool[i].activeSelf == false)
            {
                return canonHitEffectPool[i];
            }
        }
        return null;
    }
    public GameObject GetCanonFire()
    {
        for (int i = 0; i < canonFireEffectPool.Count; i++)
        {
            if (canonFireEffectPool[i].activeSelf == false)
            {
                return canonFireEffectPool[i];
            }
        }
        return null;
    }


    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("ObjectPools");

        for (int i = 0; i < maxPool; i++)
        {
            var canHit = Instantiate<GameObject>(canonHitEffectPrefab, objectPools.transform);
            canHit.name = "HitEffect_" + i.ToString("00");
            canHit.SetActive(false);
            canonHitEffectPool.Add(canHit);

            var canFire = Instantiate<GameObject>(canonFireEffectPrefab, objectPools.transform);
            canFire.name = "FireEffect_" + i.ToString("00");
            canFire.SetActive(false);
            canonFireEffectPool.Add(canFire);
        }
    }
}