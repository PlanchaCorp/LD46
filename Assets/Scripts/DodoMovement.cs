using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodoMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;

    private Vector2 goal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        Vector2 newPos = Vector2.MoveTowards(transform.position,goal, speed * Time.deltaTime);
        rb.MovePosition(newPos);
    
    }

    public void setObjective(Vector2 newObjective){
        this.goal = newObjective;
         float angle = (Mathf.Atan2(goal.y-transform.position.y, goal.x- transform.position.x) * Mathf.Rad2Deg) -90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
}
