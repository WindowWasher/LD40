using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorter : MonoBehaviour {

    //private Camera cam;

	// Use this for initialization
	void Start () {
        //cam = Camera.main;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        GetComponent<SpriteRenderer>().sortingOrder = (int) Mathf.RoundToInt(transform.position.y * 100f) * -1;
        //GetComponent<SpriteRenderer>().sortingOrder = (int)cam.WorldToScreenPoint(GetComponent<SpriteRenderer>().bounds.min).y * -1;
    }
}
