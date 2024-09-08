using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Needed for UI elements

public class UIObjectCollectionWithTypes : MonoBehaviour
{
    // Variables for the UI collection system
    public Text objectCounterText;            // UI text to show total number of collected items
    public Transform collectedItemsPanel;     // Parent UI element to display collected item icons
    public GameObject[] collectedItemPrefabs; // Array of prefabs for different collectible item icons
    public int maxCapacity = 5;               // Max number of items player can collect
    private int collectedItemCount = 0;       // Current number of collected items
    private Dictionary<string, int> collectedItems = new Dictionary<string, int>(); // Track different types of items

    // Optional audio feedback
    public AudioClip collectSound;            // Sound effect for collecting an item
    private AudioSource audioSource;

    private void Start()
    {
        // Initialize the object counter text
        UpdateUI();

        // Initialize the audio source if needed
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with a collectible item
        if (other.CompareTag("Collectible"))
        {
            // Get the collectible type from the object (you can set this in a custom script)
            CollectibleItem item = other.GetComponent<CollectibleItem>();

            // Ensure we don't exceed max capacity before collecting
            if (collectedItemCount < maxCapacity)
            {
                CollectItem(item);
                Destroy(other.gameObject); // Remove item from the game world
            }
            else
            {
                Debug.Log("Inventory full! Max capacity reached.");
            }
        }
    }

    // Function to handle item collection
    private void CollectItem(CollectibleItem item)
    {
        // Check if this item type is already in the dictionary
        if (collectedItems.ContainsKey(item.itemType))
        {
            collectedItems[item.itemType]++; // Increment the count for this item type
        }
        else
        {
            collectedItems[item.itemType] = 1; // Add the item type if it's new
        }

        collectedItemCount++; // Increase total item count

        // Optionally play a collection sound
        if (collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        // Show the collected item on the UI
        DisplayCollectedItem(item);
        UpdateUI();
    }

    // Function to display the collected item icon in the UI
    private void DisplayCollectedItem(CollectibleItem item)
    {
        // Instantiate the correct icon prefab for this item type
        foreach (GameObject prefab in collectedItemPrefabs)
        {
            // Check if the prefab's name matches the item type
            if (prefab.name == item.itemType)
            {
                GameObject newItemIcon = Instantiate(prefab, collectedItemsPanel);
                newItemIcon.transform.localScale = Vector3.one; // Ensure the correct UI scale
                break;
            }
        }
    }

    // Function to update the UI counter
    private void UpdateUI()
    {
        // Update the text counter to show how many items the player has collected out of the max capacity
        objectCounterText.text = "Items Collected: " + collectedItemCount + "/" + maxCapacity;
    }
}
