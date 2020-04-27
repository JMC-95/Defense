using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenTower : MonoBehaviour
{
    ObjectSelector objectSelector;
    GameObject BuildSellector;
    GameSystemScript gameSystem;

    const int towerVariation = 4;
    public GameObject[] towers = new GameObject[towerVariation];
    

    void Start()
    {
        var Camera = GameObject.Find("Camera 1");
        gameSystem = GetComponent<GameSystemScript>();
        objectSelector = Camera.GetComponent<ObjectSelector>();
        var Canvas = GameObject.Find("Canvas");
        BuildSellector = Canvas.transform.GetChild(0).gameObject;
        
        var genTowers = GameObject.Find("GenTowers");
        for(int i = 0; i < towerVariation; ++i)
        {
            towers[i] = genTowers.transform.GetChild(i).gameObject;
        }
    }

    public void GenArrowTower()
    {
        if (objectSelector.selectedBuildPointPos == objectSelector.nonePos)
        {
            return;
        }
        Gen(0);
    }

    public void GenMissileTower()
    {
        if (objectSelector.selectedBuildPointPos == objectSelector.nonePos)
        {
            return;
        }
        Gen(1);
    }
    public void GenMagicTower()
    {
        if (objectSelector.selectedBuildPointPos == objectSelector.nonePos)
        {
            return;
        }
        Gen(2);
    }
    public void GenCannonTower()
    {
        if (objectSelector.selectedBuildPointPos == objectSelector.nonePos)
        {
            return;
        }
        Gen(3);
    }

    public void Gen(int towerIndex)
    {
        GameObject tower = Instantiate(towers[towerIndex]) as GameObject;
        Vector3 towerPos = objectSelector.selectedBuildPointPos;
        tower.transform.position = towerPos;
        gameSystem.Gold -= 10;
        BuildSellector.SetActive(false);
        tower.SetActive(true);
    }
    
}
