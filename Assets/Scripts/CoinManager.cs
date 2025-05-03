using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public int coinsCollected = 0;
    public int attempts = 0;
    public string currentStageName = "";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("CoinManager initialized.");
        }
        else
        {
            Destroy(gameObject);
        }

        // تأكد  يتصفر دائما عند بداية المشهد
        coinsCollected = 0;
    }

    public int obstaclesPassedCount = 0;

    public void IncrementObstaclesPassed()
    {
        obstaclesPassedCount++;
        Debug.Log("Obstacles Passed: " + obstaclesPassedCount);
    }

    public int GetObstaclesPassed()
    {
        return obstaclesPassedCount;
    }

    // دالة لإضافة عملة وحفظها
    public void AddCoin()
    {
        coinsCollected++;
        SaveCoins();
        Debug.Log("Coins: " + coinsCollected);
    }

    // حفظ عدد الكوينز
    private void SaveCoins()
    {
        PlayerPrefs.SetInt("CoinsCollected", coinsCollected);
        PlayerPrefs.Save();
    }

    // تحميل عدد الكوينز
    private void LoadCoins()
    {
        coinsCollected = PlayerPrefs.GetInt("CoinsCollected", 0);
    }

    // بدء مرحلة جديدة
    public void StartNewStage(string stageName)
    {
        currentStageName = stageName;
        attempts++;
        Debug.Log("Starting " + currentStageName + " | Attempt #" + attempts);
    }
}
