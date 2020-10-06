using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchScript : MonoBehaviour
{
    CapsuleCollider playerCol;
    CharacterController playerCol2;
    float originalCapHeight;
    float originalCharHeight;
    public float reducedHeight;

    void Start()
    {
        playerCol = GetComponent<CapsuleCollider>();
        playerCol2 = GetComponent<CharacterController>();
        originalCapHeight = playerCol.height;
        originalCharHeight = playerCol2.height;
    }

    void Update()
    {
        //TODO change crouch input in Input Manager for Control recognition

        //Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl)) //Input.GetButtonDown("Crouch"):
            Crouch();
        else if (Input.GetKeyUp(KeyCode.LeftControl))
            GoUp();
    }

    void Crouch()
    {
        playerCol.height = reducedHeight;
        playerCol2.height = reducedHeight;
    }

    void GoUp()
    {
        playerCol.height = originalCapHeight;
        playerCol2.height = originalCharHeight;
    }
}
