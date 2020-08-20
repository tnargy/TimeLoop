using UnityEngine;
    

public class ThirdPersonMovement : MonoBehaviour
{
    public const float GRAVITY = -9.81f;
    
    CharacterController controller;
    Animator animator;

    [SerializeField] Transform cam;
    [SerializeField] Transform groundCheck;
    [SerializeField] float jumpHeight;
    [SerializeField] float groundDistance;
    [SerializeField] float turnSmoothTime;
    [SerializeField] LayerMask groundMask;

    bool isGrounded;
    float moveSpeed;
    float turnSmoothVelocity;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * GRAVITY);
        }

        // gravity
        velocity.y += GRAVITY * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        moveSpeed = 3f * direction.magnitude;
        if (direction.magnitude >= 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Running
                moveSpeed *= 2;
            }
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
        animator.SetFloat("speed", moveSpeed);
    }
}
