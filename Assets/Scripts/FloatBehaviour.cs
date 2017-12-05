using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBehaviour : MonoBehaviour {

    public float angle = 0f;
    public bool randomAngle = false;
    public float randMin = 1f;
    public float randMax = 6f;

    public float period = 0.75f;

    private bool floatBehaviourActive = true;
    private float timePeriod;

	// Use this for initialization
	void Start () {
        timePeriod = 0f;
        if (randomAngle)
            angle = Random.Range(randMin, randMax) * (Random.Range(0, 2) * 2 - 1);
	}

    public void EnableFloatBehaviour()
    {
        floatBehaviourActive = true;
    }

    public void DisableFloatBehaviour()
    {
        floatBehaviourActive = false;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
	
	// Update is called once per frame
	void Update () {

        if (!Mathf.Approximately(0f, angle) && floatBehaviourActive)
        {
            timePeriod = timePeriod + Time.deltaTime;

            float phase = Mathf.Sin(timePeriod / period);
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, phase * angle));
        }
	}
}
