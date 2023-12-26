using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class P_TimeManager : MonoBehaviour
{
    public Text timerText; // Referencja do tekstu licznika czasu
    public Text gameOverText; // Referencja do tekstu "GameOver"
    public Text gameWinText; // Referencja do tekstu "GameOver"
    public Button replayButton; // Referencja do przycisku "Replay"
    public float gameTime = 30f; // Czas gry w sekundach
    private float timer; // Aktualny czas od rozpoczêcia gry
    private bool isGameOver = false; // Flaga informuj¹ca, czy gra skoñczy³a siê
    private bool GameWin = false;

    void Start()
    {
        timer = 0f;
        UpdateTimerDisplay();
        gameOverText.gameObject.SetActive(false); // Ukryj napis "GameOver" na pocz¹tku
        replayButton.gameObject.SetActive(false); // Ukryj przycisk "Replay" na pocz¹tku
        replayButton.onClick.AddListener(Replay); // Dodaj obs³ugê klikniêcia do przycisku "Replay"
        gameWinText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (P_PiecesScript.CheckIfWon())
        {
            gameWinText.gameObject.SetActive(true);
            return;
        }
        if (!isGameOver)
        {
            timer += Time.deltaTime;

            // SprawdŸ, czy up³ynê³o 30 sekund
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
        // Aktualizuj tekst licznika czasu na górze ekranu
        if (timerText != null)
        {
            timerText.text = "Time left: " + timeLeft.ToString() + " s";
        }
    }

    void GameOver()
    {
        // Wyœwietl napis "GameOver"
        gameOverText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true); // Poka¿ przycisk "Replay"
        isGameOver = true;
    }

    void Replay()
    {
        // Ponownie za³aduj scenê
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
