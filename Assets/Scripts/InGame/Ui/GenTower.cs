﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenTower : MonoBehaviour
{
    ObjectSelector objectSelector;
    GameObject ButtonSellector;
    UiManager UiManager;
    GameObject GenTowers;
    GameManager gameManager;

    public GameObject[] towers;
    public GameObject[] Cons;

    void Start()
    {
        var Camera = GameObject.Find("MainCamera");
        var Canvas = GameObject.Find("Canvas");
        var uiManager = GameObject.Find("UiManager");

        GenTowers = GameObject.Find("GenTowers");
        gameManager = GameManager.Get();

        UiManager = uiManager.GetComponent<UiManager>();
        objectSelector = Camera.GetComponent<ObjectSelector>();
        ButtonSellector = Canvas.transform.GetChild(0).gameObject;

        towers = new GameObject[Type.Tower.Max];
        Cons = new GameObject[Type.Tower.Max];
        towers[Type.Tower.Archerlv1] = Resources.Load("Prefabs/Tower/ArcherTower_Lvl1") as GameObject;
        towers[Type.Tower.Archerlv2] = Resources.Load("Prefabs/Tower/ArcherTower_Lvl2") as GameObject;
        towers[Type.Tower.Archerlv3] = Resources.Load("Prefabs/Tower/ArcherTower_Lvl3") as GameObject;
        towers[Type.Tower.Archerlv4A] = Resources.Load("Prefabs/Tower/ArcherTower_Lvl4_A") as GameObject;
        towers[Type.Tower.Archerlv4B] = Resources.Load("Prefabs/Tower/ArcherTower_Lvl4_B") as GameObject;

        towers[Type.Tower.Canonlv1] = Resources.Load("Prefabs/Tower/CanonTower_Lvl1") as GameObject;
        towers[Type.Tower.Canonlv2] = Resources.Load("Prefabs/Tower/CanonTower_Lvl2") as GameObject;
        towers[Type.Tower.Canonlv3] = Resources.Load("Prefabs/Tower/CanonTower_Lvl3") as GameObject;
        towers[Type.Tower.Canonlv4A] = Resources.Load("Prefabs/Tower/CanonTower_Lvl4_A") as GameObject;
        towers[Type.Tower.Canonlv4B] = Resources.Load("Prefabs/Tower/CanonTower_Lvl4_B") as GameObject;

        towers[Type.Tower.Magelv1] = Resources.Load("Prefabs/Tower/MageTower_Lvl1") as GameObject;
        towers[Type.Tower.Magelv2] = Resources.Load("Prefabs/Tower/MageTower_Lvl2") as GameObject;
        towers[Type.Tower.Magelv3] = Resources.Load("Prefabs/Tower/MageTower_Lvl3") as GameObject;
        towers[Type.Tower.Magelv4A] = Resources.Load("Prefabs/Tower/MageTower_Lvl4_A") as GameObject;
        towers[Type.Tower.Magelv4B] = Resources.Load("Prefabs/Tower/MageTower_Lvl4_B") as GameObject;

        Cons[Type.Tower.Archerlv1] = Resources.Load("Prefabs/Cons/TB_ArcherTower_Lvl1_Cons") as GameObject;
        Cons[Type.Tower.Archerlv2] = Resources.Load("Prefabs/Cons/TB_ArcherTower_Lvl2_Cons") as GameObject;
        Cons[Type.Tower.Archerlv3] = Resources.Load("Prefabs/Cons/TB_ArcherTower_Lvl3_Cons") as GameObject;
        Cons[Type.Tower.Archerlv4A] = Resources.Load("Prefabs/Cons/TB_ArcherTower_Lvl4_A_Cons") as GameObject;
        Cons[Type.Tower.Archerlv4B] = Resources.Load("Prefabs/Cons/TB_ArcherTower_Lvl4_B_Cons") as GameObject;
        Cons[Type.Tower.Canonlv1] = Resources.Load("Prefabs/Cons/TB_CanonTower_Lvl1_Cons") as GameObject;
        Cons[Type.Tower.Canonlv2] = Resources.Load("Prefabs/Cons/TB_CanonTower_Lvl2_Cons") as GameObject;
        Cons[Type.Tower.Canonlv3] = Resources.Load("Prefabs/Cons/TB_CanonTower_Lvl3_Cons") as GameObject;
        Cons[Type.Tower.Canonlv4A] = Resources.Load("Prefabs/Cons/TB_CanonTower_Lvl4_A_Cons") as GameObject;
        Cons[Type.Tower.Canonlv4B] = Resources.Load("Prefabs/Cons/TB_CanonTower_Lvl4_B_Cons") as GameObject;
        Cons[Type.Tower.Magelv1] = Resources.Load("Prefabs/Cons/TB_MageTower_Lvl1_Cons") as GameObject;
        Cons[Type.Tower.Magelv2] = Resources.Load("Prefabs/Cons/TB_MageTower_Lvl2_Cons") as GameObject;
        Cons[Type.Tower.Magelv3] = Resources.Load("Prefabs/Cons/TB_MageTower_Lvl3_Cons") as GameObject;
        Cons[Type.Tower.Magelv4A] = Resources.Load("Prefabs/Cons/TB_MageTower_Lvl4_A_Cons") as GameObject;
        Cons[Type.Tower.Magelv4B] = Resources.Load("Prefabs/Cons/TB_MageTower_Lvl4_B_Cons") as GameObject;
    }

    public void GenArcherTowerCons()
    {
        if (objectSelector.selectedBuildPointPos == objectSelector.nonePos)
        {
            return;
        }
        GenCons(Type.Tower.Archerlv1);
    }

    public void GenMageTowerCons()
    {
        if (objectSelector.selectedBuildPointPos == objectSelector.nonePos)
        {
            return;
        }
        GenCons(Type.Tower.Magelv1);
    }

    public void GenCannonTowerCons()
    {
        if (objectSelector.selectedBuildPointPos == objectSelector.nonePos)
        {
            return;
        }
        GenCons(Type.Tower.Canonlv1);
    }

    public void GenCons(int towerType)
    {
        int cost = Type.Tower.GetBuildingPrice(towerType);

        if(gameManager.Gold < cost)
        {
            Debug.Log("You can't build! : Low gold");
            objectSelector.selectedBuildingPoint = null;
            objectSelector.selectedTower = null;
            ButtonSellector.SetActive(false);
            return;
        }

        if (objectSelector.selectedTower)
        {
            Destroy(objectSelector.selectedTower);
        }

        gameManager.UseGold(cost);

        GameObject cons = Instantiate(Cons[towerType]) as GameObject;

        cons.transform.parent = objectSelector.selectedBuildingPoint.transform;
        cons.name = Type.Tower.GetTowerName(towerType) + "Cons";

        objectSelector.selectedBuildingPoint.GetComponent<BuildingPointScript>().BuildStart(towerType);
        objectSelector.selectedBuildingPoint = null;
        ButtonSellector.SetActive(false);

        cons.transform.position = objectSelector.selectedBuildPointPos;
        cons.SetActive(true);

        cons.AddComponent<BuildingOperation>();
        cons.GetComponent<BuildingOperation>().init(towerType);
    }


    public void SetTower(int towerType, GameObject cons)
    {
        GameObject tower = Instantiate(towers[towerType]) as GameObject;
        tower.transform.parent = cons.transform.parent;
        tower.name = Type.Tower.GetTowerName(towerType);

        cons.transform.parent.GetComponent<BuildingPointScript>().BuildComplete();

        tower.transform.position = cons.transform.position;
        tower.SetActive(true);
    }
}
