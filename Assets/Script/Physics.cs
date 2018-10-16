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
    private Coroutine coroutineDragging;
    public InputManager manager;
    public Colision colision;
    private bool isClingingRight = false;
    private bool isClingingLeft = false;
    private float fastFallSpeed = 15f;
    public float wallJumpVelocityX = 6;
    public float wallJumpVelocityY = 6;
    private bool isGrounded;
    private bool isLocked = false;
    public float groundSpeed;
    public int numberJumpMax=2;
    public int numberJumpCurrent=0;
    public float groundedJumpSpeed = 100;
    public float tetherSpeed;
    public float tetherThreshHold = 3;
    public float tetherDistance = 10;
    public float airJumpSpeed = 100;
    public float airAcceleration;
    public Vector2 airFriction;
    public Vector2 groundFriction;
    public float accelerationSlide=12f;
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
    public bool IsClingingLeft
    {
        get
        {
            return isClingingLeft;
        }

        set
        {
            isClingingLeft = value;
        }
    }

    public bool IsClingingRight
    {
        get
        {
            return isClingingRight;
        }

        set
        {
            isClingingRight = value;
        }
    }

    public Vector3 Acceleration
    {
        get
        {
            return acceleration;
        }

        set
        {
            acceleration = value;
        }
    }

    public IEnumerator GetDragged(GameObject obj)
    {


        while ((obj.transform.position - transform.position).magnitude > tetherSpeed * Time.deltaTime * tetherThreshHold && tetherSpeed > 0)
        {
            GetComponent<LineRenderer>().positionCount = 2;

            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, obj.transform.position);
            Velocity = (obj.transform.position - transform.position).normalized * tetherSpeed;
            yield return null;
        }
        GetComponent<LineRenderer>().positionCount = 0;
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
            numberJumpCurrent = Mathf.Min(numberJumpCurrent, numberJumpMax - 1);
            Debug.Log("START tether");
            coroutineDragging = StartCoroutine(GetDragged(closestHit.gameObject));


            return true;
        }

    }
    private void Start()
    {
        Acceleration = new Vector3(0, 0, 0);
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
        {

            velocity.x= Mathf.Abs(velocity.x) - airFriction.x  * Mathf.Abs(velocity.x) > 0 ? velocity.x - Mathf.Sign(velocity.x) * airFriction.x * Mathf.Abs(velocity.x) : 0;
            if(velocity.y<0 && (isClingingLeft || isClingingRight))
            {
                acceleration.y += accelerationSlide;
            }
            velocity.y = Mathf.Abs(velocity.y) - airFriction.y * Mathf.Abs(velocity.y) > 0 ? velocity.y - Mathf.Sign(velocity.y)*airFriction.y * Mathf.Abs(velocity.y) : 0;
            
        }
          
        new_velocity = velocity + Acceleration * Time.deltaTime;
        Velocity = new_velocity;
    }

    private void Position()
    {
        Vector3 new_pos;
        new_pos = gameObject.transform.position + Velocity * Time.deltaTime;
        gameObject.transform.position = new_pos;
    }
    public bool FastFall()
    {
        if (isClingingLeft || isClingingRight)
        {
            isClingingLeft = false;
            isClingingRight = false;
            return true;
        }
        else if (!isGrounded)
        {
            Acceleration = new Vector3(velocity.x, - fastFallSpeed);

            return true;
        }

        return false;
    }
    public bool Move(float horizontal) //TODO return false if collision
    {
        if (horizontal > 0 && isClingingLeft)
        {
            isClingingLeft = false;
        }
        if (horizontal < 0 && isClingingRight)
        {
            isClingingRight = false;
        }
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
    public bool Jump()
    {
        if (isGrounded && numberJumpCurrent < numberJumpMax)
        {
            velocity.y = groundedJumpSpeed;
            isGrounded = false;
            numberJumpCurrent++;
            return true;
        }
        else if (!isGrounded && !isClingingLeft && !IsClingingRight && numberJumpCurrent < numberJumpMax)
        {
            velocity.y = airJumpSpeed;
            numberJumpCurrent++;
            return true;
        }
        else if (isClingingLeft)
        {
            numberJumpCurrent = 0;
            velocity.x = wallJumpVelocityX;
            velocity.y = wallJumpVelocityY;
            IsClingingLeft = false;
            return true;
        }
        else if (isClingingRight)
        {
            IsClingingRight = false;
            numberJumpCurrent = 1;
            velocity.x = -wallJumpVelocityX;
            velocity.y = wallJumpVelocityY;
            return true;
        }
        return false;
    }
    private void LateUpdate()
    {
        manager.UpdateInput();
        Gravity();
        GVelocity();
        colision.DetectColision();
    }
}
