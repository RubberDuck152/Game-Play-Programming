using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;

    public Transform cam;

    private Animator anim;
    private Hashing hash;

    private bool canDoubleJump = false;
    private bool groundedPlayer;

    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float turnSmoothTime = 0.1f;
    public float speedDampTime = 0.01f;
    public float jumpForce;

    private float gravityValue = -9.81f;
    private float timer = 0.0f;

    private Vector3 movementVector;
    private Vector3 playerVelocity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<Hashing>();

        anim?.SetLayerWeight(1, 1f);
    }

    void Update()
    {
        // Checks to see if the player is grounded
        if (controller.isGrounded)
        {
            groundedPlayer = true;
        }

        if (groundedPlayer && playerVelocity.y < 0)
        {
            movementVector = Vector3.zero;
            playerVelocity.y = 0f;
        }

        // Gets all the Input values from the keyboard / controller
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Creates a new Vector3 to move the player
        Vector3 move = new Vector3(horizontal, 0, vertical);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            anim?.SetBool(hash.movingBool, true);
        }
        else
        {
            anim?.SetBool(hash.movingBool, false);
        }

        // Jumping for the Player Character
        if (Input.GetButtonDown("Jump") && groundedPlayer || canDoubleJump && Input.GetButtonDown("Jump"))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            anim?.SetBool(hash.jumpBool, true);
            anim?.SetBool(hash.fallingBool, true);
            anim?.SetBool(hash.landingBool, true);

            groundedPlayer = false;

            if (Input.GetButtonDown("Jump") && canDoubleJump && groundedPlayer == false)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                anim?.SetBool(hash.jumpBool, true);
                anim?.SetBool(hash.fallingBool, true);
                if (groundedPlayer)
                {
                    anim?.SetBool(hash.landingBool, true);
                }
                canDoubleJump = false;
            }
        }
        else
        {
            anim?.SetBool(hash.jumpBool, false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        // Maintains the velocity whilst jumping however unable to change direction
        controller.Move(movementVector);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpeedBoost")
        {
            playerSpeed = 12.0f;
        }

        if (other.tag == "DoubleJump")
        {
            canDoubleJump = true;
        }
    }
}
