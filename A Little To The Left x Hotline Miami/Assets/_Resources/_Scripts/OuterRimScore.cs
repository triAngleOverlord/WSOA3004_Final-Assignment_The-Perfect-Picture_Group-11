using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterRimScore : MonoBehaviour
{
    public float delay = 10f;
    private float elapsed = 0f;
    ScoreTracker scoretally;
    private GameObject scoretallyobj;
    [SerializeField] public string Furniturewanted;
    private bool alreadychecked = false;
    private bool alreadycheckedfurn = false;

    // Start is called before the first frame update
    void Start()
    {
        scoretallyobj = GameObject.FindGameObjectWithTag("scoring");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            EnemyController ec = other.gameObject.GetComponent<EnemyController>();
            EnemyAttacked ea = other.gameObject.GetComponent<EnemyAttacked>();
        
        }
        
            
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Furniture" && other.GetComponent<FurnObject>().Furniture == Furniturewanted && alreadycheckedfurn == false ) //NEED TO UPDATE TO TAKE MULTIPLE OF THE SAME INTO ACCOUNT
        {
            scoretallyobj.GetComponent<ScoreTracker>().score += 1;
            alreadycheckedfurn = true;
        }
        else
        {
            alreadycheckedfurn = false;
        }
    
       
        if (other.tag == "Enemy" && other.GetComponentInChildren<ScoreDirectionSystem>().stateofenemy == "DeadbyMeleeWep" && alreadychecked == false )
        {
            scoretallyobj.GetComponent<ScoreTracker>().score += 1;
            alreadychecked = true;
        }
        else if (other.tag == "Enemy" && other.GetComponentInChildren<ScoreDirectionSystem>().stateofenemy == "DeadbyMelSwip" && alreadychecked == false )
        {
            scoretallyobj.GetComponent<ScoreTracker>().score += 2;
            alreadychecked = true;
        }
        else if (other.tag == "Enemy" && other.GetComponentInChildren<ScoreDirectionSystem>().stateofenemy == "DeadbyBull" && alreadychecked == false )
        {
            scoretallyobj.GetComponent<ScoreTracker>().score += 3;
            alreadychecked = true;
        }
        
        
        
       
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Furniture"  && other.GetComponent<FurnObject>().Furniture == Furniturewanted && alreadycheckedfurn == true )
        {
            scoretallyobj.GetComponent<ScoreTracker>().score += 1;
            alreadycheckedfurn = false;
        }
        
    }

    IEnumerator Delay()
    {
        
        yield return new WaitForSeconds(5);


        
    }

    private void Furnituretobedes(string furn)
    {

    }
}
