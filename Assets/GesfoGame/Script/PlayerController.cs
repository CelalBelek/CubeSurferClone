using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public GameObject cubeStatic;

    public float speedX;
    public float speedZ;

    public bool turn;

    public float lastPositionX;
    public float lastPositionY;
    public float lastPositionZ;

    public float newlastPositionY;

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

        DOTween.To(x => lastPositionY = x, lastPositionY, newlastPositionY, 0.5f);

        if (turn)
            cubeStatic.transform.position = new Vector3(lastPositionX, lastPositionY, cubeStatic.transform.position.z);
        else
        {
            cubeStatic.transform.position = new Vector3(cubeStatic.transform.position.x, lastPositionY, lastPositionZ);
        }
    }
}
