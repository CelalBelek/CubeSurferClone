using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        followPosition = target.position - target.forward * trailDistance;

        if (turnBool)
            followPosition.z += -horizontalOffset;
        else
            followPosition.x += horizontalOffset;

        followPosition.y += heightOffset;

        Camera.main.transform.DOMove(followPosition, 1);
        Camera.main.transform.DOLookAt(target.position, 0.1f);

        DOTween.To(x => trailDistance = x, trailDistance, newTrailDistance, 0.5f);
        DOTween.To(x => heightOffset = x, heightOffset, newHeightOffset, 0.5f);
        DOTween.To(x => horizontalOffset = x, horizontalOffset, newHorizontalOffset, 0.5f);
    }

    public void SlideCamera(bool left)
    {
        turnBool = left;

        playerController.turn = false;
    }
}
