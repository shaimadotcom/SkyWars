using UnityEngine;
using UnityEngine.SceneManagement;

public class level_script : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickSound;
    private string sceneToLoad;

    // إضافة متغير لتحديد المرحلة الحالية
    public static int currentStage = 1; // يمكنك تحديثه بناءً على المرحلة المختارة

    void PlayClickSound()
    {
        if (buttonClickSound != null)
            buttonClickSound.Play();
    }

    public void SetEasyMode()
    {
        PlayClickSound();
        PlayerPrefs.SetString("SelectedDifficulty", "Easy");
        Debug.Log("Difficulty set to Easy");
    }

    public void SetMediumMode()
    {
        PlayClickSound();
        PlayerPrefs.SetString("SelectedDifficulty", "Medium");
        Debug.Log("Difficulty set to Medium");
    }

    public void SetHardMode()
    {
        PlayClickSound();
        PlayerPrefs.SetString("SelectedDifficulty", "Hard");
        Debug.Log("Difficulty set to Hard");
    }

    // زر لاختيار المستوى
    public void SelectLevel(int stage)
    {
        PlayClickSound();
        currentStage = stage; // تعيين المرحلة التي تم اختيارها
        PlayerPrefs.SetInt("CurrentStage", currentStage);  // Save the selected level
        Debug.Log("Stage selected: " + currentStage);
    }

    public void GoToScene(string sceneName)
    {
        PlayClickSound();
        sceneToLoad = sceneName;
        Invoke("LoadSceneAfterSound", 0.2f); // تأخير بسيط عشان الصوت يشتغل
    }

    void LoadSceneAfterSound()
    {
        if (CoinManager.instance != null && !string.IsNullOrEmpty(sceneToLoad))
        {
            CoinManager.instance.StartNewStage(sceneToLoad); 
        }

        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("Loading scene: " + sceneToLoad);
    }
}
