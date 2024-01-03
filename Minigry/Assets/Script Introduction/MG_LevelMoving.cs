/**
 * MG_LevelMoving.cs
 * Handles moving to a new scene and stopping music if specified.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * MG_LevelMoving class.
 * Handles moving to a new scene and stopping music if specified.
 */
public class MG_LevelMoving : MonoBehaviour
{
    private MG_AudioManager audioManager; /** Reference to the audio manager. */
    public string targetSceneName; /** Name of the target scene to move to. */
    public bool stopMusic = false; /** Flag indicating whether to stop the music when moving to a new scene. */

    /**
     * Moves to the target scene and stops music if specified.
     */
    public void MoveToScene()
    {
        // Stop music and destroy AudioManager instance if stopMusic is true
        if (MG_AudioManager.instance != null && stopMusic)
        {
            MG_AudioManager.instance.StopMusic();
            Destroy(MG_AudioManager.instance.gameObject);
        }

        // Load the target scene if the scene name is not empty
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("Target scene name cannot be empty!");
        }
    }

    /**
     * Quits the application when the exit button is pressed.
     */
    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
