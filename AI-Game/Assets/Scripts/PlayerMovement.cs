using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerCamera;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float mouseSensitivity = 100f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    float xRotation = 0f; // To keep track of vertical rotation (for looking up and down)

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset vertical velocity when grounded
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        // Get input from WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move player in the facing direction
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        
        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Mouse look - Camera movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player horizontally (left and right)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically (up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical rotation to prevent over-rotation
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }
}
