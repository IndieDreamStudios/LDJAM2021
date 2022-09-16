using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    public GameObject showImage;
    private Image myImage;
    public Sprite[] images;
    int atualImage = 1;

    private void Start()
    {
        myImage = GetComponent<Image>();
        StartCoroutine(NextImage(atualImage));
    }

    private IEnumerator NextImage(int number)
    {

        yield return new WaitForSeconds(2f);
        //showImage.GetComponent<Image>().CrossFadeAlpha(Mathf.Clamp(100f, 0f, 255f), 1f, false);

        showImage.GetComponent<Image>().CrossFadeAlpha(0, 3f, false);

        yield return new WaitForSeconds(3f);
        if (atualImage == 4)
        {
            StartCoroutine(StartGame());
        }

        if ( atualImage < images.Length)
        {
            showImage.GetComponent<Image>().sprite = images[number];
            atualImage++;
            showImage.GetComponent<Image>().CrossFadeAlpha(1, 0.5f, false);
            StartCoroutine(NextImage(atualImage));
        }

    }

    private IEnumerator StartGame()
    {
        showImage.GetComponent<Image>().CrossFadeAlpha(0, 1f, false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game_1", LoadSceneMode.Single);

    }
}
