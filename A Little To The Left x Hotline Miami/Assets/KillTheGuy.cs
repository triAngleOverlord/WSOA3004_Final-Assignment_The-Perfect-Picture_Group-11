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
    public GameObject otherTextBox;

    void Start()
    {
        enemyController = lastEnemy.GetComponent<EnemyController>();
        txt.text = "Take note of the different enemies";
        box.SetActive(false);
    }
    void Update()
    {
        if (index == 14)
        {
            prompt.SetActive(true);
            box.SetActive(false);
        } 

        if (enemyController.hasInteractedWithBox)
        {
            prompt.SetActive(false );
            box.SetActive(true);
            txt.text = "You’ll figure out the rest! GOODLUCK!";
            //StartCoroutine (LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("Level 1");
    }

    private IEnumerator HitNext()
    {
        yield return new WaitForSeconds(5);
        txt.text = "You can pick up ORANGE boxes with I. Go ahead and block that enemy’s path.";
        Next();

        yield return new WaitForSeconds(5);
        Next();
    }

    public void Next()
    {
        index++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (index == 12)
            {
                box.SetActive(true);
                otherTextBox.SetActive(false);
                txt.text = "Take note of the different enemies";
                StartCoroutine(HitNext());
            }
        }
    }
}