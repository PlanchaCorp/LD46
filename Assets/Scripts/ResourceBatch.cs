using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBatch
{
    public ResourceBatch(float oxygen, float dodomass)
    {
        this.oxygen = oxygen;
        this.dodomass = dodomass;
    }

    public float oxygen { get; set; }
    public float dodomass { get; set; }
}
