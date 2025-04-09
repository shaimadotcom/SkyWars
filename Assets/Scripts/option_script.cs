using UnityEngine;
using UnityEngine.SceneManagement;

public class option_script : MonoBehaviour
{    
      [SerializeField] private AudioSource buttonClickSound;
       private string sceneToLoad;
      void PlayClickSound()
    {
        if (buttonClickSound != null)
            buttonClickSound.Play();
    }


    public void SetEasyMode()
    {    PlayClickSound();
        PlayerPrefs.SetString("SelectedDifficulty", "Easy");
        Debug.Log("Difficulty set to Easy");
    }

    // زر اختيار المتوسطة
    public void SetMediumMode()
    {   PlayClickSound();
        PlayerPrefs.SetString("SelectedDifficulty", "Medium");
        Debug.Log("Difficulty set to Medium");
    }

    // زر اختيار الصعوبة الصعبة
    public void SetHardMode()
    {   PlayClickSound();
        PlayerPrefs.SetString("SelectedDifficulty", "Hard");
        Debug.Log("Difficulty set to Hard");
    }
   public void GoToScene(string sceneName)
    {
        PlayClickSound();
        sceneToLoad = sceneName;
        Invoke("LoadSceneAfterSound", 0.2f); // تأخير بسيط عشان الصوت يشتغل
    }

    void LoadSceneAfterSound()
    {
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("Loading scene: " + sceneToLoad);
    }
}
