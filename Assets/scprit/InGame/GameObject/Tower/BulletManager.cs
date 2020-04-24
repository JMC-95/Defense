using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance = null;

    [Header("Object pool")]

    [SerializeField] public GameObject bulletPrefab;

    public int maxPool = 10;
    public List<GameObject> bulletPool = new List<GameObject>();

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

    public GameObject GetBullet()
    {
        for (int i = 0; i < bulletPool.Count; i ++)
        {
            if(bulletPool[i].activeSelf == false)
            {
                return bulletPool[i];
            }
        }
        return null;
    }

    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("ObjectPools");

        for(int i = 0; i < maxPool; i ++)
        {
            var obj = Instantiate<GameObject>(bulletPrefab, objectPools.transform);
            obj.name = "Bullet_" + i.ToString("00");

            obj.SetActive(false);

            bulletPool.Add(obj);
        }
    }
}
