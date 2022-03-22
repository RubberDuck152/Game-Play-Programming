using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentingToPlatform : MonoBehaviour
{
    public Transform PlayerObject;
    public CharacterController controller;
    public CharacterMovement movement;
    public Camera playerCam;
    public Camera splineCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerObject.parent = transform;
            playerCam.enabled = false;
            splineCam.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerObject.transform.parent = null;
        playerCam.enabled = true;
        splineCam.enabled = false;
    }
}
