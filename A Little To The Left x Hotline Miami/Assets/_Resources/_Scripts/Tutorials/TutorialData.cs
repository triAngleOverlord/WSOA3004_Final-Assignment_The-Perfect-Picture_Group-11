using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialData : MonoBehaviour 
{
    const string move = "Use WASD/ Arrow keys to move";
    const string pickUpWeapons = "Now that you know how to move, why don't you go pick up that knife? Use the 'Right Mouse' button to pick up the weapon";
    const string Attack = "Well, well, look who’s armed and dangerous! Use the 'Left Mouse' button to swing that knife. Pretend you’re chopping onions but but violent onions.";
    const string targetArea = "Bravo, you wield that knife like a pro! See that enemy? Before you go wild, pay attention to the following:  1. Direction: Check where the target spot is facing.If it’s facing north/up, you need to kill the enemy while also facing north, and so on. 2. Color: Look at the marks on the target spot.These indicate which enemy to kill.For example, if the enemy is orange, make sure the target spot’s marks match that color. 3. Weapon Choice: Use the right weapon for the job: - Stab wounds? Use a knife. - Torso and legs split? Grab a katana. - Guts pulled out? That’s chainsaw work.- Knocked out? Finish them with a bat or glass bottle. - Headshot? Any gun will do, just not the shotgun.  - Limbs missing? That’s a shotgun’s calling.  4. Distance: Make sure the enemy is killed nearby or directly at the target spot for the perfect distance.No pressure, but if you mess this up...well, let’s just say you’ll find out!";
    const string gunAttack = "Great job with the knife! Now it’s time to bring out the big guns. Head to the other room and grab that shotgun. It’s loud, it’s powerful, and it’s got your back. Just don’t forget, with great firepower comes great responsibility, or at least, great noise.";

    const string attackNow = "Alright, you’ve got the gun. Feeling unstoppable yet? Check out that enemy ahead. Take a moment to inspect the kill spot, angles matter, and precision is everything. Hover over them and press 'Scroll' to lock on. Aim carefully, because missing isn’t just embarrassing, it’s dangerous.";

    const string throwWeapon = "Locking on like a pro, huh? Let’s kick it up a notch: throwing weapons. Press 'Right Mouse' to throw or 'G' to drop. Here’s the deal: all weapons knock enemies down when thrown, but the results differ. Melee weapons like the knife, chainsaw, wooden axe, and katana are lethal and will kill on contact. The bat and bottle? They’re more like guns, they knock enemies down but don’t finish them. So, lock on, aim for the kill spot, and make your move. Just remember, timing is everything. Oh, and guns? Still loud enough to summon the whole neighborhood, so plan accordingly!";

    const string moveBoxes = "Think you’ve mastered weapons? Time to level up your strategy. See that box? Pick it up with 'I' and drop it to block an enemy’s path, they’ll avoid it like it’s cursed. Or, if there’s someone behind a door, slam it open for an instant knockdown. Who knew everyday objects could be so deadly?";

    const string enemyBehaviour = "By now, you’ve dealt with a variety of enemies. The knife stab? The door slam? The shotgun blast? Those were static enemies, easy targets. But don’t let their stillness fool you, they’re sharp. Then there’s the patrolling enemy you blocked with a box. They follow set routes, but they’re just as nosy. And the roaming enemies? They’re unpredictable wanderers with no set path. Stay alert and be ready for surprises.";
    const string cameraControl = "Before we wrap this up, let me clue you in on a handy trick: camera control. Hold the 'Left Shift' button and move the mouse around to survey the level. You’ll be able to see beyond walls, spot enemies, and get a better idea of the layout. Use it anytime, it’s always available. Now go on, take a look. It will help you plan your moves like a true strategist!";

    const string restarting = "That’s it for the tutorial! From here on out, it’s all you. Just remember the golden rule for a perfect kill: check the kill spot’s direction, its marks, and matching colors on the enemy. Timing, precision, and the right weapon are your keys to success. Messed up? No worries, press 'R' to restart the level. Now go show them who’s boss. Good luck, you’re going to need it!";


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

    public GameObject [] fakeDoor;
    public GameObject[] doors;

    public bool hasWeapon = false;

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

        foreach (GameObject go in fakeDoor)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in doors)
        {
            go.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInteraction.hasthrownWeapon && index == 6)
        {
            hasWeapon = true;
        }

        Dialogue();
        InstructionDone();

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && tutBackground.gameObject.activeSelf)
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
                case 9: tutTxt.text = cameraControl; break;
                case 10: tutTxt.text = restarting; break;
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
            doors[0].SetActive(true);
            fakeDoor[0].SetActive(false);
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
                doors[1].SetActive(true);
                fakeDoor[1].SetActive(false);
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
            if (playerInteraction.equippedWeapon == rangedWeapon.transform && playerController.isLockedOn && Input.GetMouseButtonDown(0))
            {
                StartCoroutine(PopUp(1));
            }
        }

        else if (index == 6 && !tutBackground.gameObject.activeSelf)
        {
            if (hasWeapon)
            {
                StartCoroutine(PopUp(5));
                fourthEnemy.state = EnemyController.idleStates.patrol;

                doors[2].SetActive(true);
                fakeDoor[2].SetActive(false);
            }
        }

        else if (index == 7 && !tutBackground.gameObject.activeSelf)
        {
            //enemy has avoided the interactable box
            if (fourthEnemy.hasInteractedWithBox)
            {
                fifthEnemy.state = EnemyController.idleStates.roamer;
                StartCoroutine(PopUp(7));
            }
        }

        else if (index == 8 && !tutBackground.gameObject.activeSelf)
        {

             StartCoroutine(PopUp(5));

        }

        else if (index == 9 && !tutBackground.gameObject.activeSelf)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                StartCoroutine(PopUp(10));
            }
        }

        if (index == 10 && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            tutBackground.gameObject.SetActive(false);
            SceneManager.LoadScene("Level 1");
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
