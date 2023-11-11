using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer Sprite;
    private Animator animator;
    [SerializeField]
    float dirX;
    bool flipX = false;
    float movementSpeed;
    float walkSpeed = 7f;
    float runningSpeed = 10f;
    float jumpForce = 14f;

    bool ctrlKeyIsPressed = false;
    bool isMoving = false;

    //Animation condition
    bool isDead = false;

    private enum AnimationState { idle, running, walking, jumping, falling, climbing, rolling }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movementSpeed = ctrlKeyIsPressed ? runningSpeed : walkSpeed;
        UpdateMovement();

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


        animator.SetInteger("animationState", UpdateAnimations());
    }

    void UpdateMovement()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);

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
}
