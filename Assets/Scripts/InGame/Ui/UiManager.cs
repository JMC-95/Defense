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
    Button[] towerButtonlv3;
    Button[] towerButtonlv4;
    Button roundStartButton;

    public Text BossText;
    EnemySpawner enemySpawnerScript;
    float changeAlpha = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        gameManager = GameManager.Get();
        genTowerSrcipt = GetComponent<GenTower>();
        towerUiScript = GetComponent<TowerUiScript>();
        enemySpawnerScript = GameObject.Find("EnemySpawnGroup").GetComponent<EnemySpawner>();

        BuildingProgressBar = Resources.Load("Prefabs/Tower/BuildingProgressBar") as GameObject;

        buildButton = new Button[Type.BuildingPointUiBotton.Max];
        var BuildButtons = canvas.transform.GetChild(0).gameObject;
        buildButton[Type.BuildingPointUiBotton.Archer] = BuildButtons.transform.GetChild(Type.BuildingPointUiBotton.Archer).gameObject.GetComponent<Button>();
        buildButton[Type.BuildingPointUiBotton.Archer].onClick.AddListener(genTowerSrcipt.GenArcherTowerCons);
        buildButton[Type.BuildingPointUiBotton.Cannon] = BuildButtons.transform.GetChild(Type.BuildingPointUiBotton.Cannon).gameObject.GetComponent<Button>();
        buildButton[Type.BuildingPointUiBotton.Cannon].onClick.AddListener(genTowerSrcipt.GenCannonTowerCons);
        buildButton[Type.BuildingPointUiBotton.Mage] = BuildButtons.transform.GetChild(Type.BuildingPointUiBotton.Mage).gameObject.GetComponent<Button>();
        buildButton[Type.BuildingPointUiBotton.Mage].onClick.AddListener(genTowerSrcipt.GenMageTowerCons);

        towerButton = new Button[Type.TowerUiBotton.Max];
        var TowerButtons = canvas.transform.GetChild(1).gameObject;
        towerButton[Type.TowerUiBotton.Upgrade] = TowerButtons.transform.GetChild(Type.TowerUiBotton.Upgrade).GetComponent<Button>();
        towerButton[Type.TowerUiBotton.Upgrade].onClick.AddListener(towerUiScript.Upgrade);
        towerButton[Type.TowerUiBotton.Sell] = TowerButtons.transform.GetChild(Type.TowerUiBotton.Sell).GetComponent<Button>();
        towerButton[Type.TowerUiBotton.Sell].onClick.AddListener(towerUiScript.Sell);

        towerButtonlv3 = new Button[3];
        var towerButtonlv3s = canvas.transform.GetChild(5).gameObject;
        towerButtonlv3[Type.TowerUiBotton.lv3A] = towerButtonlv3s.transform.GetChild(Type.TowerUiBotton.lv3A).GetComponent<Button>();
        towerButtonlv3[Type.TowerUiBotton.lv3A].onClick.AddListener(towerUiScript.UpgradeA);
        towerButtonlv3[Type.TowerUiBotton.lv3B] = towerButtonlv3s.transform.GetChild(Type.TowerUiBotton.lv3B).GetComponent<Button>();
        towerButtonlv3[Type.TowerUiBotton.lv3B].onClick.AddListener(towerUiScript.UpgradeB);
        towerButtonlv3[Type.TowerUiBotton.lv3Sell] = towerButtonlv3s.transform.GetChild(Type.TowerUiBotton.lv3Sell).GetComponent<Button>();
        towerButtonlv3[Type.TowerUiBotton.lv3Sell].onClick.AddListener(towerUiScript.Sell);

        towerButtonlv4 = new Button[1];
        var towerButtonlv4s = canvas.transform.GetChild(6).gameObject;
        towerButtonlv4[Type.TowerUiBotton.lv4] = towerButtonlv4s.transform.GetChild(Type.TowerUiBotton.lv4).GetComponent<Button>();
        towerButtonlv4[Type.TowerUiBotton.lv4].onClick.AddListener(towerUiScript.Sell);

        roundStartButton = canvas.transform.GetChild(7).gameObject.GetComponent<Button>();
        roundStartButton.onClick.AddListener(gameManager.StartRound);

        BossText = canvas.transform.GetChild(8).GetComponent<Text>();
        BossText.gameObject.SetActive(false);
        goldText = canvas.transform.GetChild(2).GetComponent<Text>();
        UpdateGoldText();
    }

    public void ShowBossEmergy()
    {
        BossText.gameObject.SetActive(true);
        if(changeAlpha > 0.0f)
        {
            BossText.color = new Vector4(BossText.color.r, BossText.color.g, BossText.color.b, BossText.color.a - changeAlpha);

            if(BossText.color.a <= 0.0f)
            {
                changeAlpha = -changeAlpha;
            }
        }
        else
        {
            BossText.color = new Vector4(BossText.color.r, BossText.color.g, BossText.color.b, BossText.color.a - changeAlpha);
            if (BossText.color.a >= 1.0f)
            {
                changeAlpha = -changeAlpha;
            }
        }
    }

    public void Update()
    {
        if(GameManager.instance.roundEnd && enemySpawnerScript.currEnemy == 0)
        {
            roundStartButton.gameObject.SetActive(true);
        }
        else
        {
            roundStartButton.gameObject.SetActive(false);
        }
    }

    public void UpdateGoldText()
    {
        goldText.text = "Gold : " + gameManager.Gold.ToString();
    }
}
