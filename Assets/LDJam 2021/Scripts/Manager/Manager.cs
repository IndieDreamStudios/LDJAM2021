using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    #region Singleton
    private static Manager instance;

    public static Manager Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public static UnityEvent OnLose = new UnityEvent();
    public static bool Lose = false;

    #region Player
    private Player player;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }
    #endregion

    #region InventoryUIManager
    private InventoryUIManager inventoryUIManager;

    public InventoryUIManager InventoryUIManager
    {
        get { return inventoryUIManager; }
        set { inventoryUIManager = value; }
    }
    #endregion

    #region UIManager
    private UIManager uiManager;

    public UIManager UIManager
    {
        get { return uiManager; }
        set { uiManager = value; }
    }
    #endregion

    #region Global Events
    public static UnityEvent LevelEnd = new UnityEvent();
    #endregion

    public void ResetManager()
    {
        inventoryUIManager = null;
        uiManager = null;
        player = null;
        Lose = false;
    }
}
