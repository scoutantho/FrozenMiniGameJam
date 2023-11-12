using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    float runningSpeed = 15f;
    float jumpForce = 14f;

    bool ctrlKeyIsPressed = false;
    bool isMoving = false;
    private bool keyHeld = false;
    private float startTime;
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
        keyPressedTimer();

        if (Input.GetKey(KeyCode.LeftControl))
        {
            ctrlKeyIsPressed = true;
        }
        else
        {
            ctrlKeyIsPressed = false;
        }

        movementSpeed = ctrlKeyIsPressed ? runningSpeed : walkSpeed;

        if (rb.bodyType != RigidbodyType2D.Static)
        {
            UpdateMovement();

            animator.SetInteger("animationState", UpdateAnimations());
        }
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
    void keyPressedTimer()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            keyHeld = true;
            startTime = Time.time;
        }

        // Check if the key is still held down after 4 seconds
        if (keyHeld && Time.time - startTime >= 3f)
        {
            // Do something here after 4 seconds of holding the key
            //Debug.Log("Key held for 4 seconds!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // Reset the keyHeld flag to prevent continuous execution
            keyHeld = false;
        }

        // Check if the key is released
        if (Input.GetKeyUp(KeyCode.R))
        {
            keyHeld = false;
        }
        //if (Input.GetKey(KeyCode.R))
        //{
        //    //timePressed = Time.time;
        //}

        //do reset after R is Up and 3 seconds
        //if (Input.GetKeyUp(KeyCode.R))
        //{
        //    timePressed = Time.time - timePressed;
        //    if (timePressed > 3f)
        //    {
        //        timePressed = 0f;
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //    }
        //    Debug.Log("Pressed for : " + timePressed + " Seconds");
        //}


    }
}
