using UnityEngine;

[ RequireComponent( typeof( Rigidbody ) ) ]
[ RequireComponent( typeof( Inventory ) ) ]
public class Character : MonoBehaviour
{
    public static Character Instance;
    public InGameMenu settingsMenu;

    public float moveSpeed = 5f;
    public float interactionDistance = 2f;
    public Transform cameraHolder;

    private float xRotation = 0f;

    private Rigidbody rb;
    public Inventory inventory;

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        inventory = GetComponent<Inventory>();

        // lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        Move( );

        if ( Input.GetKeyDown( KeyCode.E ) && interactable != null )
            interactable.onInteract.Invoke();
        
    }

    private void FixedUpdate()
    {
        Interaction();
    }

    void LateUpdate()
    {
        RotateCamera();
    }

    #endregion

    void Move()
    {
        if ( settingsMenu.IsPaused( ) )
            return;

        float moveX = Input.GetAxisRaw("Horizontal"); // (A/D)
        float moveZ = Input.GetAxisRaw("Vertical");   // (W/S)

        Vector3 move = (transform.right * moveX + transform.forward * moveZ).normalized;

        Vector3 velocity = move * moveSpeed;

        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }

    // todo: fix camera jitter when moving player and mouse
    void RotateCamera()
    {
        if ( settingsMenu.IsPaused( ) )
            return;

        float sensitivity = Settings.instance.GetMouseSensitivity( );
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;

        transform.localRotation *= Quaternion.Euler(0f, mouseX, 0f);

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


            if (interactable.gameObject != hit.collider.gameObject)
            {
                interactable = hit.collider.GetComponent<InteractableObject>();
                interactable.onCursorEnter.Invoke();
            }
        }
        else
        {
            if (interactable != null)
            {
                interactable.onCursorExit.Invoke();
                interactable = null;
            }
        }
    }
}