using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Rail : MonoBehaviour
{

    [SerializeField]
    public Transform[] waypoints;
    [SerializeField]
    
    // Start is called before the first frame update

void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        for(int i=0;i<waypoints.Length;i++){
        Gizmos.color = Color.yellow;
        if(i == waypoints.Length -1){
                Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
        }else {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i+1].position);
        }
        }
    } 
}
