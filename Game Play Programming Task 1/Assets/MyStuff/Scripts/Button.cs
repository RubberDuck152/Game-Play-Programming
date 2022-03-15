using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Button : MonoBehaviour
{
    public bool active = false;
    public bool canPress = false;
    public bool canAction = true;

    public GameObject button;
    public GameObject door;
    public GameObject player;

    public Material red;
    public Material green;

    public float moveSpeed = 3.0f;
    public float buttonMoveSpeed = 500.0f;

    public Vector3 position1;
    public Vector3 position2;
    public Vector3 buttonPos1;
    public Vector3 buttonPos2;

    public Camera buttonCamera;
    public Camera playerCamera;

    public Vector3 cameraStart;
    public Vector3 cameraFinish;

    private void Update()
    {
        cameraStart = playerCamera.transform.position;
        if (canPress)
        {
            if (Input.GetButtonDown("Main Attack"))
            {
                if (canAction)
                {
                    CameraMovementAsync();
                    MoveButton(buttonPos2, buttonPos1);
                    active = !active;
                    canAction = false;
                }
            }
        }

        if (active)
        {
            button.GetComponent<MeshRenderer>().material = green;
            MoveDoorAsync(position1);
        }
        else if (!active)
        {
            button.GetComponent<MeshRenderer>().material = red;
            MoveDoorAsync(position2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canPress = !canPress;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canPress = false;
    }

    async Task MoveDoorAsync(Vector3 goalPos)
    {
        await Task.Delay(750);
        float dist = Vector3.Distance(door.transform.position, goalPos);

        if (dist > 0.0001f)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, goalPos, moveSpeed * Time.deltaTime);
        }
    }

    async Task MoveButton(Vector3 goalPos, Vector3 startPos)
    {
        await Task.Delay(250);
        float dist = Vector3.Distance(button.transform.position, goalPos);

        if (dist > 0.0001f)
        {
            button.transform.position = Vector3.Lerp(button.transform.position, goalPos, buttonMoveSpeed * Time.deltaTime);
        }

        await Task.Delay(250);

        float newDist = Vector3.Distance(button.transform.position, startPos);

        if (newDist > 0.0001f)
        {
            button.transform.position = Vector3.Lerp(button.transform.position, startPos, buttonMoveSpeed * Time.deltaTime);
        }
    }

    async Task CameraMovementAsync()
    {
        player.GetComponent<CharacterMovement>().enabled = false;
        playerCamera.enabled = false;
        buttonCamera.enabled = true;
        Debug.Log("Start");
        await Task.Delay(3000);

        Debug.Log("I have delayed for 4 seconds");
        player.GetComponent<CharacterMovement>().enabled = true;
        playerCamera.enabled = true;
        buttonCamera.enabled = false;
        canAction = true;
    }
}
