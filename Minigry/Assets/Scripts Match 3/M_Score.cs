using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_Score : MonoBehaviour
{
    //public Text gameOverText; 
    [SerializeField] public Text gameWinText;
    public GameObject winParticleSystem;
    //public Button replayButton;
    // private bool isGameOver = false; 
    private bool isGameWin = false;
    [SerializeField] private Text scoreText;
    [SerializeField] public int ScoreToBeat = 1000;
    [SerializeField] private Text scoreToBeatText;
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
        Debug.Log("GameWIn");
        WinConfetti();
        gameWinText.gameObject.SetActive(true);
        MG_MGStatus.Instance.GamePassed("M3Played");
    }

    private void WinConfetti()
    {

        if (winParticleSystem == null)
            return;
        // Ustaw pozycjê na œrodek ekranu
        Vector3 screenCenter = new Vector3((Screen.width / 2f), Screen.height / 3f, 100f);
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);

        GameObject explosion = Instantiate(winParticleSystem, worldCenter, Quaternion.identity);


        // Zmiana rotacji obiektu na -90 stopni wzd³u¿ osi X
        explosion.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        Destroy(explosion, 4f);
        Debug.Log("Confetti Played");
    }

}

