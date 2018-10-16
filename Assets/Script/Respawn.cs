using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    public GameObject respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = respawnPoint.transform.position;
        collision.transform.position = respawnPoint.transform.GetComponent<Physics>().Velocity=new Vector3();
        collision.transform.position = respawnPoint.transform.GetComponent<Physics>().Acceleration=new Vector3();
    }
}
