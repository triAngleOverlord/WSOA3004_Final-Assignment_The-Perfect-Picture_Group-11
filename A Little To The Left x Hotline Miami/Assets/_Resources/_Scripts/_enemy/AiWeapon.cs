using UnityEngine;

public class AiWeapon : MonoBehaviour
{
    public WeaponType type;
    public enum WeaponType { melee, range }

    private float untilNxtShot;

    public void RangeStyle(GameObject projectilePrefab, Transform projectileSpawnPoint, float amountOfBullets, float spread,float speed, float timeBeforeNxtShot)
    {
        if (untilNxtShot <= 0)
        {
            for (int i = 0; i < amountOfBullets; i++)
            {
                var randomRot = Random.Range(-spread, spread);
                Quaternion rot = Quaternion.Euler(0, 0, randomRot);
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation* rot);

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.AddForce(projectile.transform.up * speed, ForceMode2D.Impulse);

                untilNxtShot = timeBeforeNxtShot;
                Destroy(projectile, 5f);
            }
        }
        else
        {
            untilNxtShot -= Time.deltaTime;
        }
    }

    public void MeleeStyle(Transform origin, float radius, LayerMask enemy, float timeBeforetNxtSlash)
    {
        Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(origin.position, radius, enemy);

        foreach (var player in enemyCollider)
        {
            if (untilNxtShot <= 0)
            {
                print("Slash slash!!");
                untilNxtShot = timeBeforetNxtSlash;
            }
            else
            {
                untilNxtShot -= Time.deltaTime;
            }
        }
    }
}
