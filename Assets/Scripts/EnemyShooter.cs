using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;     
    public Transform firePoint;           
    public float fireRate = 1.5f;         
    public float moveSpeed = 2f;        
    public float moveRange = 2f;        

    private float timer;
    private Vector3 startPosition;

    void Start()
    {
        timer = 0f; 
        startPosition = transform.position; 
    }

    void Update()
    {
      
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;  
        }

        // حركة العدو للأعلى والأسفل باستخدام PingPong
        float newY = Mathf.PingPong(Time.time * moveSpeed, moveRange); 
        transform.position = new Vector3(startPosition.x, startPosition.y + newY, startPosition.z); // تطبيق الحركة
    }

    void Shoot()
    {
      
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
