using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject explosionEffect;      public GameObject bulletPrefab;         public Transform firePoint;          

    public float fireRate = 1f;        
    private bool isDead = false;        
  void Start()
{
    if (firePoint == null)
    {
        Debug.LogError("FirePoint is not assigned in the Inspector!");
    }
    if (bulletPrefab == null)
    {
        Debug.LogError("BulletPrefab is not assigned in the Inspector!");
    }
    else
    {
        // ج العدو يطلق النار بشكل متكرر
        InvokeRepeating("Fire", 1f, fireRate);
    }
}
    void Fire()
    {
        if (!isDead)
        {
            // إطلاق الطلقة إذا لم يكن العدو ميتًا
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }

    void Update()
    {
        
        float leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        if (transform.position.x < leftBoundary)
        {
            Destroy(gameObject); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("PlayerBullet"))
        {
           
            Die();
            Destroy(other.gameObject); // تدمير الطلقة
        }
    }

    // عند موت العدو
    void Die()
    {
        isDead = true;  // وضع العدو في حالة موت
        Instantiate(explosionEffect, transform.position, Quaternion.identity); // إضافة تأثير الانفجار
        Destroy(gameObject); // تدمير العدو
    }
}
