/**
 * Script that manages the scoring system and victory conditions in the Match-3 game.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class that manages the scoring system and victory conditions in the Match-3 game.
 */
public class M_Score : MonoBehaviour
{
    [SerializeField] public Text gameWinText; /** Text displaying the win message. */
    public GameObject winParticleSystem; /** Particle system for the win confetti effect. */
    [SerializeField] private Text scoreText; /**  Text displaying the current score. */
    [SerializeField] public int ScoreToBeat = 1000; /** The target score to achieve victory. */
    [SerializeField] private Text scoreToBeatText; /**  Text displaying the target score to beat. */
    [SerializeField] private Image uiFill; /** Image component representing the fill amount for the UI score. */

    /**
    * Singleton instance of the M_Score class.
    */
    public static M_Score Instance { get; private set; }
    private int _score; /** Value representing score */

    public int Score
    {
        get => _score;

        set
        {
            if (_score == value) return;
            _score = value;
            //scoreText.text = "Wynik: " + _score;
            if (uiFill != null)
            uiFill.fillAmount = Mathf.InverseLerp(0, ScoreToBeat, _score);
            if (_score >= ScoreToBeat)
            {
                GameWin();
            }
        }
    }

    /**
     * Awake method called when the script instance is being loaded.
     */
    private void Awake() => Instance = this;

    /**
     * Start method called before the first frame update.
     */
    public void Start()
    {
        GetDifficulty();
        gameWinText.gameObject.SetActive(false);
        if (scoreToBeatText != null)
        {
            //scoreToBeatText.text = "Wynik do pokonania: " + ScoreToBeat;
            //scoreToBeatText.gameObject.SetActive(true);
        }
    }

    /**
    * Handles the game win conditions.
    */
    public void GameWin()
    {
        M_WiggleBoss wiggleBossScript = FindObjectOfType<M_WiggleBoss>();
        if (wiggleBossScript != null)
        {
            wiggleBossScript.Explode();
        }
        WinConfetti();
        gameWinText.gameObject.SetActive(true);
        MG_MGStatus.Instance.GamePassed("M3Played");
    }

    /**
     * Plays the win confetti effect.
     */
    private void WinConfetti()
    {

        if (winParticleSystem == null)
            return;
        Vector3 screenCenter = new Vector3((Screen.width / 2f), Screen.height / 3f, 100f);
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);

        GameObject explosion = Instantiate(winParticleSystem, worldCenter, Quaternion.identity);

        explosion.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        Destroy(explosion, 4f);
        Debug.Log("Confetti Played");
    }

    /**
     * Retrieves the difficulty level from player preferences.
     */
    private void GetDifficulty()
    {
        float difficultyLevel = PlayerPrefs.GetFloat("Difficulty", 1f);
        if (difficultyLevel == 1f)
        {
            ScoreToBeat = 1000;
        }
        if (difficultyLevel == 2f)
        {
            ScoreToBeat = 10000;
        }
        if (difficultyLevel == 3f)
        {
            ScoreToBeat = 25000;
        }
    }
}

