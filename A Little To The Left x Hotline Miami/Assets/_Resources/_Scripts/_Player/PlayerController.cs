using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    private Vector2 movement;

    private Rigidbody2D rb;


    //look
    private Vector2 mousePos;


    //shoot
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawner;
    private GameObject bullet;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource bulletSFx;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        GetInput();
        //Shoot();
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

        //set mousePos to the position of the mousePosition
        mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void Look()
    {
        Vector2 lookDir = mousePos - rb.position;
        var convertAngle = Mathf.Atan2 (lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = convertAngle;
    }

    private void Shoot()
    {
         if (Input.GetButtonDown ("Fire1"))
        {
            bullet = Instantiate(projectile, projectileSpawner.position, Quaternion.identity);
            muzzleFlash.Play();
            bulletSFx.Play();
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * 100f, ForceMode2D.Impulse);
        }
    }
}
