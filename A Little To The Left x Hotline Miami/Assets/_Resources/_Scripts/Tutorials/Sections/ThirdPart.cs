using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ThirdPart : MonoBehaviour
{

    public TMP_Text commentTxt;
    public GameObject enemy;
    public string[] commentMessage;

    public GameObject Wall;

    bool alreadydead = false;

    public GameObject firstblood;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
            
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            StartCoroutine(waittoupdate());
        }
        
    }

    public void OnTriggerExit2D(Collider2D col)
    {
       
    }


     private IEnumerator waittoupdate()
    {
        commentTxt.text = "Did you see those cool texts and the score up there??";
        yield return new WaitForSecondsRealtime(7f);
         commentTxt.text = "You’re here to get the BEST SCORE and PERFECT KILLS";
        yield return new WaitForSecondsRealtime(7f);
         commentTxt.text = "PERFECT KILL= direction, distance and kill method are all perfect";
        yield return new WaitForSecondsRealtime(7f);
         commentTxt.text = "Be sure of what weapons you are using and who you are killing";
        yield return new WaitForSecondsRealtime(7f);
          commentTxt.text = "If everything is WRONG you’ll get too little points";
        yield return new WaitForSecondsRealtime(7f);
          commentTxt.text = "Kill that guy in the hall!";
        yield return new WaitForSecondsRealtime(5f);
        commentTxt.text = "";
        Wall.SetActive(false);
        StartCoroutine(wait());

    }

     private IEnumerator wait()
    {
        commentTxt.text = "Notice how enemies can hear gunshots";
        yield return new WaitForSecondsRealtime(7f);
        commentTxt.text = "";
        firstblood.GetComponent<FirstPart>().fakeDoor3.SetActive(false);
        firstblood.GetComponent<FirstPart>().door3.SetActive(true);
        

    }
}
