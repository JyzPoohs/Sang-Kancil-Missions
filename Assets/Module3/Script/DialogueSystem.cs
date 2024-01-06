using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogueBox; // Reference to the UI Image object to display dialogue box
    public Text infoText; // Reference to the UI Text object to display information
    public string information; // Information to display
    private bool isPlayerInRange = false;
    private bool isDialogueBoxActive = false;

    void Start()
    {
        dialogueBox.SetActive(false); // Initially, hide the dialogue box
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDialogueBox(); // Toggle the dialogue box visibility
        }
    }

    void ToggleDialogueBox()
    {
        isDialogueBoxActive = !isDialogueBoxActive;
        dialogueBox.SetActive(isDialogueBoxActive); // Toggle the visibility of the dialogue box

        if (isDialogueBoxActive)
        {
            infoText.text = information; // Display information when showing the dialogue box
        }
        else
        {
            infoText.text = ""; // Clear the text when hiding the dialogue box
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueBox.SetActive(false); // Hide the dialogue box when player exits range
            infoText.text = ""; // Clear the text when player exits range
            isDialogueBoxActive = false; // Reset the dialogue box state
        }
    }
}
