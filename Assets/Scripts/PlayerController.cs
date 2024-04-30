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
    public float riseSpeed = 2f;
    public float diveSpeed = 1.5f;
    private CharacterController controller;
    private Vector3 velocity;
    public float gravity = -9.81f;
    private Vector3 direction;
    private Ray ray;
    public float rayYOffset = 0;
    public float interactDistance = 2f;
    public LayerMask rayMask;
    public bool underWater = false;
    private Vector3 lookDirection;

    public GameObject bodyOfPlayer;

    private bool isSprinting = false;
    private float waterVerticality = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        PlayerInput inputActions = GetComponent<PlayerInput>();
        if (underWater)
        {
            inputActions.currentActionMap = inputActions.actions.FindActionMap("Water");
        }
        else
        {
            inputActions.currentActionMap = inputActions.actions.FindActionMap("Land");
        }
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
        Vector3 rayAngle = bodyOfPlayer.transform.forward;
        rayAngle.y = bodyOfPlayer.transform.forward.y + rayYOffset;
        ray = new Ray(bodyOfPlayer.transform.position, rayAngle);
    }

    private void Walk()
    {
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= walkSpeed;
        lookDirection = moveDirection + bodyOfPlayer.transform.position;
        bodyOfPlayer.transform.LookAt(lookDirection);
        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Swim()
    {
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= (isSprinting ? sprintSpeed : swimSpeed);
        moveDirection.y = waterVerticality;
        lookDirection = moveDirection + bodyOfPlayer.transform.position;
        bodyOfPlayer.transform.LookAt(lookDirection);
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        isSprinting = !isSprinting;
    }

    public void OnRise(InputValue value)
    {
        waterVerticality = value.Get<float>() * riseSpeed;
    }

    public void OnDive(InputValue value)
    {
        waterVerticality = -value.Get<float>() * diveSpeed;
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
            else
            {
                Debug.Log("No Interactable");
            }
        }
    }
}
