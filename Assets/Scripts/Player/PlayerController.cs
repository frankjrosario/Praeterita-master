using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController cc;

    public float speed;

    Vector3 moveDirection = Vector3.zero;

    private bool useGravity;
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        moveDirection = transform.TransformDirection(xMov, 0, zMov);
        moveDirection *= speed * Time.deltaTime;

        cc.Move(moveDirection);
    }

    private void Gravity()
    {
        if(useGravity)
        {

        }
    }
}
