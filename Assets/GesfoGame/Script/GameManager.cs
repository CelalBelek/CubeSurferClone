using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject TapToPlayPanel;
    public GameObject GameWinPanel;
    public GameObject GameEndPanel;

    public bool GameBool;
    public bool GameEndBool;

    void Start()
    {
        GameBool = false;
        GameEndBool = false;
        TapToPlayPanel.SetActive(true);
        GameWinPanel.SetActive(false);
        GameEndPanel.SetActive(false);
    }

    void Update()
    {
        if(GameEndBool == false && GameBool == false)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    TapToPlayPanel.SetActive(false);
                    GameBool = true;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                TapToPlayPanel.SetActive(false);
                GameBool = true;
            }
        }
    }

    public void GameOver()
    {
        GameBool = false;
        GameEndBool = true;
        GameEndPanel.SetActive(true);
    }

    public void GameWin()
    {
        GameBool = false;
        GameEndBool = true;
        GameWinPanel.SetActive(true);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene("Main");
    }
}
