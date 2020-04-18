using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MachineAbstract
{
    public const float MAX_DODONIUM_STORAGE = 5;
    public const float RESOURCE_RECEIVE_FREQUENCY = 2;

    private bool foodIsServed;
    private bool dodoEating;

    override protected void Start()
    {
        base.Start();
        foodIsServed = false;
        dodoEating = false;
        isReceivingDodonium = true;
        resourceReceiveFrequency = RESOURCE_RECEIVE_FREQUENCY;
        maxDodoniumStorage = MAX_DODONIUM_STORAGE;
    }

    override protected void OnMouseDown()
    {
        foodIsServed = true;
        Debug.Log("Need a dispenser here!");
    }

    /// Host a dodo that came eat for the given amount of time
    public void HostEating(float dodoEatTime)
    {
        if (foodIsServed)
        {
            dodoEating = true;
            Invoke("BidFarewellToDodo", dodoEatTime);
        }
    }
    /// Bid farewell to a dodo that finished eating
    private void FinishEating() 
    {
        dodoEating = false;
        // TODO: Send fullness info to dodo
        foodIsServed = false;
    }
    /// The dodo left without finishing his plate!
    public void CancelEating()
    {
        dodoEating = false;
    }
}
