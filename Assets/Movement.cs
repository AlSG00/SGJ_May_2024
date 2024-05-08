using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public Transform groundCheck;

    public LayerMask groundMask;

    public float Speed = 10f;
    public float SprintSpeed = 20f;
    public float JumpHeight = 3f;
    public float Gravity = -9.8f;
    public float GroundDistance = 0.4f;

    Vector3 velocity;

    private bool isGrounded;

    //void Start()
    //{

    //}

    void FixedUpdate()
    {
        GravityLogic();
        MovementLogic();
    }

    private void GravityLogic()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, GroundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += Gravity * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);
    }

    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;

        controller.Move(movement * Speed * Time.fixedDeltaTime);

        //if (Input.GetKey("left shift"))
        //{
        //    controller.Move(movement * SprintSpeed * Time.fixedDeltaTime);
        //}

        //if (Input.GetButton("Jump") && isGrounded)
        //{
        //    velocity.y = Mathf.Sqrt(JumpHeight * -2 * Gravity);
        //}
    }
}
