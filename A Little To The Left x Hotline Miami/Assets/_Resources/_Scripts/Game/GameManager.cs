using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    internal static bool gameOver;
    [SerializeField] private GameObject gameOverTxt;    

    public TMP_Text SCORE;


    public int scorevalue;

    private void Start()
    {
        scorevalue = 0;
        
        gameOver = false;
        gameOverTxt.SetActive(false);
    }

    private void Update()
    {
        SCORE.text = "SCORE:"+scorevalue.ToString();
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }

        if (gameOver)
        {
            gameOverTxt.SetActive(true);
        }
        else
        {
            gameOverTxt.SetActive(false);
        }
    }
    private void ReloadScene()
    {
        if (gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
