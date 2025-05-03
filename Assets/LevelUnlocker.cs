using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LevelUnlocker : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickSound; // مصدر الصوت
    public Button levelButton;
    public int requiredCoins = 2;
    public string sceneToLoad; // اسم المرحلة اللي تروح لها

    void Start()
    {
        levelButton.onClick.AddListener(OnLevelButtonClicked); 
    }

    public void OnLevelButtonClicked()
    {
        // شغل الصوت مباشرة
        PlayClickSound();

  
        if (CoinManager.instance != null && CoinManager.instance.coinsCollected >= requiredCoins)
        {
            Invoke("LoadSceneAfterSound", 0.2f); // تأخير بسيط عشان الصوت يشتغل قبل تحميل المشهد
        }
        else
        {
            Debug.Log("Level Locked. Collect more coins!");
        }
    }

    // تشغيل الصوت
    void PlayClickSound()
    {
        if (buttonClickSound != null)
        {
            buttonClickSound.Play(); // شغل الصوت عند الضغط
        }
    }

    // تحميل المشهد بعد التأخير
    void LoadSceneAfterSound()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad); // تحميل المشهد
            Debug.Log("Loading scene: " + sceneToLoad);
        }
        else
        {
            Debug.LogWarning("No scene name set to load!");
        }
    }
}
