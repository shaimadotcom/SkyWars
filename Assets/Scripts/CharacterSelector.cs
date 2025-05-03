using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private AudioSource buttonClickSound;

    private int currentIndex = 0;
    private GameObject currentCharacterInstance;

     void Start()
    {
        ShowCharacter();
    }

    private void PlayClickSound()
    {
 
        if (buttonClickSound != null && !buttonClickSound.isPlaying) 
        {
            buttonClickSound.Play();
        }
    }

    public void NextCharacter()
    {
        PlayClickSound();  // تشغيل الصوت عند الضغط على الزر
        Invoke(nameof(NextCharacterAction), 0.05f);
    }

    private void NextCharacterAction()
    {
        currentIndex++;
        if (currentIndex >= characterPrefabs.Length)
            currentIndex = 0;

        ShowCharacter();
    }

    public void PreviousCharacter()
    {
        PlayClickSound();  
        Invoke(nameof(PreviousCharacterAction), 0.05f); 
    }

    private void PreviousCharacterAction()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = characterPrefabs.Length - 1;

        ShowCharacter();
    }

    public void SaveCharacter()
    {
        PlayClickSound(); 
        Invoke(nameof(SaveCharacterAction), 0.05f); 
    }

    private void SaveCharacterAction()
    {
        PlayerPrefs.SetInt("SelectedCharacter", currentIndex);
        PlayerPrefs.Save();
        Debug.Log("Character saved: " + currentIndex);
    }

    public void GoBack()
    {
        PlayClickSound();  
        Invoke(nameof(LoadMainMenu), 0.05f); 
    }

    private void LoadMainMenu()
    {
        Debug.Log("Button pressed: Going back to MainMenu");
        SceneManager.LoadScene("options");
    }

    private void ShowCharacter()
    {
        if (currentCharacterInstance != null)
            Destroy(currentCharacterInstance);

        currentCharacterInstance = Instantiate(characterPrefabs[currentIndex], spawnPoint.position, Quaternion.identity);
    }
}
