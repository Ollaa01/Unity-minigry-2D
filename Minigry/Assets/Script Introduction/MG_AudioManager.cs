/**
 * MG_AudioManager.cs
 * Manages audio functionalities such as playing and stopping music.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * MG_AudioManager class.
 * Manages audio functionalities such as playing and stopping music.
 */
public class MG_AudioManager : MonoBehaviour
{
    public static MG_AudioManager instance; /** Singleton instance of the audio manager. */
    public AudioSource musicSource; /** AudioSource for playing music. */

    /**
     * Awake is called when the script instance is being loaded.
     */
    private void Awake()
    {
        // Ensure only one instance of the AudioManager exists
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

    /**
     * Start is called before the first frame update.
     */
    private void Start()
    {
        GetVolume(); // Retrieve and set the volume level
    }

    /**
     * Plays the music if the AudioSource is not null and not already playing.
     */
    public void PlayMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    /**
     * Stops the music if the AudioSource is not null and currently playing.
     */
    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    /**
     * Retrieves the volume level from PlayerPrefs and sets the AudioListener volume accordingly.
     */
    private void GetVolume()
    {
        float volumeLevel = PlayerPrefs.GetFloat("Volume", 1f);
        AudioListener.volume = volumeLevel;
    }
}
