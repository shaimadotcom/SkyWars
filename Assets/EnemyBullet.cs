using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // حركة الطلقة للأمام
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    // إذا اصطدمت الطلقة مع شيء ما (مثل سفينة اللاعب)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // إذا كانت الطلقة تصطدم باللاعب
        {
            // يمكنك إضافة تأثير انفجار هنا
            Destroy(gameObject);  // تدمير الطلقة بعد الاصطدام
        }
    }
}
