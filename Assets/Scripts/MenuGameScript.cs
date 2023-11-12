using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameScript : MonoBehaviour
{
    bool IsOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        if (IsOpen)
        {
            CloseMenu();
        }
        else
        {
            IsOpen = true;
            this.gameObject.SetActive(true);
        }
    }

    public void CloseMenu()
    {
        IsOpen= false;
        this.gameObject.SetActive(false);
    }

    public void YesButton()
    {
        IsOpen = false;
        SceneManager.LoadScene(SceneManager.GetSceneByName("StartScene").name);
    }
    public void NoButton()
    {
        CloseMenu();
    }
}
