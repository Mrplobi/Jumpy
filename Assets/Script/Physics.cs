using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{

    public float gravity;
    public float mass;
    [SerializeField]
    private Vector3 acceleration;
    [SerializeField]
    private Vector3 extraImpulsion;
    [SerializeField]
    private Vector3 velocity;
    private bool isGrounded;
    private Coroutine coroutineDragging;
    private bool isLocked = false;
    public float groundSpeed;
    public int numberJumpMax = 2;
    public int numberJumpCurrent = 0;
    public float groundedJumpSpeed = 100;
    public float airJumpSpeed = 100;
    public float airAcceleration;
    public float tetherSpeed;
    public float tetherThreshHold = 3;
    public float tetherDistance = 10;
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
            acceleration.y = mass * gravity + extraImpulsion.y;
            extraImpulsion.y = 0;
        }


    }

    private void GVelocity()
    {
        Vector3 new_velocity;
        if (!isGrounded)
        {
            velocity.x = Mathf.Abs(velocity.x) - airFriction.x * Mathf.Abs(velocity.x) > 0 ? velocity.x - Mathf.Sign(velocity.x) * airFriction.x * Mathf.Abs(velocity.x) : 0;
            velocity.y = Mathf.Abs(velocity.y) - airFriction.y * Mathf.Abs(velocity.y) > 0 ? velocity.y - Mathf.Sign(velocity.y) * airFriction.y * Mathf.Abs(velocity.y) : 0;

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
    public bool Move(float horizontal) //TODO return false if collision
    {
        if (!isLocked && isGrounded)
        {
            velocity.x = horizontal * groundSpeed;
        }
        if (!isGrounded)
        {
            acceleration.x = horizontal * airAcceleration;
        }
        return true;
    }
    public IEnumerator GetDragged(GameObject obj)
    {


        while ((obj.transform.position - transform.position).magnitude > tetherSpeed * Time.deltaTime * tetherThreshHold && tetherSpeed > 0)
        {
            Velocity = (obj.transform.position - transform.position).normalized * tetherSpeed;
            yield return null;
        }
        coroutineDragging = null;
    }
    public bool Tether()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, tetherDistance, LayerMask.GetMask("Tether"));
        float closestDistance = tetherDistance;
        Collider2D closestHit = null;
        Debug.Log("Size tether hits" + hits.Length);
        foreach (Collider2D hit in hits)//get closest and get dragged to it
        {
            Debug.Log("Tether distance" + (hit.transform.position - transform.position).magnitude);
            Debug.Log(hit.gameObject);
            if (hit.GetComponent<TetherPoint>() && (hit.transform.position - transform.position).magnitude < closestDistance)
            {
                closestHit = hit;
                closestDistance = (hit.transform.position - transform.position).magnitude;
            }

        }
        Debug.Log(closestHit);
        if (!closestHit) //Must check if another one though
        {
            Debug.Log("Tether failed");
            return false;
        }
        else
        {
            Debug.Log("START tether");
            coroutineDragging = StartCoroutine(GetDragged(closestHit.gameObject));
            return true;
        }

    }
    public bool Jump()
<<<<<<< HEAD
    {               
        if (isGrounded && numberJumpCurrent<numberJumpMax)
        { 
=======
    {


        if (isGrounded && numberJumpCurrent < numberJumpMax)
        {
>>>>>>> 1cda4c755b71799f3515ae76d85472679cc704e5
            velocity.y = groundedJumpSpeed;
            isGrounded = false;
            numberJumpCurrent++;
        }
        else if (!isGrounded && numberJumpCurrent < numberJumpMax)
        {
            velocity.y = airJumpSpeed;
            numberJumpCurrent++;
            return true;
        }
        return false;
    }
    private void Update()
<<<<<<< HEAD
    { 
=======
    {
>>>>>>> 1cda4c755b71799f3515ae76d85472679cc704e5
        Gravity();
        GVelocity();
        Position();
        GetComponent<Colision>().DetectColision();
<<<<<<< HEAD
       
=======
>>>>>>> 1cda4c755b71799f3515ae76d85472679cc704e5
    }
}
