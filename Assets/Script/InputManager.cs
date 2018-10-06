using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public Physics player;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
       // Debug.Log(Input.GetAxis("Horizontal"));
        player.Move(Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump"))
        { player.Jump(); }
       
		
	}
}
