using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MachineAbstract
{
    private bool foodIsServed;

    override protected void Start()
    {
        base.Start();
        foodIsServed = false;
    }

    override protected void OnMouseDown()
    {
        foodIsServed = true;
        Debug.Log("Need a dispenser here!");
    }
}
