using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player_Movment : MonoBehaviour
{
    Animator animator;
    [SerializeField] private Rigidbody2D rb;

    public float moveSpeed = 8f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;
    private bool isTouchingWall;
    private bool isWallSliding;
    public float wallSlideSpeed = 1f;

    [Header("Jumping")]
    public int maxJumps = 2; 
    private int jumpsRemaining;

    [Header("Gravity Settings")]
    public float baseGravity = 2f;
    public float fallGravityMultiplier = 2f;
    public float maxFallSpeed = 18f;

    private bool isFacingRight = true;
    private float Horizontal;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip grabSound;

    private bool isWalkingSoundPlaying = false;

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;

        // تشغيل موسيقى الخلفية
        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    void Update()
    {
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

        if (isTouchingWall && !IsGrounded() && Horizontal != 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }

        Horizontal = Input.GetAxisRaw("Horizontal");

        // حركة اللاعب يمين يسار
        rb.velocity = new Vector2(Horizontal * moveSpeed, rb.velocity.y);

        // تشغيل صوت المشي
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && IsGrounded())
        {
            animator.SetBool("isWalking", true);

            if (!isWalkingSoundPlaying && walkSound != null)
            {
                isWalkingSoundPlaying = true;
                backgroundMusicSource.PlayOneShot(walkSound);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            isWalkingSoundPlaying = false;
        }

        // القفز
        if (Input.GetKeyDown(KeyCode.W) && jumpsRemaining > 0)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsRemaining--;

            // تشغيل صوت القفز
            if (jumpSound != null)
            {
                backgroundMusicSource.PlayOneShot(jumpSound);
            }
        }

        // نص قفزة لو ترك الزر بسرعة
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // ضبط الجاذبية لما ينزل
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallGravityMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }

        // تشغيل حركة القبض (Grab)
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("GRAB Trigger Pressed");
            animator.SetTrigger("grab");

            if (grabSound != null)
            {
                backgroundMusicSource.PlayOneShot(grabSound);
            }
        }

        // الفليب
        if ((isFacingRight && Horizontal < 0) || (!isFacingRight && Horizontal > 0))
        {
            Flip();
        }

      
        GroundCheck();
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer))
        {
            jumpsRemaining = maxJumps; 
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("obstcle"))
    {
        StartCoroutine(DieAndLoadGameOver());
    }
}

private IEnumerator DieAndLoadGameOver()
{
    if (deathSound != null && backgroundMusicSource != null)
    {
        backgroundMusicSource.PlayOneShot(deathSound);
    }

  
    this.enabled = false;

    float shrinkDuration = 0.5f;
    float elapsed = 0f;
    Vector3 originalScale = transform.localScale;
    Vector3 targetScale = Vector3.zero; 

    while (elapsed < shrinkDuration)
    {
        transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / shrinkDuration);
        elapsed += Time.deltaTime;
        yield return null;
    }

    transform.localScale = targetScale; 

    yield return new WaitForSeconds(1f); 


    SceneManager.LoadScene("gameOver");
}


}
