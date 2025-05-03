using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
   public GameObject[] coins; 
    public float xMin, xMax, yMin, yMax; 
    void Start()
    {
        ShuffleCoins();
    }

    void ShuffleCoins()
    {
        foreach (GameObject coin in coins)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(xMin, xMax),
                Random.Range(yMin, yMax)
            );

            coin.transform.position = randomPosition;
        }
    }
}