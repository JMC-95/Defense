using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerUiScript : MonoBehaviour
{
    ObjectSelector objectSelector;
    GameObject towerSelector;
    GameManager gameManager;

    public void Start()
    {
        objectSelector = GameObject.Find("MainCamera").GetComponent<ObjectSelector>();
        var canvas = GameObject.Find("Canvas");
        towerSelector = canvas.transform.GetChild(1).gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Sell()
    {
        Debug.Log("Sell Tower");
        towerSelector.SetActive(false);
        objectSelector.selectedBuildingPoint = null;
        objectSelector.selectedTower = null;
        objectSelector.selectedBuildingPoint.GetComponent<BuildingPointScript>().Sell();
        Destroy(objectSelector.selectedTower);

        //Set Gold plus 

    }

    public void Upgrade()
    {
        towerSelector.SetActive(false);
        objectSelector.selectedBuildingPoint.GetComponent<BuildingPointScript>().BuildStart();
        Debug.Log("Upgrade Tower");
    }


}
