using Unity.Netcode;
using UnityEngine;
using Cinemachine;

public class PlayerController : NetworkBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float jumpForce = 7f;
    public float gravityMultiplier = 2f;

    [Header("References")]
    public Rigidbody rb;
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    private bool isGrounded;
    private bool isSprinting = false;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true; // Prevents unwanted rotation

        CinemachineVirtualCamera cam = FindFirstObjectByType<CinemachineVirtualCamera>();
        if (cam != null)
        {
            cam.Follow = transform;
            cam.LookAt = transform;
        }
    }

    void Update()
    {
        if (!IsOwner) return;
        MovePlayer();
    }

    void MovePlayer()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        isSprinting = Input.GetKey(KeyCode.LeftShift);
        float speed = isSprinting ? sprintSpeed : walkSpeed;

        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);

        if (isGrounded && Input.GetButtonDown("Jump"))
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);

        if (!isGrounded)
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (gravityMultiplier - 1) * Time.deltaTime;
    }
}
