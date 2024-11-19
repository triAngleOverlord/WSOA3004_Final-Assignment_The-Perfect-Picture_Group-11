using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    internal static bool gameOver;
    [SerializeField] private GameObject gameOverTxt;    

    private void Start()
    {
        gameOver = false;
        gameOverTxt.SetActive(false);
    }

    private void Update()
    {
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
