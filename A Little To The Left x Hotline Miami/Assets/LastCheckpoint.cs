using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LeaveTutorial());
    }

    private IEnumerator LeaveTutorial()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Level 1");
    }
}
