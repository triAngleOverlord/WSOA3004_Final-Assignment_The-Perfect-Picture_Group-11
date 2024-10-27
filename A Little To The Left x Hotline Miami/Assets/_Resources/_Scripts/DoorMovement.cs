using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    public float startRot = 0;
    float timer=0.01f;
    public int mod = 3;
    public bool beingOpened=false;
    //increasing the rotation on the z axis makes the door move anticlockwise
    void awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        startRot = this.transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rotateClockWiseMethod()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Vector3 rot = new Vector3 (0,0,0);
            float z = this.transform.eulerAngles.z;
            rot.z = z+= mod;
            this.transform.eulerAngles = rot;
            timer = 0.01f;
        }
    }

    public void rotateAntiClockWiseMethod()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Vector3 rot = new Vector3 (0,0,0);
            float z = this.transform.eulerAngles.z;
            rot.z = z-= mod;
            this.transform.eulerAngles = rot;
            timer = 0.01f;
        }
    }

    





}
