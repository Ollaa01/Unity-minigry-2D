using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gamePlay, gameOver;

    // Start is called before the first frame update
    void Start()
    {
        if (gamePlay != null)
            gamePlay.SetActive(true);
        if (gameOver != null)
            gameOver.SetActive(false);
    }
    public void GameOver()
    {
        if (gamePlay != null)
            gamePlay.SetActive(false);
        if (gameOver != null)
            gameOver.SetActive(true);
       // Time.timeScale = 0f;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
