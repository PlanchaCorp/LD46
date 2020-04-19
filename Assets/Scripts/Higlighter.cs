using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Higlighter : MonoBehaviour
{
    public int obstructionObject;

    public Grid grid;
    [SerializeField]
    Tilemap floorTilemap;

    [Range(1, 3)]
    public int size;

    public GameObject placableElement;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector2(size, size);
        obstructionObject = 0;
    }

    void OnEnable()
    {
        transform.localScale = new Vector2(size, size);
         obstructionObject = 0;
    }
    void OnDisable(){
        placableElement = null;
    }


    void Update()
    {
        Vector3Int cell = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector2 worldPos;
        if (size % 2 != 0)
        {
            worldPos = floorTilemap.GetCellCenterWorld(cell);
        }
        else
        {
            worldPos = floorTilemap.CellToWorld(cell);
        }
        transform.position = worldPos;
        if (Input.GetMouseButtonDown(0) && obstructionObject == 0)
        {
            Instantiate(placableElement, worldPos, Quaternion.identity);
             gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        if (collision.CompareTag("Wall") || collision.CompareTag("Machine") || collision.CompareTag("Dodo"))
        {
            obstructionObject++;
            SetRed();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
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
