using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Litter : MachineAbstract
{
    public const float MAX_DODONIUM_STORAGE = 20;
    public const float RESOURCE_PRODUCTION_FREQUENCY = 1;

    [SerializeField]
    private Recycler recycler;

    private List<int> dodosPresent;

    protected override void Start()
    {
        base.Start();
        maxDodoniumStorage = MAX_DODONIUM_STORAGE;
        resourceProductionFrequency = RESOURCE_PRODUCTION_FREQUENCY;
        dodosPresent = new List<int>();
        dodoniumAccumulated = 0;
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
        dodoniumAccumulated += weight;
        if (dodoniumAccumulated > maxDodoniumStorage)
        {
            float dodoniumSurplus = dodoniumAccumulated - maxDodoniumStorage;
            dodoniumAccumulated = maxDodoniumStorage;
            return dodoniumSurplus;
        }
        return 0;
    }

    /// Preventing default production behaviour to send to the litter instead
    public override void Produce() 
    {
        if (recycler != null)
        {
            if (productionAccumulatedTime >= resourceProductionFrequency && dodoniumAccumulated > 0)
            {
                float unconsumedTime = productionAccumulatedTime % resourceProductionFrequency;
                float remainingDodonium = recycler.SendToRecycler(dodoniumAccumulated);
                productionAccumulatedTime = unconsumedTime;
                dodoniumAccumulated = remainingDodonium;
            }
        }
        // TODO: If no recycler attached, produce a nice animation
    }

    protected override void OnMouseDown()
    {
        Debug.Log("You dodoed in the wrong neighborhood");
        FillLitter(1.8f);
    }
}
