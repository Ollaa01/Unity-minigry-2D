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
    private float timer; // Aktualny czas od rozpocz�cia gry
    private bool isGameOver = false; // Flaga informuj�ca, czy gra sko�czy�a si�
    private bool GameWin = false;

    void Start()
    {
        timer = 0f;
        UpdateTimerDisplay();
        gameOverText.gameObject.SetActive(false); // Ukryj napis "GameOver" na pocz�tku
        replayButton.gameObject.SetActive(false); // Ukryj przycisk "Replay" na pocz�tku
        replayButton.onClick.AddListener(Replay); // Dodaj obs�ug� klikni�cia do przycisku "Replay"
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

            // Sprawd�, czy up�yn�o 30 sekund
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
        // Aktualizuj tekst licznika czasu na g�rze ekranu
        if (timerText != null)
        {
            timerText.text = "Time left: " + timeLeft.ToString() + " s";
        }
    }

    void GameOver()
    {
        // Wy�wietl napis "GameOver"
        gameOverText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true); // Poka� przycisk "Replay"
        isGameOver = true;
    }

    void Replay()
    {
        // Ponownie za�aduj scen�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
