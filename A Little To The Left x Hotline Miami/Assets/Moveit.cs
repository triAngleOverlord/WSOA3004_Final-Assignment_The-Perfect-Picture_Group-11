using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveit : MonoBehaviour
{
    private GameObject objectToDrag;
    [SerializeField] public GameObject holdposition;
    [SerializeField] public GameObject player;
    private bool isDragging = false;

    // Update is called once per frame
    void Update()
    {
        // If we are dragging an object
        if (isDragging && objectToDrag != null && Input.GetKey(KeyCode.E))
        {
            // Update the position of the dragged object to the player's position
            objectToDrag.transform.position = holdposition.transform.position;

            player.GetComponent<PlayerController>().speed = 4;

        }

        

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is draggable
        if (collision.gameObject.CompareTag("Draggable"))
        {
            objectToDrag = collision.gameObject;
        }

        if (objectToDrag != null)
        {
            isDragging = true;
        }
        else
        {
            player.GetComponent<PlayerController>().speed = 10;
            isDragging = false;
            objectToDrag = null;
        }

    }

   

    
}
