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
    public bool isResetSlow = false;

    float curSpeed;
    float slowSpeed;
    float originSpeed;

    public int debug = 10;

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
        isResetSlow = false;

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


        switch (Line)
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

    public void LateUpdate()
    {
        var itween = GetComponent<iTween>();
        if (itween)
        {
            if (isArleaySlow)
            {
                if (curSpeed < slowSpeed)
                {
                    itween.time = itween.time + 0.01f;
                    curSpeed = itween.time;
                }
            }

            if (isResetSlow)
            {
                if (curSpeed > originSpeed)
                {
                    itween.time = itween.time - 0.01f;
                    curSpeed = itween.time;
                }

            }
        }
    }

    public void ResetSlow()
    {
        isArleaySlow = false;
        isResetSlow = true;
    }

    public void SetSlow(float slowValue)
    {
        if (!isArleaySlow)
        {
            isArleaySlow = true;

            originSpeed = GetComponent<iTween>().time;
            curSpeed = originSpeed;
            slowSpeed = originSpeed + originSpeed * slowValue / 100.0f;
        }
    }

    public void Stop()
    {
        iTween.Stop(gameObject);
    }
}
