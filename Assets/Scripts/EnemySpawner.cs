﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;

    public void SpawnEnemy()
    {
        Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
    }

}
