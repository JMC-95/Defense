using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private GameObject[] monsterObj;

    public Transform[] archerPath;
    public Transform[] magePath;
    public Transform[] swordmanPath;

    void OnDrawGizmos()
    {
        iTween.DrawPath(archerPath);
        iTween.DrawPath(magePath);
        iTween.DrawPath(swordmanPath);
    }   

    public void Init(int arrNum)
    {
        var middlePoint = GameObject.Find("MiddleWayPoint");
        var leftPoint = GameObject.Find("LeftWayPoint");
        var rightPoint = GameObject.Find("RightWayPoint");

        monsterObj = new GameObject[3] { middlePoint, leftPoint, rightPoint };

        if (monsterObj[arrNum] != null)
        {
            if (arrNum == 0)
                archerPath = monsterObj[arrNum].GetComponentsInChildren<Transform>();
            else if (arrNum == 1)
                magePath = monsterObj[arrNum].GetComponentsInChildren<Transform>();
            else
                swordmanPath = monsterObj[arrNum].GetComponentsInChildren<Transform>();
        }

        switch (arrNum)
        {
            case 0:
                iTween.MoveTo(gameObject, iTween.Hash("path", archerPath, "speed", 10, "orienttopath", true, "looktime", 0.6, "easetype", iTween.EaseType.linear, "movetopath", true));
                break;
            case 1:
                iTween.MoveTo(gameObject, iTween.Hash("path", magePath, "speed", 10, "orienttopath", true, "looktime", 0.6, "easetype", iTween.EaseType.linear, "movetopath", true));
                break;
            case 2:
                iTween.MoveTo(gameObject, iTween.Hash("path", swordmanPath, "speed", 10, "orienttopath", true, "looktime", 0.6, "easetype", iTween.EaseType.linear, "movetopath", true));
                break;
        }
    }

    public void Stop()
    {
        iTween.Stop(gameObject);
    }
}
