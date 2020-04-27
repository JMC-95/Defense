using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public int hp;
    public int initHp;

    public GameObject hpBarPrefab;
    public GameObject hpBar;
    public Vector3 hpBarOffset = new Vector3(-0.2f, 5.5f, 0);

    private Canvas uiCanvas;
    private Image hpBarImage;
    private GameManager gameManager;

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

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Bullet")
        {
            coll.gameObject.SetActive(false);
            hp -= coll.gameObject.GetComponent<Arrow>().damage;
            hpBarImage.fillAmount = hp / (float)initHp;

            if (hp <= 0.0f)
            {
                Destroy(hpBar);
            }
        }

        if (coll.collider.tag == "LastPoint")
        {
            Destroy(hpBar);
        }
    }
}
