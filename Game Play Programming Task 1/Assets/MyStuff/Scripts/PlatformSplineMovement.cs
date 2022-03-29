using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSplineMovement : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointC;
    [SerializeField] private Transform pointD;
    [SerializeField] private Transform Platform;

    public GameObject PlayerObject;

    public float time;

    public float interpolateAmount;
    private float confirmed_time;
    private float step;

    public bool towards = false;
    float upperB;
    float lowerB;
    float moveDirection = 1.0f;

    private void Awake()
    {
        confirmed_time = time;
        upperB = confirmed_time - step;
        lowerB = step;
    }
    private void OnValidate()
    {
        step = 1 / (time);
        if(time != confirmed_time)
        {
            interpolateAmount = 0;
            confirmed_time = time;
        }
    }
    private void Update()
    {
        if ((interpolateAmount >= upperB && !towards) || (interpolateAmount <= lowerB && towards))
        {
            towards = !towards;
            moveDirection = moveDirection * -1.0f;
        }

        interpolateAmount = (interpolateAmount + moveDirection * Time.deltaTime) % time;

        Platform.position = CubicLerp(pointA.position, pointB.position, pointC.position, pointD.position, interpolateAmount * step);
    }

    private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Slerp(a, b, t);
        Vector3 bc = Vector3.Slerp(b, c, t);

        return Vector3.Slerp(ab, bc, interpolateAmount * step);
    }

    private Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab_bc = QuadraticLerp(a, b, c, t);
        Vector3 bc_cd = QuadraticLerp(b, c, d, t);

        return Vector3.Slerp(ab_bc, bc_cd, interpolateAmount * step);
    }
}
