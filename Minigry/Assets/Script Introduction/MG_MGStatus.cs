using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MG_MGStatus : MonoBehaviour
{
    public Button gameButton1;
    public Button gameButton2;
    public Button gameButton3;
    public Button gameButton4;
    public Text gameText1, gameText2, gameText3, gameText4;
    public static MG_MGStatus Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        // SprawdŸ, czy gry by³y ju¿ grane i ustaw dostêpnoœæ przycisków
        UpdateGameButtons();
    }

    private void UpdateGameButtons()
    {
        // SprawdŸ, czy gry by³y ju¿ grane i ustaw dostêpnoœæ przycisków
        bool game1Played = PlayerPrefs.HasKey("SCPlayed");
        bool game2Played = PlayerPrefs.HasKey("M3Played");
        bool game3Played = PlayerPrefs.HasKey("PuzzlePlayed");
        bool game4Played = PlayerPrefs.HasKey("SSPlayed");

        /*gameButton1.interactable = !game1Played;
        gameButton2.interactable = !game2Played;
        gameButton3.interactable = !game3Played;
        gameButton4.interactable = !game4Played;*/

        if (game1Played) { gameText1.text = "Gra CS ju¿ zosta³a rozegrana";  }
        if (game2Played) { gameText2.text = "Gra M3 ju¿ zosta³a rozegrana"; }
        if (game3Played) { gameText3.text = "Gra Puzzle ju¿ zosta³a rozegrana"; }
        if (game4Played) { gameText4.text = "Gra SS ju¿ zosta³a rozegrana"; }
        if (game1Played) { Debug.Log("Gra CS ju¿ zosta³a rozegrana"); }
        if (game2Played) { Debug.Log("Gra M3 ju¿ zosta³a rozegrana"); }
        if (game3Played) { Debug.Log("Gra Puzzle ju¿ zosta³a rozegrana"); }
        if (game4Played) { Debug.Log("Gra SS ju¿ zosta³a rozegrana"); }
    }

    public void GamePassed(string name)
    {
        // Ustaw flagê, ¿e gra zosta³a rozegrana
        PlayerPrefs.SetInt(name, 1);
        PlayerPrefs.Save();
    }
}
