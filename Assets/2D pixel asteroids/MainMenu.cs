using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickSound;

    void PlayClickSound()
    {
        if (buttonClickSound != null)
            buttonClickSound.Play();
    }

    // هذا الكود ينتقل للمشهد الذي تختاره بناءً على اسم المشهد المدخل
    public void GoToScene(string sceneName)
    {
        PlayClickSound();
        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading scene: " + sceneName);
    }

    // إغلاق اللعبة
    public void QuitApp()
    {
        PlayClickSound();
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

        Debug.Log("app quit");
    }

    // زر ContinueGame لاسترجاع المشهد الذي تم حفظه
    public void ContinueGame()
    {
        PlayClickSound();
        Debug.Log("Continuing Game...");
        
        // استرجاع اسم المشهد المحفوظ في PlayerPrefs (إذا لم يكن موجودًا سيتم استخدام "DefaultScene")
        string lastScene = PlayerPrefs.GetString("LastScene", "DefaultScene");
        
        // تحميل المشهد الذي تم حفظه
        SceneManager.LoadScene(lastScene);
    }

    // زر PlayGame لحفظ المستوى الذي اختاره اللاعب
    public void PlayGame()
    {
        PlayClickSound();

        // استرجاع المستوى الذي اختاره اللاعب أو تعيين المستوى الافتراضي إذا لم يكن هناك اختيار
        string selectedDifficulty = PlayerPrefs.GetString("SelectedDifficulty", "Easy"); 
        Debug.Log("Starting game with difficulty: " + selectedDifficulty);

        // حفظ اسم المستوى في PlayerPrefs
        PlayerPrefs.SetString("LastScene", selectedDifficulty);

        // تحميل المشهد بناءً على المستوى الذي اختاره اللاعب
        if (selectedDifficulty == "Easy")
        {
            SceneManager.LoadScene("easy");
        }
        else if (selectedDifficulty == "Medium")
        {
            SceneManager.LoadScene("Medium");
        }
        else if (selectedDifficulty == "Hard")
        {
            SceneManager.LoadScene("Hard");
        }
    }
}
