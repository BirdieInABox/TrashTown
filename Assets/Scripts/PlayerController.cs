//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f; // walking speed
    public float swimSpeed = 5f; // walking speed
    public float sprintSpeed = 10f; //sprinting speed
    private CharacterController characterController;
    private Vector3 velocity;
    public float gravity = -9.81f;
    private Vector2 direction;

    // Start is called before the first frame update
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //movement
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        Vector3 move = transform.right * direction.x + transform.forward * direction.y;
        characterController.Move(move * moveSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        gameObject.transform.LookAt(
            new Vector3(direction.x, 0, direction.y) + gameObject.transform.position
        );
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }
}
