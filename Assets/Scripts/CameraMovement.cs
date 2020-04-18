using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    public int BOUNDARY = 50;
    [SerializeField]
    public int SPEED = 5;

    private float screenWidth;
    private float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        float xDirection = 0, yDirection = 0;
        if (Input.GetAxis("Horizontal") != 0)
        {
            xDirection = Input.GetAxis("Horizontal")  / Mathf.Abs(Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            yDirection = Input.GetAxis("Vertical")  / Mathf.Abs(Input.GetAxis("Vertical"));
        }
        if (xDirection == 0) 
        {
            if (Input.mousePosition.x > screenWidth - BOUNDARY) 
            {
                xDirection = 1;
            } else if (Input.mousePosition.x < 0 + BOUNDARY || Input.GetAxis("Horizontal") < 0) 
            {
                xDirection = -1;
            }
        }
        if (yDirection == 0)
        {
            if (Input.mousePosition.y > screenHeight - BOUNDARY || Input.GetAxis("Vertical") > 0) 
            {
                yDirection = 1;
            } else if (Input.mousePosition.y < 0 + BOUNDARY || Input.GetAxis("Vertical") < 0) 
            {
                yDirection = -1;
            }
        }
        transform.Translate(new Vector2(xDirection * SPEED * Time.deltaTime, yDirection * SPEED * Time.deltaTime));
    }
}
