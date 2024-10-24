using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerState State;
    public enum PlayerState { normal, lockOn }

    [SerializeField] private float speed;
    private Vector2 movement;

    private Rigidbody2D rb;


    //look
    private Vector2 mousePos;

    [SerializeField] private bool isLockedOn;
    private GameObject enemyToLockOn;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
        LockIntoEnemies();
    }

    private void FixedUpdate()
    {
        Move();
        Look();
    }

    private void GetInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

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
            if (enemyToLockOn != null)
            {
                mousePos = enemyToLockOn.transform.position;
            }
        }
        else
        {
            State = PlayerState.normal;
            isLockedOn = false;
        }
    }
}
