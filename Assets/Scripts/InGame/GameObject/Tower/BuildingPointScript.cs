using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPointScript : MonoBehaviour
{
    public bool OnTower { get; set; } = false;
    public bool OnEmpty { get; set; } = true;
    public bool OnCons { get; set; } = false;

    public void BuildStart()
    {
        OnEmpty = false;
        OnCons = true;
    }

    public void BuildComplete()
    {
        OnCons = false;
        OnTower = true;
    }

    public void Sell()
    {
        OnTower = false;
        OnEmpty = true;
    }

    public void Upgrade()
    {
        OnCons = true;
        OnTower = false;
    }
}
