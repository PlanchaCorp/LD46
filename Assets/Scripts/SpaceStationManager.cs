using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceStationManager : MonoBehaviour
{
    [SerializeField]
    protected float RESOURCE_GENERATION_FREQUENCY = 0.5f;

    [SerializeField]
    public const float OXYGEN_INITIAL_AMOUNT = 100;
    [SerializeField]
    public const float DODOMASS_INITIAL_AMOUNT = 15;

    private List<MachineAbstract> machines;

    private float accumulatedTime;


    private float oxygenAmount; // in L
    private float dodomassAmount; // in kg
    private float bufferedOxygen;
    private float bufferedDodomass;

    // Start is called before the first frame update
    void Start()
    {
        oxygenAmount = OXYGEN_INITIAL_AMOUNT;
        dodomassAmount = DODOMASS_INITIAL_AMOUNT;
        machines = new List<MachineAbstract>();
        InvokeRepeating("Generate", RESOURCE_GENERATION_FREQUENCY, RESOURCE_GENERATION_FREQUENCY);
    }

    void Update()
    {
        accumulatedTime += Time.deltaTime;
    }

    public void BufferResources(float oxygen, float dodomass)
    {
        bufferedOxygen += oxygen;
        bufferedDodomass += dodomass;
    }
    
    /// Generating resources
    private void Generate()
    {
        if (accumulatedTime >= RESOURCE_GENERATION_FREQUENCY)
        {
            accumulatedTime = accumulatedTime % RESOURCE_GENERATION_FREQUENCY;
            oxygenAmount += bufferedOxygen;
            dodomassAmount += bufferedDodomass;
            bufferedOxygen = 0;
            bufferedDodomass = 0;
        }
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
