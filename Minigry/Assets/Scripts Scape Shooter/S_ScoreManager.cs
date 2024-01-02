using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Manages the game score and handles score-related events.
 */
public class S_ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText, highScoreText, scoreToBeatText; /** UI text objects for displaying score, high score, and score to beat. */
    [SerializeField] int scoreToBeat = 1000; /** The score required to win the game. */
    public static S_ScoreManager Instance; /** Singleton instance of S_ScoreManager. */
    private int currentScore = 0; /** The current score of the game. */
    private string HIGHSCORE_KEY = "HighScore";  /** Key used for storing and retrieving the high score in PlayerPrefs. */

    /**
     * Gets the current score.
     * @return The current score.
     */
    public int Score { get { return currentScore; } }

    /**
     * Increases the score by the specified amount.
     * If the score surpasses the score to beat, triggers the game win event.
     * @param num The amount by which to increase the score.
     */
    public void IncreaseScore(int num)
    {
        if (num > 0)
        {
            currentScore += num;
            if (currentScore >= scoreToBeat)
            {
                GameObject.FindFirstObjectByType<S_GameOverManager>().GameWin(); 
            }
        }
    }

    /**
     * Start is called before the first frame update.
     */
    void Start()
    {
        GetDifficulty();
    }

    /**
     * Sets the high score if the current score is higher.
     */
    public void SetHighScore()
    {
        int hS = PlayerPrefs.GetInt(HIGHSCORE_KEY);
        if (currentScore <= hS)
            return;
        PlayerPrefs.SetInt(HIGHSCORE_KEY, currentScore);
    }

    /**
     * Awake is called when the script instance is being loaded.
     */
    private void Awake()
    {
        Instance = this;
    }

    /**
     * Update is called once per frame.
     */
    void Update()
    {
        if (scoreText != null)
            scoreText.text = currentScore.ToString();
        if (scoreToBeatText != null)
            scoreToBeatText.text = scoreToBeat.ToString();
        if (highScoreText != null)
            highScoreText.text = PlayerPrefs.GetInt(HIGHSCORE_KEY).ToString();
    }

    /**
     * Retrieves the difficulty level from PlayerPrefs and adjusts the score to beat accordingly.
     */
    private void GetDifficulty()
    {
        float difficultyLevel = PlayerPrefs.GetFloat("Difficulty", 1f);
        Debug.Log("Aktualny poziom trudnoœci: " + difficultyLevel);
        if (difficultyLevel == 1f)
        {
            Debug.Log("score set");
            scoreToBeat = 500;
        }
        if (difficultyLevel == 2f)
        {
            scoreToBeat = 1000;
        }
        if (difficultyLevel == 3f)
        {
            scoreToBeat = 2000;
        }
    }

}
