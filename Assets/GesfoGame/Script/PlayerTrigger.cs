using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerTrigger : MonoBehaviour
{
    CameraController cameraController;
    PlayerController playerController;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        playerController = FindObjectOfType<PlayerController>();
    }

    public List<GameObject> CubesList = new List<GameObject>();

    public GameObject Player;
    public GameObject AstronotObject;
    public GameObject CubesObject;

    public Animator AstronotAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube")
        {
            other.tag = "Player";

            other.transform.parent = CubesObject.transform;

            CubesList.Add(other.gameObject);
            other.name = "Karpuz - " + CubesList.Count;

            CubesLister();
        }
        else if(other.tag == "CubePlus")
        {
            for (int i = 0; i <= other.transform.childCount; i++)
            {
                other.transform.GetChild(0).tag = "Player";

                CubesList.Add(other.transform.GetChild(0).gameObject);
                other.transform.GetChild(0).name = "Karpuz - " + CubesList.Count;

                other.transform.GetChild(0).transform.parent = CubesObject.transform;

                if (i == other.transform.childCount - 1)
                {
                    Destroy(other.gameObject);
                }
            }
            CubesLister();
        }
        else if (other.tag == "GroundTurn")
        {
            Player.transform.DORotate(new Vector3(0, 90, 0), 0.5f, RotateMode.Fast);
            Camera.main.GetComponent<CameraController>().SlideCamera(true);

            FindObjectOfType<PlayerController>().lastPositionZ = other.GetComponent<LastPositionController>().NextGround.transform.position.z;
            FindObjectOfType<PlayerController>().lastPositionX = other.GetComponent<LastPositionController>().NextGround.transform.position.x;
            
        }
        else if (other.tag == "Platform")
        {
            CubesLister();
        }
        else if (other.tag == "Fast")
        {
            playerController.speedX += 2;
            playerController.speedZ += 2;

            StartCoroutine(FastNormalize());
        }
    }

    public void CubesLister()
    {
        CameraFixed();

        if (CubesList.Count > 0)
        {
            for (int i = 0; i < CubesList.Count; i++)
            {
                CubesList[i].transform.position = new Vector3(CubesObject.transform.position.x, CubesList.Count - i, CubesObject.transform.position.z);
                AstronotAnimator.SetBool("Jump", true);
                StartCoroutine(AnimationEnd());
            }
        }
    }

    public void CameraFixed()
    {
        if (CubesList.Count < 5)
        {
            cameraController.newTrailDistance = 18.0f;
            cameraController.newHeightOffset = 8.5f;
            cameraController.newCameraDelay = 1.0f;
            cameraController.newHorizontalOffset = 6.0f;
        }
        else if (CubesList.Count < 10)
        {
            cameraController.newTrailDistance = 20.0f;
            cameraController.newHeightOffset = 9.5f;
            cameraController.newCameraDelay = 1.0f;
            cameraController.newHorizontalOffset = 5.5f;
        }
        else if (CubesList.Count < 15)
        {
            cameraController.newTrailDistance = 22.0f;
            cameraController.newHeightOffset = 11f;
            cameraController.newCameraDelay = 1.0f;
            cameraController.newHorizontalOffset = 5.0f;
        }
        else if (CubesList.Count >= 15)
        {
            cameraController.newTrailDistance = 25.0f;
            cameraController.newHeightOffset = 12f;
            cameraController.newCameraDelay = 1.0f;
            cameraController.newHorizontalOffset = 4.5f;
        }
    } 

    IEnumerator AnimationEnd()
    {
        yield return new WaitForSeconds(0.1f);
        AstronotAnimator.SetBool("Jump", false);
    }

    IEnumerator FastNormalize()
    {
        yield return new WaitForSeconds(1.0f);
        playerController.speedX -= 2;
        playerController.speedZ -= 2;
    }

    public void CubeExit(string name)
    {
        for (int i = 0; i < CubesList.Count; i++)
        {
            if (name == CubesList[i].name)
            {
                CubesList.RemoveAt(i);
            }
        }
    }
}   
