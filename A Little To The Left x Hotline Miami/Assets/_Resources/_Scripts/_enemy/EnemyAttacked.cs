using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacked : MonoBehaviour
{
    public Sprite knockedDown,stabbed,bulletWound,backUp; //temporary, may be expanded
    public GameObject bloodPool,bloodSpurt;
   [SerializeField] SpriteRenderer sr;
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

    public void killBullet()
    {
        
        sr.sprite = bulletWound;
        Instantiate (bloodPool, this.transform.position, this.transform.rotation);
        sr.sortingOrder = 2;
        //disable ai
        this.GetComponent<CircleCollider2D>().enabled=false;
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
}
