using UnityEngine;
using UnityEngine.SceneManagement;

public class collioncontrol : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;  // تأثير الانفجار
    [SerializeField] string gameOverSceneName = "gameOver"; // اسم مشهد الجيم أوفر
    [SerializeField] Collider2D bottomCollider;

    [Header("Audio Clips")]
    [SerializeField] AudioClip collisionSound;    // صوت الاصطدام
      private AudioSource audioSource;

    void Start()
    {
    
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
         
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstcle") || collision.collider == bottomCollider)
        {
            // تأثير الانفجار
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Animator animator = explosion.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("ExplosionAnimation");
            }

            // إيقاف الحركة
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }

            // تشغيل صوت الاصطدام
            if (collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }

           

            // بعد 1 ثانية نروح لمشهد الجيم أوفر (عشان نسمع الأصوات قبل التنقل)
            Invoke("LoadGameOverScene", 1f);
        }
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}