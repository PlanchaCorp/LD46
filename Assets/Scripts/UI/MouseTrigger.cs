using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrigger : MonoBehaviour
{
    enum Type { Ground, Button, Machine }
    private UiDisplay uiDisplay;
    [SerializeField]
    private Type type = Type.Machine;

    void Start()
    {
        uiDisplay = GameObject.FindGameObjectWithTag("MainUI").GetComponent<UiDisplay>();
    }
    void Update()
    {

    }
}
