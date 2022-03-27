using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public GameObject cubeStatic;

    public float speedX;
    public float speedZ;
    
    public float MobilespeedX;

    public bool turn;

    public float lastPositionX;
    public float lastPositionY;
    public float lastPositionZ;

    public float newlastPositionY;

    Touch touch;

    private void Start()
    {
        turn = true;
        lastPositionX = 0;
        lastPositionZ = 0;
    }

    void Update()
    {
        if (FindObjectOfType<GameManager>().GameBool == false)
            return;

        // Mouse
        float horizontalMove = Input.GetAxis("Horizontal") * speedX * Time.deltaTime;

        // Touch
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                if (Input.GetTouch(0).deltaPosition.x > 3)
                {
                    horizontalMove = 1 * MobilespeedX * Time.deltaTime;
                }
                else if (Input.GetTouch(0).deltaPosition.x < -3)
                {
                    horizontalMove = -1 * MobilespeedX * Time.deltaTime;
                }
            }
        }

        this.transform.Translate(horizontalMove, 0, speedZ * Time.deltaTime);

        if (turn)
            cubeStatic.transform.position = new Vector3(lastPositionX, lastPositionY, cubeStatic.transform.position.z);
        else
        {
            cubeStatic.transform.position = new Vector3(cubeStatic.transform.position.x, lastPositionY, lastPositionZ);
        }
    }
}
