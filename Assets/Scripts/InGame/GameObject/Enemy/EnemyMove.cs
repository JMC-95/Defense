using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private GameObject[] monsterObj;

    public Transform[] middlePath;
    public Transform[] leftPath;
    public Transform[] rightPath;

    public int Gold;
    public int Speed;
    public int Line;
    public bool isArleaySlow = false;

    void OnDrawGizmos()
    {
        iTween.DrawPath(middlePath);
        iTween.DrawPath(leftPath);
        iTween.DrawPath(rightPath);
    }

    public void Init(int line, int speed, int gold)
    {
        Speed = speed;
        Gold = gold;
        Line = line;
        isArleaySlow = false;

        var middlePoint = GameObject.Find("MiddleWayPoint");
        var leftPoint = GameObject.Find("LeftWayPoint");
        var rightPoint = GameObject.Find("RightWayPoint");

        monsterObj = new GameObject[3] { leftPoint, middlePoint, rightPoint };

        if (monsterObj[line] != null)
        {
            if (line == 0)
                leftPath = monsterObj[line].GetComponentsInChildren<Transform>();
            else if (line == 1)
                middlePath = monsterObj[line].GetComponentsInChildren<Transform>();
            else
                rightPath = monsterObj[line].GetComponentsInChildren<Transform>();
        }


        switch (line)
        {
            case 0:
                iTween.MoveTo(gameObject, iTween.Hash("path", leftPath, "speed", Speed, "orienttopath", true, "looktime", 0.6, "easetype", iTween.EaseType.linear, "movetopath", true));
                break;
            case 1:
                iTween.MoveTo(gameObject, iTween.Hash("path", middlePath, "speed", Speed, "orienttopath", true, "looktime", 0.6, "easetype", iTween.EaseType.linear, "movetopath", true));
                break;
            case 2:
                iTween.MoveTo(gameObject, iTween.Hash("path", rightPath, "speed", Speed, "orienttopath", true, "looktime", 0.6, "easetype", iTween.EaseType.linear, "movetopath", true));
                break;
        }
    }

    public void ResetSlow()
    {
        isArleaySlow = false;
        iTween.MoveTo(gameObject, iTween.Hash("speed", Speed));
    }

    public void SetSlow(int slowValue)
    {
        if (!isArleaySlow)
        {
            isArleaySlow = true;
            int speed = Speed - slowValue;
            if (speed < 0)
            {
                speed = 1;
            }

            iTween.MoveTo(gameObject, iTween.Hash("speed", speed));
        }
    }

    public void Stop()
    {
        iTween.Stop(gameObject);
    }
}
