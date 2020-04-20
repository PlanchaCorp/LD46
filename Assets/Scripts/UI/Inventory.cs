using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public Highlighter highlight;
    [SerializeField]
    public GameObject[] slots;
    [SerializeField]
    public PlaceableElement[] placables;

    void Start()
    {
        for (int i = 0; i < slots.Length; i++) {
            if (placables.Length > i) {
                UnityEngine.UI.Image[] images = slots[i].GetComponentsInChildren<UnityEngine.UI.Image>();
                foreach(UnityEngine.UI.Image img in images){
                    if(img.name == "SlotItem"){
                        img.sprite = placables[i].icon;
                        float currentSize = img.rectTransform.sizeDelta.x;
                        img.rectTransform.sizeDelta = new Vector2(currentSize * 3 / 5, currentSize * 3 / 5);
                    }
                }
               
            }
        }
    }
    public void OnSlotClick(PlaceableElement placable){
        highlight.size = placable.size;
        highlight.placableElement = placable.element;
        highlight.gameObject.SetActive(true);
    }
    void Update()
    {
        for(int i =1;i<=placables.Length; i++){
            if(Input.GetButtonDown("Hotkey"+ i ))
            {
                slots[i-1].GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
        }
        if (Input.GetButtonDown("Delete"))
        {
            highlight.size = new Vector2(1, 1);
            highlight.placableElement = null;
            highlight.gameObject.SetActive(true);
        }
    }
}
