using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectSelector : MonoBehaviour
{
    double ButtonTurnOffDist = 100.0;
    int screenWidth;
    int screenHeight;

    //Building point
    GameObject BuildSelector;
    public GameObject selectedBuildingPoint = null;
    public Vector3 selectedBuildPointPos;

    //Building
    GameObject TowerSelector;
    public GameObject selectedTower = null;
    public Vector3 selectedTowerPos;

    public Vector3 nonePos;

    private void Start()
    {
        var canvas = GameObject.Find("Canvas");
        BuildSelector = canvas.transform.GetChild(0).gameObject;
        BuildSelector.SetActive(false);
        TowerSelector = canvas.transform.GetChild(1).gameObject;
        TowerSelector.SetActive(false);

        screenWidth = Camera.main.scaledPixelWidth;
        screenHeight = Camera.main.scaledPixelHeight;

        nonePos = new Vector3(-99999.0f, -999999.0f, -999999.0f);
    }


    void showBuildButton(Vector3 ScreenPos)
    {
        BuildSelector.SetActive(true);
        BuildSelector.transform.position = ScreenPos;
        if (TowerSelector.activeInHierarchy)
        {
            TowerSelector.SetActive(false);
        }
    }
    void showTowerButton(Vector3 ScreenPos)
    {
        TowerSelector.SetActive(true);
        TowerSelector.transform.position = ScreenPos;
        if(BuildSelector.activeInHierarchy)
        {
            BuildSelector.SetActive(false);
        }
    }

    Double GetDist(Vector3 position1, Vector3 position2)
    {
        return Math.Sqrt(Math.Pow(position1.x - position2.x, 2) + Math.Pow(position1.y - position2.y, 2));
    }

    void turnOffButton(Vector3 ScreenPos)
    {
        if (BuildSelector.activeInHierarchy)
        {
            var buttonPosition = BuildSelector.transform.position;

            var dist = GetDist(buttonPosition, ScreenPos);
            if (dist > ButtonTurnOffDist)
            {
                selectedBuildPointPos = nonePos;
                BuildSelector.SetActive(false);
            }
        }

        if (TowerSelector.activeInHierarchy)
        {
            var buttonPosition = TowerSelector.transform.position;

            var dist = GetDist(buttonPosition, ScreenPos);
            if (dist > ButtonTurnOffDist / 2)
            {
                selectedTowerPos = nonePos;
                TowerSelector.SetActive(false);
            }
        }

    }

    Vector3 GetTargetScreenPos(GameObject target)
    {
        var targetPosition = target.transform.position;
        var targetScreenPos = Camera.main.WorldToViewportPoint(targetPosition);

        targetScreenPos.x = targetScreenPos.x * screenWidth;
        targetScreenPos.y = targetScreenPos.y * screenHeight;

        return targetScreenPos;
    }

    Vector3 GetTargetScreenPos(Vector3 targetPos)
    {
        var targetScreenPos = Camera.main.WorldToViewportPoint(targetPos);

        targetScreenPos.x = targetScreenPos.x * screenWidth;
        targetScreenPos.y = targetScreenPos.y * screenHeight;

        return targetScreenPos;
    }

    GameObject FindTower(RaycastHit[] rayCastList)
    {
        for(int i = 0; i < rayCastList.Length; ++i)
        {
            var obj = rayCastList[i].collider.gameObject;
            if (obj.name == "ArcherTower" || obj.name == "CannonTower")
            {
                if(obj.transform.position == selectedBuildPointPos)
                {
                    return rayCastList[i].collider.gameObject;
                }
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        bool isSelectObject = false;

        if (Input.GetMouseButtonDown(0))
        {
            if (BuildSelector.activeInHierarchy)
            {
                var mousePosition = Input.mousePosition;
                if (GetDist(mousePosition, BuildSelector.transform.position) < ButtonTurnOffDist)
                {
                    return;
                }
            }

            GameObject target = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var rayCastList = Physics.RaycastAll(ray);
            Vector3 hitPoint = nonePos;

            for (int i = 0; i < rayCastList.Length; ++i)
            {
                target = rayCastList[i].collider.gameObject;
                var targetName = target.name;

                if (targetName.Length >= "TB_BuildingPoint".Length)
                {
                    if (targetName.Substring(0, 16) == "TB_BuildingPoint")
                    {
                        Debug.Log("Select building point!");
                        var buildingPoint = target.GetComponent<BuildingPointScript>();
                        selectedBuildingPoint = target;
                        selectedBuildPointPos = selectedBuildingPoint.transform.position;
                        if (buildingPoint.isOnBuilding())
                        {
                            isSelectObject = true;
                            selectedTower = FindTower(rayCastList);
                            selectedTowerPos = selectedTower.transform.position;

                            showTowerButton(GetTargetScreenPos(selectedTowerPos));
                            break;
                        }
                        else
                        {
                            isSelectObject = true;
                            showBuildButton(GetTargetScreenPos(selectedBuildingPoint));
                            break;
                        }
                    }
                }              
                else
                {
                    hitPoint = rayCastList[i].point;
                }
            }
            if (!isSelectObject)
            {
                var hitScreenPos = GetTargetScreenPos(hitPoint);
                turnOffButton(hitScreenPos);
            }
        }
    }
}

