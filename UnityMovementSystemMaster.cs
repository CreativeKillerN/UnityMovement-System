using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 10f;
    public float crouchSpeed = 2f;
    public float slideForce = 5f;

    private Rigidbody rb;
    private bool isRunning = false;
    private bool isCrouching = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch();
        }
        else
        {
            isCrouching = false;
        }

        Move(movement);
    }

    void Move(Vector3 movement)
    {
        float speed = walkSpeed;

        if (isRunning)
        {
            speed = runSpeed;
            if (isCrouching)
            {
                rb.AddForce(transform.forward * slideForce, ForceMode.VelocityChange);
            }
        }

        if (isCrouching)
        {
            speed = crouchSpeed;
        }

        Vector3 newPosition = rb.position + transform.TransformDirection(movement) * speed * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Crouch()
    {
        isCrouching = true;
    }
}
