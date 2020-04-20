using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incubator : LuringMachineAbstract
{
    public const float REPRODUCE_TIME = 6.0f;
    public const float RESOURCE_PRODUCTION_FREQUENCY = 1;

    protected override void Start()
    {
        base.Start();
        occupationTime = REPRODUCE_TIME;
        resourceProductionFrequency = RESOURCE_PRODUCTION_FREQUENCY;
    }

    public override bool IsDodoLured(DodoManager dodo)
    {
        return dodo.readyToEgg >= 60.0f && base.IsDodoLured(dodo);
    }

    protected override void OnMouseDown()
    {
        Debug.Log("You dodoed in the wrong neighborhood");
    }

    /// Dodo has created a new dodo!
    public override void FinishInteraction(DodoManager dodo) 
    {
        dodo.readyToEgg = 0;
        spaceStationManager.AddDodo(this);
    }
}
