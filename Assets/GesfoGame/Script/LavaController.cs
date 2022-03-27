using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<PlayerTrigger>().CubeExit(other.name, transform.tag);
            other.transform.parent = null;
            Destroy(other.gameObject);
        }
    }
}
