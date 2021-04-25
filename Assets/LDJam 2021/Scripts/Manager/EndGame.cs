using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public static EndGame Instance { get; private set; }
    [SerializeField] private GameObject EndScreen;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Manager.OnWin.AddListener(EndThis);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    void EndThis()
    {
        EndScreen.SetActive(true);
    }

}
