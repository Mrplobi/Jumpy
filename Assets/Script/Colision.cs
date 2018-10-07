using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<Collider2D>().bounds.min.y < other.bounds.max.y && GetComponent<Collider2D>().bounds.max.y > other.bounds.max.y)//On arrive par au dessus
        {
            Physics physics = gameObject.GetComponent<Physics>();

            Debug.Log("BIPBIP DETECTED");
            physics.IsGrounded = true;
            physics.numberJumpCurrent = 0;
            physics.Velocity = new Vector3(physics.Velocity.x, 0, 0);
            transform.position = new Vector3(transform.position.x, other.bounds.max.y + GetComponent<Collider2D>().bounds.size.y / 2);
        }
    }
    public void DetectColision()
    {
        Physics physics = gameObject.GetComponent<Physics>();
        //float size = gameObject.GetComponent<Collider2D>().Distance() * gameObject.transform.localScale.y;
        Vector3 velocity = physics.Velocity;
        //Vector2 direction = new Vector2(gameObject.GetComponent<Physics>().Velocity.x, gameObject.GetComponent<Physics>().Velocity.y);
        Vector2 origin = new Vector2(gameObject.transform.position.x, gameObject.GetComponent<Collider2D>().bounds.min.y + 0.01f);
        Vector2 originU = new Vector2(gameObject.transform.position.x, gameObject.GetComponent<Collider2D>().bounds.max.y - 0.01f);
        Vector2 originL = new Vector2(gameObject.transform.position.x, gameObject.GetComponent<Collider2D>().bounds.min.y + 0.01f);
        Vector2 originR = new Vector2(gameObject.transform.position.x, gameObject.GetComponent<Collider2D>().bounds.min.y - 0.01f);
        RaycastHit2D hitD = Physics2D.Raycast(origin, -gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitU = Physics2D.Raycast(originU, gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitR = Physics2D.Raycast(originL, gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitL = Physics2D.Raycast(originR, -gameObject.transform.up, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        if (hitD)
        {
            //  Debug.Log("collision");
            physics.IsGrounded = true;
            /* Vector3 impact = new Vector3(hitD.point.x, hitD.point.y+
                 gameObject.GetComponent<Collider2D>().bounds.size.y/2, 0);
             gameObject.transform.position = impact;*/

            Vector3 newSpeed = new Vector3(physics.Velocity.x, 0, 0);
            physics.Velocity = newSpeed;
            physics.numberJumpCurrent = 0;
            transform.position = new Vector3(transform.position.x, hitD.collider.bounds.max.y + GetComponent<Collider2D>().bounds.size.y / 2);
            //check if jump buffered
            InputButton action = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
            if (action != null)
            {
                action.Invoke();

            }
        }
        else
        {
            physics.IsGrounded = false;
        }
    }

    private void Update()
    {
        // DetectColision();
    }
}

