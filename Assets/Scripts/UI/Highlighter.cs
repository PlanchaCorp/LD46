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
    private float angle;

    public GameObject placableElement;
    public float placablePrice;
    private UiDisplay uiDisplay;
    private List<GameObject> collisions;

    private SpriteRenderer spriteRenderer;
    private SpaceStationManager spaceStationManager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject spaceStation = GameObject.FindWithTag("SpaceStation");
        if (spaceStation == null)
        {
            Debug.LogError("Unable to find SpaceStation gameObject to initialize inventory!");
            return;
        }
        spaceStationManager = spaceStation.GetComponent<SpaceStationManager>();
    }

    void OnEnable()
    {
        angle = 0;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        uiDisplay = GameObject.FindGameObjectWithTag("MainUI").GetComponent<UiDisplay>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = placableElement != null;
        transform.localScale = size;
        obstructionObject = 0;
        collisions = new List<GameObject>();
        uiDisplay.UpdateCursor();
    }
    void OnDisable(){
        placableElement = null;
        uiDisplay.UpdateCursor();
    }

    void Disable() {
        gameObject.SetActive(false);
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
        if (placableElement != null)
        {
            if (Input.GetMouseButtonDown(0) && obstructionObject == 0)
            {
                if (placablePrice <= spaceStationManager.dodoniumAmount)
                {
                    if (placableElement.CompareTag("Conveyer"))
                        Instantiate(placableElement, worldPos, Quaternion.AngleAxis(angle + 90, Vector3.forward));
                    else {
                        Instantiate(placableElement, worldPos, Quaternion.AngleAxis(angle, Vector3.forward));
                        Invoke("Disable", 0.1f);
                    }
                    spaceStationManager.dodoniumAmount -= placablePrice;
                } else {
                    Debug.Log("You lack resources!");
                    // TODO: Add feedback for missing resources
                }
            }
            if (Input.GetButtonDown("Rotate")) {
                angle -= 90;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        } else
        {
            if (Input.GetMouseButtonDown(0) && collisions.Count > 0)
            {
                GameObject removedObject = collisions[0];
                bool isConveyer = removedObject.CompareTag("Conveyer");
                collisions.Remove(removedObject);
                Destroy(removedObject);
                if (!isConveyer)
                    Invoke("Disable", 0.1f);
            }
        }
        if (Input.GetMouseButton(1))
        {
            Invoke("Disable", 0.1f);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Dodo") || collision.CompareTag("Machine") || collision.CompareTag("Conveyer"))
        {
            obstructionObject++;
            SetRed();
            if (collision.CompareTag("Machine") || collision.CompareTag("Conveyer"))
                collisions.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Dodo") || collision.CompareTag("Machine") || collision.CompareTag("Conveyer"))
        {
            obstructionObject--;
            if(obstructionObject == 0) {
                setGreen();
            }
            if (collisions.Contains(collision.gameObject))
                collisions.Remove(collision.gameObject);
        }
    }
    void setGreen()
    {
        spriteRenderer.color = new Color(60.0f/255.0f, 222f/255f, 88f/255f, 162f/255f);
    }
    void SetRed()
    {
        spriteRenderer.color = new Color(188f/255f, 38f/255f, 45f/255f, 132f/255f);
    }
}
