using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject explosionEffect;   // تأثير الانفجار عند موت العدو
    public GameObject bulletPrefab;      // الطلقة التي يطلقها العدو
    public Transform firePoint;          // مكان إطلاق النار

    public float fireRate = 1f;          // سرعة الإطلاق (كم مرة يطلق النار)
    
    private bool isDead = false;         // متغير ليتأكد إذا كان العدو ميتًا أم لا

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
        // جعل العدو يطلق النار بشكل متكرر
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
        // الحصول على الحدود الفعلية للشاشة في الإحداثيات العالمية
        float leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        // تدمير العدو إذا خرج من الشاشة
        if (transform.position.x < leftBoundary)
        {
            Destroy(gameObject); // دمر العدو إذا خرج من الشاشة
        }
    }

    // عندما تصطدم الطلقة بالعدو
    void OnTriggerEnter2D(Collider2D other)
    {
        // إذا كانت الطلقة من اللاعب
        if (other.CompareTag("PlayerBullet"))
        {
            // إيقاف العدو عن الإطلاق وموته
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
