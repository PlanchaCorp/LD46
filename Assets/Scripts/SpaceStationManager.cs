using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStationManager : MonoBehaviour
{
    [SerializeField]
    public const float RESOURCE_GENERATION_FREQUENCY = 0.5f;
    [SerializeField]
    public const float OXYGEN_INITIAL_AMOUNT = 100;
    [SerializeField]
    public const float DODOMASS_INITIAL_AMOUNT = 15;

    private List<MachineAbstract> machines;

    private float timeElapsed;


    private float oxygenAmount; // in L
    private float dodomassAmount; // in kg

    // Start is called before the first frame update
    void Start()
    {
        oxygenAmount = OXYGEN_INITIAL_AMOUNT;
        dodomassAmount = DODOMASS_INITIAL_AMOUNT;
        machines = new List<MachineAbstract>();
        InvokeRepeating("Generate", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
    }
    
    /// Generating resources
    private void Generate()
    {
        if (timeElapsed >= RESOURCE_GENERATION_FREQUENCY)
        {
            float unconsumedTime = timeElapsed % RESOURCE_GENERATION_FREQUENCY;
            List<ResourceBatch> resourceBatches = new List<ResourceBatch>();
            foreach(MachineAbstract machine in machines)
            {
                resourceBatches.Add(machine.Produce((timeElapsed - unconsumedTime) / 60));
            }
            timeElapsed = unconsumedTime;
            AddResources(resourceBatches);
        }
    }

    /// Add resources to stock
    private void AddResources(List<ResourceBatch> resources)
    {
        ResourceBatch totalResources = new ResourceBatch(0, 0);
        foreach(ResourceBatch resource in resources)
        {
            totalResources.oxygen += resource.oxygen;
            totalResources.dodomass += resource.dodomass;
        }
        // This is the place for cool user feedback about added resources
        oxygenAmount += totalResources.oxygen;
        dodomassAmount += totalResources.dodomass;
    }

/// Register and unregister machines from space station
    public void RegisterMachine(MachineAbstract machine)
    {
        machines.Add(machine);
    }
    public void UnregisterMachine(MachineAbstract machine)
    {
        machines.Remove(machine);
    }
}
