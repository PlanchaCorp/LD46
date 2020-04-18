using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Litter : MachineAbstract
{
    public const float MAX_DODOMASS_STORAGE = 20;
    public const float RESOURCE_PRODUCTION_FREQUENCY = 1;

    private Recycler recycler;

    private List<int> dodosPresent;

    protected override void Start()
    {
        maxDodomassStorage = MAX_DODOMASS_STORAGE;
        resourceProductionFrequency = RESOURCE_PRODUCTION_FREQUENCY;
        dodosPresent = new List<int>();
        dodomassAccumulated = 0;
    }

    /// Counting all the dodos coming in and out
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Dodo"))
        {
            dodosPresent.Add(collider.GetInstanceID());
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Dodo"))
        {
            dodosPresent.Remove(collider.GetInstanceID());
        }
    }

    /// Dodo sending its gift to the litter. Returns the amount of gift that could not be stored
    public float FillLitter(float weight)
    {
        dodomassAccumulated += weight;
        if (dodomassAccumulated > maxDodomassStorage)
        {
            float dodomassSurplus = dodomassAccumulated - maxDodomassStorage;
            dodomassAccumulated = maxDodomassStorage;
            return dodomassSurplus;
        }
        return 0;
    }

    /// Preventing default production behaviour to send to the litter instead
    protected override void Produce() 
    {
        if (recycler != null)
        {
            if (accumulatedTime >= resourceProductionFrequency && dodomassAccumulated > 0)
            {
                float unconsumedTime = accumulatedTime % resourceProductionFrequency;
                float remainingDodomass = recycler.SendToRecycler(dodomassAccumulated);
                accumulatedTime = unconsumedTime;
                dodomassAccumulated = remainingDodomass;
            }
        }
        // TODO: If no recycler attached, produce a nice animation
    }

    protected override void OnMouseDown()
    {
        Debug.Log("You dodoed in the wrong neighborhood");
    }
}
