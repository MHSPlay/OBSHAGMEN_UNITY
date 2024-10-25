using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ RequireComponent( typeof( Rigidbody ) ) ]
<<<<<<< HEAD
public class Character : MonoBehaviour
{
=======
[RequireComponent(typeof(Inventory))]
public class Character : MonoBehaviour
{
    public static Character Instance;

>>>>>>> OBSHAGMEN_UNITY/IgnatHuesos
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1000f;
    public float interactionDistance = 2f;
    public Transform cameraHolder;

    private float xRotation = 0f;

    private Rigidbody rb;
<<<<<<< HEAD
=======
    public Inventory inventory;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
>>>>>>> OBSHAGMEN_UNITY/IgnatHuesos

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

<<<<<<< HEAD
=======
        inventory = GetComponent<Inventory>();

>>>>>>> OBSHAGMEN_UNITY/IgnatHuesos
        // lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.E) && interactable != null)
        {
            interactable.onInteract.Invoke();
        }
    }

    private void FixedUpdate()
    {
        Interaction();
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

    private InteractableObject interactable;

    void Interaction()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactionDistance);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.gameObject.layer != 3)
            {
                return;
            }

            if (interactable == null)
            {
                interactable = hit.collider.GetComponent<InteractableObject>();
                interactable.onCursorEnter.Invoke();
            }
            else
            {
                if (interactable.gameObject != hit.collider.gameObject)
                {
                    interactable = hit.collider.GetComponent<InteractableObject>();
                    interactable.onCursorEnter.Invoke();
                }
            }
            
            
            if(interactable.gameObject != hit.collider.gameObject)
            {
                interactable = hit.collider.GetComponent<InteractableObject>();
                interactable.onCursorEnter.Invoke();
            }
        }
        else
        {
            if(interactable != null)
            {
                interactable.onCursorExit.Invoke();
                interactable = null;
            }
        }
    }
}
