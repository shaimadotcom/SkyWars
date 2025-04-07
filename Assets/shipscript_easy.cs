using UnityEngine;
using UnityEngine.SceneManagement;  // إذا كنت تريد إعادة تحميل المشهد

public class shipscript_easy : MonoBehaviour
{
    public Rigidbody2D shipRigidbody;
    public float moveSpeed = 8f; // سرعة الحركة الأفقية
    public float flySpeed = 2f;  // سرعة الطيران للأعلى

    private Animator animator;
    private float horizontalInput;
    private bool isGameWon = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isGameWon)
            return;  // إذا تم الفوز، لا نفعل أي شيء

        // ناخذ مدخلات الحركة يمين/يسار
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // تحديث الأنيميشن حسب الاتجاه
        if (horizontalInput > 0)
            animator.SetInteger("direction", 1); // يمين
        else if (horizontalInput < 0)
            animator.SetInteger("direction", -1); // يسار
        else
            animator.SetInteger("direction", 0); // واقف
    }

    void FixedUpdate()
    {
        if (isGameWon)
            return;  // إذا تم الفوز لا نحرك المركبة

        // نحرك السفينة يمين/يسار فقط، مع حركة ثابتة للأعلى
        shipRigidbody.velocity = new Vector2(horizontalInput * moveSpeed, flySpeed);
    }

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
