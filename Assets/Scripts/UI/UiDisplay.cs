using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiDisplay : MonoBehaviour
{
    [SerializeField]
    private Texture2D activeCursor;
    [SerializeField]
    private Texture2D ordinaryCursor;
    [SerializeField]
    private Texture2D shootCursor;
    [SerializeField]
    private Texture2D discreteCursor;
    [SerializeField]
    private Image oxygenBar;
    [SerializeField]
    private TextMeshProUGUI dodoniumAmount;
    private Inventory inventory;

    private int cursorButtonAmount = 0;
    private bool cursorHoverGround = false;
    private bool cursorHoverMachine = false;

    // Start is called before the first frame update
    void Start()
    {
        cursorButtonAmount = 0;
        cursorHoverGround = false;
        inventory = GetComponentInChildren<Inventory>();
    }

    // Update is called once per frame
    public void UpdateAmounts(float dodonium, float oxygenRatio)
    {
        dodoniumAmount.text = dodonium.ToString("F0");
        oxygenBar.fillAmount = oxygenRatio;
    }
    public void AnimateAmounts(float dodonium, float oxygen)
    {
        string dodoniumAnimationName = null, oxygenAnimationName = null;
        string dodoniumText = "", oxygenText = "";
        if (dodonium > 0) {
            dodoniumAnimationName = "DodoniumIncrease";
            dodoniumText = "+" + dodonium.ToString("F1");
        } else if (dodonium < 0) {
            dodoniumAnimationName = "DodoniumDecrease";
            dodoniumText = dodonium.ToString("F1");
        }
        if (oxygen > 0) {
            oxygenAnimationName = "OxygenIncrease";
            oxygenText = "+" + oxygen.ToString("F1");
        } else if (oxygen < 0) {
            oxygenAnimationName = "OxygenDecrease";
            oxygenText = oxygen.ToString("F1");
        }
        if (dodoniumAnimationName != null) {
            GameObject dodoniumAnimation = Instantiate(Resources.Load<GameObject>(dodoniumAnimationName));
            dodoniumAnimation.GetComponentInChildren<TextMeshProUGUI>().text = dodoniumText;
            dodoniumAnimation.transform.parent = transform;
            dodoniumAnimation.transform.position = new Vector2(50, 160);
        }
        if (oxygenAnimationName != null) {
            GameObject oxygenAnimation = Instantiate(Resources.Load<GameObject>(oxygenAnimationName));
            oxygenAnimation.GetComponentInChildren<TextMeshProUGUI>().text = oxygenText;
            oxygenAnimation.transform.parent = transform;
            oxygenAnimation.transform.position = new Vector2(270, 60);
        }
    }

    /// Functions triggered when mouse cursor enters or leaves button
    public void CursorEntersButton()
    {
        cursorButtonAmount++;
        UpdateCursor();
    }
    public void CursorLeavesButton()
    {
        cursorButtonAmount = Mathf.Max(cursorButtonAmount - 1, 0);
        UpdateCursor();
    }
    public void CursorEntersGround()
    {
        cursorHoverGround = true;
        UpdateCursor();
    }
    public void CursorLeavesGround()
    {
        cursorHoverGround = false;
        UpdateCursor();
    }
    public void CursorEntersMachine()
    {
        cursorHoverMachine = true;
        UpdateCursor();
    }
    public void CursorLeavesMachine()
    {
        cursorHoverMachine = false;
        UpdateCursor();
    }
    public void UpdateCursor()
    {
        if (cursorButtonAmount > 0) {
            Cursor.SetCursor(activeCursor, new Vector2(26.5f, 20), CursorMode.ForceSoftware); }
        else if (inventory.highlight.gameObject.activeSelf)
            Cursor.SetCursor(discreteCursor, new Vector2(32, 32), CursorMode.ForceSoftware);
        else if (cursorHoverMachine) {
            Cursor.SetCursor(activeCursor, new Vector2(26.5f, 20), CursorMode.ForceSoftware); }
        else if (cursorHoverGround) {
            Cursor.SetCursor(shootCursor, new Vector2(32, 32), CursorMode.ForceSoftware); }
        else
            Cursor.SetCursor(ordinaryCursor, new Vector2(24, 20), CursorMode.ForceSoftware);
    }

    public bool TurretCanShoot()
    {
        return cursorButtonAmount == 0 && !inventory.highlight.gameObject.activeSelf && !cursorHoverMachine && cursorHoverGround;
    }
}
