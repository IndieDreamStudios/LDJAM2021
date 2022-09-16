using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelStart : MonoBehaviour
{
    [SerializeField] private string title;
    [SerializeField] private string subtitle;
    [SerializeField] private Image image;

    [SerializeField] private TMP_Text titleObject;
    [SerializeField] private TMP_Text subTitleObject;

    [SerializeField] private float Duration = 2f;

    private void Awake()
    {
        titleObject.text = title;
        subTitleObject.text = subtitle;

        image.CrossFadeAlpha(0f, Duration, false);
        titleObject.CrossFadeAlpha(0f, Duration, false);
        subTitleObject.CrossFadeAlpha(0f, Duration, false);
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(Duration);

        image.gameObject.SetActive(false);
    }
}
