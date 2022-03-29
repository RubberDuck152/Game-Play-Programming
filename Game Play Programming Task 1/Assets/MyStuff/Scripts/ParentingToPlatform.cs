using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentingToPlatform : MonoBehaviour
{
    public GameObject PlayerObject;
    public CharacterController Controller;
    public GameObject platform;
    public Camera playerCam;
    public Camera splineCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Platform")
        {
            Debug.Log("I have entered the platform");
            PlayerObject.transform.SetParent(platform.transform, true);
            //Controller.enabled = false;
            playerCam.enabled = false;
            splineCam.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("I have left the platform");
        PlayerObject.transform.SetParent(null);
        //Controller.enabled = true;
        playerCam.enabled = true;
        splineCam.enabled = false;
    }
}
