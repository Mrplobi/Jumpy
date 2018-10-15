using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour {


    /*
    Vector2 originDR;
    Vector2 originDL;
    Vector2 originUR;
    Vector2 originUL;
    Vector2 originRU;
    Vector2 originRD;
    Vector2 originLU;
    Vector2 originLD;
    RaycastHit2D hitDR;
    RaycastHit2D hitDL;
    RaycastHit2D hitUR;
    RaycastHit2D hitUL;
    RaycastHit2D hitRU;
    RaycastHit2D hitRD;
    RaycastHit2D hitLU;
    RaycastHit2D hitLD;

    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;

    public void DetectColision()  
    {

        Physics physics = gameObject.GetComponent<Physics>();
        Vector3 velocity = physics.Velocity;
        Vector3 newSpeed = velocity;
        Vector3 newPosition = transform.position;
        
        originDR = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x - 0.1f, gameObject.GetComponent<Collider2D>().bounds.min.y);
        originDL = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x + 0.1f, gameObject.GetComponent<Collider2D>().bounds.min.y);
        originUR = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x - 0.1f, gameObject.GetComponent<Collider2D>().bounds.max.y);
        originUL = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x + 0.1f, gameObject.GetComponent<Collider2D>().bounds.max.y);
        originRU = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x, gameObject.GetComponent<Collider2D>().bounds.max.y - 0.1f);
        originRD = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x, gameObject.GetComponent<Collider2D>().bounds.min.y + 0.1f);
        originLU = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x, gameObject.GetComponent<Collider2D>().bounds.max.y - 0.1f);
        originLD = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x, gameObject.GetComponent<Collider2D>().bounds.min.y + 0.1f);
        hitDR = Physics2D.Raycast(originDR, -gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        hitDL = Physics2D.Raycast(originDL, -gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        hitUR = Physics2D.Raycast(originUR, gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        hitUL = Physics2D.Raycast(originUL, gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        hitRU = Physics2D.Raycast(originRU, gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        hitRD = Physics2D.Raycast(originRD, gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        hitLU = Physics2D.Raycast(originLU, -gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        hitLD = Physics2D.Raycast(originLD, -gameObject.transform.right,  velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));

        
        if (hitDR || hitDL)
        {
            Debug.Log("colision down");
            down = true;
            if (hitDL)
            {
                if (hitDR)
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
                    newSpeed.y = 0;
                    physics.numberJumpCurrent = 0;
                    newPosition.x = newPosition.x + velocity.x * Time.deltaTime;
                    newPosition.y = actualColision.collider.bounds.max.y + GetComponent<Collider2D>().bounds.size.y / 2;
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
                    newSpeed.y = 0;
                    physics.numberJumpCurrent = 0;
                    newPosition.x = newPosition.x + velocity.x * Time.deltaTime;
                    newPosition.y = hitDL.collider.bounds.max.y + GetComponent<Collider2D>().bounds.size.y / 2;
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
                newSpeed.y = 0;
                physics.numberJumpCurrent = 0;
                newPosition.x = newPosition.x + velocity.x * Time.deltaTime;
                newPosition.y = hitDR.collider.bounds.max.y + GetComponent<Collider2D>().bounds.size.y / 2;
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
            down = false;
        }
        
        if (hitUL || hitUR)
        {
            Debug.Log("colision up");
            up = true;
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
                    newSpeed.y = 0;
                    newPosition.x = newPosition.x + velocity.x * Time.deltaTime; 
                    newPosition.y = actualColision.collider.bounds.min.y - GetComponent<Collider2D>().bounds.size.y / 2 - 0.01f;
                }
                else
                {
                    newSpeed.y = 0;
                    newPosition.x = newPosition.x + velocity.x * Time.deltaTime;
                    newPosition.y = hitUL.collider.bounds.min.y - GetComponent<Collider2D>().bounds.size.y / 2 - 0.01f;
                }
            }
            else
            {
                newSpeed.y = 0;
                newPosition.x = newPosition.x + velocity.x * Time.deltaTime;
                newPosition.y = hitUR.collider.bounds.min.y - GetComponent<Collider2D>().bounds.size.y / 2 - 0.01f;
            }
        }
        else
        {
            up = false;
        }

        if (hitRU || hitRD)
        {
            Debug.Log("colisionright");
            right = true;
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
                    newSpeed.x = 0;
                    newPosition.x = actualColision.collider.bounds.min.x - GetComponent<Collider2D>().bounds.size.x / 2 - 0.01f;
                    newPosition.y = newPosition.y + velocity.y * Time.deltaTime;
                }
                else
                {
                    newSpeed.x = 0;
                    newPosition.x = hitRU.collider.bounds.min.x - GetComponent<Collider2D>().bounds.size.x / 2 - 0.01f;
                    newPosition.y = newPosition.y + velocity.y * Time.deltaTime;
                }
            }
            else
            {
                newSpeed.x = 0;
                newPosition.x = hitRD.collider.bounds.min.x - GetComponent<Collider2D>().bounds.size.x / 2 - 0.01f;
                newPosition.y = newPosition.y + velocity.y * Time.deltaTime;
            }
        }
        else
        {
            right = false;
        }

        if (hitLU || hitLD)
        {
            Debug.Log("colisionleft");
            left = true;
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
                    newSpeed.x = 0;
                    newPosition.x = actualColision.collider.bounds.max.x + GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f;
                    newPosition.y = transform.position.y + velocity.y * Time.deltaTime;
                }
                else
                {
                    newSpeed.x = 0;
                    newPosition.x = hitLU.collider.bounds.max.x + GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f;
                    newPosition.y = transform.position.y + velocity.y * Time.deltaTime;
                }
            }
            else
            {
                newSpeed.x = 0;
                newPosition.x = hitLD.collider.bounds.max.x + GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f;
                newPosition.y = transform.position.y + velocity.y * Time.deltaTime;
            }
        }
        else
        {
            left = false;
        }
        physics.Velocity = newSpeed;
        transform.position = newPosition;
    }

    */


    Vector2 upLeft;
    Vector2 downLeft;
    Vector2 downRight;
    Vector2 upRight;

    RaycastHit2D hitDR;
    RaycastHit2D hitDL;
    RaycastHit2D hitUR;
    RaycastHit2D hitUL;

    public void DetectColision() {

        Physics physics = gameObject.GetComponent<Physics>();
        Vector3 velocity = physics.Velocity;
        Vector3 newSpeed = velocity;
        Vector3 newPosition = transform.position;

        newPosition.x += velocity.x * Time.deltaTime;
        newPosition.y += velocity.y * Time.deltaTime;

        upLeft = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x, gameObject.GetComponent<Collider2D>().bounds.max.y);
        downLeft = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x, gameObject.GetComponent<Collider2D>().bounds.min.y);
        downRight = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x, gameObject.GetComponent<Collider2D>().bounds.min.y);
        upRight = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x, gameObject.GetComponent<Collider2D>().bounds.max.y);

        hitUL = Physics2D.Raycast(upLeft, velocity, velocity.magnitude * Time.deltaTime, LayerMask.GetMask("Environment"));
        hitDL = Physics2D.Raycast(downLeft, velocity, velocity.magnitude * Time.deltaTime, LayerMask.GetMask("Environment"));
        hitDR = Physics2D.Raycast(downRight, velocity, velocity.magnitude * Time.deltaTime, LayerMask.GetMask("Environment"));
        hitUR = Physics2D.Raycast(upRight, velocity, velocity.magnitude * Time.deltaTime, LayerMask.GetMask("Environment"));

        if (velocity.y <= 0)
        {
            if (hitDR && hitDR.collider.bounds.max.y - 0.01f < GetComponent<Collider2D>().bounds.min.y)
            {
                Debug.Log("DR");
                physics.IsGrounded = true;
                physics.numberJumpCurrent = 0;
                newSpeed.y = 0;
                newPosition.y = Mathf.Max(hitDR.collider.bounds.max.y + GetComponent<Collider2D>().bounds.size.y / 2, newPosition.y);
                //check if jump buffered
                InputButton action = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
                if (action != null)
                {
                    action.Invoke();
                }
            }
            if (hitDL && hitDL.collider.bounds.max.y -0.01f  < GetComponent<Collider2D>().bounds.min.y)
            {
               Debug.Log("DDL");
                physics.IsGrounded = true;
                physics.numberJumpCurrent = 0;
                newSpeed.y = 0;         
                newPosition.y = Mathf.Max(hitDL.collider.bounds.max.y + GetComponent<Collider2D>().bounds.size.y / 2, newPosition.y);
                //check if jump buffered
                InputButton action = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
                if (action != null)
                {
                    action.Invoke();
                }
            }
            
            if (!hitDL && !hitDR)
            {
                physics.IsGrounded = false;
            }
            
        }

        if (velocity.y >= 0)
        {
            if (hitUR && hitUR.collider.bounds.min.y > GetComponent<Collider2D>().bounds.max.y && hitUR.collider.tag != "Crossable")
            {
               Debug.Log("UR");
                newSpeed.y = 0;
                newPosition.y = Mathf.Min(hitUR.collider.bounds.min.y - GetComponent<Collider2D>().bounds.size.y / 2 - 0.01f , newPosition.y);
            }
            if (hitUL && hitUL.collider.bounds.min.y > GetComponent<Collider2D>().bounds.max.y && hitUL.collider.tag != "Crossable")
            {
               Debug.Log("UL");
                newSpeed.y = 0;
                newPosition.y = Mathf.Min(hitUL.collider.bounds.min.y - GetComponent<Collider2D>().bounds.size.y / 2 - 0.01f, newPosition.y);
            }
        }

        if (velocity.x <= 0)
        {
            if (hitUL && hitUL.collider.bounds.max.x -0.01f < GetComponent<Collider2D>().bounds.min.x)
            {
               Debug.Log("UL");
                newSpeed.x = 0;
                newPosition.x = Mathf.Max(hitUL.collider.bounds.max.x + GetComponent<Collider2D>().bounds.size.x / 2, newPosition.x);
            }
            if (hitDL && hitDL.collider.bounds.max.x -0.01f < GetComponent<Collider2D>().bounds.min.x)
            {
               Debug.Log("LDL");
                newSpeed.x = 0;
                newPosition.x = Mathf.Max(hitDL.collider.bounds.max.x + GetComponent<Collider2D>().bounds.size.x / 2, newPosition.x);
                if(!physics.IsGrounded)
                {
                    physics.IsClingingLeft = true;
                }
                else
                {
                    physics.IsClingingLeft = false;
                }
            }
        }

        if (velocity.x >= 0)
        {
            if (hitUR && hitUR.collider.bounds.min.x +0.01f > GetComponent<Collider2D>().bounds.max.x)
            {
               Debug.Log("UR");
                newSpeed.x = 0;
                newPosition.x = Mathf.Min(hitUR.collider.bounds.min.x - GetComponent<Collider2D>().bounds.size.x / 2, newPosition.x);
            }
            if (hitDR && hitDR.collider.bounds.min.x +0.01f > GetComponent<Collider2D>().bounds.max.x)
            {
               Debug.Log("DR");
                newSpeed.x = 0;
                newPosition.x = Mathf.Min(hitDR.collider.bounds.max.x - GetComponent<Collider2D>().bounds.size.x / 2, newPosition.x);
                if (!physics.IsGrounded)
                {
                    physics.IsClingingRight = true;
                }
                else
                {
                    physics.IsClingingRight = false;
                }
            }

        }

        physics.Velocity = newSpeed;
        transform.position = newPosition;
    }
}

