using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodoMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;

    public Vector2 goal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        Vector2 newPos = Vector2.MoveTowards(transform.position,goal, speed * Time.deltaTime);
        rb.MovePosition(newPos);
    }

    
}
