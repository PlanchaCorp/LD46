using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Litter : LuringMachineAbstract
{
    public const float RELAX_TIME = 2.5f;
    public const float MAX_DODONIUM_STORAGE = 20;
    public const float RESOURCE_PRODUCTION_FREQUENCY = 1;

    [SerializeField]
    private Recycler recycler;

    protected override void Start()
    {
        base.Start();
        occupationTime = RELAX_TIME;
        maxDodoniumStorage = MAX_DODONIUM_STORAGE;
        resourceProductionFrequency = RESOURCE_PRODUCTION_FREQUENCY;
        dodoniumAccumulated = 0;
    }

    public override bool IsDodoLured(DodoManager dodo)
    {
        return dodo.mealTimeAgo > 0;
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
    }

    /// Dodo is done and is leaving!
    public override void FinishInteraction(DodoManager dodo) 
    {
        dodo.mealTimeAgo = 0;
        dodoniumAccumulated++;
        if (dodoniumAccumulated > maxDodoniumStorage)
        {
            dodoniumAccumulated = maxDodoniumStorage;
        }

        dodo.Animate("RelaxAnimation", DodoManager.AnimationPosition.TopRight);
        GameObject animation = Instantiate(Resources.Load<GameObject>("RelaxAnimation"));
        if (animation == null) {
            Debug.LogError("Could not find RelaxAnimation prefab in Resources folder!");
        } else {
            animation.transform.parent = transform;
            animation.transform.position = dodo.transform.position + new Vector3(0.4f, 0.6f, 0);
        }

        dodosPresent.Remove(dodo);
    }
}
