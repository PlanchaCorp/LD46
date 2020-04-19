using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementBehaviour : MonoBehaviour
{
    [SerializeField]
    public PlacableElement placable;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = placable.icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
