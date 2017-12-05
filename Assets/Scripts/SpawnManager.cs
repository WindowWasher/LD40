using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public List<GameObject> enemySpawners;
    private Player player;
    private int nextSpawnAmount = 100;
    private int nextSpawnCountIncrease = 500;
    private int unitsToSpawn = 1;

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void LateUpdate () {

        if (player.AmountBootyEverCollected() >= nextSpawnAmount)
        {
            for (int i = 0; i < unitsToSpawn; i++)
            {
                int spawnerIndex = Random.Range(0, enemySpawners.Count);
                EnemySpawner spawner = enemySpawners[spawnerIndex].GetComponent<EnemySpawner>();
                spawner.SpawnEnemy();
                nextSpawnAmount += 100;
            }
        }

        if (player.AmountBootyEverCollected() >= nextSpawnCountIncrease)
        {
            unitsToSpawn += 1;
            nextSpawnCountIncrease += 500;
        }
	}
}
