using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField]
    private bool IsDead = false;
    [SerializeField]
    private LayerMask dyingMask;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Spike"))
    //    {
    //        Die();
    //    }
    //}

    private void Update()
    {
        if(!IsDead && Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0f, Vector2.down, 0f, dyingMask))
        {
            Debug.Log("collision");
            IsDead = true;
            Die();
        }
    }

    private void Die()
    {        
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
    }

    public void WakeUp()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
