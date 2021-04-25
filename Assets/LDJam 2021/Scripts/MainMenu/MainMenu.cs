using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject OptionPanel;
    public GameObject CreditsPanel;

    private void Start()
    {
        MainMenuPanel.SetActive(true);
        OptionPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        Manager.Instance.ResetManager();
        SceneManager.LoadScene("MainCutScene", LoadSceneMode.Single);
    }

    public void OpenOptions()
    {
        OptionPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        OptionPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        CreditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        CreditsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
