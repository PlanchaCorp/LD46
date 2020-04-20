using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Highlighter : MonoBehaviour
{
    public int obstructionObject;

    public Grid grid;
    [SerializeField]
    Tilemap floorTilemap;

    public Vector2 size;

    public GameObject placableElement;
    private UiDisplay uiDisplay;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        uiDisplay = GameObject.FindGameObjectWithTag("MainUI").GetComponent<UiDisplay>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = size;
        obstructionObject = 0;
        uiDisplay.UpdateCursor();
    }
    void OnDisable(){
        placableElement = null;
        uiDisplay.UpdateCursor();
    }


    void LateUpdate()
    {
        Vector3Int cell = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector2 worldPos = floorTilemap.CellToWorld(cell);
        if (size.x % 2 != 0)
            worldPos.x += 0.5f;
        if (size.y % 2 != 0)
            worldPos.y += 0.5f;
        transform.position = worldPos;
        if (Input.GetMouseButtonDown(0) && obstructionObject == 0)
        {
            Instantiate(placableElement, worldPos, Quaternion.identity);
            gameObject.SetActive(false);
        }
        if (Input.GetMouseButton(1))
        {
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Machine") || collision.CompareTag("Dodo"))
        {
            obstructionObject++;
            SetRed();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Machine") || collision.CompareTag("Dodo"))
        {
            obstructionObject--;
            if(obstructionObject == 0) {
                setGreen();
            }
        }
    }
    void setGreen()
    {
        spriteRenderer.color = new Color(100.0f/255.0f, 188f/255f, 108f/255f, 132f/255f);
    }
    void SetRed()
    {
        spriteRenderer.color = new Color(188f/255f, 38f/255f, 45f/255f, 132f/255f);
    }
}
