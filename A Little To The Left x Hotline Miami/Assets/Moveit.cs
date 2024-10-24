using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveit : MonoBehaviour
{
    private GameObject objectToDrag;
        //interact with obstacles
    [SerializeField] private Transform pickedObjectPos;

    public LayerMask obstacleMask;
    private GameObject pickedObject;
    [SerializeField] private float interactDist;
    bool hasObject;


    // Update is called once per frame
    //This snippet here is the method itself
//paste it anywhere within the playerInteraction class
    private void InteractWithObjects()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, interactDist,                         obstacleMask);
            Debug.DrawRay(transform.position, transform.up * interactDist, Color.green);

            if (hit.collider != null && !hasObject)
            {
                pickedObject = hit.collider.gameObject;
                pickedObject.transform.parent = pickedObjectPos;
                pickedObject.GetComponent<Collider2D>().enabled = false;
                pickedObject.transform.localPosition = Vector3.zero;
                pickedObject.transform.localRotation = Quaternion.identity;
                hasObject = true;
            }
            else if (hasObject)
            {
                pickedObject.transform.parent = null;
                pickedObject.GetComponent<Collider2D>().enabled = true;
                pickedObject = null;
                hasObject = false;
            }
        }
    }

    

   

    
}
