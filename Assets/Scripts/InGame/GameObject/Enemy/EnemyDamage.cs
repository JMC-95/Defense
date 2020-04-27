using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public float hp = 100.0f;
    private float initHp = 100.0f;

    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(-0.2f, 5.5f, 0);
    private Canvas uiCanvas;
    private Image hpBarImage;

    void Start()
    {
        SetHpBar();
    }

    void SetHpBar()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
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
            hpBarImage.fillAmount = hp / initHp;

            if (hp <= 0.0f)
            {
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
            }
        }

        if (coll.collider.tag == "LastPoint")
        {
            hp -= coll.gameObject.GetComponent<EndPoint>().damage;
            hpBarImage.fillAmount = hp / initHp;

            if (hp <= 0.0f)
            {
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
            }
        }
    }
}
