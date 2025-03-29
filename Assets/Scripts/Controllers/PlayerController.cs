using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 4f;
    public float jumpForce = 7f;
    public float longIdleDelay = 5f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingWall;

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
    }

    void Update()
    {
        // Check if touching wall
        RaycastHit2D wallHit = Physics2D.Raycast(wallCheck.position, Vector2.right * transform.localScale.x, wallCheckDistance, groundLayer);
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
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

            // WallCheck Gizmo ajustado a la dirección del personaje
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * transform.localScale.x * wallCheckDistance);
        }
    }
}
