using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneman : MonoBehaviour
{
   public void restart()
   {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
   }

   public void quit()
   {
         Application.Quit();
   }
}
