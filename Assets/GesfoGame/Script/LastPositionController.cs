using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPositionController : MonoBehaviour
{
    public GameObject NextGround;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bomb")
        {
            other.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
