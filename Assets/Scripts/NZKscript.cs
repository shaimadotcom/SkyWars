using UnityEngine;

public class NZKscript : MonoBehaviour
{    private Rigidbody2D rb;
  void OnCollisionEnter2D(Collision2D collision)
    {
        // إذا اصطدم النيزك مع أي جسم)
        if (collision.gameObject.CompareTag("Player"))  
        {
            // أوقف حركة النيزك
            rb.velocity = Vector2.zero;  // إيقاف السرعة تماماً
            rb.isKinematic = true;  // جعل الريجيدبودي لا يتأثر بالقوى بعد التصادم
            Debug.Log("Asteroid stopped due to collision with player.");
        }
    }
}
