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

    public GameObject button;
    public GameObject door;

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
                CameraMovementAsync();
                active = !active;
            }
        }

        if (active)
        {
            MoveButton(buttonPos1);
            button.GetComponent<MeshRenderer>().material = green;
            MoveDoorAsync(position1);
        }
        else if (!active)
        {
            MoveButton(buttonPos2);
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
        await Task.Delay(100);
        float dist = Vector3.Distance(door.transform.position, goalPos);

        if (dist > 0.0001f)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, goalPos, moveSpeed * Time.deltaTime);
        }
    }

    void MoveButton(Vector3 goalPos)
    {
        float dist = Vector3.Distance(button.transform.position, goalPos);

        if (dist > 0.0001f)
        {
            button.transform.position = Vector3.Lerp(button.transform.position, goalPos, buttonMoveSpeed * Time.deltaTime);
        }
    }

    async Task CameraMovementAsync()
    {
        playerCamera.enabled = false;
        buttonCamera.enabled = true;
        Debug.Log("Start");
        await Task.Delay(3000);

        Debug.Log("I have delayed for 4 seconds");
        playerCamera.enabled = true;
        buttonCamera.enabled = false;
    }
}
