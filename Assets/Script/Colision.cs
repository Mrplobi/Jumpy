using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(GetComponent<Collider2D>().bounds.min.y< other.bounds.max.y && GetComponent<Collider2D>().bounds.max.y > other.bounds.max.y)//On arrive par au dessus
        {
            Physics physics = gameObject.GetComponent<Physics>();

            Debug.Log("BIPBIP DETECTED");
            physics.IsGrounded = true;
            physics.numberJumpCurrent = 0;
            physics.Velocity = new Vector3(physics.Velocity.x, 0, 0);
            transform.position = new Vector3(transform.position.x, other.bounds.max.y+ GetComponent<Collider2D>().bounds.size.y/2);
        }

    }
    public void DetectColision()
    {
        Physics physics = gameObject.GetComponent<Physics>();
        //float size = gameObject.GetComponent<Collider2D>().Distance() * gameObject.transform.localScale.y;
        Vector3 velocity = physics.Velocity;
        //Vector2 direction = new Vector2(gameObject.GetComponent<Physics>().Velocity.x, gameObject.GetComponent<Physics>().Velocity.y);
        Vector2 originDR = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x + 0.01f, gameObject.GetComponent<Collider2D>().bounds.min.y + 0.01f);
        Vector2 originDL = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x + 0.01f, gameObject.GetComponent<Collider2D>().bounds.min.y + 0.01f);
        Vector2 originUR = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x + 0.01f, gameObject.GetComponent<Collider2D>().bounds.max.y + 0.01f);
        Vector2 originUL = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x + 0.01f, gameObject.GetComponent<Collider2D>().bounds.max.y + 0.01f);
        Vector2 originRU = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x + 0.01f, gameObject.GetComponent<Collider2D>().bounds.max.y + 0.01f);
        Vector2 originRD = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x + 0.01f, gameObject.GetComponent<Collider2D>().bounds.min.y + 0.01f);
        Vector2 originLU = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x + 0.01f, gameObject.GetComponent<Collider2D>().bounds.max.y + 0.01f);
        Vector2 originLD = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x + 0.01f, gameObject.GetComponent<Collider2D>().bounds.min.y + 0.01f);
        RaycastHit2D hitDR = Physics2D.Raycast(originDR, -gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitDL = Physics2D.Raycast(originDL, -gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitUR = Physics2D.Raycast(originUR, gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitUL = Physics2D.Raycast(originUL, gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitRR = Physics2D.Raycast(originRU, gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitRL = Physics2D.Raycast(originRD, gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitLR = Physics2D.Raycast(originLU, -gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitLL = Physics2D.Raycast(originLD, -gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        if (hitDR || hitDL)
        {
            if (hitDL.collider)
            {
                if (hitDR.collider)
                {
                    RaycastHit2D actualColision;
                    if (hitDL.distance < hitDR.distance)
                    {
                        actualColision = hitDL;
                    }
                    else
                    {
                        actualColision = hitDR;
                    }
                    physics.IsGrounded = true;
                    Vector3 newSpeed = new Vector3(physics.Velocity.x, 0, 0);
                    physics.Velocity = newSpeed;
                    physics.numberJumpCurrent = 0;
                    transform.position = new Vector3(transform.position.x, actualColision.collider.bounds.max.y + GetComponent<Collider2D>().bounds.size.y / 2);
                    //check if jump buffered
                    InputButton action = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
                    if (action != null)
                    {
                        action.Invoke();
                    }
                }
                else
                {
                    physics.IsGrounded = true;
                    Vector3 newSpeed = new Vector3(physics.Velocity.x, 0, 0);
                    physics.Velocity = newSpeed;
                    physics.numberJumpCurrent = 0;
                    transform.position = new Vector3(transform.position.x, hitDL.collider.bounds.max.y + GetComponent<Collider2D>().bounds.size.y / 2);
                    //check if jump buffered
                    InputButton action = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
                    if (action != null)
                    {
                        action.Invoke();
                    }
                }
            }
            else
            {
                physics.IsGrounded = true;
                Vector3 newSpeed = new Vector3(physics.Velocity.x, 0, 0);
                physics.Velocity = newSpeed;
                physics.numberJumpCurrent = 0;
                transform.position = new Vector3(transform.position.x, hitDR.collider.bounds.max.y + GetComponent<Collider2D>().bounds.size.y / 2);
                //check if jump buffered
                InputButton action = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
                if (action != null)
                {
                    action.Invoke();
                }
            }
        }
        else
        {
            physics.IsGrounded = false;
        }
        if (hitUL || hitUR)
        {
            if (hitUL)
            {
                if (hitUR)
                {
                    RaycastHit2D actualColision;
                    if (hitUL.distance < hitUR.distance)
                    {
                        actualColision = hitUL;
                    }
                    else
                    {
                        actualColision = hitUR;
                    }
                    // Debug.Log("collisionUp");
                    Vector3 newSpeed = new Vector3(physics.Velocity.x, 0, 0);
                    physics.Velocity = newSpeed;
                    transform.position = new Vector3(transform.position.x, actualColision.collider.bounds.min.y - GetComponent<Collider2D>().bounds.size.y / 2 - 0.01f);
                }
                else
                {
                    // Debug.Log("collisionUp");
                    Vector3 newSpeed = new Vector3(physics.Velocity.x, 0, 0);
                    physics.Velocity = newSpeed;
                    transform.position = new Vector3(transform.position.x, hitUL.collider.bounds.min.y - GetComponent<Collider2D>().bounds.size.y / 2 - 0.01f);
                }
            }
            else
            {
                // Debug.Log("collisionUp");
                Vector3 newSpeed = new Vector3(physics.Velocity.x, 0, 0);
                physics.Velocity = newSpeed;
                transform.position = new Vector3(transform.position.x, hitUR.collider.bounds.min.y - GetComponent<Collider2D>().bounds.size.y / 2 - 0.01f);
            }
        }
        /*
        if (hitR)
        {
        //    Debug.Log("collisionRight");
            Vector3 newSpeed = new Vector3(0, physics.Velocity.y, 0);
            physics.Velocity = newSpeed;
            transform.position = new Vector3(hitR.collider.bounds.min.x - GetComponent<Collider2D>().bounds.size.x / 2 - 0.01f, transform.position.y);
            InputButton actionJump = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
            InputButton actionMove = GetComponent<InputManager>().SearchForFailedAction(GetComponent<InputManager>().MoveLeft, 10);

            if (actionJump != null && actionMove != null && !physics.IsGrounded)
            {
                Debug.Log("WallJUMP");
            }
        }
        if (hitL)
        {
          //  Debug.Log("collisionLeft");
            Vector3 newSpeed = new Vector3(0, physics.Velocity.y, 0);
            physics.Velocity = newSpeed;
            transform.position = new Vector3(hitL.collider.bounds.max.x + GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f, transform.position.y);
            InputButton actionJump = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
            InputButton actionMove = GetComponent<InputManager>().SearchForFailedAction(GetComponent<InputManager>().MoveRight, 10);

            if (actionJump != null && actionMove !=null && !physics.IsGrounded)
            {
                Debug.Log("WallJUMP");
            }
        }*/
    }

    private void Update()
    {
       // DetectColision();
    }
}

