using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceStationManager : MonoBehaviour
{
    [SerializeField]
    protected float RESOURCE_GENERATION_FREQUENCY = 1f;

    [SerializeField]
    public float OXYGEN_INITIAL_AMOUNT = 250;

    [SerializeField]
    public float OXYGEN_MAX_AMOUNT = 300;
    [SerializeField]
    public const float DODONIUM_INITIAL_AMOUNT = 15;

    private List<MachineAbstract> machines;

    private float productionAccumulatedTime;


    public int dodoAmount { get; set; }
    public float oxygenAmount; // in L
    public float dodoniumAmount; // in kg
    private float bufferedOxygen;
    private float bufferedDodonium;

    // Start is called before the first frame update
    void Start()
    {
        dodoAmount = 1;
        oxygenAmount = OXYGEN_INITIAL_AMOUNT;
        dodoniumAmount = DODONIUM_INITIAL_AMOUNT;
        machines = new List<MachineAbstract>();
        InvokeRepeating("Generate", RESOURCE_GENERATION_FREQUENCY, RESOURCE_GENERATION_FREQUENCY);
    }

    void Update()
    {
        productionAccumulatedTime += Time.deltaTime;
    }

    /// The station is receiving resources which will be waiting to be inserted into the station systems
    public void BufferOxygen(float oxygen)
    {
        bufferedOxygen += oxygen;
    }
    public void BufferDodonium(float dodonium)
    {
        bufferedDodonium += dodonium;
    }
    /// A machine is requesting resources
    public float GiveOxygen(float oxygenRequested)
    {
        if (oxygenAmount > oxygenRequested)
        {
            oxygenAmount -= oxygenRequested;
            return oxygenRequested;
        } else {
            oxygenAmount = 0;
            return oxygenAmount;
        }
    }
    public float GiveDodonium(float dodoniumRequested)
    {
        if (dodoniumAmount > dodoniumRequested)
        {
            dodoniumAmount -= dodoniumRequested;
            return dodoniumRequested;
        } else {
            dodoniumAmount = 0;
            return dodoniumAmount;
        }
    }
    
    /// Generating resources
    private void Generate()
    {
        if (productionAccumulatedTime >= RESOURCE_GENERATION_FREQUENCY && (bufferedOxygen > 0 || bufferedDodonium > 0))
        {
            productionAccumulatedTime = productionAccumulatedTime % RESOURCE_GENERATION_FREQUENCY;
            oxygenAmount += bufferedOxygen;
            dodoniumAmount += bufferedDodonium;
            bufferedOxygen = 0;
            bufferedDodonium = 0;
            Debug.Log("Your station now has " + oxygenAmount + "L of oxygen and " + dodoniumAmount + "kg of dodonium.");
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
