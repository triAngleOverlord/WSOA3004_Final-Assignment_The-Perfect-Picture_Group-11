using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Image customCursor;
    [SerializeField] private Sprite normalCursor;
    [SerializeField] private Sprite lckedOnCursor;


    public PlayerState State;
    public enum PlayerState { normal, lockOn }

    [SerializeField] private float speed;
    private Vector2 movement;

    private Rigidbody2D rb;


    //look
    private Vector2 mousePos;

    [SerializeField] private bool isLockedOn;
    private GameObject enemyToLockOn;

    //Animations
    [SerializeField] private Animator[] animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cursor.lockState = CursorLockMode.Confined;

        Cursor.visible = false;
    }

    void Update()
    {
        GetInput();
        LockIntoEnemies();
        CursorUpdate();
    }

    private void FixedUpdate()
    {
        Move();
        Look();
    }

    private void CursorUpdate()
    {
        customCursor.transform.position= mousePos;

        customCursor.sprite = State==PlayerState.normal ? normalCursor : lckedOnCursor;
        customCursor.color = State == PlayerState.normal ? Color.black : Color.red;
    }

    private void GetInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement != Vector2.zero)
        {
            if (animator != null)
            {
                foreach (var anim in animator)
                {
                    anim.SetFloat("movement", 1);
                }
            }
        }
        else
        {
            if (animator != null)
            {
                foreach (var anim in animator)
                {
                    anim.SetFloat("movement", 0);
                }
            }
        }

        if (State == PlayerState.normal)
        {
            //set mousePos to the position of the mousePosition
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void Look()
    {
        Vector2 lookDir = mousePos - rb.position;
        var convertAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = convertAngle;
    }


    private void LockIntoEnemies()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if (EnemyController.isOverMe && !isLockedOn)
            {
                State = PlayerState.lockOn;

                enemyToLockOn = EnemyController.me;
                isLockedOn = true;
            }
            else if (isLockedOn)
            {
                State = PlayerState.normal;
                enemyToLockOn = null;
                isLockedOn = false;
            }
        }

        if (State == PlayerState.lockOn)
        {
            if (enemyToLockOn.GetComponent<EnemyController>() != null)
            {
                mousePos = enemyToLockOn.transform.position;
            }
            else
            {
                State = PlayerState.normal;
                isLockedOn = false;
            }
        }
    }
}
