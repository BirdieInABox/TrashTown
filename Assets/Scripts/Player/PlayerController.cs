//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IEventListener
{
    public float walkSpeed = 5f; // walking speed
    public float swimSpeed = 5f; // swimming speed
    public float sprintSpeed = 10f; //fast swimming speed
    public float riseSpeed = 2f; //underwater rising speed
    public float diveSpeed = 1.5f; //underwater diving speed
    private CharacterController controller;

    [SerializeField]
    private Upgrades upgrade;
    private Vector3 velocity;
    public float gravity = -9.81f; //The gravitational forces on the player
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
    private PlayerInput inputActions;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        DontDestroyOnLoad(gameObject);
        inputActions = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        EventManager.MainStatic.AddListener(this);
    }

    private void OnLevelWasLoaded()
    {
        if (
            SceneManager.GetActiveScene().name == ((Scenes)0).ToString()
            || SceneManager.GetActiveScene().name == ((Scenes)1).ToString()
        )
        {
            EventManager.MainStatic.AddListener(this);
            sprintSpeed = upgrade.GetSpeed();
        }
        UpdateControls();
    }

    private void UpdateControls()
    {
        if (SceneManager.GetActiveScene().name == ((Scenes)1).ToString())
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            underWater = true;
            inputActions.currentActionMap = inputActions.actions.FindActionMap(
                ((Scenes)3).ToString()
            );
        }
        else if (SceneManager.GetActiveScene().name == ((Scenes)4).ToString())
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            underWater = false;
            inputActions.currentActionMap = inputActions.actions.FindActionMap(
                ((Scenes)2).ToString()
            );
        }
    }

    private void ToggleDialogue()
    {
        if (inputActions.currentActionMap == inputActions.actions.FindActionMap("Dialogue"))
        {
            inputActions.currentActionMap = (
                underWater
                    ? inputActions.actions.FindActionMap("Water")
                    : inputActions.actions.FindActionMap("Land")
            );
        }
        else
        {
            inputActions.currentActionMap = inputActions.actions.FindActionMap("Dialogue");
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
    }

    public void OnEventReceived(EventData receivedEvent)
    {
        if (receivedEvent.Type == EventType.DialogueToggled)
        {
            ToggleDialogue();
        }
        else if (receivedEvent.Type == EventType.CraftingToggled)
        {
            ToggleDialogue();
        }
        else if (receivedEvent.Type == EventType.SetSpawn)
        {
            GoToSpawn((Transform)receivedEvent.Data);
        }
    }

    private void GoToSpawn(Transform spawn)
    {
        this.transform.position = spawn.position;
        this.transform.rotation = spawn.rotation;
    }

    public void OnPause(InputValue value)
    {
        if (inputActions.currentActionMap == inputActions.actions.FindActionMap("UI"))
        {
            inputActions.currentActionMap = (
                underWater
                    ? inputActions.actions.FindActionMap("Water")
                    : inputActions.actions.FindActionMap("Land")
            );
        }
        else
        {
            inputActions.currentActionMap = inputActions.actions.FindActionMap("UI");
        }
        EventManager.MainStatic.FireEvent(new EventData(EventType.GamePaused));
    }

    private void Walk()
    {
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;
        moveDirection *= walkSpeed;
        lookDirection = moveDirection + bodyOfPlayer.transform.position;
        //lookDirection.y = ;
        bodyOfPlayer.transform.LookAt(lookDirection);
        moveDirection.y += gravity;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Swim()
    {
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
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

    private void sendRay()
    {
        Vector3 rayAngle = bodyOfPlayer.transform.forward;
        rayAngle.y = bodyOfPlayer.transform.forward.y + rayYOffset;
        ray = new Ray(bodyOfPlayer.transform.position, rayAngle);
    }

    public void OnInteract(InputValue value)
    {
        //Ray from center of player towards the faced direction
        sendRay();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WaterBarrier")
        {
            other.gameObject.GetComponent<WaterBarrier>().CheckUpgrade(upgrade.airBottle);
        }
    }
}
