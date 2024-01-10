using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource.Play(); // This line plays the background music.
    }
}
