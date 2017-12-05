using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    public List<GameObject> patrolPoints;
    public float pauseAtWayPointDuration = 0f;
    public bool onStartSkipFirst = false;
    public bool randomPause = false;
    public float minPause = 0f;
    public float maxPause = 3f;


    private float timeArrivedAtWayPoint = 0f;
    private Queue<GameObject> pPoints;
    private GameObject target;
    private Enemy enemy;

	// Use this for initialization
	void Start () {
        pPoints = new Queue<GameObject>(patrolPoints);
        SetNextPatrolPoint();
        enemy = GetComponent<Enemy>();
	}

    void SetNextPatrolPoint()
    {
        // Grab the first patrol point
        target = pPoints.Dequeue();

        // Place the the point back into the queue
        // So we can move in revere once the point is reached
        pPoints.Enqueue(target);
    }

    bool HasArrived(Transform arivalPoint)
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < 0.3f)
        {
            timeArrivedAtWayPoint = Time.time;
            if (randomPause)
                pauseAtWayPointDuration = Random.Range(minPause, maxPause);

            if (onStartSkipFirst)
            {
                pauseAtWayPointDuration = 0f;
                onStartSkipFirst = false;
            }
            return true;
        }

        return false;
    }

	// Update is called once per frame
	void Update () {

        if(enemy != null && !enemy.isAttacking())
        {
            if (Time.time < timeArrivedAtWayPoint + pauseAtWayPointDuration)
                return;

            enemy.MoveTo(target.transform.position, true);

            if (HasArrived(target.transform))
                SetNextPatrolPoint();
        }
	}
}
