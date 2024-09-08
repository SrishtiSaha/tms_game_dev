using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Needed for UI elements

// Set Up in Unity
// Defense Power-Up Object:

// Attach the DefensePowerUp script to your power-up object.
// Assign the shieldPrefab with the shield visual prefab and the shieldPoint with the transform where the shield should appear.
// Health Power-Up Object:

// Attach the HealthPowerUp script to your health power-up object.
// Set the healthIncreasePercentage to 10% or adjust as needed.
// Player Character:

// Attach the PlayerHealth script to your player character.
// Assign the healthBar with a Slider component and the healthText with a Text component in your UI.
// UI Setup:

// Health Bar: Create a Slider in the UI to represent the player's health. Configure it with a minimum value of 0 and a maximum value of 1.
// Health Text: Create a Text component to display the health percentage.

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health value
    private float currentHealth;   // Current health value

    public Slider healthBar;       // UI Slider to display health
    public Text healthText;        // UI Text to show current health percentage

    private GameObject activeShield;    // Stores the active shield object
    private bool isInvincible = false;  // Tracks whether the player is invincible

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
        UpdateHealthUI();          // Update health bar and text on start
    }

    // Function to increase health by a percentage
    public void IncreaseHealth(float percentage)
    {
        float increaseAmount = maxHealth * (percentage / 100f);
        currentHealth = Mathf.Min(maxHealth, currentHealth + increaseAmount); // Ensure health does not exceed maxHealth
        UpdateHealthUI();
    }

    // Function to take damage (example)
    public void TakeDamage(float damage)
    {
        if (isInvincible)
        {
            Debug.Log("Player is invincible! No damage taken.");
            return; // Ignore damage if invincible
        }

        currentHealth = Mathf.Max(0, currentHealth - damage); // Ensure health does not drop below 0
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Function to activate the shield
    public void ActivateShield(GameObject shieldPrefab, Transform shieldPoint, float duration)
    {
        if (activeShield != null) return; // Ensure there's no existing shield

        // Instantiate the shield around the player
        activeShield = Instantiate(shieldPrefab, shieldPoint.position, Quaternion.identity);
        activeShield.transform.SetParent(transform); // Attach the shield to the player

        // Set the player to invincible
        isInvincible = true;

        // Start the shield duration timer
        StartCoroutine(DeactivateShieldAfterTime(duration));
    }

    // Coroutine to deactivate the shield after the duration
    private IEnumerator DeactivateShieldAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration); // Wait for the shield duration to pass

        // Deactivate the shield
        if (activeShield != null)
        {
            Destroy(activeShield);
        }

        // Set invincibility to false
        isInvincible = false;
    }

    // Function to update the health bar and text
    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth; // Update slider value
        }

        if (healthText != null)
        {
            healthText.text = "Health: " + Mathf.RoundToInt((currentHealth / maxHealth) * 100) + "%"; // Update health text
        }
    }

    // Function to handle player death
    private void Die()
    {
        // Handle player death (e.g., restart level, game over screen)
        Debug.Log("Player has died.");
    }
}
