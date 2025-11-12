using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunc : MonoBehaviour
{
    public bool isInside;
    public bool buttonPressed;
    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E))
        {
            buttonPressed = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInside = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInside = false;
        }
    }
}
