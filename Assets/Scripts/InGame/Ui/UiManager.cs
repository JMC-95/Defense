using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    Text goldText;
    GenTower genTowerSrcipt;
    TowerUiScript towerUiScript;
    GameObject canvas;
    GameManager gameManager;

    public GameObject BuildingProgressBar;
    Button[] buildButton;
    Button[] towerButton;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        gameManager = GameManager.Get();
        genTowerSrcipt = GetComponent<GenTower>();
        towerUiScript = GetComponent<TowerUiScript>();

        BuildingProgressBar = Resources.Load("Tower/BuildingProgressBar") as GameObject;

        buildButton = new Button[Type.Tower.Max];
        var BuildButtons = canvas.transform.GetChild(0).gameObject;
        buildButton[Type.BuildingPointUiBotton.Archer] = BuildButtons.transform.GetChild(Type.BuildingPointUiBotton.Archer).gameObject.GetComponent<Button>();
        buildButton[Type.BuildingPointUiBotton.Archer].onClick.AddListener(genTowerSrcipt.GenArcherTowerCons);
        buildButton[Type.BuildingPointUiBotton.Cannon] = BuildButtons.transform.GetChild(Type.BuildingPointUiBotton.Cannon).gameObject.GetComponent<Button>();
        buildButton[Type.BuildingPointUiBotton.Cannon].onClick.AddListener(genTowerSrcipt.GenCannonTowerCons);
        buildButton[Type.BuildingPointUiBotton.Mage] = BuildButtons.transform.GetChild(Type.BuildingPointUiBotton.Mage).gameObject.GetComponent<Button>();
        buildButton[Type.BuildingPointUiBotton.Mage].onClick.AddListener(genTowerSrcipt.GenMageTowerCons);

        towerButton = new Button[Type.TowerUiBotton.Max];
        var TowerButtons = canvas.transform.GetChild(1).gameObject;
        towerButton[Type.TowerUiBotton.Sell] = TowerButtons.transform.GetChild(Type.TowerUiBotton.Sell).GetComponent<Button>();
        towerButton[Type.TowerUiBotton.Sell].onClick.AddListener(towerUiScript.Sell);
        towerButton[Type.TowerUiBotton.Upgrade] = TowerButtons.transform.GetChild(Type.TowerUiBotton.Upgrade).GetComponent<Button>();
        towerButton[Type.TowerUiBotton.Upgrade].onClick.AddListener(towerUiScript.Upgrade);

        goldText = canvas.transform.GetChild(2).GetComponent<Text>();
        UpdateGoldText();
    }

    public void UpdateGoldText()
    {
        goldText.text = "Gold : " + gameManager.Gold.ToString();
    }
}
