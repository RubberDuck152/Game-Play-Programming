using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnContact : MonoBehaviour
{
    public GameObject obj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(obj);
        }
    }
}
