using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyModeScript : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs;  // مصفوفة الشخصيات
    [SerializeField] private Transform spawnPoint;           // نقطة الاستنساخ
    private GameObject currentCharacterInstance;             // الشخصية المستنسخة

    void Start()
    {
     
        LoadSelectedCharacter();

        // إذا كان المشهد هو "easy"
        if (SceneManager.GetActiveScene().name == "easy")
        {
          
            if (currentCharacterInstance != null)
            {
                int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
                if (selectedCharacterIndex != 0) // لا تطبق تأثير التصغير على الشخصية رقم صفر
                {
                    currentCharacterInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);  
                }
            }
        }
    }

void LoadSelectedCharacter()
{
 
    int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);  


    if (currentCharacterInstance != null)
    {
        Destroy(currentCharacterInstance);
    }

    currentCharacterInstance = Instantiate(characterPrefabs[selectedCharacterIndex], spawnPoint.position, Quaternion.identity);

    if (selectedCharacterIndex == 0 && currentCharacterInstance != null)
    {
        currentCharacterInstance.transform.rotation = Quaternion.Euler(0, 0, 90);  

    Debug.Log("Loaded character index: " + selectedCharacterIndex);
}
}}
