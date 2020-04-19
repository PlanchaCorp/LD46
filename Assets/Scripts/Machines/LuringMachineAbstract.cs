using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LuringMachineAbstract : MachineAbstract
{

    [SerializeField]
    public Transform[] occupationPlaces;

    protected List<DodoManager> dodosPresent;
    [HideInInspector]
    public float occupationTime = 0;

    protected override void Start()
    {
        base.Start();
        dodosPresent = new List<DodoManager>();
    }
    public abstract bool IsDodoLured(DodoManager dodo);
    public virtual void StartInteraction(DodoManager dodo)
    {
        if (dodosPresent.Count < occupationPlaces.Length)
        {
            dodosPresent.Add(dodo);
        }
    }
    public virtual void FinishInteraction(DodoManager dodo)
    {
        dodosPresent.Remove(dodo);
    }
    public virtual void CancelInteraction(DodoManager dodo)
    {
        dodosPresent.Remove(dodo);
    }
}
