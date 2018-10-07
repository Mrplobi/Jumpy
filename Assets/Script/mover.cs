using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverH : MonoBehaviour {

	public float platformSpeed;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position + platformSpeed)
	}
}
