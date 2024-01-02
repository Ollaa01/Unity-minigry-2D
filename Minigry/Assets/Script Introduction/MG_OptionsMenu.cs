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
        // Ustaw warto�ci pocz�tkowe na podstawie PlayerPrefs
        //volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        SetDifficulty(PlayerPrefs.GetFloat("Difficulty", 2f));


    }

    public void SaveOptions()
    {
        // Zapisz ustawienia do PlayerPrefs
        //PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        //PlayerPrefs.SetFloat("Difficulty", GetDifficulty());

        // Sprawd� warto�� poziomu trudno�ci
        float difficultyLevel = PlayerPrefs.GetFloat("Difficulty", 1f);
        Debug.Log("Aktualny poziom trudno�ci: " + difficultyLevel);
    }

    // Metoda wywo�ywana po zmianie g�o�no�ci
    public void OnVolumeChanged(float volume)
    {
        // Zastosuj zmiany w g�o�no�ci
        ApplyVolume(volume);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    private void ApplyVolume(float volume)
    {
        // Zastosuj zmiany w g�o�no�ci dla bie��cej sceny
        AudioListener.volume = volume;
    }


    // Metoda wywo�ywana po klikni�ciu przycisku "�atwy"
    public void OnEasyButtonClicked()
    {
        SetDifficulty(1f);
    }

    // Metoda wywo�ywana po klikni�ciu przycisku "�redni"
    public void OnMediumButtonClicked()
    {
        SetDifficulty(2f);
    }

    // Metoda wywo�ywana po klikni�ciu przycisku "Trudny"
    public void OnHardButtonClicked()
    {
        SetDifficulty(3f);
    }

    private void SetDifficulty(float difficulty)
    {
        // Zastosuj zmiany w poziomie trudno�ci
        Debug.Log("Difficulty changed to: " + difficulty);
        // Dodaj odpowiednie akcje, kt�re maj� by� wykonane po zmianie trudno�ci
        PlayerPrefs.SetFloat("Difficulty", difficulty);
    }

    private float GetDifficulty()
    {
        // Zwr�� aktualny poziom trudno�ci
        if (easyButton.interactable) return 1f;
        if (mediumButton.interactable) return 2f;
        if (hardButton.interactable) return 3f;

        return 2f; // Domy�lna warto��
    }

    public void Update()
    {
    }
}
