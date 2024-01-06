using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject ChestClose, ChestOpen;
    bool isOpen = false;

    void Start()
    {
        // Make sure the chest starts closed
        ChestClose.SetActive(true);
        ChestOpen.SetActive(false);
    }

    void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the chest state (open/close) when E is pressed
            isOpen = !isOpen;
            ChestClose.SetActive(isOpen);
            ChestOpen.SetActive(!isOpen);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Only set isOpen to true when the player enters the trigger area
            isOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reset isOpen to false when the player exits the trigger area
            isOpen = false;
        }
    }
}
