using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : LuringMachineAbstract
{
    public const float EAT_TIME = 5.0f;
    public const float MAX_DODONIUM_STORAGE = 5;
    public const int FOOD_STORAGE = 3;
    public const float RESOURCE_RECEIVE_FREQUENCY = 2;

    private int foodServed;

    override protected void Start()
    {
        base.Start();
        occupationTime = EAT_TIME;
        foodServed = 0;
        isReceivingDodonium = true;
        resourceReceiveFrequency = RESOURCE_RECEIVE_FREQUENCY;
        maxDodoniumStorage = MAX_DODONIUM_STORAGE;
    }

    public override bool IsDodoLured(DodoManager dodo)
    {
        return dodo.hunger > DodoManager.DODO_HUNGER && foodServed > dodosPresent.Count && base.IsDodoLured(dodo);
    }

    protected override void OnMouseDown()
    {
        if (foodServed < FOOD_STORAGE && dodoniumAccumulated > 0)
        {
            int fillingDodonium = (int) Mathf.Min(Mathf.Floor(dodoniumAccumulated), FOOD_STORAGE - foodServed);
            dodoniumAccumulated -= fillingDodonium;
            foodServed += fillingDodonium;
            // TODO: User feedback for serving food
        }
        Debug.Log("Need a dispenser here!");
    }

    /// Host a dodo that came eat for the given amount of time
    public override bool StartInteraction(DodoManager dodo)
    {
        if (dodo.hunger > DodoManager.DODO_HUNGER && foodServed > dodosPresent.Count)
            return base.StartInteraction(dodo);
        return false;
    }
    /// Bid farewell to a dodo that finished eating
    public override void FinishInteraction(DodoManager dodo) 
    {
        dodo.hunger = 0;
        dodo.hungerAnimationProgress = 0;
        dodo.mealTimeAgo += 0.01f;
        foodServed--;

        dodo.Animate("FedAnimation", DodoManager.AnimationPosition.TopRight);

        base.FinishInteraction(dodo);
    }
}
