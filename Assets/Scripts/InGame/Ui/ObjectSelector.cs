using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectSelector : MonoBehaviour
{
    double ButtonTurnOffDist;
    int screenWidth;
    int screenHeight;

    GameObject CameraPanel;
    //Building point
    GameObject BuildSelector;
    public GameObject selectedBuildingPoint = null;
    public Vector3 selectedBuildPointPos;

    //Tower
    GameObject[] TowerSelector;
    public GameObject selectedTower = null;
    public Vector3 selectedTowerPos;

    public Vector3 nonePos;
    public int activatedTowerSelectorNum;

    private void Start()
    {
        var canvas = GameObject.Find("Canvas");
        BuildSelector = canvas.transform.GetChild(0).gameObject;
        BuildSelector.SetActive(false);

        TowerSelector = new GameObject[3];
        TowerSelector[Type.TowerUiBotton.Generall] = canvas.transform.GetChild(1).gameObject;
        TowerSelector[Type.TowerUiBotton.Generall].SetActive(false);
        TowerSelector[Type.TowerUiBotton.Lv3] = canvas.transform.GetChild(5).gameObject;
        TowerSelector[Type.TowerUiBotton.Lv3].SetActive(false);
        TowerSelector[Type.TowerUiBotton.Lv4] = canvas.transform.GetChild(6).gameObject;
        TowerSelector[Type.TowerUiBotton.Lv4].SetActive(false);
        activatedTowerSelectorNum = -1;

        CameraPanel = canvas.transform.GetChild(3).gameObject;

        screenWidth = Camera.main.scaledPixelWidth;
        screenHeight = Camera.main.scaledPixelHeight;

        var rect = BuildSelector.GetComponent<RectTransform>().rect;
        ButtonTurnOffDist = rect.width * 2 + 50;

        nonePos = new Vector3(-99999.0f, -999999.0f, -999999.0f);
    }


    void showBuildButton(Vector3 ScreenPos)
    {
        BuildSelector.SetActive(true);
        BuildSelector.transform.position = ScreenPos;
        if (activatedTowerSelectorNum != -1)
        {
            TowerSelector[activatedTowerSelectorNum].SetActive(false);
        }
    }
    void showTowerButton(Vector3 ScreenPos, int mode)
    {
        TowerSelector[mode].SetActive(true);
        TowerSelector[mode].transform.position = ScreenPos;
        activatedTowerSelectorNum = mode;

        if (BuildSelector.activeInHierarchy)
        {
            BuildSelector.SetActive(false);
        }
    }

    Double GetDist(Vector3 position1, Vector3 position2)
    {
        return Math.Sqrt(Math.Pow(position1.x - position2.x, 2) + Math.Pow(position1.y - position2.y, 2));
    }

    void turnOffButton(Vector3 hitPoint)
    {
        if (selectedBuildingPoint)
        {
            var buttonPosition = BuildSelector.transform.position;

            var dist = GetDist(buttonPosition, hitPoint);
            if (dist > ButtonTurnOffDist)
            {
                selectedBuildingPoint = null;
                selectedBuildPointPos = nonePos;
                BuildSelector.SetActive(false);
            }
        }

        if (activatedTowerSelectorNum != -1)
        {
            var buttonPosition = TowerSelector[activatedTowerSelectorNum].transform.position;

            var dist = GetDist(buttonPosition, hitPoint);
            if (dist > ButtonTurnOffDist)
            {
                selectedTowerPos = nonePos;
                selectedTower = null;
                TowerSelector[activatedTowerSelectorNum].SetActive(false);
                activatedTowerSelectorNum = -1;
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

    // Update is called once per frame
    void Update()
    {
        bool isUseTouch = false;
        bool isUseMouse = false;
        Vector3 clickPosition;

        if (BuildSelector.activeInHierarchy || activatedTowerSelectorNum != -1)
        {
            CameraPanel.SetActive(false);
        }
        else
        {
            CameraPanel.SetActive(true);
        }

        //Detect click
        if (Input.touchCount > 0)
        {
            isUseTouch = true;
        }
        else if (Input.GetMouseButton(0))
        {
            isUseMouse = true;
        }

        //GetPosition
        if (isUseMouse)
        {
            clickPosition = Input.mousePosition;

        }
        else if (isUseTouch)
        {
            clickPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        //Check Selector on
        if (BuildSelector.activeInHierarchy)
        {
            if (Vector3.Distance(BuildSelector.transform.position, clickPosition) < ButtonTurnOffDist)
            {
                return;
            }
        }
        if (activatedTowerSelectorNum != -1)
        {
            if (Vector3.Distance(TowerSelector[activatedTowerSelectorNum].transform.position, clickPosition) < ButtonTurnOffDist)
            {
                return;
            }
        }

        //Selector raycast
        GameObject target = null;
        bool isSelectObject = false;

        Ray ray = Camera.main.ScreenPointToRay(clickPosition);
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
                    var buildingPoint = target.GetComponent<BuildingPointScript>();

                    if (buildingPoint.OnTower)
                    {
                        Debug.Log("Select tower!");

                        isSelectObject = true;
                        int towerType = buildingPoint.TowerType;
                        selectedBuildingPoint = target;
                        selectedBuildPointPos = target.transform.position;
                        selectedTower = target.transform.GetChild(0).gameObject;
                        selectedTowerPos = selectedTower.transform.position;

                        if(Type.Tower.GetTowerLv(towerType) == 3)
                        {
                            showTowerButton(GetTargetScreenPos(selectedTowerPos), Type.TowerUiBotton.Lv3);
                        }
                        else if(Type.Tower.GetTowerLv(towerType) == 4)
                        {
                            showTowerButton(GetTargetScreenPos(selectedTowerPos), Type.TowerUiBotton.Lv4);
                        }
                        else
                        {
                            showTowerButton(GetTargetScreenPos(selectedTowerPos), Type.TowerUiBotton.Generall);
                        }
                        break;
                    }
                    else if (buildingPoint.OnCons)
                    {
                        Debug.Log("Select cons!");
                        activatedTowerSelectorNum = -1;
                        break;
                    }
                    else if (buildingPoint.OnEmpty)
                    {
                        Debug.Log("Select building point!");

                        activatedTowerSelectorNum = -1;
                        selectedBuildingPoint = target;
                        selectedBuildPointPos = selectedBuildingPoint.transform.position;
                        isSelectObject = true;
                        showBuildButton(GetTargetScreenPos(selectedBuildPointPos));
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
            turnOffButton(hitPoint);
        }
    }
}

