using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship_hard : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    Animator animator;
    float verticalInput;

    void Start()
    {
        // الحصول على الأنيميتر من الـ GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ناخذ مدخلات الحركة فوق/تحت
        verticalInput = Input.GetAxisRaw("Vertical");

        // تحديث الأنيميشن حسب الاتجاه
        if (verticalInput > 0)
            animator.SetInteger("direction", 1); // للأعلى
        else if (verticalInput < 0)
            animator.SetInteger("direction", -1); // للأسفل
        else
            animator.SetInteger("direction", 0); // واقف

        // حركة فوق وتحت )
        transform.Translate(Vector3.up * verticalInput * moveSpeed * Time.deltaTime);

        // إطلاق النار
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
       
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
       
        rb.velocity = firePoint.up * 10f; 
    }
}
