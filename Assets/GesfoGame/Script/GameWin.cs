using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<PlayerTrigger>().CubeExit(other.name, transform.tag);
            this.GetComponent<BoxCollider>().isTrigger = false;
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.parent = this.transform;
        }
    }
}
