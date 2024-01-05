/**
 * MG_MGStatus.cs
 * Manages and updates the status of mini-games.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * MG_MGStatus class.
 * Manages and updates the status of mini-games.
 */
public class MG_MGStatus : MonoBehaviour
{
    public Button gameButton1; /** Reference to the first game button. */
    public Button gameButton2; /** Reference to the second game button. */
    public Button gameButton3; /** Reference to the third game button. */
    public Button gameButton4; /** Reference to the fourth game button. */
    public Text gameText1, gameText2, gameText3, gameText4; /** References to the texts associated with each game button. */
    public static MG_MGStatus Instance; /** Singleton instance of MG_MGStatus. */

    /**
     * Awake is called when the script instance is being loaded.
     */
    private void Awake()
    {
        Instance = this; 
    }

    /**
     * Start is called before the first frame update.
     */
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Difficulty"))
        {
            PlayerPrefs.SetFloat("Difficulty", 2f);
        }
        UpdateGameButtons(); 
    }

    /**
     * Updates the game buttons based on whether the games have been played.
     */
    private void UpdateGameButtons()
    {
        bool game1Played = PlayerPrefs.HasKey("SCPlayed");
        bool game2Played = PlayerPrefs.HasKey("M3Played");
        bool game3Played = PlayerPrefs.HasKey("PuzzlePlayed");
        bool game4Played = PlayerPrefs.HasKey("SSPlayed");

        if (game1Played) { Debug.Log("Game SC has already been played."); }
        if (game2Played) { Debug.Log("Game M3 has already been played."); }
        if (game3Played) { Debug.Log("Game Puzzle has already been played."); }
        if (game4Played) { Debug.Log("Game SS has already been played."); }
    }

    /**
     * Marks a game as passed and saves the status.
     * @param name The name of the game to mark as passed.
     */
    public void GamePassed(string name)
    {
        PlayerPrefs.SetInt(name, 1);
        PlayerPrefs.Save();
    }
}
