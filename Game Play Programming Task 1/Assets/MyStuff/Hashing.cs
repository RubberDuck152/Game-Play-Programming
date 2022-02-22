using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hashing : MonoBehaviour
{
    public int sneakBool;
    public int jumpBool;
    public int sprintBool;
    public int landingBool;
    public int speedFloat;
    public int movingBool;

    private void Awake()
    {
        movingBool = Animator.StringToHash("Moving");
        jumpBool = Animator.StringToHash("Jump");
        landingBool = Animator.StringToHash("Landing");
        speedFloat = Animator.StringToHash("AnimationSpeed");
    }
}
