using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public Highlighter highlight;
    [SerializeField]
    public Image[] slots;
    [SerializeField]
    public PlaceableElement[] placables;

    void Start()
    {
        for (int i = 0; i < slots.Length; i++) {
            if (placables.Length > i) {
                slots[i].sprite = placables[i].icon;
                float currentSize = slots[i].rectTransform.sizeDelta.x;
                slots[i].rectTransform.sizeDelta = new Vector2(currentSize * 3 / 5, currentSize * 3 / 5);
            }
        }
    }
    public void OnSlotClick(PlaceableElement placable){
        highlight.size = placable.size;
        highlight.placableElement = placable.element;
        highlight.gameObject.SetActive(true);
    }
}
