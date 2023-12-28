using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gamePlay, gameOver, gameWin;

    // Start is called before the first frame update
    void Start()
    {
        if (gamePlay != null)
            gamePlay.SetActive(true);
        if (gameOver != null)
            gameOver.SetActive(false);
        if (gameWin != null)
            gameWin.SetActive(false);
    }
    public void GameOver()
    {
        if (gamePlay != null)
            gamePlay.SetActive(false);
        if (gameOver != null)
            gameOver.SetActive(true);
       // Time.timeScale = 0f;
    }

    public void GameWin()
    {
        if (gamePlay != null)
            gamePlay.SetActive(false);
        if (gameWin != null)
            gameWin.SetActive(true);
        MG_MGStatus.Instance.GamePassed("SSPlayed");

    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index); //tutaj skrypt do prze³adowania sceny :)
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
