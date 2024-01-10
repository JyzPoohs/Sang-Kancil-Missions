using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitsCollection : MonoBehaviour
{
    private int cherryPoints = 0;
    private int pearPoints = 0;
    public int cherryNeeds = 15;
    public int pearNeeds = 12;

    private int rescuedFriends = 0;

    [SerializeField] private TextMeshProUGUI cherryText;
    [SerializeField] private TextMeshProUGUI pearText;
    [SerializeField] private TextMeshProUGUI rescueFriendsText;

    private void Update()
    {
        if (rescueFriendsText != null)
        {
            string text = rescueFriendsText.text;

            if (!string.IsNullOrEmpty(text) && text.Length > 0)
            {
                // Get the first character from the text
                char firstChar = text[0];

                // Convert the first character to an integer
                if (int.TryParse(firstChar.ToString(), out int firstDigit))
                {
                    rescuedFriends = firstDigit;
                }
            }
        }

        if (cherryPoints >= cherryNeeds && pearPoints >= pearNeeds && rescuedFriends >= 3)
        {
            Debug.Log("You win!");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            cherryPoints++;
            cherryText.text = cherryPoints + "/" + cherryNeeds;
        }
        else if (collision.gameObject.CompareTag("Pear"))
        {
            Destroy(collision.gameObject);
            pearPoints++;
            pearText.text = pearPoints + "/" + pearNeeds;
        }

    }

    public void SetRescuedFriends(int friends)
    {
        rescuedFriends = friends;
    }

    public int GetTotalCollectedFruits()
    {
        return cherryPoints + pearPoints;
    }

    public void DecreaseFruitsForBubbleActivation(int amount)
    {
        int totalFruits = GetTotalCollectedFruits();
        Debug.Log("total fruits: " + totalFruits);

        int decrement = amount / 2;
        // Ensure fruits are decremented without going below zero
        if (cherryPoints >= decrement && pearPoints >= decrement)
        {
            Debug.Log("both decrement");
            cherryPoints -= decrement;
            pearPoints -= decrement;
        }
        else
        {
            if (cherryPoints >= amount)
            {
                Debug.Log("cherry points decrement");
                cherryPoints -= amount;
            }
            else if (pearPoints >= amount)
            {
                pearPoints -= amount;
            }
        }

        Debug.Log("cherry points: " + cherryPoints);
        // Update UI text for cherry and pear
        cherryText.text = cherryPoints + "/" + cherryNeeds;
        pearText.text = pearPoints + "/" + pearNeeds;
    }
}
