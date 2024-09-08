using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables to control player movement
    public float moveSpeed = 5f;      // Speed of player movement
    public float jumpForce = 10f;     // Force applied to the player when jumping
    public float rollSpeed = 7f;      // Speed when rolling
    public int maxJumpCount = 3;      // Max number of jumps allowed (triple jump)

    private Rigidbody2D rb;
    private bool isGrounded = false;  // Check if the player is grounded
    private int jumpCount = 0;        // Counter for jumps
    private bool isRolling = false;   // Check if the player is rolling

    // Layer mask to detect ground
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    // Caching input to reduce redundant checks
    private float horizontalInput;
    private bool jumpPressed;

    private void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Cache input for movement and jumping
        horizontalInput = Input.GetAxis("Horizontal");
        jumpPressed = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);

        // Handle jump logic
        Jump();

        // Handle rolling
        Roll();
    }

    private void FixedUpdate()
    {
        // Handle movement in FixedUpdate for smoother physics-based motion
        Move();

        // Check if player is grounded (better suited here for physics accuracy)
        CheckGrounded();
    }

    // Function to move the player left and right
    private void Move()
    {
        // Apply horizontal movement, prevent overriding roll velocity
        if (!isRolling)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
    }

    // Function to handle jumping, including double and triple jumps
    private void Jump()
    {
        // Jump if the player is grounded or hasn't exceeded max jumps
        if (jumpPressed && (isGrounded || jumpCount < maxJumpCount))
        {
            // Apply upward force for jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Increment the jump count if not grounded
            if (!isGrounded)
            {
                jumpCount++;
            }
        }
    }

    // Check if the player is grounded by using a ground check
    private void CheckGrounded()
    {
        // Perform a circle cast to check if the player is touching the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset the jump count when the player touches the ground
        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    // Function to handle rolling movement (e.g., press Left Shift to roll)
    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isRolling)
        {
            // Start rolling (e.g., change animation state)
            isRolling = true;

            // Apply roll speed in the direction the player is moving
            float rollDirection = horizontalInput;
            rb.velocity = new Vector2(rollDirection * rollSpeed, rb.velocity.y);

            // Optionally, add a timer to stop rolling after a certain time
            StartCoroutine(StopRollAfterTime(0.5f));  // Stops rolling after 0.5 seconds
        }
    }

    // Coroutine to stop the rolling action after a set time
    IEnumerator StopRollAfterTime(float rollDuration)
    {
        yield return new WaitForSeconds(rollDuration);
        isRolling = false;
    }

    // Draw the ground check radius for visualization in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
