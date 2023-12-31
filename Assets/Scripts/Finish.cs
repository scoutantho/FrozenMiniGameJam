using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource enterSound;
    private Animator animator;
    private bool IsEntranceStillStanding = true;
    private bool IsInDoorFrame = false;
    private bool NextLevelLoading = false;

    [SerializeField]
    private GameObject toldYou;
    [SerializeField]
    private GameObject enterDoor;

    // Start is called before the first frame update
    void Start()
    {
        enterSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //Debug.Log($"Entrance is still standing : {IsEntranceStillStanding} and player is in door frame : {IsInDoorFrame}");
        if (IsInDoorFrame && IsEntranceStillStanding && !NextLevelLoading)
        {
           if (enterDoor != null) enterDoor.SetActive(true);
            if (Input.GetAxisRaw("Vertical") >= 0.1f)
            {
                //finishSound.Play();
                NextLevelLoading = true;
                Invoke("CompleteLevel", 0.5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && IsEntranceStillStanding)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                IsEntranceStillStanding = false;
                animator.SetTrigger("IsDestroyed");
                if (toldYou != null)
                {
                    toldYou.SetActive(true);
                    enterDoor.SetActive(false);
                }
            }
            IsInDoorFrame = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Player leave door frame");
        if (collision.gameObject.CompareTag("Player"))
        {
            IsInDoorFrame = false;
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        NextLevelLoading = false;
    }
}
