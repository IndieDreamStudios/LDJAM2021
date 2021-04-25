using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float Duration = 2f;

    private void Start()
    {
        Manager.LevelEnd.AddListener(HandleLevelEnd);
    }

    void HandleLevelEnd()
    {
        image.CrossFadeAlpha(0.5f, Duration, false);
        
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(Duration);

        image.gameObject.SetActive(true);
    }
}
