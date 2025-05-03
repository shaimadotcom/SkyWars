using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newlevel_enemyspawn : MonoBehaviour
{
    public Transform[] waypoints;  
    public float speed = 0.5f; 
    private int currentWaypointIndex = 0;  

    // Update is called once per frame
    void Update()
    {
        
        if (waypoints.Length == 0) return;

        // التحرك نحو النقطة الحالية
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

        // إذا وصل العدو إلى النقطة الحالية، انتقل إلى النقطة ال
        if (transform.position == waypoints[currentWaypointIndex].position)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;  
        }
    }
}
