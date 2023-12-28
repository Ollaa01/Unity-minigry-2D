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
            scoreText.text = "Wynik: " + _score;
            if (_score >= ScoreToBeat)
            {
                GameWin();
            }
        }
    }
    [SerializeField] private Text scoreText;
    [SerializeField] private int ScoreToBeat = 1000;
    [SerializeField] private Text scoreToBeatText;
    private void Awake() => Instance = this;

    public void Start()
    {
        gameWinText.gameObject.SetActive(false);
        if (scoreToBeatText != null)
        {
            scoreToBeatText.text = "Wynik do pokonania: " + ScoreToBeat;
            scoreToBeatText.gameObject.SetActive(true);
        }
    }
    public void GameWin()
    {
        gameWinText.gameObject.SetActive(true);
        MG_MGStatus.Instance.GamePassed("M3Played");
    }
}
