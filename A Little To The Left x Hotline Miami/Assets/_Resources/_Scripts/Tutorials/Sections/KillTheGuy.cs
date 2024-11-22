using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillTheGuy : MonoBehaviour
{
    public TMP_Text txt;
    public int index = 11;

    public GameObject box;

    public GameObject prompt;

    public GameObject lastEnemy;
    private EnemyController enemyController;

    void Start()
    {
        enemyController = lastEnemy.GetComponent<EnemyController>();
        txt.text = "Take note of the different enemies";
    }
    void Update()
    {
        if (index == 12)
        {
            box.SetActive(true);
            txt.text = "You can pick up ORANGE boxes. Go ahead and block that enemy’s path.";
        }

        if (index == 13)
        {
            prompt.SetActive(true);
            box.SetActive(false);
        } 

        if (enemyController.hasInteractedWithBox)
        {
            prompt.SetActive(false );
            box.SetActive(true);
            txt.text = "You’ll figure out the rest! GOODLUCK!";
            StartCoroutine (LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("Level 1");
    }

    public void Next()
    {
        index++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            print("yeah");
        }
    }
}
