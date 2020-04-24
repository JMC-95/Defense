using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{

    public GameObject newCube;
    public Vector3 nonePosition;

    private void Start()
    {
        nonePosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var trans = GetClickedObject();
            if (trans != nonePosition)  
            {
                GameObject tp = Instantiate(newCube) as GameObject;
                tp.transform.position = trans;
            }
        }
    }

    private Vector3 GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
        {
            target = hit.collider.gameObject;
            return hit.point;
        }

        return nonePosition;
    }

}
