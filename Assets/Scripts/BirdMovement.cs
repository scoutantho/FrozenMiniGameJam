using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BirdMovement : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    //private Rigidbody2D rb;
    private bool FlyAway = false;
    private Animator animator;
    private SpriteRenderer Sprite;

    [SerializeField]
    private GameObject[] waypoints;
    private int currentWaypoint = 0;

    public float moveSpeed = 1.5f;
    public float movementIntervalMin = 10f;
    public float movementIntervalMax = 15f;
    private bool IsWalking = false;

    private enum AnimationState { idle, idle2, walking }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        Sprite = GetComponent<SpriteRenderer>();

        InvokeRepeating("PlayRandomAnimation", UnityEngine.Random.Range(0f, 2f), UnityEngine.Random.Range(0f, 5f));
        //InvokeRepeating("MoveBird", UnityEngine.Random.Range(0f, movementIntervalMax), UnityEngine.Random.Range(movementIntervalMin, movementIntervalMax));

    }

    // Update is called once per frame
    void Update()
    {
        if (FlyAway)
        {
            float randomX = transform.position.x + 5 * (Sprite.flipX ? -2 : 2);
            float randomY = transform.position.y + 10;
            Vector2 moveDirection = new Vector2(randomX, randomY).normalized;
            transform.Translate(moveDirection * 3f * Time.deltaTime);
        }
        else
        {
            if (IsWalking)
            {
                MoveBird();
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FlyAway = true;
            animator.SetTrigger("FlyAway");

            // Player is near your object
            Debug.Log("Player is near the object!");
        }
    }

    // Called when another Collider exits this object's trigger zone
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has moved away from your object
            Debug.Log("Fly bird Fly ! ");
        }
    }

    void MoveBird()
    {
        if (IsWalking)
        {
            if (Vector2.Distance(waypoints[currentWaypoint].transform.position, transform.position) < .1f)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
            Vector2 moveDirection = (waypoints[currentWaypoint].transform.position);
            bool flipX = false;
            if ((moveDirection.x - transform.position.x) < 0f)
            {
                flipX = true;
            }
            else if ((moveDirection.x - transform.position.x) > 0f)
            {
                flipX = false;
            }
            Sprite.flipX = flipX;
            transform.position = Vector2.MoveTowards(transform.position, moveDirection, Time.deltaTime * moveSpeed);
        }
    }

    void PlayRandomAnimation()
    {
        if (!FlyAway)
        {
            // Generate a random number to choose the animation
            int randomAnimation = UnityEngine.Random.Range(0, 3);
            IsWalking = randomAnimation == 2;
            animator.SetInteger("animationState", randomAnimation);
        }
    }
}
