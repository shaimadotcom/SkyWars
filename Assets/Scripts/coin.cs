using UnityEngine;

public class coin : MonoBehaviour
{public AudioClip pickupSound;

void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        CoinManager.instance.AddCoin();
        Destroy(gameObject);
    }
}
}