using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject DeadScreen;
    public Slider healthBar;

    private void Awake()
    {
        Manager.Instance.UIManager = this;
        Manager.OnLose.AddListener(OnLose);
    }

    private void OnPlayerTakeDamage()
    {
        healthBar.value = Manager.Instance.Player.GetHealth();
    }

    public void AddHealthListener()
    {
        Manager.Instance.Player.onPlayerTakeDamage.AddListener(OnPlayerTakeDamage);
    }

    public void OnLose()
    {
        Time.timeScale = 0f;
        DeadScreen.SetActive(true);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
