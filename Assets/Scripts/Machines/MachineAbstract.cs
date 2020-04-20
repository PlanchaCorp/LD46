using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MachineAbstract : MonoBehaviour
{
    protected float resourceProductionFrequency = 0.5f;
    protected float resourceReceiveFrequency = 0.5f;

    protected SpaceStationManager spaceStationManager;

    protected float oxygenPerMinuteGenerating = 0;
    protected float dodoniumPerMinuteGenerating = 0;
    protected float oxygenAccumulated = 0;
    protected float dodoniumAccumulated = 0;
    protected float maxOxygenStorage = 0;
    protected float maxDodoniumStorage = 0;
    protected float receiveAccumulatedTime = 0;
    protected float productionAccumulatedTime = 0;
    protected bool isReceivingOxygen = false;
    protected bool isReceivingDodonium = false;


    protected virtual void Start()
    {
        productionAccumulatedTime = 0;
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
        productionAccumulatedTime += Time.deltaTime;
        receiveAccumulatedTime += Time.deltaTime;
    }

    /// Mouse interaction to override
    protected virtual void OnMouseDown()
    {
        Debug.Log("Non implemented interaction on \"" + gameObject.name + "\" machine.");
    }

    /// Generating resources
    public virtual void Produce()
    {
        if (productionAccumulatedTime >= resourceProductionFrequency && 
                (oxygenPerMinuteGenerating > 0 || dodoniumPerMinuteGenerating > 0 || oxygenAccumulated > 0 || dodoniumAccumulated > 0))
        {
            if (spaceStationManager != null) 
            {
                if (!isReceivingOxygen)
                {
                    spaceStationManager.BufferOxygen(oxygenPerMinuteGenerating * (productionAccumulatedTime / 60) + oxygenAccumulated);
                    oxygenAccumulated = 0;
                }
                if (!isReceivingDodonium)
                {
                    spaceStationManager.BufferDodonium(dodoniumPerMinuteGenerating * (productionAccumulatedTime / 60) + dodoniumAccumulated);
                    dodoniumAccumulated = 0;
                }
            }
            productionAccumulatedTime %= resourceProductionFrequency;
        }
    }
    /// Requesting resources
    public virtual void Receive()
    {
        if (receiveAccumulatedTime >= resourceReceiveFrequency && 
                (isReceivingOxygen || isReceivingDodonium) &&
                (oxygenAccumulated < maxOxygenStorage || dodoniumAccumulated < maxDodoniumStorage))
        {
            float unconsumedTime = receiveAccumulatedTime % resourceReceiveFrequency;
            if (spaceStationManager != null) 
            {
                if (isReceivingOxygen)
                {
                    oxygenAccumulated += spaceStationManager.GiveOxygen(oxygenPerMinuteGenerating * (productionAccumulatedTime / 60) + oxygenAccumulated);
                }
                if (isReceivingOxygen)
                {
                    dodoniumAccumulated += spaceStationManager.GiveDodonium(dodoniumPerMinuteGenerating * (productionAccumulatedTime / 60) + dodoniumAccumulated);
                }
            }
            receiveAccumulatedTime = unconsumedTime;
        }
    }
}
