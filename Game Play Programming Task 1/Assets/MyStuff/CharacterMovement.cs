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

    public bool armed = false;
    private bool canDoubleJump = false;
    private bool groundedPlayer;

    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float turnSmoothTime = 0.1f;
    public float speedDampTime = 0.01f;
    public float jumpForce;

    private float gravityValue = -9.81f;
    private float timer = 0.0f;

    float turnSmoothVelocity;

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
        if (Input.GetKeyDown("f"))
        {
            armed = !armed;
        }
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
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (armed == false)
        {
            anim?.SetBool(hash.armedBool, false);
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
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
        }
        else
        {
            anim?.SetBool(hash.armedBool, true);

            if (Input.GetButtonDown("Main Attack"))
            {
                anim?.SetBool(hash.attack1Bool, true);
            }
            else
            {
                anim?.SetBool(hash.attack1Bool, false);
            }

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
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
