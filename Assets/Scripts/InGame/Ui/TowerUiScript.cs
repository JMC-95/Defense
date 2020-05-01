using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerUiScript : MonoBehaviour
{
    ObjectSelector objectSelector;
    GameObject[] towerSelector;
    GameManager gameManager;
    GenTower genTowerScript;

    public void Start()
    {
        towerSelector = new GameObject[3];
        objectSelector = GameObject.Find("MainCamera").GetComponent<ObjectSelector>();
        var canvas = GameObject.Find("Canvas");
        towerSelector[Type.TowerUiBotton.Generall] = canvas.transform.GetChild(1).gameObject;
        towerSelector[Type.TowerUiBotton.Lv3] = canvas.transform.GetChild(5).gameObject;
        towerSelector[Type.TowerUiBotton.Lv4] = canvas.transform.GetChild(6).gameObject;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        genTowerScript = GameObject.Find("UiManager").GetComponent<GenTower>();
    }

    public void Sell()
    {
        Debug.Log("Sell Tower");
        int towerType = objectSelector.selectedBuildingPoint.GetComponent<BuildingPointScript>().TowerType;
        int price = Type.Tower.GetTotalPrice(towerType) * 7 / 10;

        gameManager.UseGold(-price);

        towerSelector[objectSelector.activatedTowerSelectorNum].SetActive(false);
        objectSelector.selectedTower.transform.parent.GetComponent<BuildingPointScript>().Sell();

        objectSelector.activatedTowerSelectorNum = -1;
        Destroy(objectSelector.selectedTower);
        objectSelector.selectedBuildingPoint = null;
        objectSelector.selectedTower = null;

        //Set Gold plus 

    }

    public void Upgrade()
    {
        towerSelector[objectSelector.activatedTowerSelectorNum].SetActive(false);
        int curTowerType = objectSelector.selectedBuildingPoint.GetComponent<BuildingPointScript>().TowerType;
        genTowerScript.GenCons(curTowerType + 1);
        objectSelector.activatedTowerSelectorNum = -1;

        Debug.Log("Upgrade Tower");
    }

    public void UpgradeA()
    {
        towerSelector[objectSelector.activatedTowerSelectorNum].SetActive(false);
        int curTowerType = objectSelector.selectedBuildingPoint.GetComponent<BuildingPointScript>().TowerType;
        genTowerScript.GenCons(curTowerType + 1);
        objectSelector.activatedTowerSelectorNum = -1;

        Debug.Log("Upgrade Towerlv4A");
    }

    public void UpgradeB()
    {
        towerSelector[objectSelector.activatedTowerSelectorNum].SetActive(false);
        int curTowerType = objectSelector.selectedBuildingPoint.GetComponent<BuildingPointScript>().TowerType;
        genTowerScript.GenCons(curTowerType + 2);
        objectSelector.activatedTowerSelectorNum = -1;

        Debug.Log("Upgrade Towerlv4B");
    }


}
