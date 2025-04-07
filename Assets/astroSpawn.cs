using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroSpawn : MonoBehaviour
{
    public GameObject astro;
    public float spawnRate = 1.5f;
    public float minYSpacing = 2.5f;
    public float moveSpeedMin = 3f;
    public float moveSpeedMax = 6f;

    private float timer = 0;
    private List<float> recentSpawnYs = new List<float>();
    private Camera mainCamera;
    private float spawnX;
    
    void Start()
    {
        mainCamera = Camera.main;
        CalculateSpawnBounds();
        StartCoroutine(SpawnAsteroids());
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            StartCoroutine(SpawnAsteroids());
            timer = 0;
        }
    }

    void CalculateSpawnBounds()
    {
        float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        spawnX = mainCamera.transform.position.x + cameraWidth + 2f;
        
    }

    IEnumerator SpawnAsteroids()
    {
        if (astro == null) yield break;

        Vector3 minSpawn = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.1f, 0));
        Vector3 maxSpawn = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.9f, 0));

        float spawnY = GetValidSpawnY(minSpawn.y, maxSpawn.y);
        
        if (spawnY == Mathf.Infinity)
        {
            spawnY = Random.Range(minSpawn.y, maxSpawn.y);
            recentSpawnYs.Clear();
        }

        recentSpawnYs.Add(spawnY);
        if (recentSpawnYs.Count > 3) recentSpawnYs.RemoveAt(0);

        GameObject newAstro = Instantiate(astro, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
        ConfigureAstro(newAstro);

        yield return null;
    }

    float GetValidSpawnY(float minY, float maxY)
    {
        for (int i = 0; i < 20; i++)
        {
            float candidateY = Random.Range(minY, maxY);
            if (IsPositionValid(candidateY)) return candidateY;
        }
        return Mathf.Infinity;
    }

    bool IsPositionValid(float y)
    {
        foreach (float prevY in recentSpawnYs)
        {
            if (Mathf.Abs(y - prevY) < minYSpacing) return false;
        }
        return true;
    }

    void ConfigureAstro(GameObject astroObj)
    {
        // Random scale and speed
        float randomScale = Random.Range(0.7f, 1.3f);
        astroObj.transform.localScale = Vector3.one * randomScale;
        
        // Inverse relationship between size and speed
        float speed = Mathf.Lerp(moveSpeedMax, moveSpeedMin, 
            (randomScale - 0.7f) / (1.3f - 0.7f));
        
        // Setup movement and auto-destruction
        AstroMover mover = astroObj.GetComponent<AstroMover>();
       
    }
}


public class AstroMover : MonoBehaviour
{
    private float speed;
    private float destroyX;

    public void Setup(float moveSpeed, float destroyPositionX)
    {
        speed = moveSpeed;
        destroyX = destroyPositionX;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}