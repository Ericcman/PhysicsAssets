using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public AudioSource footstepAudio;  // Assign in Inspector
    public AudioClip footstepClip;     // Assign a footstep sound
    public float footstepInterval = 0.5f; // Time between footstep sounds
    private float footstepTimer = 0f;

    // Movement settings
    public float moveSpeed = 5f;
    public float sprintSpeed = 5f;

    // Mouse look settings
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    public float minLookY = -60f;
    public float maxLookY = 60f;

    // Head bob settings
    public float headBobSpeed = 10f; 
    public float headBobAmount = 0.1f; 
    private float originalPosY;

    // Internal variables
    private float rotationX = 0f;
    private bool isWalking = false;

    // References to the Camera and Rigidbody
    private Transform playerCamera;
    private Rigidbody rb;

    void Start()
    {
        // Store the original Y position of the camera for resetting later
        playerCamera = Camera.main.transform;
        originalPosY = playerCamera.localPosition.y;

        // Lock the cursor to the center of the screen
        footstepAudio = GetComponent<AudioSource>();

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Prevent the Rigidbody from rotating due to physics
    }

    void OnApplicationQuit()
    {
        // Unlock cursor when quitting the game
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }

            // Mouse Look (Horizontal look)
            float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        transform.Rotate(Vector3.up * mouseX); // Rotate player around the Y-axis

        // Mouse Look (Vertical look)
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minLookY, maxLookY); // Clamp up and down movement
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f); // Rotate camera on the X-axis for up/down look

        // Player Movement (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal"); 
        float moveZ = Input.GetAxis("Vertical");   

        // Determine speed based on whether shift is held (Sprint)
        float speed = (Input.GetKey(KeyCode.LeftShift)) ? sprintSpeed : moveSpeed;

        // Calculate movement directions relative to the camera (the movement is always in world space)
        Vector3 move = (transform.right * moveX) + (transform.forward * moveZ);

        // Ensure movement is parallel to the ground (ignore y axis)
        move.y = 0f;

        // Apply movement using Rigidbody's velocity 
        Vector3 targetVelocity = move * speed;
        targetVelocity.y = rb.velocity.y; // Keep the player grounded
        rb.velocity = targetVelocity;

        // Check if the player is moving
        isWalking = moveX != 0 || moveZ != 0;

        // Apply Head Bob if walking
        if (isWalking)
        {
            ApplyHeadBob();

            // Play footsteps at intervals
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                footstepAudio.PlayOneShot(footstepClip);  // Play footstep sound
                footstepTimer = footstepInterval; // Reset timer
            }
        }
        else
        {
            ResetHeadBob();
        }
    }

    void ApplyHeadBob()
    {
        // Bobbing based on sine wave (sin time * speed) to make a smooth up-and-down movement
        float newY = originalPosY + Mathf.Sin(Time.time * headBobSpeed) * headBobAmount;

        // Apply the calculated vertical position to the camera's local position
        playerCamera.localPosition = new Vector3(playerCamera.localPosition.x, newY, playerCamera.localPosition.z);
    }

    void ResetHeadBob()
    {
        // Reset the camera's Y position back to its original height
        if (Mathf.Abs(playerCamera.localPosition.y - originalPosY) > 0.01f)
        {
            playerCamera.localPosition = new Vector3(playerCamera.localPosition.x, Mathf.Lerp(playerCamera.localPosition.y, originalPosY, Time.deltaTime * 10f), playerCamera.localPosition.z);
        }
    }
   
}

