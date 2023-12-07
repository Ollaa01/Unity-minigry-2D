using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_Score : MonoBehaviour
{
    //public Text gameOverText; 
    [SerializeField] public Text gameWinText; 
    //public Button replayButton;
   // private bool isGameOver = false; 
    private bool isGameWin = false;
    public static M_Score Instance { get; private set; }
    private int _score;
    public int Score
    {
        get => _score;

        set
        {
            if (_score == value) return;
            _score = value;
            scoreText.text = "Score: " + _score;
            // Dodaj warunek sprawdzaj¹cy, czy osi¹gniêto wymagany wynik
            if (_score >= ScoreToBeat)
            {
                GameWin();
            }
        }
    }
    [SerializeField] private Text scoreText;
    [SerializeField] private int ScoreToBeat = 1000; 
    private void Awake() => Instance = this;

    public void Start()
    {
        gameWinText.gameObject.SetActive(false);
    }
    public void GameWin()
    {
        gameWinText.gameObject.SetActive(true);
    }
}
