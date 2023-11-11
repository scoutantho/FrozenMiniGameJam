using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer Sprite;
    private Animator animator;
    private BoxCollider2D boxCollider;
    [SerializeField]
    private LayerMask jumpableGround;

    float dirX;
    bool flipX = false;
    float movementSpeed;
    float walkSpeed = 7f;
    float runningSpeed = 10f;
    float jumpForce = 14f;

    bool ctrlKeyIsPressed = false;
    bool isMoving = false;

    private enum AnimationState { idle, running, walking, jumping, falling, climbing, rolling }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ctrlKeyIsPressed = true;
        }
        else { ctrlKeyIsPressed = false; }

        movementSpeed = ctrlKeyIsPressed ? runningSpeed : walkSpeed;

        UpdateMovement();
        
        animator.SetInteger("animationState", UpdateAnimations());
    }

    void UpdateMovement()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (dirX < 0f)
        {
            flipX = true;
        }
        else if (dirX > 0f)
        {
            flipX = false;
        }

        Sprite.flipX = flipX;
        isMoving = dirX != 0;
    }
    int UpdateAnimations()
    {
        AnimationState state;
        if (ctrlKeyIsPressed && isMoving)
        {
            state = AnimationState.running;
        }
        else if (!ctrlKeyIsPressed && isMoving)
        {
            state = AnimationState.walking;
        }
        else
        {
            state = AnimationState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = AnimationState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = AnimationState.falling;
        }

        return (int)state;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 1f, jumpableGround);
    }
}
