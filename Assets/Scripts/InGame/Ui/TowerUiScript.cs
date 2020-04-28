using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerUiScript : MonoBehaviour
{
    ObjectSelector objectSelector;
    GameObject towerSelector;

    public void Start()
    {
        objectSelector = GameObject.Find("MainCamera").GetComponent<ObjectSelector>();
        var canvas = GameObject.Find("Canvas");
        towerSelector = canvas.transform.GetChild(1).gameObject;
    }

    public void Sell()
    {
        Debug.Log("Sell Tower");
        towerSelector.SetActive(false);
        objectSelector.selectedBuildingPoint = null;
        objectSelector.selectedTower = null;
        objectSelector.selectedBuildingPoint.GetComponent<BuildingPointScript>().SetBuilding(false);
        Destroy(objectSelector.selectedTower);
    }

    public void Upgrade()
    {
        towerSelector.SetActive(false);
        Debug.Log("Upgrade Tower");
    }


}
