using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    private GameManager gameManager;
    private EnemySpawner enemySpawnerScript;

    public GameObject hpBarPrefab;
    public GameObject hpBar;
    public Vector3 hpBarOffset = new Vector3(0, 8.0f, 0);

    private Canvas uiCanvas;
    public Image hpBarImage;

    public int CurHp;
    public int InitHp;

    public bool isDie;

    public void Start()
    {
        enemySpawnerScript = GameObject.Find("EnemySpawnGroup").GetComponent<EnemySpawner>();
    }

    public void SetHpBar(int hp)
    {
        if (!gameManager)
        {
            gameManager = GameManager.Get();
        }

        InitHp = hp;
        CurHp = InitHp;

        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpBar = hpBar.GetComponent<EnemyHpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;
    }

    public void Die()
    {
        if(isDie == false)
        {
            isDie = true;
            enemySpawnerScript.currEnemy -= 1;
            gameObject.GetComponent<EnemyAI>().state = EnemyAI.State.Die;
            gameManager.UseGold(-GetComponent<EnemyMove>().Gold);
            Destroy(hpBar);
        }
    }

    public void ArriveEnd()
    {
        enemySpawnerScript.currEnemy -= 1;
        gameObject.SetActive(false);
        gameManager.looseLife();
        Destroy(hpBar);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LastPoint")
        {
            ArriveEnd();
        }
    }
}
