using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LuringMachineAbstract : MachineAbstract
{

    [SerializeField]
    public Transform[] occupationPlaces;
    public DodoManager[] occupyingDodos;

    protected List<DodoManager> dodosPresent;
    [HideInInspector]
    public float occupationTime = 0;

    protected override void Start()
    {
        base.Start();
        dodosPresent = new List<DodoManager>();
        occupyingDodos = new DodoManager[occupationPlaces.Length];
    }
    public virtual bool IsDodoLured(DodoManager dodo)
    {
        return dodosPresent.Count < occupationPlaces.Length;
    }
    public virtual bool StartInteraction(DodoManager dodo)
    {
        if (dodosPresent.Count < occupationPlaces.Length)
        {
            dodosPresent.Add(dodo);
            occupyingDodos[dodo.machineOccupationId] = dodo;
            return true;
        }
        return false;
    }
    public virtual void FinishInteraction(DodoManager dodo)
    {
        dodosPresent.Remove(dodo);
        occupyingDodos[dodo.machineOccupationId] = null;
    }
    public virtual void CancelInteraction(DodoManager dodo)
    {
        dodosPresent.Remove(dodo);
        occupyingDodos[dodo.machineOccupationId] = null;
    }
}
