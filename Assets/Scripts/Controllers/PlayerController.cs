using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 4f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingWall;
    private Animator animator;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Wall Check")]
    public Transform wallCheck;
    public float wallCheckDistance = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        Vector2 boxSize = new Vector2(0.1f, 1f);
        float castDistance = wallCheckDistance;

        RaycastHit2D wallHit = Physics2D.BoxCast(
            wallCheck.position, boxSize, 0f, Vector2.right * transform.localScale.x, castDistance, groundLayer);

        isTouchingWall = wallHit.collider != null;


        // Movement Input
        float moveInput = Input.GetAxis("Horizontal");

        // Block movement towards the wall
        if (isTouchingWall && ((moveInput > 0 && transform.localScale.x > 0) || (moveInput < 0 && transform.localScale.x < 0)))
        {
            moveInput = 0;
        }

        // Apply movement
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Rotate sprite
        if (moveInput > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetFloat("movement", Mathf.Abs(moveInput));
        animator.SetBool("isJumping", !isGrounded && rb.linearVelocity.y > 0);

        if (!isGrounded && rb.linearVelocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("isFalling", false);
        }

        // Jump
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Check ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null && wallCheck != null)
        {
            // Gizmo para el GroundCheck (círculo)
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

            // Gizmo para el WallCheck (BoxCast)
            Gizmos.color = Color.blue;
            Vector2 boxSize = new Vector2(0.1f, 1f); // Mismo tamaño que en el BoxCast
            float castDistance = wallCheckDistance;

            // Dibuja el cubo en la posición final del BoxCast
            Vector3 castEndPosition = wallCheck.position +
                                     Vector3.right * transform.localScale.x * castDistance;
            Gizmos.DrawWireCube(castEndPosition, boxSize);

            // Dibuja una línea desde el origen hasta el cubo
            Gizmos.DrawLine(wallCheck.position, castEndPosition);
        }
    }
}
