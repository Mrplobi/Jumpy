using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

    public float gravity;
    public float mass;
    private Vector3 acceleration;
    public Vector3 velocity;

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
        new_velocity = velocity + acceleration * Time.deltaTime;
        velocity = new_velocity;
    }

    private void Position()
    {
        Vector3 new_pos;
        new_pos = gameObject.transform.position + velocity * Time.deltaTime;
        gameObject.transform.position = new_pos;
    }

    private void Update()
    {
        Gravity();
        GVelocity();
        Position();
    }
}
