using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

    public float gravity;
    public float mass;
    [SerializeField]
    private Vector3 acceleration;
    [SerializeField]
    private Vector3 extraImpulsion;
    [SerializeField]
    private Vector3 velocity;
    private bool isGrounded;
    private bool isLocked = false;
    public float groundSpeed;
    public int numberJumpMax=2;
    public int numberJumpCurrent=0;
    public float groundedJumpSpeed = 100;
    public float airJumpSpeed = 100;
    public float airAcceleration;
    public Vector2 airFriction;
    public Vector2 groundFriction;
    public Vector3 Velocity
    {
        get
        {
            return velocity;
        }

        set
        {
            velocity = value;
        }
    }

    
    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }

        set
        {
            isGrounded = value;
        }
    }

    private void Start()
    {
        acceleration = new Vector3(0, 0, 0);
        Velocity = new Vector3(0, 0, 0);
    }

    private void Gravity()
    {
        acceleration.y = 0;
        if (!isGrounded)
        {
            acceleration.y = mass*gravity + extraImpulsion.y;
            extraImpulsion.y = 0;
        }
        
    }

    private void GVelocity()
    {
        Vector3 new_velocity;
        if (!isGrounded)
        { velocity.x= Mathf.Abs(velocity.x) - airFriction.x  * Mathf.Abs(velocity.x) > 0 ? velocity.x - Mathf.Sign(velocity.x) * airFriction.x * Mathf.Abs(velocity.x) : 0;
            velocity.y = Mathf.Abs(velocity.y) - airFriction.y * Mathf.Abs(velocity.y) > 0 ? velocity.y - Mathf.Sign(velocity.y)*airFriction.y * Mathf.Abs(velocity.y) : 0;

        }
          
        new_velocity = velocity + acceleration * Time.deltaTime;
        Velocity = new_velocity;
    }

    private void Position()
    {
        Vector3 new_pos;
        new_pos = gameObject.transform.position + Velocity * Time.deltaTime;
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
            velocity.y = groundedJumpSpeed;
            numberJumpCurrent++;
        }
        else if(!isGrounded && numberJumpCurrent < numberJumpMax)
        {
            velocity.y = airJumpSpeed;
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
