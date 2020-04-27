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

    Button[] buildButton;
    Button[] towerButton;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        gameManager = GameManager.Get();
        genTowerSrcipt = GetComponent<GenTower>();
        towerUiScript = GetComponent<TowerUiScript>();

        buildButton = new Button[Type.Tower.Max];
        var BuildButtons = canvas.transform.GetChild(0).gameObject;
        buildButton[Type.Tower.Arrow] = BuildButtons.transform.GetChild(Type.Tower.Arrow).gameObject.GetComponent<Button>();
        buildButton[Type.Tower.Arrow].onClick.AddListener(genTowerSrcipt.GenArrowTower);
        buildButton[Type.Tower.Cannon] = BuildButtons.transform.GetChild(Type.Tower.Cannon).gameObject.GetComponent<Button>();
        buildButton[Type.Tower.Cannon].onClick.AddListener(genTowerSrcipt.GenCannonTower);

        towerButton = new Button[Type.TowerUiBotton.Max];
        var TowerButtons = canvas.transform.GetChild(1).gameObject;
        towerButton[Type.TowerUiBotton.Sell] = TowerButtons.transform.GetChild(Type.TowerUiBotton.Sell).gameObject.GetComponent<Button>();
        towerButton[Type.TowerUiBotton.Sell].onClick.AddListener(towerUiScript.Sell);
        towerButton[Type.TowerUiBotton.Upgrade] = TowerButtons.transform.GetChild(Type.TowerUiBotton.Upgrade).gameObject.GetComponent<Button>();
        towerButton[Type.TowerUiBotton.Upgrade].onClick.AddListener(towerUiScript.Upgrade);

        goldText = canvas.transform.GetChild(2).GetComponent<Text>();
        UpdateGoldText();
    }

    private void UpdateGoldText()
    {
        goldText.text = "Gold : " + gameManager.Gold.ToString();
    }
}
