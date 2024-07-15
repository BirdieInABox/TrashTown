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

    [SerializeField] //a reference to the player's upgrades
    private Upgrades upgrade;
    public float gravity = -9.81f; //The gravitational forces on the player
    private Vector2 direction; //The 2-axes inputs
    private Ray ray; //the raycast for interactions
    public float rayYOffset = 0; //the ray's angular offset)
    public float interactDistance = 2f; //the distance at which the player can interact
    public LayerMask rayMask; //the layers the player can interact with

    //whether the player is under water or not
    //(can be hidden in inspector for release, needs to be public for debugging)
    public bool underWater = false;
    private Vector3 lookDirection; //The direction the player should be facing
    public GameObject bodyOfPlayer; //the visual and physical body of the player
    private bool isSprinting = false; //whether the player is currently sprinting
    private float waterVerticality = 0f; //the 3rd axis used for underwater movement
    private PlayerInput inputActions; //the input component, which holds the input maps

    private void Awake()
    {
        //Get components
        controller = GetComponent<CharacterController>();
        inputActions = GetComponent<PlayerInput>();
        //Make this persistent between scenes
        //DontDestroyOnLoad(gameObject);
    }

    /*   private void Start()
       {
           //Load the game
           Load();
       }*/

    private void Start()
    {
        //If the current scene is at index 0 or 1 of the Scenes enum (Land or Water)
        if (
            SceneManager.GetActiveScene().name == ((Scenes)0).ToString()
            || SceneManager.GetActiveScene().name == ((Scenes)1).ToString()
        )
        {
            //Activate the player's body
            this.transform.GetChild(0).gameObject.SetActive(true);
            //Add this as listener to the event system
            EventManager.MainStatic.AddListener(this);
            //get the seaScooter's speed. This only needs to be done on scene changes,
            //as it only affects underwater movement and a new sea scooter can only
            //be crafted on land.
            sprintSpeed = upgrade.GetSpeed();
            //Update controls according to the current scene
            UpdateControls();
        }
        else //if in loading screen or main menu, hide the player
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Switches between input action map, depending on the current scene
    /// </summary>
    private void UpdateControls()
    {
        //if the current scene is at index 1 of the Scenes enum (Water)
        if (SceneManager.GetActiveScene().name == ((Scenes)1).ToString())
        {
            //switch to underwater, which affects which movement type is being called
            underWater = true;
            //Switch input action map to one with the name of the scene at index 1 (Water)
            inputActions.currentActionMap = inputActions.actions.FindActionMap(
                ((Scenes)1).ToString()
            );
        }
        else
        {
            //switch to on land, which affects which movement type is being called
            underWater = false;
            //Switch input action map to one with the name of the scene at index 0 (Land)
            inputActions.currentActionMap = inputActions.actions.FindActionMap(
                ((Scenes)0).ToString()
            );
        }
    }

    /// <summary>
    /// Toggles the action map between Dialogue and Land/Water
    /// </summary>
    private void ToggleDialogue()
    {
        //If it's already using the Dialogue action map
        if (inputActions.currentActionMap == inputActions.actions.FindActionMap("Dialogue"))
        {
            //Version for Debugging, as the debug scenes' names don't align with the action maps' names
            /* inputActions.currentActionMap = (
                 underWater
                     ? inputActions.actions.FindActionMap("Water")
                     : inputActions.actions.FindActionMap("Land")
             );*/

            //Use the action map whose name is the same as the scene name (Land/Water)
            inputActions.currentActionMap = inputActions.actions.FindActionMap(
                SceneManager.GetActiveScene().name
            );
        }
        else //If it's not currently using the Dialogue action map
        {
            //Use the dialogue action map
            inputActions.currentActionMap = inputActions.actions.FindActionMap("Dialogue");
        }
    }

    private void Update()
    {
        //if underwater, using water movement
        if (underWater)
            Swim();
        else //else, use land movement
        {
            Walk();
        }
    }

    /// <summary>
    /// Called upon EventSystem sending an event
    /// </summary>
    /// <param name="receivedEvent">the received event, including type and content</param>
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
    }

    /// <summary>
    /// Called by Pause key, sends event "GamePaused"
    /// </summary>
    public void OnPause(InputValue value)
    {
        //If it's currently using the UI action map
        if (inputActions.currentActionMap == inputActions.actions.FindActionMap("UI"))
        {
            //Version for Debugging, as the debug scenes' names don't align with the action maps' names
            /* inputActions.currentActionMap = (
                 underWater
                     ? inputActions.actions.FindActionMap("Water")
                     : inputActions.actions.FindActionMap("Land")
             );*/

            //Use the action map whose name is the same as the scene name (Land/Water)
            inputActions.currentActionMap = inputActions.actions.FindActionMap(
                SceneManager.GetActiveScene().name
            );
        }
        else //if it isn't using the UI action map
        {
            //use the UI action map
            inputActions.currentActionMap = inputActions.actions.FindActionMap("UI");
        }
        //Sends GamePaused event through the event system
        EventManager.MainStatic.FireEvent(new EventData(EventType.GamePaused));
    }

    /// <summary>
    /// Moves the CharacterController
    /// </summary>
    private void Walk()
    {
        //set direction to new vector3 using 2-axis movement, excluding vertical movement
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        //Get direction through camera direction
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        // nullify y-movement
        moveDirection.y = 0;
        //Add movement speed
        moveDirection *= walkSpeed;
        //calculate the body's faced direction
        lookDirection = moveDirection + bodyOfPlayer.transform.position;
        //Have the body face that direction
        bodyOfPlayer.transform.LookAt(lookDirection);
        //Add gravity to the player
        moveDirection.y += gravity;
        //Move the player, normalized over time, instead of frames
        controller.Move(moveDirection * Time.deltaTime);
    }

    /// <summary>
    /// Moves the CharacterController under water
    /// </summary>
    private void Swim()
    {
        //set direction to new vector3 using 2-axis movement, excluding vertical movement
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        //Get direction through camera direction
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        //Add sprint speed or swim speed, depending on status of isSprinting
        moveDirection *= (isSprinting ? sprintSpeed : swimSpeed);
        //Add vertical axis from rise/dive input
        moveDirection.y = waterVerticality;
        //calculate the body's faced direction
        lookDirection = moveDirection + bodyOfPlayer.transform.position;
        //Have the body face that direction
        bodyOfPlayer.transform.LookAt(lookDirection);
        //Move the player, normalized over time, instead of frames
        controller.Move(moveDirection * Time.deltaTime);
    }

    /// <summary>
    /// Called by Move keys (WASD, Arrows)
    /// </summary>
    public void OnMove(InputValue value)
    {
        //Get 2-axis movement values between -1 and 1
        direction = value.Get<Vector2>();
    }

    /// <summary>
    /// Called when OnSprint key is pressed down, called again on release
    /// </summary>
    public void OnSprint(InputValue value)
    {
        isSprinting = !isSprinting;
    }

    /// <summary>
    /// Called by Rise Key (standard: space)
    /// </summary>
    public void OnRise(InputValue value)
    {
        //Add value between 1 and 0 to waterVerticality, affected by riseSpeed
        waterVerticality = value.Get<float>() * riseSpeed;
    }

    /// <summary>
    /// Called by Rise Key (standard: control)
    /// </summary>
    public void OnDive(InputValue value)
    {
        //Add value between -1 and 0 to waterVerticality, affected by diveSpeed
        waterVerticality = -value.Get<float>() * diveSpeed;
    }

    /// <summary>
    /// Send a raycast in the direction the player body is facing
    /// </summary>
    private void sendRay()
    {
        //Get the direction the body is facing
        Vector3 rayAngle = bodyOfPlayer.transform.forward;
        //Add vertical offset
        rayAngle.y = bodyOfPlayer.transform.forward.y + rayYOffset;
        //send ray from player's position
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

    /// <summary>
    /// called when entering a trigger
    /// </summary>
    /// <param name="other">the collider of the other object</param>
    private void OnTriggerEnter(Collider other)
    {
        //If the collided object has the tag "Water Barrier"
        if (other.gameObject.tag == "WaterBarrier")
        {
            //Check if the player can pass the barrier
            other.gameObject.GetComponent<WaterBarrier>().CheckUpgrade(upgrade.airBottle);
        }
    }

    /// <summary>
    /// As this is the only persistent object in the project, and we only want to load the game once,
    /// the game gets loaded once by the PlayerController
    /// </summary>
    private void Load()
    {
        //Send an event of type "LoadGame" to the event system
        EventManager.MainStatic.FireEvent(new EventData(EventType.LoadGame));
    }
}
