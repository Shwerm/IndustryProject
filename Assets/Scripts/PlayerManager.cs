using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;  // Speed of forward and backward movement.
    [SerializeField] private float rotationSpeed = 100f;  // Speed of rotation left and right.
    [SerializeField] private float jumpForce = 4f;  // Force applied when jumping.
    [SerializeField] private bool isGrounded;  // Flag to check if the object is grounded.

    private Rigidbody rb;  // Reference to the Rigidbody component.


    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Get the Rigidbody component on start.
    }


    void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with an object tagged as "Ground."
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


    void OnCollisionExit(Collision collision)
    {
        // Check if the player is no longer colliding with an object tagged as "Ground."
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


    void Update()
    {
        // Movement input from the player.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction.
        Vector3 moveDirection = transform.forward * verticalInput * moveSpeed;

        // Calculate rotation input and rotation direction.
        float rotationInput = horizontalInput * rotationSpeed;

        // Apply movement and rotation to the Rigidbody.
        rb.MovePosition(rb.position + moveDirection * Time.deltaTime);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * rotationInput * Time.deltaTime));

        // Jumping: apply vertical force if grounded and jump button is pressed.
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;  // Set to false to prevent double jumping.
        }
    }
}

