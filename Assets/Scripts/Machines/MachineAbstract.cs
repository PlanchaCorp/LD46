using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MachineAbstract : MonoBehaviour
{
    protected float resourceProductionFrequency = 0.5f;

    private SpaceStationManager spaceStationManager;

    protected float oxygenPerMinuteGenerating;
    protected float dodomassPerMinuteGenerating;
    protected float oxygenAccumulated;
    protected float dodomassAccumulated;
    protected float maxOxygenStorage;
    protected float maxDodomassStorage;
    protected float accumulatedTime;

    protected virtual void Start()
    {
        accumulatedTime = 0;
        GameObject spaceStation = GameObject.FindWithTag("SpaceStation");
        if (spaceStation == null)
        {
            Debug.LogError("Unable to find SpaceStation gameObject to register \"" + gameObject.name + "\" machine!");
            return;
        }
        spaceStationManager = spaceStation.GetComponent<SpaceStationManager>();
        if (spaceStationManager == null)
        {
            Debug.LogError("Unable to find SpaceStation manager script to register \"" + gameObject.name + "\" machine!");
        }
        spaceStationManager.RegisterMachine(this);

        InvokeRepeating("Produce", resourceProductionFrequency, resourceProductionFrequency);
    }

    protected virtual void Update()
    {
        accumulatedTime += Time.deltaTime;
    }

    /// Mouse interaction to override
    protected virtual void OnMouseDown()
    {
        Debug.Log("Non implemented interaction on \"" + gameObject.name + "\" machine.");
    }

    /// Generating resources
    protected virtual void Produce()
    {
        if (accumulatedTime >= resourceProductionFrequency && oxygenPerMinuteGenerating > 0 && dodomassPerMinuteGenerating > 0)
        {
            float unconsumedTime = accumulatedTime % resourceProductionFrequency;
            if (spaceStationManager != null) 
            {
                spaceStationManager.BufferResources(oxygenPerMinuteGenerating * (accumulatedTime / 60) + oxygenAccumulated, 
                        dodomassPerMinuteGenerating * (accumulatedTime / 60) + dodomassAccumulated);
            }
            accumulatedTime = unconsumedTime;
            oxygenAccumulated = 0;
            dodomassAccumulated = 0;
        }
    }
}
