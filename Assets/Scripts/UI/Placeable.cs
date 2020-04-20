using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Placeable", menuName = "Placeable")]
public class PlaceableElement : ScriptableObject
{
    public Sprite icon;
    public int size;

    public GameObject element;
}
