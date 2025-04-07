using System.Collections;
using UnityEngine;

public class NYZKrespwan : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float minSpawnDelay = 6f;  // زيادة تأخير بين النيازك
    public float maxSpawnDelay = 8f;  // زيادة تأخير بين النيازك
    public float spawnY = 10f;  // موقع البداية للنيزك
    
    public float maxSpeed = 1f;  // تقليل السرعة أكثر
    public float gravityScale = 0.1f;  // التأثير البطيء للجاذبية

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

            // إنشاء النيزك في موقع عشوائي ضمن حدود الشاشة
            Vector2 spawnPos = new Vector2(Random.Range(screenLeft, screenRight), spawnY);
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

            // Debug Log لمتابعة مكان إنشاء النيزك
            Debug.Log("Spawning asteroid at: " + spawnPos);

            // تحديد سرعة النيزك
            Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.down * maxSpeed;  // تعيين سرعة السقوط

            // تعديل تأثير الجاذبية لجعل السقوط أبطأ
            rb.gravityScale = gravityScale;

         
        }
    }
}
