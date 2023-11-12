using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    private static GameMusic instance = null;
    public static GameMusic Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    //private AudioSource _audioSource;
    //private void Awake()
    //{
    //    DontDestroyOnLoad(transform.gameObject);
    //    _audioSource = GetComponent<AudioSource>();
    //}

    //public void PlayMusic()
    //{
    //    if (_audioSource.isPlaying) return;
    //    _audioSource.Play();
    //}

    //public void StopMusic()
    //{
    //    _audioSource.Stop();
    //}
}
