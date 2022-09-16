using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleObject : MonoBehaviour
{
    [SerializeField] private string nextLevelName;

    private bool playerToOtherLevel = false;
    float time = 0f;

    Vector2 scale;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.CompareTag("Player"))
        {
            playerToOtherLevel = true;
            Manager.LevelEnd?.Invoke();
            StartCoroutine(goToNextLevel());
        }
    }

    private void FixedUpdate()
    {
        if (!playerToOtherLevel) return;
        time += Time.deltaTime;

        
        if (time < 2f)
        {
            scale = Manager.Instance.Player.transform.localScale;
            var fromAngle = Manager.Instance.Player.transform.rotation;
            var toAngle = Quaternion.Euler(Manager.Instance.Player.transform.eulerAngles + Vector3.forward * 90f);
            Manager.Instance.Player.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, Time.deltaTime);
            Manager.Instance.Player.transform.localScale = new Vector2(scale.x - 0.01f, scale.y - 0.01f);
        }
        else
        {
            playerToOtherLevel = false;
        }
    }


    IEnumerator goToNextLevel()
    {
        // Teleport To Center of object
        Manager.Instance.Player.transform.position = transform.position;

        yield return new WaitForSeconds(2f);
        // Change Level
        SceneManager.LoadScene(nextLevelName, LoadSceneMode.Single);

    }
}
