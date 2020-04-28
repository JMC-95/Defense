using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public int hp;
    public int initHp;

    public GameObject hpBarPrefab;
    public GameObject hpBar;
    public Vector3 hpBarOffset = new Vector3(0, 7.0f, 0);

    private GameManager gameManager;
    private Canvas uiCanvas;
    private Image hpBarImage;

    public void SetHpBar()
    {
        if (!gameManager)
        {
            gameManager = GameManager.Get();
        }

        initHp = GetComponent<EnemyScript>().eachTurnHpMax[gameManager.curRound];
        hp = initHp;

        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpBar = hpBar.GetComponent<EnemyHpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            other.gameObject.SetActive(false);
            hp -= other.gameObject.GetComponent<Arrow>().damage;
            hpBarImage.fillAmount = hp / (float)initHp;

            if (hp <= 0.0f)
            {
                Destroy(hpBar);
                GetComponent<EnemyAI>().state = EnemyAI.State.Die;
            }
        }

        if (other.tag == "LastPoint")
        {
            Destroy(hpBar);
        }
    }
}
