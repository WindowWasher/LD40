using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Actor {

    private int booty = 0;
    private int totalBootyEverCollected = 0;

    public int valueNeededForHealthBonus = 6;
    public int valueNeededForAttachBonus = 18;

    private int valueOfKills = 0;
    private int nextHealthBonus;
    private int nextAttachBonus;

    private int attackBonus = 0;

    [SerializeField]
    Text playerHealth;

    [SerializeField]
    Text bootyPlundered;

    [SerializeField]
    Text experienceEarned;

    [SerializeField]
    Text attackDmg;

    [SerializeField]
    Text nextHpUp;

    [SerializeField]
    Text nextAttackUp;

    protected override void Start()
    {
        base.Start();

        nextHealthBonus = valueNeededForHealthBonus;
        nextAttachBonus = valueNeededForAttachBonus;

        updateGui();
    }

    void FixedUpdate() {
        UpdateBonuses();

        float hMove = Input.GetAxis("Horizontal");
        float vMove = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().velocity = new Vector2(hMove * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, vMove * maxSpeed);

        if (hMove > 0 && !facingRight)
            Flip();
        else if (hMove < 0 && facingRight)
            Flip();

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            fireBullet.Fire(projectile, facingRight, fireLocation.transform, gameObject.tag);
        }

        if (health <= 0)
        {
            debrisSpawner.SpawnDebris(transform);
            Destroy(gameObject);
        }
	}

    public int getBonusDamage()
    {
        return attackBonus;
    }

    public void addKillValue(int value)
    {
        valueOfKills += value;
        updateGui();
    }

    private void UpdateBonuses()
    {
        if (valueOfKills >= nextHealthBonus)
        {
            health += 1;
            maxHealth += 1;
            nextHealthBonus += valueNeededForHealthBonus;
            valueNeededForHealthBonus += 5;
            updateGui();
        }

        if (valueOfKills >= nextAttachBonus)
        {
            attackBonus += 1;
            nextAttachBonus += valueNeededForAttachBonus;
            valueNeededForAttachBonus += 15;
            updateGui();
        }
    }

    private void updateGui()
    {
        playerHealth.text = "Player Health: " + Mathf.Max(0, health) + " of " + maxHealth;
        bootyPlundered.text = "Booty Plundered: " + booty;
        experienceEarned.text = "Experience: " + valueOfKills;
        attackDmg.text = "Attack Power: " + (1 + attackBonus);
        nextHpUp.text = "Next HP Up: " + nextHealthBonus;
        nextAttackUp.text = "Next Atack Up: " + nextAttachBonus;
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        updateGui();
    }

    public void AddBooty(int amount)
    {
        booty += amount;
        totalBootyEverCollected += amount;
        bootyPlundered.text = "Booty Plundered: " + booty;
    }

    public int AmountBootyEverCollected()
    {
        return totalBootyEverCollected;
    }

    public override bool attemptHeal(int amount)
    {
        bool success = base.attemptHeal(amount);
        updateGui();

        return success;
    }
}
