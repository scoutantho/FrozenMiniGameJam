using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameScript : MonoBehaviour
{
    public GameObject CanvaMenu;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CanvaMenu.gameObject.SetActive(!CanvaMenu.gameObject.activeSelf);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CanvaMenu.gameObject.SetActive(false);
    }

    public static void YesButton()
    {
        SceneManager.LoadScene(0);
    }
    public void NoButton()
    {
        CanvaMenu.gameObject.SetActive(!CanvaMenu.gameObject.activeSelf);
    }
}
