using UnityEngine;
using UnityEngine.SceneManagement;

public class shipscript_easy : MonoBehaviour
{
    public Rigidbody2D shipRigidbody;
    public float moveSpeed = 8f; // سرعة الحركة الأفقية
    public float flySpeed = 1f;  // سرعة الطيران للأعلى، سنجعلها أبطأ

    private Animator animator;
    private float horizontalInput;
    private bool isGameWon = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        // جلب الفهرس المحفوظ للشخصية المختارة من PlayerPrefs
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);

        // تعيين السرعات بناءً على الشخصية المختارة
        switch (selectedCharacterIndex)
        {
            case 0: // الشخصية الأولى
                moveSpeed = 8f;  // السرعة الأفقية
                flySpeed = 1f;   // سرعة الطيران للأعلى بطيئة
                break;
            case 1: // الشخصية الثانية
                moveSpeed = 3f;  // السرعة الأفقية
                flySpeed = 0.5f; // سرعة الطيران للأعلى بطيئة جدًا
                break;
            case 2: // الشخصية الثالثة
                moveSpeed = 2f;  // السرعة الأفقية
                flySpeed = 0.3f; // سرعة الطيران بطيئة جدًا
                break;
            default:
                moveSpeed = 8f;
                flySpeed = 1f;
                break;
        }

        Debug.Log("Selected character index: " + selectedCharacterIndex);
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
