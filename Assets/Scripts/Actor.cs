using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    public float maxSpeed = 3f;
    public GameObject fireLocation;
    public DebrisSpawner debrisSpawner;
    public GameObject projectile;
    public int health = 10;
    protected int maxHealth;

    protected FireBullet fireBullet;
    protected bool facingRight = true;

    // Use this for initialization
    protected virtual void Start () {
        maxHealth = health;
        fireBullet = gameObject.AddComponent<FireBullet>();
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }
	
    protected void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
    }

    public virtual bool attemptHeal(int amount)
    {
        if (health >= maxHealth)
            return false;
        else
            health += Mathf.Min(amount, maxHealth - health);
            return true;

    }
}
