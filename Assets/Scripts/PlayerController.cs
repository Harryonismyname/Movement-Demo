using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    public CharacterController controller;
    public Transform cam;
    public float movementSpeed = 5.0f;
    private Vector3 movement;
    private float lookSensitivity = 1f;
    public Vector3 rotation = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPaused)
        {
            // Analogue Movement Controls
            if(gameManager.movementType == "Analogue")
            {
                // Gathering Data for movement
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                movement = new Vector3(horizontal, 0, vertical).normalized;
                if (movement.magnitude >= 0.1f)
                {
                    AnalogueMovement(movement);
                }
            }
            
            if (gameManager.cameraType == "Third-Person")
            {
                // Gathering Data for rotation
                float _yRot = Input.GetAxisRaw("Mouse X");
                Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;
                ThirdPersonRotate(_rotation);

            }
            if(gameManager.cameraType == "First-Person")
            {
                FirstPersonRotate();
            }
        }
    }
    void FixedUpdate()
    {
        if (!gameManager.isPaused)
        {
        }
    }
    // Camera Functions
    void ThirdPersonRotate(Vector3 _rotation)
    {
        rotation = _rotation;
        // rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        transform.rotation = transform.rotation * Quaternion.Euler(_rotation);
    }

    void FirstPersonRotate()
    {
        transform.rotation = cam.rotation;
    }

    // Movement Funtions
    void AnalogueMovement(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
    }
}
