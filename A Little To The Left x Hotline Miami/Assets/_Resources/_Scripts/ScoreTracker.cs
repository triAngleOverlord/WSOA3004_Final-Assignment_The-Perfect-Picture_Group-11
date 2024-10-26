using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreTracker : MonoBehaviour
{

      
    [SerializeField] public Text scoretext;

   [SerializeField] public float score;
    // Start is called before the first frame update
    void Start()
    {
       score = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        //scoretext.text =  score.ToString();
    }
}
