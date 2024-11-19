using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreTracker : MonoBehaviour
{

      
   // [SerializeField] public TextMeshPro scoretext;

    public float score;
    // Start is called before the first frame update
    void Start()
    {
        score = score + 1;
    }

    // Update is called once per frame
    void Update()
    {
      //  scoretext.text =  score.ToString();
    }
}
