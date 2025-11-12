using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public ShaderEffect_CorruptedVram corruptVram;
    public bool isEnding;
    public bool beninging;
    public GameObject end;
    public GameObject start;
    public NormalMovement movement;
    public ConstantForce2D cForce2D;
    public Animator whiteScreenAnim;
    public GameObject quit;
    void Update()
    {
        if (isEnding)
        {

            corruptVram.shift = Mathf.Lerp(corruptVram.shift, 20f, 0.2f * Time.deltaTime);
        }

        if (corruptVram.shift >= 14f)
        {
            isEnding = false;
            corruptVram.shift = 0f;
            transform.position = new Vector3(0.5f, 0.5f, 0);
            beninging = true;
            end.SetActive(false);
        }
        
        if (transform.position.x >= 26)
        {
            movement.enabled = false;
            transform.rotation = Quaternion.Euler(0, 0, 90);
            Vector2 force = cForce2D.force;
            force.x = Mathf.Lerp(force.x, 50f, 1f * Time.deltaTime);
            cForce2D.force = force;
            movement.flag = start;
            StartCoroutine(EndGame());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            corruptVram.enabled = true;
            isEnding = true;
        }
    }
    
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        whiteScreenAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        quit.SetActive(true);
    }
}
