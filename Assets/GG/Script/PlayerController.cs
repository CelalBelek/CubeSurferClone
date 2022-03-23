using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject cubeStatic;

    public float speedX;
    public float speedZ;

    public bool turn;

    public float lastPositionX;
    public float lastPositionZ;
    public float lastPositionZ2;

    private void Start()
    {
        turn = true;
        lastPositionX = 0;
        lastPositionZ = 0;
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * speedX * Time.deltaTime;
        this.transform.Translate(horizontalMove, 0, speedZ * Time.deltaTime);

        if (turn)
            cubeStatic.transform.position = new Vector3(lastPositionX, 0, cubeStatic.transform.position.z);
        else
        {
            cubeStatic.transform.position = new Vector3(cubeStatic.transform.position.x, 0, lastPositionZ);
        }
    }
}
