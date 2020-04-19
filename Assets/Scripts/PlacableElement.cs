using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element", menuName = "Elements")]
public class PlacableElement : ScriptableObject
{
    public Sprite icon;
    public int size;

    public GameObject element;
}
