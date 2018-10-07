using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButton {
    public delegate bool InputAction();
    public InputAction actualAction;
    public bool actionSucceeded;
    // Use this for initialization
    public InputButton(InputAction inputAction)
    {
        this.actualAction = inputAction;
    }
    public void Invoke()
    {
        actionSucceeded=actualAction.Invoke();
    }
}
