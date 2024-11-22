using System.Collections;
using TMPro;
using UnityEngine;

public class FirstPart : MonoBehaviour
{
    public TMP_Text commentTxt;
    public GameObject commentBox;
    public string[] commentMessage;

    public GameObject [] tasks;

    public int index = 0;
    public int commentIndex = 0;

    PlayerInteraction playerInteraction;

    public GameObject door1;
    public GameObject door2;
    public GameObject door3;

    public GameObject fakeDoor1;
    public GameObject fakeDoor2;
    public GameObject fakeDoor3;

    public GameObject nxtBtn;
    public GameObject weapon1;
    void Start()
    {
        ManageArray(tasks, true);
        playerInteraction = FindObjectOfType<PlayerInteraction>();

        fakeDoor1.SetActive(true);
        fakeDoor2.SetActive(true);
        fakeDoor3.SetActive(true);

        door1.SetActive(false);
        door2.SetActive(false);
        door3.SetActive(false);

        nxtBtn.SetActive(true);
    }

    void Update()
    {
        CompleteTasks();
        
        if (index == 0)
        {
            commentTxt.text = commentMessage[0];
        }
        else if (index == 1)
        {
            commentTxt.text = commentMessage[1];
        }

        else if (index == 2)
        {
            commentTxt.text = commentMessage[2];
        }
        else if (index == 3)
        {
            commentTxt.text = commentMessage[3];
        }

        else if (index == 4)
        {
            commentTxt.text = commentMessage[4];
        }
        else if (index == 5)
        {
            commentTxt.text = commentMessage[5];
        }
    }

    public void NextButton()
    {
        index += 1;
    }

    private void CompleteTasks()
    {

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                tasks[0].SetActive(false);
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (playerInteraction.equippedWeapon == weapon1.transform)
                {
                    tasks[1].SetActive(false);
                }

            }
            if (playerInteraction.hasWeapon && Input.GetMouseButtonDown(0))
            {
                tasks[2].SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.G) && playerInteraction.hasWeapon)
            {
                tasks[3].SetActive(false);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                tasks[4].SetActive(false);
            }
        
        foreach (var task in tasks)
        {
            if (!task.activeSelf)
            {
                fakeDoor1.SetActive(false);
                door1.SetActive(true);
            }
        }

    }

    private void ManageArray(GameObject[] tasks, bool state)
    {
        if (state)
        {
            foreach (GameObject task in tasks)
            {
                task.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject task in tasks)
            {
                task.SetActive(false);
            }
        }
    }
}
