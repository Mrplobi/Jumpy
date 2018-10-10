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
    public bool DetectColision()
    {
        Physics physics = gameObject.GetComponent<Physics>();
        Vector3 velocity = physics.Velocity;
        Vector2 originDR = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x - 0.15f, gameObject.GetComponent<Collider2D>().bounds.min.y +0.01f);
        Vector2 originDL = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x + 0.15f, gameObject.GetComponent<Collider2D>().bounds.min.y +0.01f);
        Vector2 originUR = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x - 0.15f, gameObject.GetComponent<Collider2D>().bounds.max.y + 0.01f);
        Vector2 originUL = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x + 0.15f, gameObject.GetComponent<Collider2D>().bounds.max.y + 0.01f);
        Vector2 originRU = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x + 0.01f, gameObject.GetComponent<Collider2D>().bounds.max.y - 0.15f);
        Vector2 originRD = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x + 0.01f, gameObject.GetComponent<Collider2D>().bounds.min.y + 0.15f);
        Vector2 originLU = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x + 0.01f, gameObject.GetComponent<Collider2D>().bounds.max.y - 0.15f);
        Vector2 originLD = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x + 0.01f, gameObject.GetComponent<Collider2D>().bounds.min.y + 0.15f);
        RaycastHit2D hitDR = Physics2D.Raycast(originDR, -gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitDL = Physics2D.Raycast(originDL, -gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitUR = Physics2D.Raycast(originUR, gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitUL = Physics2D.Raycast(originUL, gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitRU = Physics2D.Raycast(originRU, gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitRD = Physics2D.Raycast(originRD, gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitLU = Physics2D.Raycast(originLU, -gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitLD = Physics2D.Raycast(originLD, -gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));

        
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
                    return true;
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
                    return true;
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
                return true;
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
                    return true;
                }
                else
                {
                    // Debug.Log("collisionUp");
                    Vector3 newSpeed = new Vector3(physics.Velocity.x, 0, 0);
                    physics.Velocity = newSpeed;
                    transform.position = new Vector3(transform.position.x, hitUL.collider.bounds.min.y - GetComponent<Collider2D>().bounds.size.y / 2 - 0.01f);
                    return true;
                }
            }
            else
            {
                // Debug.Log("collisionUp");
                Vector3 newSpeed = new Vector3(physics.Velocity.x, 0, 0);
                physics.Velocity = newSpeed;
                transform.position = new Vector3(transform.position.x, hitUR.collider.bounds.min.y - GetComponent<Collider2D>().bounds.size.y / 2 - 0.01f);
                return true;
            }
        }if (hitRU || hitRD)
        {
            Debug.Log("colisionright");
            if (hitRU)
            {
                if (hitRD)
                {
                    RaycastHit2D actualColision;
                    if (hitRU.distance < hitRD.distance)
                    {
                        actualColision = hitRU;
                    }
                    else
                    {
                        actualColision = hitRD;
                    }
                    //    Debug.Log("collisionRight");
                    Vector3 newSpeed = new Vector3(0, physics.Velocity.y, 0);
                    physics.Velocity = newSpeed;
                    transform.position = new Vector3(actualColision.collider.bounds.min.x - GetComponent<Collider2D>().bounds.size.x / 2 - 0.01f, transform.position.y);
                    InputButton actionJump = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
                    InputButton actionMove = GetComponent<InputManager>().SearchForFailedAction(GetComponent<InputManager>().MoveLeft, 10);
                    return true;
                }
                else
                {
                    //    Debug.Log("collisionRight");
                    Vector3 newSpeed = new Vector3(0, physics.Velocity.y, 0);
                    physics.Velocity = newSpeed;
                    transform.position = new Vector3(hitRU.collider.bounds.min.x - GetComponent<Collider2D>().bounds.size.x / 2 - 0.01f, transform.position.y);
                    InputButton actionJump = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
                    InputButton actionMove = GetComponent<InputManager>().SearchForFailedAction(GetComponent<InputManager>().MoveLeft, 10);
                    return true;
                }
            }
            else
            {
                //    Debug.Log("collisionRight");
                Vector3 newSpeed = new Vector3(0, physics.Velocity.y, 0);
                physics.Velocity = newSpeed;
                transform.position = new Vector3(hitRD.collider.bounds.min.x - GetComponent<Collider2D>().bounds.size.x / 2 - 0.01f, transform.position.y);
                InputButton actionJump = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
                InputButton actionMove = GetComponent<InputManager>().SearchForFailedAction(GetComponent<InputManager>().MoveLeft, 10);
                return true;
            }
        }

        if (hitLU || hitLD)
        {
            Debug.Log("colisionleft");
            if (hitLU)
            {
                if (hitLD)
                {
                    RaycastHit2D actualColision;
                    if (hitLU.distance < hitLD.distance)
                    {
                        actualColision = hitLU;
                    }
                    else
                    {
                        actualColision = hitLD;
                    }
                    //  Debug.Log("collisionLeft");
                    Vector3 newSpeed = new Vector3(0, physics.Velocity.y, 0);
                    physics.Velocity = newSpeed;
                    transform.position = new Vector3(actualColision.collider.bounds.max.x + GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f, transform.position.y);
                    InputButton actionJump = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
                    InputButton actionMove = GetComponent<InputManager>().SearchForFailedAction(GetComponent<InputManager>().MoveRight, 10);
                    return true;
                }
                else
                {
                    //  Debug.Log("collisionLeft");
                    Vector3 newSpeed = new Vector3(0, physics.Velocity.y, 0);
                    physics.Velocity = newSpeed;
                    transform.position = new Vector3(hitLU.collider.bounds.max.x + GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f, transform.position.y);
                    InputButton actionJump = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
                    InputButton actionMove = GetComponent<InputManager>().SearchForFailedAction(GetComponent<InputManager>().MoveRight, 10);
                    return true;
                }
            }
            else
            {
                //  Debug.Log("collisionLeft");
                Vector3 newSpeed = new Vector3(0, physics.Velocity.y, 0);
                physics.Velocity = newSpeed;
                transform.position = new Vector3(hitLD.collider.bounds.max.x + GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f, transform.position.y);
                InputButton actionJump = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
                InputButton actionMove = GetComponent<InputManager>().SearchForFailedAction(GetComponent<InputManager>().MoveRight, 10);
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
       // DetectColision();
    }
}

