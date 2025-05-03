using System.Collections;
using UnityEngine;

public class NYZKrespwan : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float minSpawnDelay = 6f;  
    public float maxSpawnDelay = 8f;  
    public float spawnY = 10f;  
    
    public float maxSpeed = 1f;  
    public float gravityScale = 0.1f; 

    private float screenLeft;
    private float screenRight;

    void Start()
    {
        // حساب الحدود اليسرى واليمنى للشاشة باستخدام ScreenToWorldPoint
        screenLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        screenRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            // انتظر فترة عشوائية قبل إنشاء النيزك التالي
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

           
            Vector2 spawnPos = new Vector2(Random.Range(screenLeft, screenRight), spawnY);
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
if (CoinManager.instance != null)
{
    CoinManager.instance.IncrementObstaclesPassed();
}

           
            Debug.Log("Spawning asteroid at: " + spawnPos);

            // تحديد سرعة النيزك
            Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.down * maxSpeed; 

            rb.gravityScale = gravityScale;

         
        }
    }
}
