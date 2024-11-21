using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class GameManager : MonoBehaviour
{
    internal static bool gameOver;
    [SerializeField] private GameObject gameOverTxt;    

    public TMP_Text SCORE;


    public int scorevalue;

    public GameObject GETMESCALE; // Reference to the prefab with a Text component
    public Canvas canvas; 

    public GameObject SCALE;


    private void Start()
    {
        scorevalue = 0;
        
        gameOver = false;
        gameOverTxt.SetActive(false);
        StartCoroutine(START());
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


   // public IEnumerator START()
   // {
        

        // Position it in the top-left corner
        
        
     //   GameObject textComponent = GETMESCALE;
       // textComponent.GetComponent<Text>().enabled = true;
       
      //  textComponent.GetComponent<Text>().text = "!!!ART DEMANDS PARTS!!!";
        

        // Wait for 5 seconds
        // yield return new WaitForSeconds(1f);

      //  textComponent.GetComponent<Text>().enabled = false;
        // Destroy the instantiated object
  //  }

  public IEnumerator START()
{
    // Instantiate the text object in the Canvas
    //GameObject instance = Instantiate(SCALE, canvas.transform);

    // Position it near the top-left corner
   // RectTransform rectTransform = instance.GetComponent<RectTransform>();
   

    // Update the text
    TextMeshProUGUI textComponent = SCALE.GetComponent<TextMeshProUGUI>();
    if (textComponent != null)
    {
        textComponent.text = "!!!ART DEMANDS PARTS!!!";
    }

    // Wait for 5 seconds
    yield return new WaitForSeconds(1f);

    textComponent.text = "...";
    

    // Destroy the instantiated object
   // Destroy(instance);
}


}
