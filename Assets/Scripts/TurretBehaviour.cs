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


    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, mousePos, Color.blue);
        Vector3 delta = mousePos - transform.position;
        float angle = (Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if(Input.GetMouseButtonDown(0)){
            Fire();
        }
    }

    private void Fire()
    {
       Instantiate(arrow,cannon.position,cannon.rotation);
    }
}
