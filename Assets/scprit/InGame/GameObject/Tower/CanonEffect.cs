using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonEffect : MonoBehaviour
{
    public float eraseTime;
    private Transform tr;

    void Awake()
    {
        tr = GetComponent<Transform>();
    }

        void OnEnable()
    {
        eraseTime = 0f;
    }

    void OnDisable()
    {
        eraseTime = 0f;
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
    }

    void Update()
    {
        if(eraseTime != 10.0f)
        {
            eraseTime++;
        }

        if(eraseTime >= 10.0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
