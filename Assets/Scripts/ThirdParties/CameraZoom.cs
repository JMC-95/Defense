using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float moveSpeed;
    public Transform cam;

    Vector2 prevPos = Vector2.zero;
    Vector3 pos;
    float prevDistance = 0.0f;

    void Start()
    {
        cam = Camera.main.transform;
        pos = cam.position;
    }

    public void OnDrag()
    {
        int touchCount = Input.touchCount;

        if (touchCount == 1 && pos.y < 80)
        {
            if (prevPos == Vector2.zero)
            {
                prevPos = Input.GetTouch(0).position;
                return;
            }

            Vector2 dir = (Input.GetTouch(0).position - prevPos).normalized;
            Vector3 vec = new Vector3(dir.x, 0, dir.y);

            cam.position -= vec * moveSpeed * Time.deltaTime;
            prevPos = Input.GetTouch(0).position;

            if (pos.y < 50)
            {
                if (cam.position.x < -15) cam.position = new Vector3(-15, cam.position.y, cam.position.z);
                if (cam.position.x > 40) cam.position = new Vector3(40, cam.position.y, cam.position.z);
                if (cam.position.z < -80) cam.position = new Vector3(cam.position.x, cam.position.y, -80);
                if (cam.position.z > -30) cam.position = new Vector3(cam.position.x, cam.position.y, -30);
            }
            else if (pos.y < 60)
            {
                if (cam.position.x < -5) cam.position = new Vector3(-5, cam.position.y, cam.position.z);
                if (cam.position.x > 30) cam.position = new Vector3(30, cam.position.y, cam.position.z);
                if (cam.position.z < -81) cam.position = new Vector3(cam.position.x, cam.position.y, -81);
                if (cam.position.z > -50) cam.position = new Vector3(cam.position.x, cam.position.y, -50);
            }
            else if (pos.y < 70)
            {
                if (cam.position.x < 0) cam.position = new Vector3(0, cam.position.y, cam.position.z);
                if (cam.position.x > 20) cam.position = new Vector3(20, cam.position.y, cam.position.z);
                if (cam.position.z < -82) cam.position = new Vector3(cam.position.x, cam.position.y, -82);
                if (cam.position.z > -65) cam.position = new Vector3(cam.position.x, cam.position.y, -60);
            }
            else if (pos.y < 80)
            {
                if (cam.position.x < 10) cam.position = new Vector3(10, cam.position.y, cam.position.z);
                if (cam.position.x > 10) cam.position = new Vector3(10, cam.position.y, cam.position.z);
                if (cam.position.z < -83) cam.position = new Vector3(cam.position.x, cam.position.y, -83);
                if (cam.position.z > -75) cam.position = new Vector3(cam.position.x, cam.position.y, -75);
            }
        }
        else if (touchCount == 2)
        {
            if (prevDistance == 0)
            {
                prevDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                return;
            }

            float curDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            float move = prevDistance - curDistance;

            if (move < 0) pos.y -= moveSpeed * Time.deltaTime;
            else if (move > 0) pos.y += moveSpeed * Time.deltaTime;

            if (pos.y < 40) pos.y = 40;
            if (pos.y > 82) pos.y = 82;

            cam.position = pos;
            prevDistance = curDistance;
        }
    }

    public void ExitDrag()
    {
        prevPos = Vector2.zero;
        prevDistance = 0.0f;
    }
}
