using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int maxLife = 3; // Set the maximum number of lives
    private int currentLife; // Variable to store current life count
    public Image[] hearts; // Array of heart images representing the player's life
    public Sprite fullHeartSprite; // Sprite for a full heart
    private Bubble bubble; // Reference to the Bubble script

    void Start()
    {
        bubble = GetComponent<Bubble>(); // Get the Bubble script component
        currentLife = maxLife; // Initialize the current life count
        UpdateHeartsUI(); // Update the UI to display initial hearts based on the life count
    }

    void Update()
    {
        if (currentLife <= 0)
        {
            Debug.Log("Game Over!"); // Debug message to check if the game over condition is met
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the scene if the player has no lives left
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("current: " + currentLife);
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Danger"))
        {
            if (currentLife > 0)
            {
                --currentLife; // Decrease the life count
                UpdateHeartsUI(); // Update the UI to reflect the changed life count
            }
        }

        if (other.gameObject.CompareTag("Life"))
        {
            if (currentLife < maxLife)
            {
                currentLife++; // Increase the life count
                Destroy(other.gameObject); // Destroy the extra life object
                UpdateHeartsUI(); // Update the UI to reflect the increased life count
            }
            else
            {
                Destroy(other.gameObject); // Destroy the extra life object if the player already has the maximum number of lives
            }
        }

        if (other.gameObject.CompareTag("Underwater") && !bubble.IsBubbleActive())
        {
            if (currentLife > 0)
            {
                --currentLife; // Decrease the life count
                UpdateHeartsUI(); // Update the UI to reflect the changed life count
            }
        }
    }


    // Function to update the UI hearts based on the current life count
    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLife)
            {
                hearts[i].sprite = fullHeartSprite; // Set heart to full if it represents a remaining life
                hearts[i].enabled = true; // Make the heart visible
            }
            else
            {
                hearts[i].enabled = false; // Hide the heart if it represents a lost life
            }
        }
    }

    public void DecreaseLifeOverTime(float rate)
    {
        if (currentLife > 0)
        {
            // Decrease the life count based on the rate and Time.deltaTime
            currentLife -= Mathf.FloorToInt(rate * Time.deltaTime);

            // Ensure the life count doesn't go below zero
            currentLife = Mathf.Max(currentLife, 0);

            UpdateHeartsUI(); // Update the UI to reflect the changed life count
        }
    }



}
