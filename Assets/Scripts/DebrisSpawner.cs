using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour {

    public List<GameObject> debrisLst;

	// Use this for initialization
	void Start () {
        debrisLst = new List<GameObject>();
	}

    public void SpawnDebris(Transform loc)
    {
        Debug.Log("Spawning Debris at " + loc.position);
        GameObject debrisObj = debrisLst[Random.Range(0, debrisLst.Count)];
        Instantiate(debrisObj, loc.position, Quaternion.identity);

        /*
        // Attempting to add a random rotation on initial spawn so each debris spawn looks unique
        foreach (Transform child in debrisObj.transform)
        {
            Debug.Log(child.transform.gameObject.name);
            Debug.Log(child.transform.localRotation);
            child.transform.localRotation = Quaternion.Euler(new Vector3(0,0, Random.rotation.z)) * child.transform.rotation;
            Debug.Log(child.transform.localRotation);
            Debug.Log(" ");
        }
        */
        
    }

}
