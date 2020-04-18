using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseExempleInteractor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("MOUSE DOWN ON " + gameObject.name);
    }
}
