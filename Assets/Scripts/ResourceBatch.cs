using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBatch
{
    public ResourceBatch(float oxygen, float dodonium)
    {
        this.oxygen = oxygen;
        this.dodonium = dodonium;
    }

    public float oxygen { get; set; }
    public float dodonium { get; set; }
}
