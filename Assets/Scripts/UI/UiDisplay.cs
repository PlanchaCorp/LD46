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
    private SpaceStationManager spaceStationManager;

    private int cursorButtonAmount = 0;
    private bool cursorHoverGround = false;
    private bool cursorHoverMachine = false;

    // Start is called before the first frame update
    void Start()
    {
        cursorButtonAmount = 0;
        cursorHoverGround = false;
        inventory = GetComponentInChildren<Inventory>();
        GameObject spaceStation = GameObject.FindWithTag("SpaceStation");
        if (spaceStation == null)
        {
            Debug.LogError("Unable to find SpaceStation gameObject to initialize UI!");
        } else {
            spaceStationManager = spaceStation.GetComponent<SpaceStationManager>();
        }
        InvokeRepeating("ScarceUpdate", 0, 0.15f);
    }

    // Update is called once per frame
    private void ScarceUpdate()
    {
        oxygenBar.fillAmount = spaceStationManager.oxygenAmount / spaceStationManager.OXYGEN_MAX_AMOUNT;
        dodoniumAmount.text = spaceStationManager.dodoniumAmount.ToString();
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
