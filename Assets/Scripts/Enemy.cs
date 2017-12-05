using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor {

    public int killValue = 1;
    public bool isAgressive = false;
    public float aggroRange = 2f;

    public float safeDistance = 2.0f;
    
    // If an enemy is not naturally aggressive, then the tollerence can be used to determine how many shots the play can fire before the enemy engages in combat.
    public int tollerance = 0;

    private int hitsTaken = 0;

    private bool attacking = false;
    private GameObject player;

    public bool isAttacking()
    {
        return attacking;
    }

    protected override void Start()
    {
        base.Start();
        if (GameObject.FindGameObjectsWithTag("Player").Length > 0)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FaceTarget(Vector3 targetPos)
    {
        if (targetPos.x < transform.position.x && facingRight)
            Flip();
        else if (targetPos.x > transform.position.x && !facingRight)
            Flip();
    }

    public void MoveTo(Vector3 targetPos, bool patrolling=false)
    {
        FaceTarget(targetPos);

        var prevPos = transform.position;

        var moveTowards = Vector2.MoveTowards(transform.position, targetPos, maxSpeed * Time.deltaTime);

        var newPos = transform.position;

        if (!patrolling && Mathf.Abs(targetPos.x - transform.position.x) > safeDistance)
            newPos.x = moveTowards.x;
        else if (patrolling && Mathf.Abs(targetPos.x - transform.position.x) > 0.1f)
            newPos.x = moveTowards.x;

        if (Mathf.Abs(targetPos.y - transform.position.y) > 0.05f)
            newPos.y = moveTowards.y;

        transform.position = newPos;
    }

    private void AggressiveMoveToward()
    {
        if (player != null)
        {
            MoveTo(player.transform.position);
            FaceTarget(player.transform.position);
        }
    }

	// Update is called once per frame
	void Update () {

        if (attacking && player != null)
        {
            AggressiveMoveToward();

            if (Mathf.Abs(player.transform.position.y - transform.position.y) < 0.75f)
                fireBullet.Fire(projectile, facingRight, fireLocation.transform, gameObject.tag);
        }
        else if (attacking && player == null)
            attacking = false;

        if (player != null && isAgressive && !attacking)
        {
            var distance = Vector2.Distance(transform.position, player.transform.position);

            if (Mathf.Abs(distance) <= aggroRange)
                attacking = true;
        }

        if (health <= 0)
        {
            player.GetComponent<Player>().addKillValue(killValue);
            debrisSpawner.SpawnDebris(transform);
            Destroy(gameObject);
        }
	}

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        hitsTaken += 1;

        if (!attacking && (hitsTaken > tollerance || isAgressive))
            attacking = true;
        else
            FaceTarget(player.transform.position);
    }

    
}
