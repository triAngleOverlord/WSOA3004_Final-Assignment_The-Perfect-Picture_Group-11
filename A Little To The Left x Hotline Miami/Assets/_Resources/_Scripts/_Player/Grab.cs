using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
     private GameObject objectToDrag;

     public Transform objectdragspot;
    private bool isDragging = false;

    [SerializeField] public GameObject player;

    // Update is called once per frame
    void Update()
    {
        // If we are dragging an object
        if (isDragging && objectToDrag != null)
        {
            // Update the position of the dragged object to the player's position
            objectToDrag.transform.position = objectdragspot.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is draggable
        if (collision.gameObject.CompareTag("Draggable"))
        {
            objectToDrag = collision.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // Keep dragging the object as long as we are in contact and pressing a button
        if (objectToDrag != null && Input.GetKey(KeyCode.E))
        {
            isDragging = true;
            player.GetComponent<PlayerController>().speed = 2;
        }

        if (objectToDrag != null && Input.GetKey(KeyCode.G))
        {
            isDragging = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Stop dragging if we exit collision with the object
        if (collision.gameObject == objectToDrag)
        {
            isDragging = false;
            objectToDrag = null;
            player.GetComponent<PlayerController>().speed = 10;
        }
    }
}

