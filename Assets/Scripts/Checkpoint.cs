using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public NormalMovement move;
    public SpriteRenderer spriteRender;
    public Sprite checkpoint;
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Check"))
        {
            move.flag = other.gameObject;
            spriteRender = other.gameObject.GetComponent<SpriteRenderer>();
            spriteRender.sprite = checkpoint;
            other.gameObject.tag = "CheckCollected";
        }
    }
}
