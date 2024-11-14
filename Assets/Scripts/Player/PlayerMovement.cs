using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;
    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Import RB2D dan rumus-rumus soal
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = (-2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed));
        stopFriction = (-2 * maxSpeed / (timeToStop * timeToStop));
    }

    void Update() // Update rutin
    {
        stopClamp = stopClamp / 5;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveDirection = new Vector2(horizontalInput, verticalInput);
    }

    public void Move()
    {
        Vector2 currentVelocity = rb.velocity;
        Vector2 friction = GetFriction();

        // Logic displit menjadi 2 bagian (X dan Y)
        if (moveDirection.x != 0)
        {
            currentVelocity.x += moveDirection.x * moveVelocity.x * Time.fixedDeltaTime;
            currentVelocity.x += friction.x * Time.fixedDeltaTime;
        }
        else
        {
            // Stopping Logic
            float stoppingPower = Mathf.Max(stopFriction.x * Time.fixedDeltaTime, 0.1f); // Minimal
            float targetVelocity = 0f;
            
            // Stopping saat kecepatan tinggi
            if (Mathf.Abs(currentVelocity.x) > maxSpeed.x * 0.5f)
            {
                stoppingPower *= 1.5f;
            }
            
            currentVelocity.x = Mathf.Lerp(currentVelocity.x, targetVelocity, stoppingPower);
            
            if (Mathf.Abs(currentVelocity.x) < stopClamp.x)
            {
                currentVelocity.x = 0;
            }
        }

        if (moveDirection.y != 0)
        {
            currentVelocity.y += moveDirection.y * moveVelocity.y * Time.fixedDeltaTime;
            currentVelocity.y += friction.y * Time.fixedDeltaTime;
        }
        else
        {
            float stoppingPower = Mathf.Max(stopFriction.y * Time.fixedDeltaTime, 0.1f); 
            float targetVelocity = 0f;
            
            if (Mathf.Abs(currentVelocity.y) > maxSpeed.y * 0.5f)
            {
                stoppingPower *= 1.5f; 
            }
            
            currentVelocity.y = Mathf.Lerp(currentVelocity.y, targetVelocity, stoppingPower);
            
            if (Mathf.Abs(currentVelocity.y) < stopClamp.y)
            {
                currentVelocity.y = 0;
            }
        }

        // Clamp 
        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -maxSpeed.x, maxSpeed.x);
        currentVelocity.y = Mathf.Clamp(currentVelocity.y, -maxSpeed.y, maxSpeed.y);

        rb.velocity = currentVelocity;
    }

    public Vector2 GetFriction()
    {
        Vector2 friction = Vector2.zero;
        
        // Jika bergerak = moveFriction, jika tidak = stopFriction
        if (moveDirection.x != 0)
        {
            friction.x = moveFriction.x * Mathf.Sign(rb.velocity.x);
            if (Mathf.Abs(rb.velocity.x) > maxSpeed.x)
            {
                friction.x *= 2f; // 2x friction saat kecepatan melebihi maxSpeed
            }
        }
        else
        {
            friction.x = stopFriction.x * Mathf.Sign(rb.velocity.x);
        }

        if (moveDirection.y != 0)
        {
            friction.y = moveFriction.y * Mathf.Sign(rb.velocity.y);
            if (Mathf.Abs(rb.velocity.y) > maxSpeed.y)
            {
                friction.y *= 2f;
            }
        }
        else
        {
            friction.y = stopFriction.y * Mathf.Sign(rb.velocity.y);
        }

        return friction;
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0; // Jika kecepatan > 0, maka bergerak 
    }
}