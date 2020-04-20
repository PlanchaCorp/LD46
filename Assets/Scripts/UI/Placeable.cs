using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Placeable", menuName = "Placeable")]
public class PlaceableElement : ScriptableObject
{
    public Sprite icon;
    public Vector2 size;

    public GameObject element;
}
