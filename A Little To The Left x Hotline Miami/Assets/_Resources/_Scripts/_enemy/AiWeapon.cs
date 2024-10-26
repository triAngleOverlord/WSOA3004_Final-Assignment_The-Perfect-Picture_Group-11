using UnityEngine;

public class AiWeapon : MonoBehaviour
{
    public WeaponType type;
    public enum WeaponType { melee, range }

    private float untilNxtShot;
    private float timeBeforeNxtShot = 0.5f;
    public void RangeStyle(GameObject projectilePrefab, Transform projectileSpawnPoint, float speed)
    {
        if (untilNxtShot <= 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            untilNxtShot = timeBeforeNxtShot;

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(projectileSpawnPoint.up * speed, ForceMode2D.Impulse);

            Destroy(projectile, 3f);
        }
        else
        {
            untilNxtShot -= Time.deltaTime;
        }
    }

    public void MeleeStyle(Transform origin, float radius, LayerMask enemy)
    {
        Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(origin.position, radius, enemy);

        foreach (var player in enemyCollider)
        {
            if (untilNxtShot <= 0)
            {
                print("Slash slash!!");
                untilNxtShot = timeBeforeNxtShot;
            }
            else
            {
                untilNxtShot -= Time.deltaTime;
            }
        }
    }
}
