using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refiner : MachineAbstract
{
    public const float MAX_OXYGEN_STORAGE = 80;
    public const float RESOURCE_PRODUCTION_FREQUENCY = 10;
    public const float OXYGEN_PRODUCTION_AMOUNT = 1.5f;

    
    protected override void Start()
    {
        base.Start();
        maxDodoniumStorage = MAX_OXYGEN_STORAGE;
        resourceProductionFrequency = RESOURCE_PRODUCTION_FREQUENCY;
        dodoniumPerMinuteGenerating = OXYGEN_PRODUCTION_AMOUNT;
        dodoniumAccumulated = 5;
    }

    protected override void OnMouseDown()
    {
        Debug.Log("Refining the refinement of the refinest");
    }
}
