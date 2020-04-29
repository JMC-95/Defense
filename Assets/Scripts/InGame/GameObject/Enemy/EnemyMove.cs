using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private GameObject[] monsterObj;

    public Transform[] middlePath;
    public Transform[] leftPath;
    public Transform[] rightPath;

    void OnDrawGizmos()
    {
        iTween.DrawPath(middlePath);
        iTween.DrawPath(leftPath);
        iTween.DrawPath(rightPath);
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
                middlePath = monsterObj[arrNum].GetComponentsInChildren<Transform>();
            else if (arrNum == 1)
                leftPath = monsterObj[arrNum].GetComponentsInChildren<Transform>();
            else
                rightPath = monsterObj[arrNum].GetComponentsInChildren<Transform>();
        }

        switch (arrNum)
        {
            case 0:
                iTween.MoveTo(gameObject, iTween.Hash("path", middlePath, "speed", 10, "orienttopath", true, "looktime", 0.6, "easetype", iTween.EaseType.linear, "movetopath", true));
                break;
            case 1:
                iTween.MoveTo(gameObject, iTween.Hash("path", leftPath, "speed", 10, "orienttopath", true, "looktime", 0.6, "easetype", iTween.EaseType.linear, "movetopath", true));
                break;
            case 2:
                iTween.MoveTo(gameObject, iTween.Hash("path", rightPath, "speed", 10, "orienttopath", true, "looktime", 0.6, "easetype", iTween.EaseType.linear, "movetopath", true));
                break;
        }
    }

    public void Stop()
    {
        iTween.Stop(gameObject);
    }
}
