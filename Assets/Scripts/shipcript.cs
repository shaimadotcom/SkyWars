using UnityEngine;
using UnityEngine.SceneManagement;  

public class shipcript : MonoBehaviour
{
    public Rigidbody2D shipRigidbody;
    public float flapStrength = 5f;  // قوة القفز
    public float moveSpeed = 2f;     // سرعة الحركة الأفقية
 
 private bool isGameWon = false;
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // تحريك السفينة إلى اليمين
        float moveInput = 1f;
        shipRigidbody.velocity = new Vector2(moveInput * moveSpeed, shipRigidbody.velocity.y);

        // عند الضغط على Space → قفزة وتفعيل أنميشن القفز
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shipRigidbody.velocity = Vector2.up * flapStrength;
            animator.SetBool("isjump", true);
        }

        // إذا السفينة قاعدة تنزل أو وقفت، نطفي الأنميشن
        if (shipRigidbody.velocity.y <= 0)
        {
            animator.SetBool("isjump", false);
        }
    }

    // عند الاصطدام بالكائن الذي يمثل الفوز
    void OnTriggerEnter2D(Collider2D other)
    {
        // تحقق إذا كانت المركبة تلامس منطقة الفوز
        if (other.CompareTag("WinZone"))
        {
            WinGame();  // قم بإجراء الفوز
        }
    }

    void WinGame()
    {
        isGameWon = true;  // منع الحركة بعد الفوز
        Debug.Log("You Win!");  
 SceneManager.LoadScene("WinScene");
    }
}
