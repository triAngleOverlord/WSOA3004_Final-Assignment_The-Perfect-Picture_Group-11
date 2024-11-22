using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Player")
        {
            FindObjectOfType<SecondPart>().secondPartGo = true;
            FindObjectOfType<SecondPart>().startPartTwo();
        }
    }
}
