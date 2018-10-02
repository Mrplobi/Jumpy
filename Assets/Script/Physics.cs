using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

    public float gravity;
    public float mass;
    private bool isGrounded;
    private Vector3 acceleration;
    private Vector3 velocity;
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
        if (!isGrounded)
        {
            acceleration.y = mass*gravity;
        }
        
    }

    private void GVelocity()
    {
        Vector3 new_velocity;
        new_velocity = velocity + acceleration * Time.deltaTime;
        Velocity = new_velocity;
    }

    private void Position()
    {
        Vector3 new_pos;
        new_pos = gameObject.transform.position + Velocity * Time.deltaTime;
        gameObject.transform.position = new_pos;
    }

    private void Update()
    {
        Gravity();
        GVelocity();
        Position();
    }
}
