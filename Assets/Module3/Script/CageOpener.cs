using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CageOpener : MonoBehaviour
{
    public GameObject cage; // Reference to the cage GameObject
    public int rescuedFriends; // Counter for the rescued friends
    public int friendsRequired = 3; // Number of friends required to open the cage
    [SerializeField] private TextMeshProUGUI rescueFriendsText; // Reference to the text UI element

    private bool cageOpened = false; // Flag to track if the cage is opened
    public Image keyImage;
    private bool getKey = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (keyImage != null)
        {
            getKey = keyImage.enabled;
        }

        if (Input.GetKeyDown(KeyCode.O) && !cageOpened && getKey)
        {
            OpenCage();
            Debug.Log("Rescued friends: " + rescuedFriends);
            if (rescueFriendsText != null)
            {
                rescueFriendsText.text = rescuedFriends + "/" + friendsRequired;
            }
        }
    }


    void OpenCage()
    {
        // Check if the cage GameObject is valid and not already opened
        if (cage != null && !cageOpened)
        {
            // Deactivate the cage (make it invisible)
            cage.SetActive(false);

            cageOpened = true;

            // Additional actions when the cage opens can be added here
            Debug.Log("Cage opened!");

            // Increment the number of rescued friends here if required
            // rescuedFriends++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cage"))
        {
            cageOpened = false;
        }
    }
}
