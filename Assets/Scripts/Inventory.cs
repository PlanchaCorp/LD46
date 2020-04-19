using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject highlight;
    void Start()
    {
        
    }
    public void OnSlotClick(PlacableElement placable){
        //PlacableElement placable = GetComponent<ElementBehaviour>().placable;
        highlight.GetComponent<Higlighter>().size = placable.size;
        highlight.GetComponent<Higlighter>().placableElement = placable.element;
        highlight.SetActive(true);
    }
}
