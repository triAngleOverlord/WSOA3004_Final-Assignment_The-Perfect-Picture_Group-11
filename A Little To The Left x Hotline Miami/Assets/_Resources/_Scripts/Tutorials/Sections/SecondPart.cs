using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SecondPart : MonoBehaviour
{
    [SerializeField] private GameObject knifeEnemy;
    [SerializeField] private GameObject batAndGunEnemy;

    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject gun;

    public GameObject[] tasks;

    public TMP_Text commentTxt;
    public GameObject commentBox;
    public string[] commentMessage;

    public int index = 0;
    public int commentIndex = 0;

    private FirstPart firstPar;
    public bool secondPartGo;
    void Start()
    {
        firstPar = GetComponent<FirstPart>();
        bat.SetActive(false);
        gun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (secondPartGo) 
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
        }
    }

    public void startPartTwo()
    {
        ManageArray(tasks, true);
    }
    public void NextButton()
    {
        index += 1;
    }
    private void CompleteTasks()
    {

        if (FindObjectOfType<PlayerController>().isLockedOn == true)
        {
            tasks[0].SetActive(false);
        }
        if (knifeEnemy.GetComponent<EnemyController>().deathBy == "Knife")
        {
            tasks[1].SetActive(false);
            bat.SetActive(true);
        }
        if (batAndGunEnemy.GetComponent<EnemyController>().baseState == EnemyController.enemyState.knockedDown)
        {
            tasks[2].SetActive(false);
            gun.SetActive(true);
        }

        if (batAndGunEnemy.GetComponent<EnemyController>().deathBy == "Gun")
        {
            tasks[3].SetActive(false);
            firstPar.fakeDoor2.SetActive(false);
            firstPar.door2.SetActive(true);
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
