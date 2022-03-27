using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<PlayerTrigger>().CubeExit(other.name, transform.tag);
            this.GetComponent<BoxCollider>().isTrigger = false;
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.parent = this.transform;
            /*
            if (FindObjectOfType<PlayerController>().turn)
                other.transform.localPosition = new Vector3(-3, 0, 0);*/
        }
    }
}
