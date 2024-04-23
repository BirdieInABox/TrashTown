//Author: Kim Effie Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f; // walking speed
    public float swimSpeed = 5f; // walking speed
    public float sprintSpeed = 10f; //sprinting speed
    private CharacterController controller;
    private Vector3 velocity;
    public float gravity = -9.81f;
    private Vector3 direction;
    private Ray ray;
    public float rayYOffset = 0;
    public float interactDistance = 2f;
    public LayerMask rayMask;
    public bool underWater = false;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    { //movement
        if (underWater)
            Swim();
        else
        {
            Walk();
        }
        //Ray from center of camera towards the faced direction
        sendRay();
    }

    private void sendRay()
    {
        Vector3 rayAngle = this.transform.forward;
        rayAngle.y = this.transform.forward.y + rayYOffset;
        ray = new Ray(this.transform.position, this.transform.forward);
    }

    private void Walk()
    {
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= walkSpeed;
        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Swim() { }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void OnInteract(InputValue value)
    {
        RaycastHit hitInfo;
        //If movement allowed and raycast hit an object on chosen layer
        if (Physics.Raycast(ray, out hitInfo, interactDistance, rayMask))
        {
            //If hit object has component Interactable
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                //Call Interact method of Interactable
                hitInfo.collider.GetComponent<Interactable>().Interact();
            }
        }
    }
}
