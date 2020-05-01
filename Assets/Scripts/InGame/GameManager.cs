using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GenInfomation
{
    public GenInfomation(int enemyNumber, int line, int genCount, int hp, int speed, int gold)
    {
        EnemyName = Type.Enemy.ToString(enemyNumber);
        Line = line;
        GenCount = genCount;
        Hp = hp;
        Speed = speed;
        Gold = gold;
    }
    public string EnemyName;
    public int Line;
    public int GenCount;
    public int Hp;
    public int Speed;
    public int Gold;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject enemySpawner;
    public EnemySpawner enemySpawnerScript;
    public UiManager uiManagerScript;

    private float pastTime = 0.0f;
    private float waveDelay = 3.0f;
    public int Gold = 300;

    public int enemyType;
    public int waveCount = 0;

    public bool isWaveEnd = true;
    public bool isGameOver = false;

    [Header("Enemy Resources")]
    public GameObject[] enemyPrefabs;
    public string bagicPath = "Prefabs/Enemy/";
    public string[] enemyStrList;

    //first key : Round, second key : wave
    public Dictionary<int, Dictionary<int, List<GenInfomation>>> GenInfoMation;

    public int curRound;
    public int roundMax;
    public int curWave;
    public int waveMax;

    //GenInfomation define
    private Dictionary<int, Dictionary<int, List<GenInfomation>>> GetGenInfo()
    {
        var ret = new Dictionary<int, Dictionary<int, List<GenInfomation>>>();

        //1Round
        var tempDictionary1 = new Dictionary<int, List<GenInfomation>>();
        //1-1
        var templist11 = new List<GenInfomation>();
        templist11.Add(new GenInfomation(Type.Enemy.Slime, Type.Line.Middle, 7, 30, 5, 10));
        tempDictionary1.Add(0, templist11);
        //1-2
        var templist12 = new List<GenInfomation>();
        templist12.Add(new GenInfomation(Type.Enemy.Bat, Type.Line.Left, 7, 40, 15, 15));
        tempDictionary1.Add(1, templist12);
        //1-3
        var templist13 = new List<GenInfomation>();
        templist13.Add(new GenInfomation(Type.Enemy.Spider, Type.Line.Right, 7, 50, 10, 20));
        tempDictionary1.Add(2, templist13);
        //1-4
        var templist14 = new List<GenInfomation>();
        templist14.Add(new GenInfomation(Type.Enemy.Slime, Type.Line.Middle, 10, 30, 5, 10));
        templist14.Add(new GenInfomation(Type.Enemy.Bat, Type.Line.Left, 10, 40, 15, 15));
        tempDictionary1.Add(3, templist14);
        //1-5
        var templist15 = new List<GenInfomation>();
        templist15.Add(new GenInfomation(Type.Enemy.Bat, Type.Line.Left, 10, 40, 15, 15));
        templist15.Add(new GenInfomation(Type.Enemy.Spider, Type.Line.Right, 10, 50, 10, 20));
        tempDictionary1.Add(4, templist15);
        //1-6
        var templist16 = new List<GenInfomation>();
        templist16.Add(new GenInfomation(Type.Enemy.Dragon, Type.Line.Middle, 1, 500, 10, 300));
        tempDictionary1.Add(5, templist16);
        ret.Add(0, tempDictionary1);

        //2Round
        var tempDictionary2 = new Dictionary<int, List<GenInfomation>>();
        //2-1
        var templist21 = new List<GenInfomation>();
        templist21.Add(new GenInfomation(Type.Enemy.MonsterPlant, Type.Line.Middle, 10, 100, 5, 25));
        tempDictionary2.Add(0, templist21);
        //2-2
        var templist22 = new List<GenInfomation>();
        templist22.Add(new GenInfomation(Type.Enemy.TurtleShell, Type.Line.Left, 10, 150, 10, 30));
        tempDictionary2.Add(1, templist22);
        //2-3
        var templist23 = new List<GenInfomation>();
        templist23.Add(new GenInfomation(Type.Enemy.Skeleton, Type.Line.Right, 10, 200, 10, 35));
        tempDictionary2.Add(2, templist23);
        //2-4
        var templist24 = new List<GenInfomation>();
        templist24.Add(new GenInfomation(Type.Enemy.MonsterPlant, Type.Line.Middle, 15, 100, 5, 25));
        templist24.Add(new GenInfomation(Type.Enemy.TurtleShell, Type.Line.Left, 15, 150, 10, 30));
        tempDictionary2.Add(3, templist24);
        //2-5
        var templist25 = new List<GenInfomation>();
        templist25.Add(new GenInfomation(Type.Enemy.TurtleShell, Type.Line.Left, 15, 150, 10, 30));
        templist25.Add(new GenInfomation(Type.Enemy.Skeleton, Type.Line.Right, 15, 200, 10, 35));
        tempDictionary2.Add(4, templist25);
        //2-6
        var templist26 = new List<GenInfomation>();
        templist26.Add(new GenInfomation(Type.Enemy.Dragon, Type.Line.Middle, 1, 750, 10, 500));
        tempDictionary2.Add(5, templist26);
        ret.Add(1, tempDictionary2);

        //3Round
        var tempDictionary3 = new Dictionary<int, List<GenInfomation>>();
        //3-1
        var templist31 = new List<GenInfomation>();
        templist31.Add(new GenInfomation(Type.Enemy.EvilMage, Type.Line.Middle, 10, 250, 15, 40));
        tempDictionary3.Add(0, templist31);
        //3-2
        var templist32 = new List<GenInfomation>();
        templist32.Add(new GenInfomation(Type.Enemy.Orc, Type.Line.Left, 10, 300, 15, 45));
        tempDictionary3.Add(1, templist32);
        //3-3
        var templist33 = new List<GenInfomation>();
        templist33.Add(new GenInfomation(Type.Enemy.Golem, Type.Line.Right, 10, 400, 10, 50));
        tempDictionary3.Add(2, templist33);
        //3-4
        var templist34 = new List<GenInfomation>();
        templist34.Add(new GenInfomation(Type.Enemy.EvilMage, Type.Line.Middle, 10, 250, 15, 40));
        templist34.Add(new GenInfomation(Type.Enemy.Orc, Type.Line.Left, 15, 250, 15, 45));
        tempDictionary3.Add(3, templist34);
        //3-5
        var templist35 = new List<GenInfomation>();
        templist35.Add(new GenInfomation(Type.Enemy.Orc, Type.Line.Left, 7, 300, 15, 45));
        templist35.Add(new GenInfomation(Type.Enemy.Golem, Type.Line.Right, 10, 400, 10, 50));
        tempDictionary3.Add(4, templist35);
        //3-6
        var templist36 = new List<GenInfomation>();
        templist36.Add(new GenInfomation(Type.Enemy.EvilMage, Type.Line.Middle, 10, 250, 15, 40));
        templist36.Add(new GenInfomation(Type.Enemy.Orc, Type.Line.Left, 7, 300, 15, 45));
        templist36.Add(new GenInfomation(Type.Enemy.Golem, Type.Line.Right, 10, 400, 10, 50));
        tempDictionary3.Add(5, templist36);
        ret.Add(2, tempDictionary3);

        //4Round
        var tempDictionary4 = new Dictionary<int, List<GenInfomation>>();
        //4-1
        var templist41 = new List<GenInfomation>();
        templist41.Add(new GenInfomation(Type.Enemy.Dragon, Type.Line.Left, 1, 1000, 15, 700));
        templist41.Add(new GenInfomation(Type.Enemy.Dragon, Type.Line.Middle, 1, 1000, 15, 700));
        templist41.Add(new GenInfomation(Type.Enemy.Dragon, Type.Line.Right, 1, 1000, 15, 700));
        tempDictionary4.Add(0, templist41);
        ret.Add(3, tempDictionary4);

        return ret;
    }

    static public GameManager Get()
    {
        //if (!instance)
        //{
        //    return instance = new GameManager();
        //}
        return instance;
    }

    public void LoadEnemyPrefabs()
    {
        enemyPrefabs = new GameObject[Type.Enemy.Max];

        for (int i = 0; i < Type.Enemy.Max; ++i)
        {
            enemyPrefabs[i] = Resources.Load(bagicPath + Type.Enemy.ToString(i)) as GameObject;
        }
    }

    public void SetGenInfomation()
    {
        GenInfoMation = GetGenInfo();
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        Gold = 1000;
        isWaveEnd = true;
        enemyType = 0;
        waveCount = 0;
        curRound = 0;
        roundMax = 3;
        waveMax = 5;

        var uiManager = GameObject.Find("UiManager");
        uiManagerScript = uiManager.GetComponent<UiManager>();

        enemySpawner = GameObject.Find("EnemySpawnGroup");
        enemySpawnerScript = enemySpawner.GetComponent<EnemySpawner>();
    }

    public void UseGold(int cost)
    {
        Gold -= cost;
        uiManagerScript.UpdateGoldText();
    }


    void Update()
    {
        if (!isGameOver && !isWaveEnd)
        {
            if (enemySpawnerScript.genCount == enemySpawnerScript.genCountLimit)
            {
                if (enemySpawnerScript.currEnemy == 0)
                {
                    curWave += 1;
                    isWaveEnd = true;
                    if (curWave > waveMax)
                    {
                        curWave = 0;
                        curRound += 1;
                        if (curRound > roundMax)
                        {
                            isGameOver = true;
                        }
                    }
                }
            }
        }
        else
        {
            pastTime += Time.deltaTime;

            if (pastTime > waveDelay)
            {
                isWaveEnd = false;
                pastTime = 0.0f;
                enemySpawnerScript.ResetGenInfo();
                StartCoroutine(enemySpawnerScript.CreateEnemy());
            }
        }
    }
}
