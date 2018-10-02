using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour {

    private void DetectColision()
    {
        Physics physics = gameObject.GetComponent<Physics>();
        Vector3 velocity = physics.Velocity;
        //Vector2 direction = new Vector2(gameObject.GetComponent<Physics>().Velocity.x, gameObject.GetComponent<Physics>().Velocity.y);
        Vector2 origin = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - gameObject.GetComponent<SpriteRenderer>().size.y / 2);
        RaycastHit2D hitD = Physics2D.Raycast(origin, -gameObject.transform.up, velocity.y * Time.deltaTime);
        RaycastHit2D hitU = Physics2D.Raycast(origin, gameObject.transform.up, velocity.y * Time.deltaTime);
        RaycastHit2D hitR = Physics2D.Raycast(origin, gameObject.transform.right, velocity.x * Time.deltaTime);
        RaycastHit2D hitL = Physics2D.Raycast(origin, -gameObject.transform.up, velocity.x * Time.deltaTime);
        if (hitD)
        {
            Debug.Log("collision");
            physics.IsGrounded = true;
            Vector3 impact = new Vector3(hitD.point.x, hitD.point.y + gameObject.GetComponent<SpriteRenderer>().size.y /2, 0);
            gameObject.transform.position = impact;
            Vector3 newSpeed = new Vector3(physics.Velocity.x, 0, 0);
            physics.Velocity = newSpeed;
        }
    }

    private void FixedUpdate()
    {
        DetectColision();
    }
}

