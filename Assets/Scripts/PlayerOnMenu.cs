using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnMenu : MonoBehaviour
{
    private Rigidbody2D rb;
    bool isLocked = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && isLocked)
        {
            isLocked = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.velocity = new Vector2(0, -0.1f);
        }
    }
}
