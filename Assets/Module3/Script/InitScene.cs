using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision) // When the player enters the trigger area
    {
        if (collision.CompareTag("Player")) // If the player enters the trigger area
        {
            Debug.Log("Player entered trigger area");
            LoadNextScene(); // Load the next scene
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("MeetEagle"); // Load the scene named "Scene2"
    }
}
