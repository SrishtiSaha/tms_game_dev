using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    public float healthIncreasePercentage = 10f; // Health increase percentage when collected

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with the health power-up
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth component and increase health
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.IncreaseHealth(healthIncreasePercentage);
                Destroy(gameObject); // Destroy the health power-up object
            }
        }
    }
}
