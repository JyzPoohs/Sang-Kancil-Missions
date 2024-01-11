using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu3 : MonoBehaviour
{
    public GameObject startMenuUI;
    public void StartGame()
    {
        startMenuUI.SetActive(false);
    }

    // Function to go to the level selection scene
    public void GoToLevelSelection()
    {
        Debug.Log("Go to level selection");
        //SceneManager.LoadScene("LevelSelectionScene");
    }

    // Function to go to the main menu scene
    public void GoToMainMenu()
    {
        Debug.Log("Go to main menu");
        //SceneManager.LoadScene("MainMenuScene");
    }
}
