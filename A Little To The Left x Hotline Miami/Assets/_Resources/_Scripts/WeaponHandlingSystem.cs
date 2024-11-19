using UnityEngine;

public class WeaponHandlingSystem : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (gameObject.transform.parent != null)
        {
            if (gameObject.transform.parent.name == "weaponPos")
            {
                LayerHandle("Fallback");
                rb.bodyType = RigidbodyType2D.Kinematic;
                if (col.enabled && col != null)
                {
                    col.isTrigger = true;
                }

                if (gameObject.tag == "Melee")
                {
                    anim.enabled = true;
                }
            }
            else if (gameObject.transform.parent.name == "melee" || gameObject.transform.parent.name == "Ranged")
            {
                LayerHandle("Fallback");
                rb.bodyType = RigidbodyType2D.Kinematic;

                if (col.enabled && col != null)
                {
                    col.isTrigger = true;
                }

                if (gameObject.tag == "Melee")
                {
                    anim.enabled = true;
                }
            }
            else
            {
                if (rb.velocity.magnitude <= 0)
                {
                    LayerHandle("Weapons");
                }

                rb.bodyType = RigidbodyType2D.Dynamic;

                if (col.enabled && col != null)
                {
                    col.isTrigger = false;
                }

                if (gameObject.tag == "Melee")
                {
                    anim.enabled = false;
                }
            }
        }

        else if (gameObject.transform.parent == null)
        {
            if (rb.velocity.magnitude <= 0)
            {
                LayerHandle("Weapons");
            }

            rb.bodyType = RigidbodyType2D.Dynamic;

            if (col.enabled && col != null)
            {
                col.isTrigger = false;
            }

            if (gameObject.tag == "Melee")
            {
                anim.enabled = false;
            }
        }
    }

    private void LayerHandle(string name)
    {
        int layer = LayerMask.NameToLayer(name);
        gameObject.layer = layer;
    }
}
