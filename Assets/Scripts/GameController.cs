using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;  // إذا كنت تستخدم TextMesh Pro

public class GameController : MonoBehaviour
{
    [Header("Enemy Hearts Settings")]
    [SerializeField] Image[] hearts; // 3 قلوب للعدو
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite twoThirdHeart;
    [SerializeField] Sprite oneThirdHeart;
    [SerializeField] Sprite emptyHeart;

    [Header("Timer Bar Settings")]
    [SerializeField] Image[] timerBar; // 5 صور للتايمر
    [SerializeField] Sprite fullBar;
    [SerializeField] Sprite fourFifthsBar;
    [SerializeField] Sprite threeFifthsBar;
    [SerializeField] Sprite twoFifthsBar;
    [SerializeField] Sprite oneFifthBar;
    [SerializeField] Sprite emptyBar;

    [Header("Game Settings")]
    [SerializeField] float timeLimit = 30f;

     // إذا كنت تستخدم TextMesh Pro

    private float enemyHealth = 3f; // صحة العدو (كل قلب = 1)
    private float currentTime;
    private bool isGameActive;

    void Start()
    {
        currentTime = timeLimit;
        isGameActive = true;
        UpdateHearts();
        UpdateTimerBar();
    }

    void Update()
    {
        if (!isGameActive) return;

        currentTime -= Time.deltaTime;
    
        UpdateTimerBar();

        if (currentTime <= 0)
        {
            isGameActive = false;
            SceneManager.LoadScene("gameOver");
        }
    }

    // استدعاء هذه الدالة عند إصابة العدو
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        UpdateHearts();

        if (enemyHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            float heartStatus = enemyHealth - i;

            if (heartStatus >= 1f)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (heartStatus >= 0.66f)
            {
                hearts[i].sprite = twoThirdHeart;
            }
            else if (heartStatus >= 0.33f)
            {
                hearts[i].sprite = oneThirdHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

 

    void UpdateTimerBar()
    {
        float timePercentage = currentTime / timeLimit;

        // تحديث شريط التايمر بناءً على النسبة
        if (timePercentage >= 0.8f)
        {
            SetTimerBarSprite(fullBar);
        }
        else if (timePercentage >= 0.6f)
        {
            SetTimerBarSprite(fourFifthsBar);
        }
        else if (timePercentage >= 0.4f)
        {
            SetTimerBarSprite(threeFifthsBar);
        }
        else if (timePercentage >= 0.2f)
        {
            SetTimerBarSprite(twoFifthsBar);
        }
        else if (timePercentage > 0)
        {
            SetTimerBarSprite(oneFifthBar);
        }
        else
        {
            SetTimerBarSprite(emptyBar);
        }
    }

    void SetTimerBarSprite(Sprite sprite)
    {
        // تعيين الصورة لكل عنصر في شريط التايمر
        foreach (Image img in timerBar)
        {
            img.sprite = sprite;
        }
    }

    void DestroyEnemy()
    {
        isGameActive = false;
        Debug.Log("Loading WinScene..."); // تأكد من أنه يتم الوصول لهذه السطر
        SceneManager.LoadScene("WinScene");
    }
}
