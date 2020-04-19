using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Higlighter : MonoBehaviour
{
    public bool canPlace = false;

    public Grid grid;
    [SerializeField]
    Tilemap floorTilemap;

    [Range(1,3)]
    public int size;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
         transform.localScale= new Vector2(size,size);
    }

    // Update is called once per frame
    void OnEnable(){
         transform.localScale= new Vector2(size,size);
    }


         void Update()
    {
       Vector3Int cell = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
       Vector2 worldPos;
       if( size % 2 != 0){
       worldPos = floorTilemap.GetCellCenterWorld(cell);
    } else {
       worldPos = floorTilemap.CellToWorld(cell);
    }
       Debug.Log(worldPos);
       transform.position = worldPos;
    }
     void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag.Equals("Wall")){
            canPlace = false;
            SetRed();
        }
    }
    
     void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag.Equals("Wall")){
             canPlace = true;
             setGreen();
        }
    }
    void setGreen(){
        Debug.Log("green");
    spriteRenderer.color= Color.green;// new Color(100,188,108,132);
    }
    void SetRed(){
        spriteRenderer.color= Color.red;// new Color(188,38,45,132);
    }
}
