using System.Collections;
using UnityEngine;

public class TurtleShellAttack : MonoBehaviour
{
    // Variables related to the shell attack
    public GameObject shellPrefab;       // The shell to throw
    public Transform throwPoint;         // The point from where the shell will be thrown
    public float shellSpeed = 15f;       // The speed of the shell
    public float shellReturnDelay = 2f;  // Time before the shell returns to the turtle
    private bool shellInMotion = false;  // Track if the shell is currently thrown

    private GameObject activeShell;      // The active shell in the scene
    private Rigidbody2D shellRb;         // The Rigidbody2D of the thrown shell

    private void Update()
    {
        // Check for input to throw the shell (e.g., press "F" to attack)
        if (Input.GetKeyDown(KeyCode.F) && !shellInMotion)
        {
            ThrowShell();
        }
    }

    // Function to handle the throwing of the shell
    private void ThrowShell()
    {
        // Create the shell at the throw point with no rotation
        activeShell = Instantiate(shellPrefab, throwPoint.position, Quaternion.identity);

        // Get the Rigidbody2D component of the shell
        shellRb = activeShell.GetComponent<Rigidbody2D>();

        // Calculate the direction of the throw based on the turtle's facing direction
        Vector2 throwDirection = (transform.localScale.x > 0) ? Vector2.right : Vector2.left;

        // Apply force to the shell in the throw direction to simulate projectile motion
        shellRb.velocity = throwDirection * shellSpeed;

        // Mark that the shell is in motion
        shellInMotion = true;

        // Start coroutine to return the shell after a delay
        StartCoroutine(ReturnShellAfterTime(shellReturnDelay));
    }

    // Coroutine to return the shell to the turtle after a set time
    IEnumerator ReturnShellAfterTime(float returnDelay)
    {
        // Wait for the return delay
        yield return new WaitForSeconds(returnDelay);

        // Return the shell to the turtle
        ReturnShell();
    }

    // Function to return the shell to the turtle
    private void ReturnShell()
    {
        if (activeShell != null)
        {
            // Stop the shell's motion and reset its position to the turtle's body
            shellRb.velocity = Vector2.zero;
            activeShell.transform.position = throwPoint.position;

            // Optionally, you can play an animation or effect here for the return

            // Destroy the active shell object once it's back
            Destroy(activeShell);

            // Reset the shell state
            shellInMotion = false;
        }
    }
}
