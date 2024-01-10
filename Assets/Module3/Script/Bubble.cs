using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bubble : MonoBehaviour
{
    public int fruitsRequired = 1; // Number of fruits required to activate the bubble
    public float bubbleDuration = 60f; // Duration of the bubble in seconds
    private bool bubbleActive = false; // Flag to track if the bubble is active
    private bool getBubble = false; // Flag to track if the bubble is obtained
    private PlayerLife playerLife; // Reference to the PlayerLife script
    private FruitsCollection fruitsCollection;
    public GameObject bubble; // Reference to the bubble game object
    public Text timerText; // Reference to the bubble text
    private float timeLeft; // Time left for the bubble
    [SerializeField] private AudioSource bubbleSoundEffect;
    public Image bubbleImage;
    private bool isUnderwater = false; // Flag to track if the player is underwater

    void Start()
    {
        playerLife = GetComponent<PlayerLife>();
        fruitsCollection = GetComponent<FruitsCollection>();
        timeLeft = bubbleDuration;
        timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (bubbleImage != null)
        {
            getBubble = bubbleImage.enabled;
        }

        if (Input.GetKeyDown(KeyCode.B) && CanActivateBubble() && getBubble)
        {
            bubbleSoundEffect.Play();
            ActivateBubble();
            bubble.SetActive(true);
            timerText.gameObject.SetActive(true);
        }

        if (IsBubbleActive())
        {
            UpdateBubbleTimer();
        }
        else
        {
            bubble.SetActive(false);
            timerText.gameObject.SetActive(false);
        }

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            getBubble = true;

        }
        if (other.gameObject.CompareTag("Underwater"))
        {
            isUnderwater = true;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Underwater"))
        {
            isUnderwater = false;
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
        // Decrease the fruits count using the FruitsCollection script
        fruitsCollection.DecreaseFruitsForBubbleActivation(fruitsRequired);

        StartCoroutine(BubbleTimer());
    }

    void UpdateBubbleTimer()
    {
        timeLeft -= Time.deltaTime;
        Debug.Log("Time left: " + timeLeft);

        if (timeLeft <= 0)
        {
            bubbleActive = false;
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
        bubble.SetActive(false);
    }

    public bool IsBubbleActive()
    {
        return bubbleActive;
    }
}
