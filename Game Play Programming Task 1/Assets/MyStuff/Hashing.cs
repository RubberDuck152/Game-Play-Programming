using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hashing : MonoBehaviour
{
    public int sneakBool;
    public int jumpBool;
    public int landingBool;
    public int speedFloat;
    public int movingBool;
    public int fallingBool;

    private void Awake()
    {
        movingBool = Animator.StringToHash("Moving");
        jumpBool = Animator.StringToHash("Jump");
        landingBool = Animator.StringToHash("Landing");
        speedFloat = Animator.StringToHash("AnimationSpeed");
        fallingBool = Animator.StringToHash("Falling");
    }
}
