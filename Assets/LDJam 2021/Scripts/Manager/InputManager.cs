using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    #region Singleton
    private static InputManager instance;

    public static InputManager Instance { get { return instance; } }


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
    }
    #endregion

    #region Events
    public  UnityEvent onUPPressed;
    public  UnityEvent onDownPressed;
    public  UnityEvent onLeftPressed;
    public  UnityEvent onRightPressed;
    public  UnityEvent onMouseLeftButtonPressed;
    public  UnityEvent onN1Pressed;
    public  UnityEvent onN2Pressed;
    public  UnityEvent onN3Pressed;
    #endregion

    private void Update()
    {
        #region Up
        if ( Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            onUPPressed?.Invoke();
        }
        #endregion

        #region Down
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            onDownPressed?.Invoke();
        }
        #endregion

        #region Left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            onLeftPressed?.Invoke();
        }
        #endregion

        #region Right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            onRightPressed?.Invoke();
        }
        #endregion

        #region Mouse Button Left
        if (Input.GetMouseButtonDown(0))
        {
            onMouseLeftButtonPressed?.Invoke();
        }
        #endregion

        #region Number 1 
        if (Input.GetKeyDown(KeyCode.Alpha1) )
        {
            onN1Pressed?.Invoke();
        }
        #endregion

        #region Number 2 
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            onN2Pressed?.Invoke();
        }
        #endregion

        #region Number 3 
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            onN3Pressed?.Invoke();
        }
        #endregion
    }
}
