using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    public CharacterController controller;
    
    // Click-to-Move Variables
    public NavMeshAgent player;
    public Camera pointyCam;
    
    //Movement Variables
    public float movementSpeed = 5.0f;
    public Vector3 movement;
    private float gravity = 9.8f;
    private float vSpeed = 0;
    float turnSmoothVelocity;
    float turnSpeed = 0.1f;

    // Camera Control Variables
    private float lookSensitivity = 1f;
    public Vector3 rotation = Vector3.zero;
    public Transform cam;

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
            // Gathering Data for movement
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            if (controller.isGrounded)
            {
                vSpeed = 0;
            }
            vSpeed -= gravity * Time.deltaTime;
            controller.Move(new Vector3(0f, vSpeed, 0f));
            if (gameManager.cameraType == "Third-Person" && gameManager.movementType != "Tank")
            {
                // Gathering Data for rotation
                float _yRot = Input.GetAxisRaw("Mouse X");
                Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;
                ThirdPersonRotate(_rotation);

            }
            if (gameManager.cameraType == "First-Person" && gameManager.movementType != "Tank")
            {
                FirstPersonRotate();
            }
            // Analogue Movement Controls
            if (gameManager.movementType == "Analogue")
            {
                player.ResetPath();
                movement = new Vector3(horizontal, 0, vertical).normalized;
                if (movement.magnitude >= 0.1f)
                {
                    AnalogueMovement(movement);
                }
            }
            // Teleport Movement Controls
            if(gameManager.movementType == "Teleport")
            {
                player.ResetPath();
                movement = new Vector3(Mathf.Round(horizontal), 0f, Mathf.Round(vertical));
                if(Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
                {
                    if (movement.x == 1 || movement.x == -1 || movement.z == 1 || movement.z == -1)
                    {
                        TeleportMovement(movement);
                    }
                }
            }
            // Tank Movement Controls
            if(gameManager.movementType == "Tank")
            {
                player.ResetPath();
                movement = new Vector3(0f, 0f, vertical);
                Vector3 _rotation = new Vector3(0f, horizontal, 0f) * lookSensitivity;
                if (_rotation.magnitude >= 0.1f)
                {
                    transform.rotation = transform.rotation * Quaternion.Euler(_rotation);
                }
                if (movement.magnitude >= 0.1f)
                {
                    TankMovemvent(movement);
                }
            }
            // Click to Move Controls
            if(gameManager.movementType == "Click-To-Move")
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 destination = ClickToMove();
                    player.SetDestination(destination);
                }
                if (player.remainingDistance >= 0.1f)
                {
                    controller.Move(player.velocity.normalized * movementSpeed * Time.deltaTime);
                }
            }
        }
    }
    // Camera Functions
    void ThirdPersonRotate(Vector3 _rotation)
    {
        rotation = _rotation;
        if (_rotation.magnitude >= 0.1f)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(_rotation);
        }
    }

    void FirstPersonRotate()
    {
        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yRot, 0f);
        if (_rotation.magnitude >= 0.1f)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(_rotation);
        }
    }

    // Movement Funtions                                                                                                                                                  
    void AnalogueMovement(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        if (gameManager.cameraType == "Top-Down" || gameManager.cameraType == "Orthographic")
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        moveDir.y = vSpeed;
        controller.Move(moveDir.normalized * movementSpeed * Time.deltaTime);

    }
    void TeleportMovement(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg+ cam.eulerAngles.y;
        if (gameManager.cameraType == "Top-Down" || gameManager.cameraType == "Orthographic")
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        moveDir.y = vSpeed;
        controller.Move(moveDir*movementSpeed);
    }
    void TankMovemvent(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        if (gameManager.cameraType == "Top-Down" || gameManager.cameraType == "Orthographic")
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + transform.eulerAngles.y;
        }
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f)*Vector3.forward;
        moveDir.y = vSpeed;
        controller.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
    }
    Vector3 ClickToMove()
    {
        Ray ray = pointyCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
                
            return hit.point;
        }
        else
        {
            return hit.point;
        }
    }
}
