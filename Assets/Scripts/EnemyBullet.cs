using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // حركة الطلقة للأمام
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

       void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  
        {
           
            Destroy(gameObject);  // تدمير الطلقة بعد الاصطدام
        }
    }
}
