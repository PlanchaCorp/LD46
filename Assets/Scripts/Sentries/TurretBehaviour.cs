using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private Transform cannon;
    private Camera mainCamera;

    [SerializeField]
    private Rail rail;
    private Vector3 mousePos;
    // Start is called before the first frame update
    public int nextPoint;
    public int previousPoint;

    public bool canFire;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        previousPoint = 0;
        canFire = true;
        nextPoint = rail.waypoints.Length - 1;
        transform.position = (getPrevious() + getNext()) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, mousePos, Color.blue);
        Vector3 delta = mousePos - transform.position;
        float angle = (Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            Fire();
        }
        var newPos = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            newPos = Vector2.MoveTowards(transform.position, getNext(), 10 * Time.deltaTime);
            rb.MovePosition(newPos);

        }
        if (Input.GetKey(KeyCode.E))
        {
            newPos = Vector2.MoveTowards(transform.position, getPrevious(), 10 * Time.deltaTime);
            rb.MovePosition(newPos);

        }
    }

    private void Fire()
    {
        Instantiate(arrow, cannon.position, cannon.rotation);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("collision");
        int index = FindIndex(collider.name);
        if (index == nextPoint)
        {
            increase();
        }
        else
        if (index == previousPoint)
        {
            decrease();
        }
    }
    private int FindIndex(string name)
    {
        for (int i = 0; i < rail.waypoints.Length; i++)
        {
            if (rail.waypoints[i].name == name)
            {
                return i;
            }
        }
        return 0;
    }
    private void increase()
    {

        previousPoint = nextPoint;

        if (nextPoint == rail.waypoints.Length - 1)
        {
            nextPoint = 0;
        }
        else
        {
            nextPoint++;
        }
    }
    private void decrease()
    {

        nextPoint = previousPoint;

        if (previousPoint == 0)
        {
            previousPoint = rail.waypoints.Length - 1;
        }
        else
        {
            previousPoint--;
        }
    }
    private Vector2 getNext()
    {
        return rail.waypoints[nextPoint].position;
    }
    private Vector2 getPrevious()
    {
        return rail.waypoints[previousPoint].position;
    }

}
