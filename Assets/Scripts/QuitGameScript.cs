using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenMenu()
    {       
        this.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void YesButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void NoButton()
    {
        CloseMenu();
    }
}
