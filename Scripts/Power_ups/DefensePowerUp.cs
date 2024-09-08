using UnityEngine;

public class DefensePowerUp : MonoBehaviour
{
    public GameObject shieldPrefab;     // The shield visual prefab
    public Transform shieldPoint;       // Location on the player where the shield will be placed
    public float shieldDuration = 20f;  // Duration the shield stays active

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with the defense power-up
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth component and activate the shield
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ActivateShield(shieldPrefab, shieldPoint, shieldDuration);
                Destroy(gameObject); // Destroy the power-up object
            }
        }
    }
}
