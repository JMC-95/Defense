using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingOperation : MonoBehaviour
{

    GenTower genTower;
    float buildingDuration;
    float pastTime;
    int towerType;

    public void Start()
    {
        var uiManagerObj = GameObject.Find("UiManager");
        genTower = uiManagerObj.GetComponent<GenTower>();
    }


    public void init(int type)
    {
        towerType = type;
        pastTime = 0.0f;
        buildingDuration = Type.Tower.GetBuildingDuration(type);
    }

    private void Update()
    {
        pastTime += Time.deltaTime;
        if(pastTime > buildingDuration)
        {
            genTower.SetTower(towerType, this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
