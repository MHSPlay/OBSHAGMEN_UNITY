using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ RequireComponent( typeof( Rigidbody ) ) ]
public class Character : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1000f;
    public Transform cameraHolder;

    private float xRotation = 0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
    }

    void LateUpdate()
    {
        RotateCamera();
    }

    // note: writen by MHSPlay
    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // (A/D)
        float moveZ = Input.GetAxisRaw("Vertical");   // (W/S)

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.y = 0;
        move.Normalize(); // fixed speed diagonal move

        Vector3 velocity = move * moveSpeed;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }

    // note: writen by MHSPlay
    // todo: fix camera jitter when moving player and mouse
    void RotateCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        transform.localRotation *= Quaternion.Euler( 0f, mouseX, 0f);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
