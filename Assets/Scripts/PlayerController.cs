using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    public Rigidbody rb;
    public float movementSpeed = 5.0f;
    public float jumpForce = 5f;
    public Vector3 movement;
    public float lookSensitivity = 4.0f;
    public Vector3 rotation = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPaused)
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            float _yRot = Input.GetAxisRaw("Mouse X");
            Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;
            Rotate(_rotation);
        }
    }
    void FixedUpdate()
    {
        if (!gameManager.isPaused)
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                AnalogueMovement(movement);
            }
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            }
        }
    }
    void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }
    void AnalogueMovement(Vector3 direction)
    {
        if (Input.GetAxisRaw("Vertical") == 1f)
        {
            rb.MovePosition(transform.position + transform.forward + (direction * movementSpeed * Time.deltaTime));
        }
        else if (Input.GetAxisRaw("Vertical")==-1f)
        {
            rb.MovePosition(transform.position - transform.forward + (direction * (movementSpeed - 4f) * Time.deltaTime));
        }
        else if(Input.GetAxisRaw("Horizontal")== 1f)
        {
            rb.MovePosition(transform.position + transform.right + (direction * movementSpeed * Time.deltaTime));
        }
        else if (Input.GetAxisRaw("Horizontal") == -1f)
        {
            rb.MovePosition(transform.position - transform.right - (direction * movementSpeed * Time.deltaTime));
        }
    }
}
