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


    float sensitivity;

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
        if ( Input.GetKeyDown( KeyCode.E ) && interactable != null )
            interactable.onInteract.Invoke();
    }

    private void FixedUpdate()
    {
        Interaction();

        // if we call move there we don't need to multiply on delta-time
        if ( !settingsMenu.IsPaused( ) )
            Move();
    }

    void LateUpdate()
    {
        if ( !settingsMenu.IsPaused( ) )
            RotateCamera( );
    }

    #endregion

    void Move( ) {

        float moveX = Input.GetAxisRaw("Horizontal"); // (A/D)
        float moveZ = Input.GetAxisRaw("Vertical");   // (W/S)

        Vector3 moveDir = ( transform.right * moveX + transform.forward * moveZ ).normalized;

        Vector3 vel = moveDir * moveSpeed;
        rb.velocity = new Vector3( vel.x, rb.velocity.y, vel.z );
    }

    // todo: fix camera jitter when player moving and mouse moving too, focused on once object
    void RotateCamera( ) {

        sensitivity = ( float )Config.instance.GetSettings( "fMouseSensitivity" );
        float mouseX = Input.GetAxisRaw( "Mouse X" ) * sensitivity;
        float mouseY = Input.GetAxisRaw( "Mouse Y" ) * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp( xRotation, -89f, 89f );

        transform.localRotation *= Quaternion.Euler( 0f, mouseX, 0f );
        cameraHolder.localRotation = Quaternion.Euler( xRotation, 0f, 0f );
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