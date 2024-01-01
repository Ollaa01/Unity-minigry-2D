using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class P_TimeManager : MonoBehaviour
{
    public Text timerText; 
    public Text gameOverText; 
    public Text gameWinText;
    public Button gameNextButton;
    public Button replayButton;
    public GameObject winParticleSystem;
    public float gameTime = 30f; 
    private float timer; 
    private bool isGameOver = false;
    private bool wasConfettiPlayed = false;
    //private bool GameWin = false;

    private void Awake()
    {
        WinConfetti();
        gameNextButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
        replayButton.onClick.AddListener(Replay);
        gameWinText.gameObject.SetActive(false);
        wasConfettiPlayed = false;
        Debug.Log("Awake wykonano");
    }
    void Start()
    {
        timer = 0f;
        UpdateTimerDisplay();
        Debug.Log("Start wykonano");
        /*
        gameOverText.gameObject.SetActive(false); 
        replayButton.gameObject.SetActive(false); 
        replayButton.onClick.AddListener(Replay); 
        gameWinText.gameObject.SetActive(false); */
    }

    void Update()
    {
        if (P_PiecesScript.CheckIfWon())
        {
            WinConfetti();
            //gameWinText.gameObject.SetActive(true);
            gameNextButton.gameObject.SetActive(true);
            return;
        }
        if (!isGameOver)
        {
            timer += Time.deltaTime;

            if (timer >= gameTime && P_PiecesScript.CheckIfWon() == false)
            {
                GameOver();
            }

            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        float timeLeft = gameTime - Mathf.Round(timer);
        if (timerText != null)
        {
            timerText.text = "Time left: " + timeLeft.ToString() + " s";
        }
    }

    void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true); 
        isGameOver = true;
    }

    void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void WinConfetti()
    {
        if (wasConfettiPlayed == false)
        {
            if (winParticleSystem == null)
                return;
            wasConfettiPlayed = true;
            // Ustaw pozycj� na �rodek ekranu
            Vector3 screenCenter = new Vector3((Screen.width / 2f), Screen.height / 3f, 100f);
            Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);

            GameObject explosion = Instantiate(winParticleSystem, worldCenter, Quaternion.identity);


            // Zmiana rotacji obiektu na -90 stopni wzd�u� osi X
            explosion.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            Destroy(explosion, 4f);
        }
    }
}
