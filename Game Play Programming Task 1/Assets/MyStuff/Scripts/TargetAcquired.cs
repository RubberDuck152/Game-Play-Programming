using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAcquired : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject cameraObj;
    public Transform player;
    public Transform target;
    public Transform cameraPos;

    public bool active = false;

    private void Update()
    {
        if (active)
        {
            mainCamera.transform.LookAt(target);
            cameraObj.transform.position = cameraPos.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            target = other.GetComponent<CapsuleCollider>().transform;
            active = true;
            mainCamera.GetComponent<FollowCamera>().enabled = false;
            Debug.Log("I have entered this zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        active = false;
        mainCamera.transform.LookAt(null);
        mainCamera.GetComponent<FollowCamera>().enabled = true;
        Debug.Log("I have exited this zone");
    }
}
