using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.GetComponent<Physics>() == null)
        {
            Debug.Log("no physics found");
        }
        else
        {
            Debug.Log("found Physics");
            Vector3 newPos = new Vector3(0, 0, 0);
            collision.transform.position = newPos;
        }
    }
}

