using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float platformSpeed;
    public Vector3 startingPoint;
    public Vector3 destination;
    private bool isComing = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.position.x < transform.position.x)
        {
            collision.transform.position = new Vector3(GetComponent<BoxCollider2D>().bounds.min.x - collision.GetComponent<BoxCollider2D>().bounds.size.x / 2, collision.transform.position.y);
        }
        if (collision.transform.position.x > transform.position.x)
        {
            collision.transform.position = new Vector3(GetComponent<BoxCollider2D>().bounds.max.x + collision.GetComponent<BoxCollider2D>().bounds.size.x / 2, collision.transform.position.y);
        }
    }

    void MovePlateform()
    {
        if (transform.position.x + platformSpeed * Time.deltaTime > destination.x)
        {
            isComing = true;
        }
        if (transform.position.x - platformSpeed * Time.deltaTime < startingPoint.x)
        {
            isComing = false;
        }
        if (isComing == false)
        {
            transform.position = new Vector3(transform.position.x + platformSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - platformSpeed * Time.deltaTime, transform.position.y);
        }
        
    }

    private void Update()
    {
        MovePlateform();
    }
}
