using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [Header("Audio Clips")]
    [SerializeField] AudioClip collisionSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            // تأثيرات الانفجار
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform);
            Destroy(explosion, 0.5f);

            // تشغيل الصوت
            if (collisionSound != null) audioSource.PlayOneShot(collisionSound);

            // إرسال ضرر إلى الـ GameController
            GameController gameController = FindObjectOfType<GameController>();
            if (gameController != null)
            {
                gameController.TakeDamage(0.33f); // كل إصابة تنقص 33% من القلب
            }

            Destroy(collision.gameObject);
        }
    }
}