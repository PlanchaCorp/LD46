using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refiner : MachineAbstract
{
    public const float MAX_OXYGEN_STORAGE = 80;
    public const float RESOURCE_PRODUCTION_FREQUENCY = 10;
    public const float OXYGEN_PRODUCTION_AMOUNT = 15;

    
    protected override void Start()
    {
        base.Start();
        maxOxygenStorage = MAX_OXYGEN_STORAGE;
        resourceProductionFrequency = RESOURCE_PRODUCTION_FREQUENCY;
        oxygenPerMinuteGenerating = OXYGEN_PRODUCTION_AMOUNT;
        oxygenAccumulated = 0;
    }

    protected override void OnMouseDown()
    {
        Debug.Log("Refining the refinement of the refinest");
    }
}
