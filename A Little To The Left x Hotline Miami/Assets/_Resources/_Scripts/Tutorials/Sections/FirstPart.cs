using System.Collections;
using TMPro;
using UnityEngine;

public class FirstPart : MonoBehaviour
{
    public TMP_Text commentTxt;
    public GameObject commentBox;
    public string[] commentMessage;

    public GameObject [] tasks;

    public int index = 0;
    public int commentIndex = 0;

    PlayerInteraction playerInteraction;

    public GameObject door1;
    public GameObject door2;
    public GameObject door3;

    public GameObject fakeDoor1;
    public GameObject fakeDoor2;
    public GameObject fakeDoor3;

    public GameObject nxtBtn;
    public GameObject weapon1;

    public GameObject Wall;
    void Start()
    {
    
        playerInteraction = FindObjectOfType<PlayerInteraction>();

        fakeDoor1.SetActive(true);
        fakeDoor2.SetActive(true);
        fakeDoor3.SetActive(true);

        door1.SetActive(false);
        door2.SetActive(false);
       door3.SetActive(false);

        nxtBtn.SetActive(true);

        StartCoroutine(waittoupdate());
    }

    void Update()
    {
       // CompleteTasks();
        
    }

    public void NextButton()
    {
        index += 1;
    }

   // private void CompleteTasks()
 //   {

          //  if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
          //  {
         //       Debug.Log("imherenot");
          //      tasks[0].SetActive(false);
          //  }
         ///   if (Input.GetMouseButtonDown(1))
         //   {
         //       if (playerInteraction.equippedWeapon == weapon1.transform)
         //       {
          //          Debug.Log("imherenot");
          //          tasks[1].SetActive(false);
          //      }

         //   }
         //   if (playerInteraction.hasWeapon && Input.GetMouseButtonDown(0))
          //  {
             //   Debug.Log("imherenot");
           //     tasks[2].SetActive(false);
          //  }
//
          //  if (Input.GetKeyDown(KeyCode.G)) //&& !)
        //    {
         //       Debug.Log("imhere");
//                tasks[3].SetActive(false);
          //  }
       //     if (Input.GetKey(KeyCode.LeftShift))
         //   {
           //     Debug.Log("imherenot");
//                tasks[4].SetActive(false);
        //    }
        
      //  for(int i = 0; i < 5; i++)
     //   {
       ///     if (tasks[i].activeSelf == true)
     //       {
         //       Debug.Log("why");
       //         return;
         //   }
           // else if (tasks[i].activeSelf == false)
           // {
            //    Debug.Log("why not");
             //   fakeDoor1.SetActive(false);
              //  door1.SetActive(true);
           // }
       // }


    //}

     private IEnumerator waittoupdate()
    {
        commentTxt.text = commentMessage[0];
        yield return new WaitForSecondsRealtime(7f);
         commentTxt.text = commentMessage[1];
        yield return new WaitForSecondsRealtime(7f);
         commentTxt.text = commentMessage[2];
        yield return new WaitForSecondsRealtime(7f);
          commentTxt.text = commentMessage[3];
        yield return new WaitForSecondsRealtime(7f);
           commentTxt.text = commentMessage[4];
        yield return new WaitForSecondsRealtime(7f);
          commentTxt.text = commentMessage[5];
        yield return new WaitForSecondsRealtime(5f);
        commentTxt.text = "";
        StartCoroutine(wait());
    }

     private IEnumerator wait()
    {
       
        yield return new WaitForSecondsRealtime(1f);
        
        this.gameObject.GetComponent<FirstPart>().fakeDoor1.SetActive(false);
        this.gameObject.GetComponent<FirstPart>().door1.SetActive(true);
        

    }


   
}
