using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ScoreManager : MonoBehaviour
{
    
    [SerializeField] private Text scoreText, highScoreText;
    public static S_ScoreManager Instance;
    private int currentScore = 0;
    private string HIGHSCORE_KEY = "HighScore";
    public int Score { get { return currentScore; } }
    // Start is called before the first frame update

    public void IncreaseScore(int num)
    {
        if (num > 0)
            currentScore += num;
    }
    void Start()
    {
        
    }
    public void SetHighScore()
    {
        int hS = PlayerPrefs.GetInt(HIGHSCORE_KEY);
        if (currentScore <= hS)
            return;
        PlayerPrefs.SetInt(HIGHSCORE_KEY, currentScore);
    }

    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (scoreText != null)
            scoreText.text = currentScore.ToString();
        if (highScoreText != null)
            highScoreText.text = PlayerPrefs.GetInt(HIGHSCORE_KEY).ToString();
    }
    
}
