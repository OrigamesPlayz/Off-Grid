using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    [Header("Button")]
    public ButtonFunc buttonFunc;

    [Header("Camera Effects")]
    public PlayerCamFollow pCamFollow;
    public ShaderEffect_Tint tint;
    public ShaderEffect_BleedingColors bleedingColors;

    [Header("Player")]
    public Transform playerT;
    public FirstMovement firstMove;
    public NormalMovement normalMove;

    [Header("Bools")]

    public bool blackScreenActivated;
    public bool gameStarted;

    [Header("Misc")]
    public GameObject blackScreen;
    public GameObject gate;
    public GameObject controlsUI;
    public Transform flag;

    void Update()
    {
        if (buttonFunc.buttonPressed && !blackScreenActivated)
        {
            bleedingColors.intensity = Mathf.Lerp(bleedingColors.intensity, 20f, 1f * Time.deltaTime);
            tint.u = Mathf.Lerp(tint.u, 2.5f, 1f * Time.deltaTime);
            tint.v = Mathf.Lerp(tint.v, 2.5f, 0.5f * Time.deltaTime);
            firstMove.enabled = false;
        }

        if (tint.v >= 1.7f && !blackScreenActivated)
        {
            controlsUI.SetActive(false);
            blackScreen.SetActive(true);

            if (blackScreen.activeSelf)
            {
                StartCoroutine(StartGame());
            }
        }
    }

    IEnumerator StartGame()
    {
        blackScreenActivated = true;
        playerT.position = flag.position;
        firstMove.enabled = false;
        bleedingColors.intensity = 0f;
        tint.u = 1f;
        tint.v = 1f;
        yield return new WaitForSeconds(3);
        blackScreen.SetActive(false);
        normalMove.enabled = true;
        controlsUI.SetActive(true);
        pCamFollow.enabled = true;
        gate.SetActive(false);
        gameStarted = true;
    }
}
