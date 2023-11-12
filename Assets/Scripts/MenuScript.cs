using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject CanvaCredit;
    [SerializeField]
    private GameObject CanvaButton;
    [SerializeField]
    private GameObject CanvaQuit;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<GameMusic>().PlayMusic();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ClickQuit()
    {
        CanvaQuit.GetComponent<QuitGameScript>().OpenMenu();
    }
    public void StartCredit()
    {
        CanvaButton.SetActive(false);
        CanvaCredit.SetActive(true);
    }
    public void CloseCredit()
    {
        CanvaCredit.SetActive(false);
        CanvaButton.SetActive(true);
    }
}
