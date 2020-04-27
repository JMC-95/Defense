using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemScript : MonoBehaviour
{
    public int Gold = 100;
    Text goldText;
    GenTower genTower;
    GameObject canvas;

    const int selectorButtonMax = 4;
    Button[] selctorButtons = new Button[selectorButtonMax];

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        genTower = GetComponent<GenTower>();

        var buildSelector = canvas.transform.GetChild(0).gameObject;

        selctorButtons[0] = buildSelector.transform.GetChild(0).gameObject.GetComponent<Button>();
        selctorButtons[0].onClick.AddListener(genTower.GenArrowTower);
        selctorButtons[1] = buildSelector.transform.GetChild(1).gameObject.GetComponent<Button>();
        selctorButtons[1].onClick.AddListener(genTower.GenMissileTower);
        selctorButtons[2] = buildSelector.transform.GetChild(2).gameObject.GetComponent<Button>();
        selctorButtons[2].onClick.AddListener(genTower.GenMagicTower);
        selctorButtons[3] = buildSelector.transform.GetChild(3).gameObject.GetComponent<Button>();
        selctorButtons[3].onClick.AddListener(genTower.GenMissileTower);

        goldText = canvas.transform.GetChild(2).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "Gold : " + Gold.ToString();
    }
}
