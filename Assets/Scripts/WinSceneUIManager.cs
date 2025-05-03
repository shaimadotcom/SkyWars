using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinSceneUIManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI obstaclesText;
    public GameObject badge;     public Sprite stage1Badge; 
    public Sprite stage2Badge;  
    public Sprite stage3Badge; 

    void Start()
    {Debug.Log("CoinManager instance: " + CoinManager.instance);
       
        if (coinsText == null || obstaclesText == null || badge == null)
        {
            Debug.LogError("One or more UI components are not assigned in the Inspector!");
            return;
        }

        int coins = CoinManager.instance.coinsCollected;
        int obstaclesPassed = CoinManager.instance.GetObstaclesPassed();

        coinsText.text = "" + coins;
        obstaclesText.text = "" + obstaclesPassed;

        // Retrieve the current stage from PlayerPrefs
        int currentStage = PlayerPrefs.GetInt("CurrentStage", 1); 

        Debug.Log("Current Stage: " + currentStage); 
        SetBadgeForStage(currentStage);
    }

    void SetBadgeForStage(int stage)
    {
        if (badge == null)
        {
            Debug.LogError("Badge GameObject is not assigned!");
            return;
        }

        Image badgeImage = badge.GetComponent<Image>();

        if (badgeImage == null)
        {
            Debug.LogError("No Image component found on the badge GameObject.");
            return;
        }

        switch (stage)
        {
            case 1:
                badgeImage.sprite = stage1Badge;
                break;
            case 2:
                badgeImage.sprite = stage2Badge;
                break;
            case 3:
                badgeImage.sprite = stage3Badge;
                break;
            default:
                badge.SetActive(false);
                break;
        }
    }
}
