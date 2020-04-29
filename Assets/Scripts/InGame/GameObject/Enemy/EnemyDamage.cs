using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public int CurHp;
    public int InitHp;
    public GameObject hpBarPrefab;
    public GameObject hpBar;
    public Vector3 hpBarOffset = new Vector3(0, 8.0f, 0);

    private GameManager gameManager;
    EnemySpawner enemySpawnerScript;
    private Canvas uiCanvas;
    public Image hpBarImage;
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
            Destroy(hpBar);
            gameObject.GetComponent<EnemyAI>().state = EnemyAI.State.Die;
        }
    }

    public void ArriveEnd()
    {
        enemySpawnerScript.currEnemy -= 1;
        Destroy(hpBar);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LastPoint")
        {
            ArriveEnd();
        }
    }
}
