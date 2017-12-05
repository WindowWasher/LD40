using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public int healAmount = 1;
    private bool used = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return; 

        Player player = other.GetComponent<Player>();

        if (used)
            return;

        if (player != null && player.attemptHeal(healAmount))
        {
            used = true;
            Destroy(gameObject);
        }
    }

}
