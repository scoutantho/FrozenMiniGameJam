using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    private Animator animator;
    private bool IsCrateStillStanding = true;
    private bool IsInFrame = false;
    private bool IsAlreadyLooted = false;

    [SerializeField]
    private GameObject StopBreakIt;
    
    [SerializeField]
    private GameObject GetASword;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInFrame && IsCrateStillStanding && !IsAlreadyLooted)
        {
            if (Input.GetAxisRaw("Vertical") >= 0.1f)
            {
                //finishSound.Play();
                IsAlreadyLooted = true;
                Invoke("OptainSword", 0.1f);
                GetASword.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && IsCrateStillStanding)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                IsCrateStillStanding = false;
                animator.SetTrigger("IsDestroyed");
                StopBreakIt.SetActive(true);
            }
            IsInFrame = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Player leave door frame");
        if (collision.gameObject.CompareTag("Player"))
        {
            IsInFrame = false;
        }
    }

    private void OptainSword()
    {
        Debug.Log("You obtain a sword");
    }

}
