using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Unity.VisualScripting;
public class scr : MonoBehaviour
{

    public GameObject tipbox;
       public Canvas canvas; 
    // Start is called before the first frame update
    void Start()
    {
        
    
        
    }

   
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gettem();
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            getout();
        }
    }

    private void gettem()
    {
        TextMeshProUGUI textComponent = tipbox.GetComponent<TextMeshProUGUI>();
        textComponent.text = "Press I to pick up";
    }

    private void getout()
    {
        TextMeshProUGUI textComponent = tipbox.GetComponent<TextMeshProUGUI>();
        textComponent.text = "";
    }
}
