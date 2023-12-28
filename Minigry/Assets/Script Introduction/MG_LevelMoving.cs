using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MG_LevelMoving : MonoBehaviour
{
    private MG_AudioManager audioManager;
    public string targetSceneName;
    public bool stopMusic = false;

    public void MoveToScene()
    {
        if (MG_AudioManager.instance != null && stopMusic)
        {
            MG_AudioManager.instance.StopMusic();
            Destroy(MG_AudioManager.instance.gameObject);
        }
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("Target scene name cannot be empty!");
        }
    }
    public void OnExitButtonPressed()
    {
        Application.Quit(); 
    }
}
