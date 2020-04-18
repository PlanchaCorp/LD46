using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MachineAbstract : MonoBehaviour
{
    protected float resourceProductionFrequency = 0.5f;

    private SpaceStationManager spaceStationManager;

    protected float oxygenPerMinuteGenerating = 0;
    protected float dodomassPerMinuteGenerating = 0;
    protected float oxygenAccumulated = 0;
    protected float dodomassAccumulated = 0;
    protected float maxOxygenStorage = 0;
    protected float maxDodomassStorage = 0;
    protected float accumulatedTime = 0;

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
    public virtual void Produce()
    {
        if (accumulatedTime >= resourceProductionFrequency && (oxygenPerMinuteGenerating > 0 || dodomassPerMinuteGenerating > 0 || oxygenAccumulated > 0 || dodomassAccumulated > 0))
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
