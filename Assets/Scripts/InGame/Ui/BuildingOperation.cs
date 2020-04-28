using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingOperation : MonoBehaviour
{

    GenTower genTower;
    UiManager uiManagerScript;

    float yOffset = 50.0f;
    GameObject buildingProgressBar;
    Slider sliderScript;
    Camera camera;

    Vector3 ScreenPos;
    float buildingDuration;
    float pastTime;
    float progressRatio;
    int towerType;

    RectTransform parentRect;
    RectTransform childRect;

    public void init(int type)
    {
        if(!genTower)
        {
            var uiManagerObj = GameObject.Find("UiManager");
            genTower = uiManagerObj.GetComponent<GenTower>();
            uiManagerScript = uiManagerObj.GetComponent<UiManager>();
        }

        towerType = type;
        pastTime = 0.0f;
        progressRatio = 0.0f;
        buildingDuration = Type.Tower.GetBuildingDuration(type);

        buildingProgressBar = Instantiate(uiManagerScript.BuildingProgressBar) as GameObject;
        buildingProgressBar.transform.parent = GameObject.Find("InGameUi").transform.GetChild(0).transform;
        buildingProgressBar.SetActive(true);
        sliderScript = buildingProgressBar.GetComponent<Slider>();

        camera = Camera.main;
        ScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        ScreenPos.y += yOffset;
        sliderScript.value = 0.0f;

        buildingProgressBar.transform.position = ScreenPos;

        parentRect = buildingProgressBar.GetComponent<RectTransform>();
        childRect = buildingProgressBar.transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void Update()
    {
        pastTime += Time.deltaTime;

        if(pastTime > buildingDuration)
        {
            genTower.SetTower(towerType, this.gameObject);
            Destroy(this.gameObject);
            Destroy(buildingProgressBar);
        }
    }

    private void LateUpdate()
    {
        progressRatio = pastTime / buildingDuration;

        var localPos = Vector2.zero;
        sliderScript.value = progressRatio;
    }

}
