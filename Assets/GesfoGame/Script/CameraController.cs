using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController playerController;

    public Transform target;

    public float trailDistance = 18.0f;
    public float heightOffset = 8.5f;
    public float cameraDelay = 1.0f;
    public float horizontalOffset = 6.0f;

    public float newTrailDistance;
    public float newHeightOffset;
    public float newCameraDelay;
    public float newHorizontalOffset;

    private Vector3 followPosition;
    public bool turnBool;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        followPosition.x += horizontalOffset;
    }

    private void LateUpdate()
    {
        if (trailDistance < newTrailDistance)
        {
            trailDistance += Time.deltaTime;
        }
        else if (trailDistance > newTrailDistance)
        {
            trailDistance -= Time.deltaTime;
        }

        if (heightOffset < newHeightOffset)
        {
            heightOffset += Time.deltaTime;
        }
        else if (trailDistance > newHeightOffset)
        {
            heightOffset -= Time.deltaTime;
        }

        if (horizontalOffset < newHorizontalOffset)
        {
            horizontalOffset += Time.deltaTime;
        }
        else if (trailDistance > newHorizontalOffset)
        {
            horizontalOffset -= Time.deltaTime;
        }

        followPosition = target.position - target.forward * trailDistance;

        if (turnBool)
            followPosition.z += -horizontalOffset;
        else
            followPosition.x += horizontalOffset;

        followPosition.y += heightOffset;
        transform.position += (followPosition - transform.position) * cameraDelay;

        transform.LookAt(target.transform);
    }

    public void SlideCamera(bool left)
    {
        turnBool = left;

        playerController.turn = false;

        FindObjectOfType<PlayerTrigger>().GetComponent< PlayerTrigger>().CameraFixed();
    }
}
