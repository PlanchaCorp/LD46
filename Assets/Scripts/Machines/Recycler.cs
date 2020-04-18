using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycler : MachineAbstract
{
    public const float MAX_DODONIUM_STORAGE = 80;
    public const float RESOURCE_PRODUCTION_FREQUENCY = 4;
    
    protected override void Start()
    {
        base.Start();
        maxDodoniumStorage = MAX_DODONIUM_STORAGE;
        resourceProductionFrequency = RESOURCE_PRODUCTION_FREQUENCY;
        dodoniumAccumulated = 0;
    }

    public float SendToRecycler(float dodonium)
    {
        dodoniumAccumulated += dodonium;
        if (dodoniumAccumulated > maxDodoniumStorage)
        {
            float dodoniumSurplus = dodoniumAccumulated - maxDodoniumStorage;
            dodoniumAccumulated = maxDodoniumStorage;
            return dodoniumSurplus;
        }
        return 0;
    }

    protected override void OnMouseDown()
    {
        Debug.Log("Yo I'll dodo you what I want, what I dodo dodo want");
    }
}
