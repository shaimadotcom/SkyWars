using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collionHard : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip collisionSound;
    [SerializeField] string gameOverSceneName = "gameOver";

    private AudioSource audioSource;
    private bool isDead = false;

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
        if (collision.CompareTag("EnemyBullet") && !isDead)
        {
            isDead = true;
            Debug.Log("Player hit by enemy bullet!");

            // انفجار
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Animator animator = explosion.GetComponent<Animator>();
            if (animator != null) animator.Play("ExplosionAnimation");

            // صوت
            if (collisionSound != null) audioSource.PlayOneShot(collisionSound);

            Destroy(collision.gameObject);

            // إخفاء اللاعب بدل تدميره
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            //   تاخير الكوروتين
            StartCoroutine(LoadGameOverWithDelay(0.5f));
        }
    }

    IEnumerator LoadGameOverWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Loading Scene: " + gameOverSceneName);
        SceneManager.LoadScene(gameOverSceneName);
    }
}
