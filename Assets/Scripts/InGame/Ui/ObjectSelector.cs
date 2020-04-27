using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectSelector : MonoBehaviour
{
    GameObject BuildSelector;
    double ButtonTurnOffDist = 120.0;
    int screenWidth;
    int screenHeight;

    public Vector3 selectedBuildPointPos;
    public Vector3 nonePos;

    private void Start()
    {
        var canvas = GameObject.Find("Canvas");
        BuildSelector = canvas.transform.GetChild(0).gameObject;
        BuildSelector.SetActive(false);

        screenWidth = Camera.main.scaledPixelWidth;
        screenHeight = Camera.main.scaledPixelHeight;

        nonePos = new Vector3(-99999.0f, -999999.0f, -999999.0f);
    }


    void showButton(Vector3 ScreenPos)
    {
        BuildSelector.SetActive(true);
        BuildSelector.transform.position = ScreenPos;
    }

    Double GetDist(Vector3 position1, Vector3 position2)
    {
        return Math.Sqrt(Math.Pow(position1.x - position2.x, 2) + Math.Pow(position1.y - position2.y, 2));
    }

    void turnOffButton(Vector3 ScreenPos)
    {
        if (!BuildSelector.activeInHierarchy || ScreenPos == nonePos)
        {
            return;
        }

        var buttonPosition = BuildSelector.transform.position;

        var dist = GetDist(buttonPosition, ScreenPos);
        if (dist > ButtonTurnOffDist)
        {
            selectedBuildPointPos = nonePos;
            BuildSelector.SetActive(false);
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
        bool SelectBuildPositon = false;

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
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

                        SelectBuildPositon = true;
                        selectedBuildPointPos = target.transform.position;

                        showButton(GetTargetScreenPos(target));
                    }
                }
                else
                {
                    hitPoint = rayCastList[i].point;
                }
            }
            if (!SelectBuildPositon)
            {
                var hitScreenPos = GetTargetScreenPos(hitPoint);
                turnOffButton(hitScreenPos);
            }
        }
    }
}

