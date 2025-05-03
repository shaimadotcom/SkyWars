using UnityEngine;
using UnityEngine.SceneManagement;

public class option_script1 : MonoBehaviour{

    [SerializeField] private AudioSource buttonClickSound;

    private void PlayClickSound()
    {
        if (buttonClickSound != null)
            buttonClickSound.Play();
    }

    public void GoToLevels()
    {
        PlayClickSound();
        Debug.Log("Button pressed: Going to Level-Select");
        SceneManager.LoadScene("Level-Select");
    }

    public void GoToShop()
    {
        PlayClickSound();
        Debug.Log("Button pressed: Going to Shop");
        SceneManager.LoadScene("Shop");
    }

    public void GoBack()
    {
        PlayClickSound();
        Debug.Log("Button pressed: Going back to MainMenu");
        SceneManager.LoadScene("Main-Menu"); 
    }
}
