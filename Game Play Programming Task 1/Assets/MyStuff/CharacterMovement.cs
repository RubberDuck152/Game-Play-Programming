using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;

    public Transform cam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float speedDampTime = 0.01f;
    float turnSmoothVelocity;

    private Animator anim;
    private Hashing hash;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<Hashing>();

        anim.SetLayerWeight(1, 1f);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool jump = Input.GetButtonUp("Jump");

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool(hash.movingBool, true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool(hash.movingBool, false);
        }


        if (jump)
        {
            anim.SetBool(hash.jumpBool, true);
            anim.SetBool(hash.landingBool, true);
        }
    }
}
