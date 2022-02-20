using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hashing : MonoBehaviour
{
    public int sneakBool;
    public int jumpBool;
    public int sprintBool;
    public int backwardsBool;
    public int speedFloat;
    public int movingBool;

    private void Awake()
    {
        movingBool = Animator.StringToHash("Moving");
        jumpBool = Animator.StringToHash("Jumping");
        backwardsBool = Animator.StringToHash("Velocity X");
        speedFloat = Animator.StringToHash("AnimationSpeed");
    }
}
