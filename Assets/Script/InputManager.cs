using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public Physics player;
    public InputButton[] buffer;
    int bufferIndexIn=0;
    int bufferIndexOut=0;
    int bufferSizeMax = 20;
    // Use this for initialization
    void Start () {
        buffer = new InputButton[bufferSizeMax];
	}

    /// <summary>
    /// Returns the last Inputbutton that matches inputAction delegate and that failed
    /// </summary>
    /// <param name="inputAction"></param>
    /// <returns></returns>
	public InputButton SearchForFailedAction(InputButton.InputAction inputAction) //TODO
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// Used to facilitate equality of delegate
    /// </summary>
    /// <returns></returns>
    public bool MoveMethod()
    {
         return player.Move(Input.GetAxis("Horizontal")); 
    }
	// Update is called once per frame
	void LateUpdate () {
        // Debug.Log(Input.GetAxis("Horizontal"));
        
        buffer[bufferIndexIn]=new InputButton(MoveMethod);
        bufferIndexIn = (bufferIndexIn + 1) % bufferSizeMax;
        if (Input.GetButtonDown("Jump"))
        {   buffer[bufferIndexIn]= new InputButton(player.Jump);
            bufferIndexIn = (bufferIndexIn + 1) % bufferSizeMax;
        }


        //On dépile tout
        while(bufferIndexOut != bufferIndexIn) // c'est ici qu'on gèrerait les prio si il y en avait
        {
            buffer[bufferIndexOut].Invoke();
            bufferIndexOut = (bufferIndexOut + 1) % bufferSizeMax;
            // buffer.RemoveAt(0);
        }
        

    }
}
