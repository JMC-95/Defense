using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPointScript : MonoBehaviour
{
    public bool OnTower { get; set; } = false;
    public bool OnEmpty { get; set; } = true;
    public bool OnCons { get; set; } = false;

    public int TowerType;

    private void Start()
    {
        TowerType = -1;
    }

    public void BuildStart(int towerType)
    {
        OnEmpty = false;
        OnCons = true;
        TowerType = towerType;
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
        TowerType = -1;
    }

    public void Upgrade()
    {
        OnCons = true;
        OnTower = false;
    }
}
