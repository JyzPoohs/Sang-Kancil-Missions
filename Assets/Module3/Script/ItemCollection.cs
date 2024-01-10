using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemCollection : MonoBehaviour
{
    public GameObject bubble;
    public GameObject key1;
    public GameObject key2;
    public GameObject key3;

    public Image bubbleImage;
    public Image key1Image;
    public Image key2Image;
    public Image key3Image;



    // Start is called before the first frame update
    void Start()
    {
        bubbleImage.enabled = false;
        key1Image.enabled = false;
        key2Image.enabled = false;
        key3Image.enabled = false;
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            Destroy(collision.gameObject);
            bubbleImage.enabled = true;
            bubble.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Key1"))
        {
            Destroy(collision.gameObject);
            key1Image.enabled = true;
            key1.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Key2"))
        {
            Destroy(collision.gameObject);
            key2Image.enabled = true;
            key2.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Key3"))
        {
            Destroy(collision.gameObject);
            key3Image.enabled = true;
            key3.SetActive(true);
        }
    }
}
