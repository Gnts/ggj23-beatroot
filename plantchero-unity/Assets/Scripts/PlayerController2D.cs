using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController2D : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;

    bool facingRight = true;
    float moveDirection = 0;
    public bool isGrounded = false;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;
    public Animator animator;
    private Vector2 move_vector;
    private bool jump;
    public Transform throwTransform;
    public GameObject carrotFab;
    public GameObject potatoFab;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        //animator = GetComponent<Animator>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;

    }

    public void Movement(InputAction.CallbackContext ctx)
    {
        move_vector = ctx.ReadValue<Vector2>();
        moveDirection = move_vector.x;
        // if (isGrounded || r2d.velocity.magnitude < 0.01f && moveDirection < 0.05f)
        // {
        //     moveDirection = 0;
        // }

        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                var localScale = t.localScale;
                localScale = new Vector3(Mathf.Abs(localScale.x), localScale.y, transform.localScale.z);
                t.localScale = localScale;
                animator.SetBool ("facingRight", facingRight);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                var localScale = t.localScale;
                localScale = new Vector3(-Mathf.Abs(localScale.x), localScale.y, localScale.z);
                t.localScale = localScale;
                animator.SetBool ("facingRight", facingRight);
            }
        }
    }
    
    public void Interact(InputAction.CallbackContext ctx)
    {
        
    }
    
    public void Jump(InputAction.CallbackContext ctx)
    {
        if (isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }
    }
    
    public void Shoot(InputAction.CallbackContext ctx)
    {
        if(!ctx.started) return;
        bool isCarrot = true;
        var fab = isCarrot ? carrotFab : potatoFab;
        var veggie = Instantiate(fab);
        var direction = facingRight ? Vector2.right : Vector2.left;
        veggie.transform.position = throwTransform.position;
        veggie.transform.rotation = facingRight ? Quaternion.Euler(0,0,90) : Quaternion.Euler(0,0,-90);
        var rg = veggie.GetComponent<Rigidbody2D>();

        if (isCarrot)
        {
            rg.AddForce((direction.normalized) * 200);
        }
        else
        {
            rg.AddForce((direction.normalized + Vector2.up) * 150);
        }
    }
    
    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        //Animator updates
        if (Mathf.Abs(r2d.velocity.x) > 0.001f ) animator.SetBool ("isRunning", true);
        else animator.SetBool ("isRunning", false);

        if (r2d.velocity.y > 0.01f && !isGrounded) 
        {
            animator.SetBool ("isJumping", true);
        }
        else 
        {
            animator.SetBool("isJumping", false);
        }
        
        
        if (r2d.velocity.y < -0.01f && !isGrounded) 
        {
            animator.SetBool ("isFalling", true);
        }
        else 
        {
            animator.SetBool("isFalling", false);
        }

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }
}