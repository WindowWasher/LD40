using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMove : MonoBehaviour {

    public float maxRangeFromStart = 2f;
    public float pauseAtDestination = 0f;
    public bool randomPause = false;
    public float minPause = 0f;
    public float maxPause = 3f;

    private Vector3 startPosition;
    private Vector3 targetPos;
    private float timeArrivedAtWayPoint = 0f;

    private Enemy enemy;
    private FloatBehaviour floatBehaviour;

    private float onCollideWait = 0.1f;
    private float timeSinceLastCollUpdate = 0f;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        enemy = GetComponent<Enemy>();
        floatBehaviour = GetComponent<FloatBehaviour>();
        targetPos = GetNextPosition();
		
	}

    void OnCollissionEnter2D(Collider2D colli)
    {
        if (!enemy.isAttacking())
            targetPos = GetNextPosition();
    }

    void OnCollisionStay2D(Collision2D colli)
    {
        if (!enemy.isAttacking() && (Time.time > timeSinceLastCollUpdate + onCollideWait))
        {
            timeSinceLastCollUpdate = Time.time;
            targetPos = GetNextPosition();
        }
    }

    Vector3 GetNextPosition()
    {
        float initX = startPosition.x;
        float initY = startPosition.y;

        Vector3 newPos = new Vector3(Random.Range(initX - maxRangeFromStart, initX + maxRangeFromStart), Random.Range(initY - maxRangeFromStart, initY + maxRangeFromStart), 0f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, newPos, Vector2.Distance(transform.position, targetPos));
        Debug.DrawLine(transform.position, newPos, Color.red);

        if (hit.collider != null && hit.collider != GetComponent<Collider2D>())
        {
            Debug.Log(hit.collider.gameObject.tag);
            return transform.position;
        }

        if (Vector3.Distance(newPos, transform.position) < maxRangeFromStart / 2)
            return transform.position;

        return newPos; 
    }

    bool hasArrived(Vector3 arivalPoint)
    {
        float dist = Vector3.Distance(transform.position, arivalPoint);
        if (dist < 0.3f)
        {
            timeArrivedAtWayPoint = Time.time;
            floatBehaviour.EnableFloatBehaviour();
            if (randomPause)
                pauseAtDestination = Random.Range(minPause, maxPause);

            return true;
        }

        floatBehaviour.DisableFloatBehaviour();

        return false;
    }
	
	// Update is called once per frame
	void Update () {

        if (enemy != null && !enemy.isAttacking())
        {

            Debug.DrawLine(transform.position, targetPos, Color.green);
            if (Time.time < timeArrivedAtWayPoint + pauseAtDestination)
            {
                return;
            }

            enemy.MoveTo(targetPos, true);

            if (hasArrived(targetPos))
                targetPos = GetNextPosition();

        }

	}
}
