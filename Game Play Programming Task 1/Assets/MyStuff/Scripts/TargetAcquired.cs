using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAcquired : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player;
    public GameObject cameraObj;
    public Transform target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            mainCamera.transform.parent = player.transform;
            mainCamera.transform.LookAt(target);
            cameraObj.GetComponent<FollowCamera>().CameraTarget = target;
            Debug.Log("I have entered this zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        mainCamera.transform.parent = null;
        mainCamera.transform.LookAt(null);
        cameraObj.GetComponent<FollowCamera>().CameraTarget = player;
        Debug.Log("I have exited this zone");
    }
}
