using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycler : MachineAbstract
{
    public const float MAX_DODOMASS_STORAGE = 80;
    public const float RESOURCE_PRODUCTION_FREQUENCY = 4;
    
    protected override void Start()
    {
        base.Start();
        maxDodomassStorage = MAX_DODOMASS_STORAGE;
        resourceProductionFrequency = RESOURCE_PRODUCTION_FREQUENCY;
        dodomassAccumulated = 0;
    }

    public float SendToRecycler(float dodomass)
    {
        dodomassAccumulated += dodomass;
        if (dodomassAccumulated > maxDodomassStorage)
        {
            float dodomassSurplus = dodomassAccumulated - maxDodomassStorage;
            dodomassAccumulated = maxDodomassStorage;
            return dodomassSurplus;
        }
        return 0;
    }

    protected override void OnMouseDown()
    {
        Debug.Log("Yo I'll dodo you what I want, what I dodo dodo want");
    }
}
