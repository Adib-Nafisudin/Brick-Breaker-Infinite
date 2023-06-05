using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    // Panels
    public GameObject MenuPanel;
    public GameObject GamePanel;
    public GameObject WinPanel;

    // Text
    public Text Title;
    public Text ControlDesc;

    // Object Collab
    public GameObject BallScript;
    public GameObject BrickScript;
    public GameObject PaddleScript;

    // Bool
    private static bool Paused;
    private static bool Over = false;
    private static bool ForceNoPause = false;
    // Start is called before the first frame update
    void Start()
    {
        ResumeGame();
        MenuPanel.SetActive(false);
        GamePanel.SetActive(true);
        WinPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (Paused)
        {
            case true:
                    if (Input.GetKeyDown(KeyCode.Escape) && Over != true)
                    {
                        ResumeButtonClicked();
                    }
                break;
            case false:
                if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        PauseButtonClicked();
                    }
                break;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            BrickScript.gameObject.GetComponent<BrickLoader>().Win();
        }
        if (Input.GetKeyDown("r"))
        {
            RestartButtonClicked();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitButtonClicked();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            switch (ForceNoPause)
            {
                case true:
                ForceNoPause = false;
                    break;
                case false:
                ForceNoPause = true;
                    break;
            }
            Debug.Log(ForceNoPause);            
        }
    }
    public void PauseButtonClicked()
    {
        MenuPanel.SetActive(true);
        GamePanel.SetActive(false);
        WinPanel.SetActive(false);
        PauseGame();
    }
    public void ResumeButtonClicked()
    {
        MenuPanel.SetActive(false);
        GamePanel.SetActive(true);
        WinPanel.SetActive(false);
        ResumeGame();
    }
    public void NextLevelButtonClicked()
    {
        ResumeButtonClicked();
        BallScript.gameObject.GetComponent<BallController>().Restart();
        BallScript.gameObject.GetComponent<BallController>().AddSpeed();
        BrickScript.gameObject.GetComponent<BrickLoader>().NextStart();
        PaddleScript.gameObject.GetComponent<PaddleController>().Restart();
    }
    public void RestartButtonClicked()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
        ResumeButtonClicked();
        PaddleScript.gameObject.GetComponent<PaddleController>().Restart();
        Over = false;
    }
    public void QuitButtonClicked()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex - 1);
        ResumeButtonClicked();
    }

    public void PauseGame(){
        if (ForceNoPause == false)
        {
            Time.timeScale = 0;
            Paused = true;   
        }
    }
    public void ResumeGame(){
        Time.timeScale = 1;
        Paused = false;
    }
    public void WinGame(){
        PauseGame();
        GamePanel.SetActive(false);
        WinPanel.SetActive(true);
    }
    public void GameOver(){
        PauseButtonClicked();
        Title.text = "GAME OVER";
        ControlDesc.text = "Press \"Q\" to Quit\nScore: " + BrickScript.gameObject.GetComponent<BrickLoader>().ScoreText.text;
        Over = true;
    }
}
