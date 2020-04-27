using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    Text goldText;
    GenTower genTowerSrcipt;
    GameObject canvas;

    Button[] buildButton;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        genTowerSrcipt = GetComponent<GenTower>();

        buildButton = new Button[Type.Tower.Max];
        var BuildButtons = canvas.transform.GetChild(0).gameObject;

        buildButton[Type.Tower.Arrow] = BuildButtons.transform.GetChild(Type.Tower.Arrow).gameObject.GetComponent<Button>();
        buildButton[Type.Tower.Arrow].onClick.AddListener(genTowerSrcipt.GenArrowTower);

        buildButton[Type.Tower.Cannon] = BuildButtons.transform.GetChild(Type.Tower.Cannon).gameObject.GetComponent<Button>();
        buildButton[Type.Tower.Cannon].onClick.AddListener(genTowerSrcipt.GenCannonTower);

        goldText = canvas.transform.GetChild(2).GetComponent<Text>();
    }
}
