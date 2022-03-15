using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAcquired : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;
    public Transform target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mainCamera.transform.parent = player.transform;
            mainCamera.transform.LookAt(target);
            (player.GetComponent("FollowCamera") as MonoBehaviour).enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        mainCamera.transform.parent = null;
        mainCamera.transform.LookAt(null);
        (player.GetComponent("FollowCamera") as MonoBehaviour).enabled = true;
    }
}
