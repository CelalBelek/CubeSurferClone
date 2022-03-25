using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerTrigger : MonoBehaviour
{
    CameraController cameraController;
    PlayerController playerController;
    EmeraldManager emeraldManager;

    public List<GameObject> CubesList = new List<GameObject>();

    public GameObject Player;
    public GameObject AstronotObject;
    public GameObject CubesObject;
    public GameObject BombPrefabs;
    public GameObject Particle;

    public Animator AstronotAnimator;

    public float forcePower;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        playerController = FindObjectOfType<PlayerController>();
        emeraldManager = FindObjectOfType<EmeraldManager>();

        CameraFixed();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bomb();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube")
        {
            other.tag = "Player";
            AstronotAnimator.SetBool("Jump", true);

            other.transform.parent = CubesObject.transform;

            CubesList.Add(other.gameObject);
            other.name = "Karpuz - " + CubesList.Count;

            CubesLister();
        }
        else if(other.tag == "CubePlus")
        {
            AstronotAnimator.SetBool("Jump", true);
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

            FindObjectOfType<PlayerController>().lastPositionZ = other.GetComponent<LastPositionController>().NextGround.transform.position.z + 1;
            FindObjectOfType<PlayerController>().lastPositionX = other.GetComponent<LastPositionController>().NextGround.transform.position.x;

            Camera.main.GetComponent<CameraController>().SlideCamera(true);
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
        else if (other.tag == "Emerald")
        {
            emeraldManager.AddEmearlds(other.transform.position, 1);
            emeraldManager.emeraldCount++;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Jump")
        {
            Destroy(other);
            JumpEffect();
        }
    }

    public void CubesLister()
    {
        CameraFixed();
        /*
        if (!Particle.activeSelf)
        {
            Particle.SetActive(true);
        }
        else if (Particle.activeSelf)
        {
            Particle.SetActive(false);
        }*/

        if (CubesList.Count > 0)
        {
            for (int i = 0; i < CubesList.Count; i++)
            {
                CubesList[i].transform.position = new Vector3(CubesObject.transform.position.x, CubesList.Count - i, CubesObject.transform.position.z);
                StartCoroutine(AnimationEnd());
            }
        }
    }

    public void CameraFixed()
    {
        if (CubesList.Count < 5)
        {
            cameraController.newTrailDistance = 12.0f;
            cameraController.newHeightOffset = 6.0f;
            cameraController.newCameraDelay = 1.0f;
            cameraController.newHorizontalOffset = 4.0f;
            playerController.newlastPositionY = 0.0f;
        }
        else if (CubesList.Count < 10)
        {
            cameraController.newTrailDistance = 15.0f;
            cameraController.newHeightOffset = 7.5f;
            cameraController.newCameraDelay = 1.0f;
            cameraController.newHorizontalOffset = 4.0f;
            playerController.newlastPositionY = 2.0f;
        }
        else if (CubesList.Count < 15)
        {
            cameraController.newTrailDistance = 17.0f;
            cameraController.newHeightOffset = 8f;
            cameraController.newCameraDelay = 1.0f;
            cameraController.newHorizontalOffset = 4.0f;
            playerController.newlastPositionY = 5.0f;
        }
        else if (CubesList.Count >= 15)
        {
            cameraController.newTrailDistance = 20.0f;
            cameraController.newHeightOffset = 10f;
            cameraController.newCameraDelay = 1.0f;
            cameraController.newHorizontalOffset = 4.5f;
            playerController.newlastPositionY = 9.0f;
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
        CameraFixed();
    }

    public void JumpEffect()
    {
        CameraFixed();

        if (CubesList.Count > 0)
        {
            for (int i = 0; i < CubesList.Count; i++)
            {
                CubesList[i].GetComponent<Rigidbody>().AddForce(new Vector3(CubesList[i].transform.position.y, forcePower));
            }
        }

        transform.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(JumpEnd());
    }

    IEnumerator JumpEnd()
    {
        yield return new WaitForSeconds(1.5f);
        transform.GetComponent<BoxCollider>().enabled = true;
    }

    public void Bomb()
    {
        GameObject bomb = Instantiate(BombPrefabs, AstronotObject.transform.position, Quaternion.identity);
        
        if (playerController.turn)
            bomb.GetComponent<Rigidbody>().AddForce(bomb.transform.forward * 3000);
        else
            bomb.GetComponent<Rigidbody>().AddForce(bomb.transform.right * 3000);
       
    }
}   
