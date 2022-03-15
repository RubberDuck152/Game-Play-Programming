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
    private float interpolateAmount;

    private void Update()
    {
        interpolateAmount = (interpolateAmount + Time.deltaTime) % 1f;
        Platform.position = CubicLerp(pointA.position, pointB.position, pointC.position, pointD.position, interpolateAmount);
    }

    private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Slerp(a, b, t);
        Vector3 bc = Vector3.Slerp(b, c, t);

        return Vector3.Slerp(ab, bc, interpolateAmount);
    }

    private Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 D, float t)
    {
        Vector3 ab_bc = QuadraticLerp(a, b, c, t);
        Vector3 bc_cd = QuadraticLerp(b, c, D, t);

        return Vector3.Slerp(ab_bc, bc_cd, interpolateAmount);
    }
}
