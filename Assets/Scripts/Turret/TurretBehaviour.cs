using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform cannon;
    private Camera mainCamera;

    private Rail rail;
    public bool canFire;
    private Vector3 mousePos;
    // Start is called before the first frame update
    private int nextPoint;
    private int previousPoint;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rail = GetComponentInParent<Rail>();
        mainCamera = Camera.main;
        previousPoint = rail.waypoints.Length - 1;
        nextPoint = 0;
        canFire = true;
        transform.position = (getPrevious() + getNext()) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, mousePos, Color.blue);
        Vector3 delta = mousePos - transform.position;

        Vector2 position2d = new Vector2(transform.position.x, transform.position.y);
        float angleMin = Vector2.SignedAngle(position2d - getPrevious(), Vector2.right);
        float angleMax = Vector2.SignedAngle(position2d - getNext(), Vector2.right);
        float willenAngle = Mathf.Atan2(delta.y, delta.x) * -Mathf.Rad2Deg;

        if (((willenAngle < angleMin - 90 && willenAngle > angleMin - 180) || (willenAngle > angleMax && willenAngle < angleMax + 90)))
            willenAngle = angleMax;
        else if (((willenAngle < angleMin && willenAngle > angleMin - 90) || (willenAngle > angleMax + 90 && willenAngle < angleMax + 180)))
            willenAngle = angleMin;
        transform.rotation = Quaternion.AngleAxis(-willenAngle - 90, Vector3.forward);
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            Fire();
        }
        var newPos = Vector2.zero;
        if (Input.GetAxis("TurretMovement") > 0)
        {
            newPos = Vector2.MoveTowards(transform.position, getPrevious(), 10 * Time.deltaTime);
            rb.MovePosition(newPos);

        } else if (Input.GetAxis("TurretMovement") < 0)
        {
            newPos = Vector2.MoveTowards(transform.position, getNext(), 10 * Time.deltaTime);
            rb.MovePosition(newPos);
        }
    }

    private void Fire()
    {
        Instantiate(bullet, cannon.position, cannon.rotation);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        int index = collider.transform.GetSiblingIndex();
        if (index == nextPoint)
        {
            GoToNextWaypoint();
        } else if (index == previousPoint)
        {
            GoToPreviousWaypoint();
        }
    }

    private void GoToNextWaypoint()
    {
        previousPoint = nextPoint;
        nextPoint = (nextPoint == rail.waypoints.Length - 1) ? 0 : ++nextPoint;
    }
    private void GoToPreviousWaypoint()
    {
        nextPoint = previousPoint;
        previousPoint = (previousPoint == 0) ? rail.waypoints.Length - 1 : --previousPoint;
    }

    private Vector2 getPrevious()
    {
        return rail.waypoints[previousPoint].position;
    }
    private Vector2 getNext()
    {
        return rail.waypoints[nextPoint].position;
    }

}
