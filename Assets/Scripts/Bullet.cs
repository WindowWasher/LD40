using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int bulletDmg = 1;
    public float fireRate = 0.3f;
    public float bulletSpeed = 4f;

    public string shotBy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return;

        Actor actor = other.GetComponent<Actor>();

        if (actor != null && actor.tag != shotBy)
        {
            if (shotBy == "Player")
                bulletDmg += GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getBonusDamage();
            actor.TakeDamage(bulletDmg);
        }

        if (other.tag != shotBy && other.tag != "Floating Debris" && other.tag != "Bullet")
        {
            Destroy(gameObject, 0.05f);
        }

    }

}
