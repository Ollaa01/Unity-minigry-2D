using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    private void Start()
    {
        // Ustaw wartoœci pocz¹tkowe na podstawie PlayerPrefs
        //volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        SetDifficulty(PlayerPrefs.GetFloat("Difficulty", 2f));


    }

    public void SaveOptions()
    {
        // Zapisz ustawienia do PlayerPrefs
        //PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        //PlayerPrefs.SetFloat("Difficulty", GetDifficulty());

        // SprawdŸ wartoœæ poziomu trudnoœci
        float difficultyLevel = PlayerPrefs.GetFloat("Difficulty", 1f);
        Debug.Log("Aktualny poziom trudnoœci: " + difficultyLevel);
    }

    // Metoda wywo³ywana po zmianie g³oœnoœci
    public void OnVolumeChanged(float volume)
    {
        // Zastosuj zmiany w g³oœnoœci
        ApplyVolume(volume);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    private void ApplyVolume(float volume)
    {
        // Zastosuj zmiany w g³oœnoœci dla bie¿¹cej sceny
        AudioListener.volume = volume;
    }


    // Metoda wywo³ywana po klikniêciu przycisku "£atwy"
    public void OnEasyButtonClicked()
    {
        SetDifficulty(1f);
    }

    // Metoda wywo³ywana po klikniêciu przycisku "Œredni"
    public void OnMediumButtonClicked()
    {
        SetDifficulty(2f);
    }

    // Metoda wywo³ywana po klikniêciu przycisku "Trudny"
    public void OnHardButtonClicked()
    {
        SetDifficulty(3f);
    }

    private void SetDifficulty(float difficulty)
    {
        // Zastosuj zmiany w poziomie trudnoœci
        Debug.Log("Difficulty changed to: " + difficulty);
        // Dodaj odpowiednie akcje, które maj¹ byæ wykonane po zmianie trudnoœci
        PlayerPrefs.SetFloat("Difficulty", difficulty);
    }

    private float GetDifficulty()
    {
        // Zwróæ aktualny poziom trudnoœci
        if (easyButton.interactable) return 1f;
        if (mediumButton.interactable) return 2f;
        if (hardButton.interactable) return 3f;

        return 2f; // Domyœlna wartoœæ
    }

    public void Update()
    {
    }
}
