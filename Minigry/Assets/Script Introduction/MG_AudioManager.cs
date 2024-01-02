using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MG_AudioManager : MonoBehaviour
{
    public static MG_AudioManager instance;

    public AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GetVolume();
        Debug.Log("Before: " + musicSource.isPlaying);
        PlayMusic();
        Debug.Log("Played: " + musicSource.isPlaying);
    }
    public void PlayMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    private void GetVolume()
    {
        float volumeLevel = PlayerPrefs.GetFloat("Volume", 1f);
        Debug.Log("Aktualny poziom g³oœnoœcii: " + volumeLevel);
        AudioListener.volume = volumeLevel;
    }
}
