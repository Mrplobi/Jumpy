using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Physics player;
    public InputButton[] buffer;
    int bufferIndexIn = 0;
    int bufferIndexOut = 0;
    int bufferSizeMax = 20;
    // Use this for initialization
    void Start()
    {
        buffer = new InputButton[bufferSizeMax];
    }
    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
    /// <summary>
    /// Returns the last Inputbutton that matches inputAction delegate and that failed
    /// </summary>
    /// <param name="inputAction"></param>
    /// <returns></returns>
	public InputButton SearchForFailedAction(InputButton.InputAction inputAction, int threshHold) //TODO
    {
        for (int i = 0; i < threshHold; i++)
        {
            //Debug.Log((bufferIndexIn - i) % bufferSizeMax);
            if (buffer[mod((bufferIndexIn - i), bufferSizeMax)] != null &&
                buffer[mod((bufferIndexIn - i), bufferSizeMax)].actualAction == inputAction &&
                !buffer[mod((bufferIndexIn - i), bufferSizeMax)].actionSucceeded)
            {
                return buffer[mod((bufferIndexIn - i), bufferSizeMax)];
            }
        }
        return null;
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
    void LateUpdate()
    {
        // Debug.Log(Input.GetAxis("Horizontal"));

        buffer[bufferIndexIn] = new InputButton(MoveMethod);
        bufferIndexIn = (bufferIndexIn + 1) % bufferSizeMax;
        if (Input.GetButtonDown("Jump"))
        {
            buffer[bufferIndexIn] = new InputButton(player.Jump);
            bufferIndexIn = (bufferIndexIn + 1) % bufferSizeMax;
        }
        if (Input.GetButtonDown("Tether"))
        {
            buffer[bufferIndexIn] = new InputButton(player.Tether);
            bufferIndexIn = (bufferIndexIn + 1) % bufferSizeMax;
        }


        //On dépile tout
        while (bufferIndexOut != bufferIndexIn) // c'est ici qu'on gèrerait les prio si il y en avait
        {
            buffer[bufferIndexOut].Invoke();
            bufferIndexOut = (bufferIndexOut + 1) % bufferSizeMax;
            // buffer.RemoveAt(0);
        }


    }
}
