using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMageTowerCrystal : MonoBehaviour
{
    public GameObject Crystal;
    public float moveTime;
    public bool moveUp;

    void Start()
    {
        moveTime = 0f;
        moveUp = true;
    }

    void Update()
    {
        if(moveTime >= 0.5)
        {
            moveUp = false;            
        }
        if (moveTime <= 0)
        {
            moveUp = true;
        }

        if(moveUp == true)
        {
            moveTime += Time.deltaTime;
            Crystal.transform.position += new Vector3(0, 0.005f, 0);
        }

        if(moveUp == false)
        {
            moveTime -= Time.deltaTime;
            Crystal.transform.position += new Vector3(0, -0.005f, 0);
        }
    }


}
