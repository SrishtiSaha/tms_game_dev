using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Needed for UI elements

// UI ELEMENTS:

// Displays a counter (Text) showing how many items have been collected.
// Shows the icon of each collected object in the UI (Image).
// Item Collection Logic:

// When the player collects an object, it’s destroyed in the game world, but a new UI element (icon) is instantiated and shown in the corner of the screen.
// The collected item is also added to the list of items, and the count is updated.
// Dynamic UI Update:

// Each time the player collects an object, the UI is updated to reflect the new count and show the new icon.


// Unity Setup:
// UI Setup:

// Text Component: Create a Text component in your UI to display the collected item count (e.g., "Items Collected: 0"). Assign this Text component to the objectCounterText field in the script.
// Image Component: Create an Image component in your UI to display the collected item icons (e.g., an empty slot where the icon will appear). Assign this Image component to the collectedItemIcon field.
// Collected Item Prefab:

// This will be a small icon representing the collected item (a visual UI element). Create a prefab for it and assign it to the collectedItemPrefab field. You can use a small sprite (like a power-up icon) for this.
// Canvas: Ensure you have a Canvas in your scene that holds your UI elements (Text, Image, etc.). The Canvas should be set to Screen Space - Overlay to ensure it displays on the screen.

// Collectible Objects:

// The collectible objects in your game should have a Collider2D (set to Is Trigger) and be tagged with Collectible. When the player touches them, they will be collected and shown in the UI.

public class UIObjectCollector : MonoBehaviour
{
    // Variables for the UI collection system
    public Text objectCounterText;      // UI text element to show the count of collected objects
    public Image collectedItemIcon;     // UI Image to represent the collected item icon in the corner of the screen
    public GameObject collectedItemPrefab; // Visual prefab for the collected item (icon)

    private int collectedItemCount = 0; // Keeps track of how many items the player has collected
    private List<GameObject> collectedItems = new List<GameObject>(); // List to store collected item prefabs

    private void Start()
    {
        // Initialize the object counter text
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has collided with a collectible item
        if (other.CompareTag("Collectible"))
        {
            // Collect the item
            CollectItem(other.gameObject);
        }
    }

    // Function to handle item collection
    private void CollectItem(GameObject item)
    {
        // Destroy the collected item in the game world
        Destroy(item);

        // Add 1 to the collected item count
        collectedItemCount++;

        // Optionally, display the collected item icon on the UI (can use collectedItemPrefab)
        GameObject newItem = Instantiate(collectedItemPrefab, collectedItemIcon.transform);
        newItem.transform.localScale = Vector3.one; // Ensures the correct scale in UI

        // Store the new collected item in the list
        collectedItems.Add(newItem);

        // Update the UI to show the new count and the collected items
        UpdateUI();
    }

    // Function to update the UI elements
    private void UpdateUI()
    {
        // Update the text counter to show the number of collected items
        objectCounterText.text = "Items Collected: " + collectedItemCount;
    }
}
