using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booty : MonoBehaviour {

    public int value = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.AddBooty(value);
            Destroy(gameObject);
        }
    }
}
