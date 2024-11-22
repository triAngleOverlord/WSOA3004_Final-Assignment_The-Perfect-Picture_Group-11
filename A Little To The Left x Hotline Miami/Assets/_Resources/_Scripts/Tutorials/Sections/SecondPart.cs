using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SecondPart : MonoBehaviour
{
   

    

    public TMP_Text commentTxt;
    public GameObject commentBox;
    public string[] commentMessage;

    public GameObject firstblood;

    

    

    
    public GameObject enemy;
   

    public GameObject Wall;

    bool alreadydead = false;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            StartCoroutine(waittoupdate());
        }
        
    }

    


     private IEnumerator waittoupdate()
    {
        commentTxt.text = commentMessage[0];
        yield return new WaitForSecondsRealtime(10f);
         commentTxt.text = commentMessage[1];
        yield return new WaitForSecondsRealtime(10f);         
        commentTxt.text = "";
        StartCoroutine(wait());
        

    }

     private IEnumerator wait()
    {
       
        yield return new WaitForSecondsRealtime(1f);
        commentTxt.text = "";
        firstblood.GetComponent<FirstPart>().fakeDoor2.SetActive(false);
        firstblood.GetComponent<FirstPart>().door2.SetActive(true);
        

    }

   

}
