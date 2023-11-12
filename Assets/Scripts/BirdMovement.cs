using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    //private Rigidbody2D rb;
    private bool FlyAway = false;
    private bool CanAppear = false;
    private Animator animator;
    private SpriteRenderer Sprite;

    [SerializeField]
    private GameObject[] waypoints;
    private int currentWaypoint = 0;

    public float moveSpeed = 1.5f;

    private enum AnimationState { idle, idle2, walking}
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        //boxCollider.offset = new Vector2(2, 2);
        Sprite = GetComponent<SpriteRenderer>();
        // Start with a random animation
        PlayRandomAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        if (FlyAway)
        {
            Debug.Log("Fly bird Fly ! ");
        }
        else
        {
            MoveBird();
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FlyAway = true;
            // Player is near your object
            Debug.Log("Player is near the object!");
        }
    }

    // Called when another Collider exits this object's trigger zone
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FlyAway = false;
            // Player has moved away from your object
            Debug.Log("Player has moved away from the object.");
        }
    }

    void MoveBird()
    {
        if (Vector2.Distance(waypoints[currentWaypoint].transform.position, transform.position)< .1f)
        {
            currentWaypoint++;
            if(currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, Time.deltaTime * moveSpeed);
        
    }

    void PlayRandomAnimation()
    {
        // Generate a random number to choose the animation
        int randomAnimation = UnityEngine.Random.Range(0, 2);

        //if(randomAnimation == 3)
        //{
        //    MoveBird();
        //}
        animator.SetInteger("animationState", randomAnimation);
        
    }
}
