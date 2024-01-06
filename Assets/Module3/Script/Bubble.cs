using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public int fruitsRequired = 1; // Number of fruits required to activate the bubble
    public float bubbleDuration = 60f; // Duration of the bubble in seconds
    public float timeBeforeDamage = 10f; // Time before player starts losing life without the bubble
    public float lifeDecreaseRate = 1f; // Rate at which life decreases without the bubble
    private bool bubbleActive = false; // Flag to track if the bubble is active
    private float timeLeftBeforeDamage; // Time left before the player starts losing life

    private PlayerLife playerLife; // Reference to the PlayerLife script
    private FruitsCollection fruitsCollection;
    public GameObject bubble; // Reference to the bubble game object
    public Text timerText; // Reference to the bubble text
    private float timeLeft; // Time left for the bubble


    void Start()
    {
        playerLife = GetComponent<PlayerLife>();
        fruitsCollection = GetComponent<FruitsCollection>();
        timeLeftBeforeDamage = 10f;
        timeLeft = bubbleDuration;
        timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && CanActivateBubble())
        {
            ActivateBubble();
            bubble.SetActive(true);
            timerText.gameObject.SetActive(true);
        }

        if (bubbleActive)
        {
            UpdateBubbleTimer();
        }
        else
        {
            // Decrease player's life if the bubble is not active
            bubble.SetActive(false);
            timeLeftBeforeDamage -= Time.deltaTime;
            if (timeLeftBeforeDamage <= 0)
            {
                if (playerLife != null)
                {
                    // Decrease the player's life using the PlayerLife script
                    playerLife.DecreaseLifeOverTime(lifeDecreaseRate);
                }
            }
        }

    }

    bool CanActivateBubble()
    {
        // Check if player has collected enough fruits to activate the bubble
        int totalFruits = fruitsCollection.GetTotalCollectedFruits();
        return totalFruits >= fruitsRequired;
    }

    void ActivateBubble()
    {
        Debug.Log("Bubble activated");
        // Activate the bubble and reset the time before life decrease
        bubbleActive = true;
        timeLeft = bubbleDuration; // Reset timeLeft to bubbleDuration when bubble activates
        timeLeftBeforeDamage = timeBeforeDamage;
        // Decrease the fruits count using the FruitsCollection script
        fruitsCollection.DecreaseFruitsForBubbleActivation(fruitsRequired);

        StartCoroutine(BubbleTimer());
    }

    void UpdateBubbleTimer()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            bubbleActive = false;
            timeLeftBeforeDamage = timeBeforeDamage;
            bubble.SetActive(false);
            timerText.gameObject.SetActive(false);
        }

        if (timerText != null)
        {
            timerText.text = Mathf.RoundToInt(timeLeft) + "s";
        }
    }

    IEnumerator BubbleTimer()
    {
        yield return new WaitForSeconds(bubbleDuration);
        bubbleActive = false;
        timeLeftBeforeDamage = timeBeforeDamage;
        bubble.SetActive(false);
    }

    public bool IsBubbleActive()
    {
        return bubbleActive;
    }
}
