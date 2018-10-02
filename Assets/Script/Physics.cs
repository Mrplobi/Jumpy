using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

    public float gravity;
    public float mass;
    private Vector3 acceleration;
    public Vector3 velocity;
    private bool isGrounded;
    private bool isLocked = false;
    public float groundSpeed;
    public int numberJumpMax=2;
    public int numberJumpCurrent=0;
    public float groundedJumpImpulsion = 100;
    public float airJumpImpulsion = 100;
    public float airAcceleration;
    public Vector2 airFriction;
    public Vector2 groundFriction;
    
    private void Start()
    {
        acceleration = new Vector3(0, 0, 0);
        velocity = new Vector3(0, 0, 0);
    }

    private void Gravity()
    {
        acceleration.y = mass*gravity;
    }

    private void GVelocity()
    {
        Vector3 new_velocity;
        if (!isGrounded)
        { velocity.x= Mathf.Abs(velocity.x) - airFriction.x > 0 ? velocity.x - Mathf.Sign(velocity.x) * airFriction.x:0;
            velocity.y = Mathf.Abs(velocity.y) - airFriction.y > 0 ? velocity.y - Mathf.Sign(velocity.x)*airFriction.y : 0;

        }
          
        new_velocity = velocity + acceleration * Time.deltaTime;
        velocity = new_velocity;
    }

    private void Position()
    {
        Vector3 new_pos;
        new_pos = gameObject.transform.position + velocity * Time.deltaTime;
        gameObject.transform.position = new_pos;
    }
    public void Move(float horizontal)
    {
        if (!isLocked && isGrounded)
        {
            velocity.x = horizontal * groundSpeed;
        }
        if (!isGrounded)
        {
            acceleration.x = horizontal * airAcceleration;
        }

    }
    public void Jump()
    {
        Debug.Log("Bonjourno");
        if(isGrounded && numberJumpCurrent<numberJumpMax)
        { 
        acceleration.y += groundedJumpImpulsion;
            numberJumpCurrent++;
        }
        else if(!isGrounded && numberJumpCurrent < numberJumpMax)
        {
            acceleration.y += airJumpImpulsion;
        numberJumpCurrent++;
        }
    }
    private void Update()
    {
        Gravity();
        GVelocity();
        Position();
    }
}
