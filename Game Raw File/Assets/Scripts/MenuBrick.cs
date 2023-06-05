using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBrick : MonoBehaviour
{
    public GameObject HowToPanel;
    public GameObject Credit;
    private bool ActivePanel = false;

    public void ToGameplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ToHowToPanel()
    {
        HowToPanel.SetActive(true);
        ActivePanel = true;
    }
    public void ToCredit()
    {
        Credit.SetActive(true);
        ActivePanel = true;
    }
    private void OnMouseDown()
    {
        if (ActivePanel == false)
        {
            switch (gameObject.name)
            {
                case "StartBrick":
                    ToGameplay();
                    break;
                case "HowToPlay":
                    ToHowToPanel();
                    break;
                case "CreditBrick":
                    ToCredit();
                    break;
                default:
                    break;
            }
        }
    }
    public void MainMenu()
    {
        HowToPanel.SetActive(false);
        Credit.SetActive(false);
        ActivePanel = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        MainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
