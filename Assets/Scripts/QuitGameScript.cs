using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.SetActive(false);
    }

    public void Menu()
    {       
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

    public void YesButton()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void NoButton()
    {
        Menu();
    }
}
