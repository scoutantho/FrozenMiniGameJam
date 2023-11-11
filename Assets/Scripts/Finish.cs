using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private bool IsEntranceStillStanding = true;
    // Start is called before the first frame update
    void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && IsEntranceStillStanding)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                IsEntranceStillStanding = false;
                Debug.Log("Destroy Entrance");
            }
        }
    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        Debug.Log($"Entrance is : {IsEntranceStillStanding}");
        if (collision.gameObject.CompareTag("Player") && IsEntranceStillStanding)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                //finishSound.Play();
                Invoke("CompleteLevel", 0.5f);
            }
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
