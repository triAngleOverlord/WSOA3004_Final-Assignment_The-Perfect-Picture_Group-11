using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialData : MonoBehaviour 
{
    const string move = "Use WASD/ Arrow keys to move";
    const string pickUpWeapons = "Now that you know how to move, why don't you go pick up that knife? Use the 'Right Mouse' button to pick up the weapon";
    const string Attack = "Well, well, look who’s armed and dangerous! Use the 'Left Mouse' button to swing that knife. Pretend you’re chopping onions but but violent onions.";
    const string targetArea = "Bravo, you wield that knife like a pro! See that enemy? Before you go wild, check their color and the kill spot’s visuals. Slashes mean stab. Holes mean shoot. Colors gotta match too. No pressure, but get it wrong, and... well, you’ll find out. ";
    const string gunAttack = "Nice work with the knife! Now, let’s graduate to the big leagues. Head to the other room and grab that shotgun. Yes, it's as loud as it looks.";
    const string attackNow = "Alright, you’ve got the gun. Feeling dangerous yet? Now, see that enemy? Check the kill spot, angles matter. Precision is key, not just wild shooting. Hover over them and press the 'Scroll' button to lock on. Because missing would be, well, embarrassing.";
    const string throwWeapon = "Look at you, mastering the art of locking on. Feeling fancy? Let’s take it up a notch, throw that weapon. 'Right Mouse' to toss it like a pro, or 'G' to drop it like yesterday’s trash. The rules? Melee weapons = instant kill, ranged weapons = knock ‘em flat. Lock on, aim for the sweet spot, and let it fly. But hey, don’t go playing dodgeball with everything you see. Timing’s key, genius. Oh, and a heads-up...guns are loud. Like, ‘invite all the enemies to a party’ loud. Use them wisely unless you’re ready to deal with the crowd.";
    const string moveBoxes = "Throwing weapons? Old news. Let’s move on to some pro-level trickery. See that box? Pick it up with 'I' and drop it wherever you want. Place it in the path of that patrolling enemy, they’ll steer clear like it’s cursed. Blocking enemies? That’s strategy, my friend. Oh, and if someone’s lurking behind a door? Slam it open, and boom, instant knockdown. Who knew doors could be so deadly?";
    const string enemyBehaviour = "Remember that enemy you stabbed with a knife? Or the one you flattened with a door? Maybe the one you blasted with a shotgun? Yeah, those were the easy ones—static enemies. They don’t move an inch, but don’t be fooled—they’re watching, listening, and ready to pounce if you wander into their sight. Now, the one you blocked with a box? That’s a patrolling enemy.They’ve got a route to follow but are just as nosy.And see that other one, wandering aimlessly like it lost its keys? That’s a roaming enemy—no set path, unpredictable as your internet connection on a bad day.Approach it carefully, or it’ll surprise you when you least expect it.";
    const string restarting = "And with that, my friend, our little tutorial tour comes to an end. From here on out, it’s all you. Remember the golden rule: aim for a perfect kill. Check the kill spot’s direction, its marks, and the matching colors on the enemy. That’s your blueprint for a flawless takedown. Need a do-over? No problem—press 'R' to restart the level whenever you want.Now, go out there and show them who’s boss.Good luck… you’ll need it.";

    public GameObject tutBackground;
    public TMP_Text tutTxt;
    public int index;
    private bool isCoroutineRunning = false;
    private bool isRunning = false;
    public GameObject meleeWeapon;
    public GameObject rangedWeapon;
    public GameObject enemyOne;
    public GameObject enemyTwo;

    //by the door
    public GameObject enemyThree;
    //patrolling enemy
    public GameObject enemyFour;
    //wandering enemy
    public GameObject enemyFive;

    private PlayerInteraction playerInteraction;
    PlayerController playerController;
    private EnemyController firstEnemy;
    private EnemyController secondEnemy;
    private EnemyController fourthEnemy;
    private EnemyController fifthEnemy;

    public GameObject interactableBox;

    public Transform cam;

    void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        playerController = FindObjectOfType<PlayerController>();
        index = 0;

        meleeWeapon.SetActive(false);
        rangedWeapon.SetActive(false);
        enemyOne.SetActive(false);

        firstEnemy = enemyOne.GetComponent<EnemyController>();
        secondEnemy = enemyTwo.GetComponent<EnemyController>();
        fourthEnemy = enemyFour.GetComponent<EnemyController>();
        fifthEnemy = enemyFive.GetComponent<EnemyController>();
    }

    void Update()
    {
        Dialogue();
        InstructionDone();

        if (Input.GetMouseButtonDown(0) && tutBackground.gameObject.activeSelf)
        {
            tutBackground.gameObject.SetActive(false);
        }


        if (tutBackground.gameObject.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
        }

        //camera move
        if (index == 3 && tutBackground.activeSelf)
        {
            //zoom in on enemy one
            CameraMovement(enemyOne.transform.position);
        }

        else if (index == 4 && tutBackground.activeSelf)
        {
            //zoom in on ranged weapon
            CameraMovement(rangedWeapon.transform.position);
        }
        else if (index == 5 && tutBackground.gameObject.activeSelf)
        {
            //zoom in on enemy two
            CameraMovement(enemyTwo.transform.position);
        }
        else if (index == 7 && tutBackground.gameObject.activeSelf)
        {
            //zoom in on interactable box
            CameraMovement(interactableBox.transform.position);
        }
    }

    private void CameraMovement(Vector3 destination)
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, destination, Time.unscaledDeltaTime * 2f);
    }

    private void Dialogue()
    {
        switch (index)
        {
                case 0: tutTxt.text = move; break;
                case 1: tutTxt.text = pickUpWeapons; break;
                case 2: tutTxt.text = Attack;  break;
                case 3: tutTxt.text = targetArea;  break;
                case 4: tutTxt.text = gunAttack; break;
                case 5: tutTxt.text = attackNow; break;
                case 6: tutTxt.text = throwWeapon; break;
                case 7: tutTxt.text = moveBoxes; break;
                case 8: tutTxt.text = enemyBehaviour;  break;
                case 9: tutTxt.text = restarting; break;
        }
    }

    private void InstructionDone()
    {
        //move
        if (index == 0 && !tutBackground.gameObject.activeSelf)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                StartCoroutine(PopUp(5));
            }
        }

        //pick up weapon
        else if (index == 1 && !tutBackground.gameObject.activeSelf)
        {
            StartCoroutine(EnableGameObjects(meleeWeapon, 1));
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(PopUp(1));
            }
        }
        //attack
        else if (index == 2 && !tutBackground.gameObject.activeSelf)
        {
            StartCoroutine(EnableGameObjects(enemyOne, 1));
            if (Input.GetMouseButtonDown(0) && playerInteraction.equippedWeapon == meleeWeapon.transform)
            {
                StartCoroutine(PopUp(5));
            }
        }
        //attack with melee
        else if (index == 3 && !tutBackground.gameObject.activeSelf)
        {
            if (firstEnemy.baseState == EnemyController.enemyState.dead)
            {
                StartCoroutine(PopUp(3));
                StartCoroutine(EnableGameObjects(rangedWeapon, 1));
            }
        }

        else if (index == 4 && !tutBackground.gameObject.activeSelf)
        {
            if (playerInteraction.equippedWeapon == rangedWeapon.transform)
            {
                StartCoroutine(PopUp(.5f));
            }
            StartCoroutine(EnableGameObjects(enemyTwo, 1));
        }

        else if (index == 5 && !tutBackground.gameObject.activeSelf)
        {
            if (playerInteraction.equippedWeapon == rangedWeapon.transform && playerController.isLockedOn && Input.GetMouseButtonDown(0) && secondEnemy.baseState == EnemyController.enemyState.dead )
            {
                StartCoroutine(PopUp(1));
                fourthEnemy.state = EnemyController.idleStates.patrol;
                fifthEnemy.state = EnemyController.idleStates.roamer;
            }
        }

        else if (index == 6 && !tutBackground.gameObject.activeSelf)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(PopUp(5));
            }
        }

        else if (index == 7 && !tutBackground.gameObject.activeSelf)
        {
            //enemy has avoided the interactable box
            if (fourthEnemy.hasInteractedWithBox)
            {
                StartCoroutine(PopUp(5));
            }
        }

        if (index == 8 && !tutBackground.gameObject.activeSelf)
        {
            StartCoroutine(PopUp(5));
        }
    }

    private IEnumerator PopUp(float seconds)
    {
        if (isCoroutineRunning) yield break;    
        isCoroutineRunning = true;
        yield return new WaitForSecondsRealtime(seconds);
        index += 1;
        tutBackground.gameObject.SetActive(true);
        isCoroutineRunning=false;
    }

    private IEnumerator EnableGameObjects(GameObject element, float seconds)
    {
        if (isRunning) yield break;
        isRunning = true;
        yield return new WaitForSecondsRealtime(seconds);
        element.SetActive(true);
        isRunning = false;
    }
}
