using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public enum ThingsToDo
{
    sceneSettings,
    LoadCanva
}
public class enterDoor : MonoBehaviour
{
    private AudioSource enterSound;
    private Animator animator;
    private bool IsInDoorFrame = false;
    private bool NextLevelLoading = false;
    [SerializeField]
    private ThingsToDo thingsToDo;
    [SerializeField]
    private Canvas CanvaToBeLoaded;
    [SerializeField]
    private Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        enterSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInDoorFrame && !NextLevelLoading)
        {
            if (Input.GetAxisRaw("Vertical") >= 0.1f)
            {
                Debug.Log("enterdoor trigger");
                //finishSound.Play();
                NextLevelLoading = true;
                Invoke("EnterDoor", 0.1f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player enter door frame");
            IsInDoorFrame = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player leave door frame");
            IsInDoorFrame = false;
        }
    }

    private void EnterDoor()
    {
        if (thingsToDo == ThingsToDo.LoadCanva)
        {
            playerRb.bodyType = playerRb.bodyType = RigidbodyType2D.Static;
            CanvaToBeLoaded.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene((int)thingsToDo);
        }
        NextLevelLoading = false;
    }
}
