using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float health = 300f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.tag);
        if (collision.tag == "Projectile")
        {
            health -= collision.GetComponent<Projectile>().GetDamage();
            Destroy(collision.gameObject);

            if (health <= 0)
            {
                Destroy(this.gameObject);
            }

        }
    }
}
