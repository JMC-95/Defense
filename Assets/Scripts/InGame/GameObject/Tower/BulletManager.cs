using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance = null;

    //[Header("Object pool")]
    [SerializeField] public GameObject arrowPrefab;
    [SerializeField] public GameObject FireArrowPrefab;
    [SerializeField] public GameObject canonBallPrefab;
    [SerializeField] public GameObject canonLocketPrefab;
    [SerializeField] public GameObject canonMissilePrefab;

    public int maxPool = 10;
    public List<GameObject> arrowPool = new List<GameObject>();
    public List<GameObject> fireArrowPool = new List<GameObject>();
    public List<GameObject> canonBallPool = new List<GameObject>();
    public List<GameObject> canonLocketPool = new List<GameObject>();
    public List<GameObject> canonMissilePool = new List<GameObject>();

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
    //==================================================================
    //==                        A R R O W                             ==
    //==================================================================
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
    public GameObject GetFireArrow()
    {
        for (int i = 0; i < fireArrowPool.Count; i++)
        {
            if (fireArrowPool[i].activeSelf == false)
            {
                return fireArrowPool[i];
            }
        }
        return null;
    }
    //==================================================================
    //==                        C A N O N                             ==
    //==================================================================
    public GameObject GetCanonBall()
    {
        for (int i = 0; i < canonBallPool.Count; i++)
        {
            if (canonBallPool[i].activeSelf == false)
            {
                return canonBallPool[i];
            }
        }
        return null;
    }
    public GameObject GetCanonLocket()
    {
        for (int i = 0; i < canonLocketPool.Count; i++)
        {
            if (canonLocketPool[i].activeSelf == false)
            {
                return canonLocketPool[i];
            }
        }
        return null;
    }
    public GameObject GetCanonMissile()
    {
        for (int i = 0; i < canonMissilePool.Count; i++)
        {
            if (canonMissilePool[i].activeSelf == false)
            {
                return canonMissilePool[i];
            }
        }
        return null;
    }

    //  Pooling

    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("ObjectPools");

        for(int i = 0; i < maxPool; i ++)
        {
            var arr = Instantiate<GameObject>(arrowPrefab, objectPools.transform);
            arr.name = "Arrow_" + i.ToString("00");
            arr.SetActive(false);
            arrowPool.Add(arr);

            var arrFr = Instantiate<GameObject>(FireArrowPrefab, objectPools.transform);
            arrFr.name = "FireArrow_" + i.ToString("00");
            arrFr.SetActive(false);
            fireArrowPool.Add(arrFr);

            var canB = Instantiate<GameObject>(canonBallPrefab, objectPools.transform);
            canB.name = "CanonBall_" + i.ToString("00");
            canB.SetActive(false);
            canonBallPool.Add(canB);

            var canL = Instantiate<GameObject>(canonLocketPrefab, objectPools.transform);
            canL.name = "CanonLocket_" + i.ToString("00");
            canL.SetActive(false);
            canonLocketPool.Add(canL);

            var canM = Instantiate<GameObject>(canonMissilePrefab, objectPools.transform);
            canM.name = "CanonMissile_" + i.ToString("00");
            canM.SetActive(false);
            canonMissilePool.Add(canM);
        }
    }
}