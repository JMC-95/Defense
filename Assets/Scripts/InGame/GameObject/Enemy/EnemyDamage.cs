using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public int hp;
    public int initHp;

    public GameObject hpBarPrefab;
    public GameObject hpBar;
    public Vector3 hpBarOffset = new Vector3(0, 8.0f, 0);

    private GameManager gameManager;
    private Canvas uiCanvas;
    public Image hpBarImage;

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
        if (other.tag == "LastPoint")
        {
            Destroy(hpBar);
        }
    }
}
