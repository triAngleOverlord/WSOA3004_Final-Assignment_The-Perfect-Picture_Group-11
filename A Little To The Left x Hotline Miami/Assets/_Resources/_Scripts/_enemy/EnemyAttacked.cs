using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttacked : MonoBehaviour
{
    public Sprite knockedDown,stabbed,bulletWound,backUp; //temporary, may be expanded
    public GameObject bloodPool,bloodSpurt;
   [SerializeField] SpriteRenderer sr;

   [SerializeField] List<GameObject> DirectionGrid;
    bool EnemyKnockedDown=false;
    float knockDownTimer = 4.0f;
    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyKnockedDown==true)
        {
            knockDown();
        }
    }

    public void knockDownEnemy()
    {
        EnemyKnockedDown = true;
    }

    void knockDown()
    {
        knockDownTimer -= Time.deltaTime;
        sr.sprite = knockedDown;
        this.GetComponent<CircleCollider2D>().enabled=false;
        sr.sortingOrder = 2;

        if(knockDownTimer <= 0) 
        {
            EnemyKnockedDown = false;
            sr.sprite = backUp;
            this.GetComponent<CircleCollider2D>().enabled=true;
            sr.sortingOrder = 5;
            knockDownTimer = 4.0f;
        }

        //disable ai
    }

    public void killBullet(string name)
    {
        if(name == "South")
        {
            sr.sprite = bulletWound; //back shot
        }
        else if (name == "SouthEast")
        {
             sr.sprite = bulletWound; //back shot
            RotateSE();
        }
        else if (name == "SouthWest")
        {
             sr.sprite = bulletWound; //back shot
            RotateSW();
        }
        else if(name == "North")
        {
            sr.sprite = bulletWound; //front shot
        }
        else if(name == "NorthEast")
        {
            sr.sprite = bulletWound; //front shot
            RotateNE();
        }
        else if(name == "NorthWest")
        {
            sr.sprite = bulletWound; //front shot
            RotateNW();
        }
        else if (name == "East")
        {
            sr.sprite = bulletWound;//front or back doesnt matter
            RotateRight();
        }
        else if (name == "West")
        {
            sr.sprite = bulletWound;//front or back doesnt matter
            RotateLeft();
        }
        else
        {
            sr.sprite = bulletWound;
        }
       
      
        //sr.sprite = bulletWound;
        Instantiate (bloodPool, this.transform.position, this.transform.rotation);
        sr.sortingOrder = 2;
        //disable ai
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponent<CircleCollider2D>().isTrigger = true;
        
        
        this.gameObject.tag = "Dead";
    }

    public void killMelee()
    {
        sr.sprite = stabbed;
        Instantiate (bloodPool, this.transform.position, this.transform.rotation);
        Instantiate (bloodSpurt, this.transform.position, this.transform.rotation);
        sr.sortingOrder = 2;
        //disable ai
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.gameObject.tag = "Dead";
    }

    public void Disable()
    {
        for (int i = 0; i < DirectionGrid.Count; i++)
            {
                DirectionGrid[i].SetActive(false);
            }
    }

    void RotateRight () { this.gameObject.transform.Rotate (Vector3.forward * 90); }

    void RotateLeft () { this.gameObject.transform.Rotate (Vector3.forward * -90); }

    void RotateSE () { this.gameObject.transform.Rotate (Vector3.forward * -45); }
     void RotateSW () { this.gameObject.transform.Rotate (Vector3.forward * 45); }

    void RotateNE () { this.gameObject.transform.Rotate (Vector3.forward * 45); }
    void RotateNW () { this.gameObject.transform.Rotate (Vector3.forward * -45); }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        
         if(collision.tag == "Bullet") {}
    }


}


