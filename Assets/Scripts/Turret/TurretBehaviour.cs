﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform cannon;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float shootingSpeed = 8;
    private Camera mainCamera;

    private Rail rail;
    private UiDisplay uiDisplay;
    private Vector3 mousePos;
    // Start is called before the first frame update
    [SerializeField]
    private int nextPoint;
    [SerializeField]
    private int previousPoint;
    private float nextShootingTime;

    private Rigidbody2D rb;
    void Start()
    {
        nextShootingTime = 0;
        uiDisplay = GameObject.FindGameObjectWithTag("MainUI").GetComponent<UiDisplay>();
        rb = GetComponent<Rigidbody2D>();
        rail = GetComponentInParent<Rail>();
        mainCamera = Camera.main;
        previousPoint = rail.waypoints.Length - 1;
        nextPoint = 0;
        transform.position = (getPrevious() + getNext()) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, mousePos, Color.blue);
        Vector3 delta = mousePos - transform.position;

        Vector3 position = new Vector3(transform.position.x, transform.position.y, 0);
        float angleMin = Vector2.SignedAngle(position - getPrevious(), Vector2.right);
        float angleMax = Vector2.SignedAngle(position - getNext(), Vector2.right);
        float willenAngle = Mathf.Atan2(delta.y, delta.x) * -Mathf.Rad2Deg;

        if (((willenAngle < angleMin - 90 && willenAngle > angleMin - 180) || (willenAngle > angleMax && willenAngle < angleMax + 90)))
            willenAngle = angleMax;
        else if (((willenAngle < angleMin && willenAngle > angleMin - 90) || (willenAngle > angleMax + 90 && willenAngle < angleMax + 180)))
            willenAngle = angleMin;
        transform.rotation = Quaternion.AngleAxis(-willenAngle - 90, Vector3.forward);
        if (Input.GetMouseButton(0) && uiDisplay.TurretCanShoot() && Time.time > nextShootingTime)
        {
            Fire();
        }
        var newPos = Vector2.zero;
        if (Input.GetAxis("TurretMovement") > 0)
        {
            newPos = Vector2.MoveTowards(transform.position, getPrevious(), Mathf.Min((transform.position - getPrevious()).magnitude, speed * Time.deltaTime));
            rb.MovePosition(newPos);

        } else if (Input.GetAxis("TurretMovement") < 0)
        {
            newPos = Vector2.MoveTowards(transform.position, getNext(), Mathf.Min((transform.position - getNext()).magnitude, speed * Time.deltaTime));
            rb.MovePosition(newPos);
        }
    }

    private void Fire()
    {
        nextShootingTime = Time.time + 1 / shootingSpeed;
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
        nextPoint = (nextPoint == rail.waypoints.Length - 1) ? 0 : nextPoint + 1;
    }
    private void GoToPreviousWaypoint()
    {
        nextPoint = previousPoint;
        previousPoint = (previousPoint == 0) ? rail.waypoints.Length - 1 : previousPoint - 1;
    }

    private Vector3 getPrevious()
    {
        return rail.waypoints[previousPoint].position;
    }
    private Vector3 getNext()
    {
        return rail.waypoints[nextPoint].position;
    }

}
