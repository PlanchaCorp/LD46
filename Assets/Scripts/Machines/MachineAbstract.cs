using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MachineAbstract : MonoBehaviour
{
    [SerializeField]
    protected float oxygenPerMinuteGenerating = 0;
    [SerializeField]
    protected float dodomassPerMinuteGenerating = 0;

    private SpaceStationManager spaceStationManager;

    protected virtual void Start()
    {
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
    }

    /// Mouse interaction to override
    protected virtual void OnMouseDown()
    {
        Debug.Log("Non implemented interaction on \"" + gameObject.name + "\" machine.");
    }
    
    /// Default resource generation behavior
    public virtual ResourceBatch Produce(float generatingTime)
    {
        return new ResourceBatch(oxygenPerMinuteGenerating * generatingTime, dodomassPerMinuteGenerating * generatingTime);
    }
}
