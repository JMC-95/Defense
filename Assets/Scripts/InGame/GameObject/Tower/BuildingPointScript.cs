using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPointScript : MonoBehaviour
{

    bool OnBuilding = false;

    public bool isOnBuilding() { return OnBuilding; }

    public void SetBuilding(bool onBuilding)
    {
        OnBuilding = onBuilding;
    }
}
