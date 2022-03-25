using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(4.0f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
