using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrigger : MonoBehaviour
{
    enum Type { Ground, Button, Machine }
    private UiDisplay uiDisplay;
    [SerializeField]
    private Type type = Type.Ground;

    void Start()
    {
        uiDisplay = GameObject.FindGameObjectWithTag("MainUI").GetComponent<UiDisplay>();
    }
    void OnMouseEnter()
    {
        if (type == Type.Ground )
            uiDisplay.CursorEntersGround();
        else if (type == Type.Button)
            uiDisplay.CursorEntersButton();
        else if (type == Type.Machine)
            uiDisplay.CursorEntersMachine();
    }void OnMouseExit()
    {
        if (type == Type.Ground )
            uiDisplay.CursorLeavesGround();
        else if (type == Type.Button)
            uiDisplay.CursorLeavesButton();
        else if (type == Type.Machine)
            uiDisplay.CursorLeavesMachine();
    }
}
