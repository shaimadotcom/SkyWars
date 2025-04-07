using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;       // الطلقة التي يطلقها العدو
    public Transform firePoint;           // مكان إطلاق النار
    public float fireRate = 1.5f;         // سرعة الإطلاق (كم مرة يطلق النار)
    public float moveSpeed = 2f;          // سرعة الحركة للأعلى والأسفل
    public float moveRange = 2f;          // مدى الحركة للأعلى والأسفل

    private float timer;
    private Vector3 startPosition;

    void Start()
    {
        timer = 0f; // تهيئة التوقيت ليبدأ الحساب عند بدء اللعبة
        startPosition = transform.position; // حفظ موقع العدو الحالي كنقطة انطلاق للحركة
    }

    void Update()
    {
        // زيادة التوقيت مع مرور الزمن
        timer += Time.deltaTime;

        // إذا مر الوقت المطلوب للإطلاق
        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;  // إعادة التوقيت ليبدأ العد من جديد
        }

        // حركة العدو للأعلى والأسفل باستخدام PingPong
        float newY = Mathf.PingPong(Time.time * moveSpeed, moveRange); // حساب الحركة
        transform.position = new Vector3(startPosition.x, startPosition.y + newY, startPosition.z); // تطبيق الحركة
    }

    void Shoot()
    {
        // إطلاق الطلقة من موقع firePoint
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
