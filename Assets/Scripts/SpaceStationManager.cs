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
    public float OXYGEN_PER_DODO_PER_MINUTE = 2.0f;
    [SerializeField]
    public const float DODONIUM_INITIAL_AMOUNT = 15;
    [SerializeField]
    public GameObject dodoPrefab;

    private List<MachineAbstract> machines;
    private UiDisplay uiDisplay;


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
        uiDisplay = GameObject.FindGameObjectWithTag("MainUI").GetComponent<UiDisplay>();
        machines = new List<MachineAbstract>();
        InvokeRepeating("Generate", RESOURCE_GENERATION_FREQUENCY, RESOURCE_GENERATION_FREQUENCY);
        InvokeRepeating("ConsumeOxygen", RESOURCE_GENERATION_FREQUENCY, RESOURCE_GENERATION_FREQUENCY);
    }

    void Update()
    {
    }

    public void AddDodo(MachineAbstract birthPlace)
    {
        dodoAmount++;
        GameObject newDodo = Instantiate(dodoPrefab);
        newDodo.transform.position = birthPlace.transform.position;
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
        if (bufferedOxygen > 0 || bufferedDodonium > 0)
        {
            oxygenAmount += bufferedOxygen;
            dodoniumAmount += bufferedDodonium;
            uiDisplay.AnimateAmounts(bufferedDodonium, bufferedOxygen);
            bufferedOxygen = 0;
            bufferedDodonium = 0;
            uiDisplay.UpdateAmounts(dodoniumAmount, oxygenAmount / OXYGEN_MAX_AMOUNT);
            Debug.Log("Your station now has " + oxygenAmount + "L of oxygen and " + dodoniumAmount + "kg of dodonium.");
        }
    }

    private void ConsumeOxygen()
    {
        oxygenAmount -= Mathf.Max(dodoAmount * OXYGEN_PER_DODO_PER_MINUTE * (RESOURCE_GENERATION_FREQUENCY / 60), 0);
        if (oxygenAmount == 0)
        {
            Debug.Log("The dodos are now extinct.");
            // TODO: GameOver after dodo death animation
        } else if (oxygenAmount < OXYGEN_PER_DODO_PER_MINUTE * dodoAmount * 5) {
            Debug.Log("The dodos are struggling to breathe...");
        }
        uiDisplay.UpdateAmounts(dodoniumAmount, oxygenAmount / OXYGEN_MAX_AMOUNT);
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
