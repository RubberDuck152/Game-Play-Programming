using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool active = false;
    public bool canPress = false;
    public GameObject button;
    public GameObject door;
    public Material red;
    public Material green;

    public Vector3 goalPos;

    public float moveSpeed = 3.0f;
    public Vector3 position1;
    public Vector3 position2;

    private void Update()
    {
        if (canPress)
        {
            if (Input.GetButtonDown("Main Attack"))
            {
                Debug.Log("Active!");
                active = !active;
            }
        }

        if (active)
        {
            button.GetComponent<MeshRenderer>().material = green;
            MoveDoor(position1);
        }
        else if (!active)
        {
            button.GetComponent<MeshRenderer>().material = red;
            MoveDoor(position2);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Contact!");
            canPress = !canPress;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canPress = false;
    }

    void MoveDoor(Vector3 goalPos)
    {
        float dist = Vector3.Distance(door.transform.position, goalPos);

        if (dist > 0.1f)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, goalPos, moveSpeed * Time.deltaTime);
        }
    }
}
