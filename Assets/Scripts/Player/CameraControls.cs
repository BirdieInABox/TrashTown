using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float sensitivity = 100f; //mouse sensitivity
    public Transform playerBody; //player controller to rotate

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock cursor at start
        Cursor.visible = false;
    }

    // <|°_°|>
    // Update is called once per frame
    void Update()
    {
        //mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

       // playerBody.Rotate(Vector3.up * mouseX);

        //rotate camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
