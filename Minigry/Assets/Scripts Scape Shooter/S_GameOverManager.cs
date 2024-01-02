using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gamePlay, gameOver, gameWin, winParticleSystem;
    [SerializeField] private AudioClip winSound;
    private AudioSource audioSource;
    private bool isGameWin = false;

    // Start is called before the first frame update
    void Start()
    {
        isGameWin = false;
        if (gamePlay != null)
            gamePlay.SetActive(true);
        if (gameOver != null)
            gameOver.SetActive(false);
        if (gameWin != null)
            gameWin.SetActive(false);
    }
    public void GameOver()
    {
        if (isGameWin == true)
            return;
        if (gamePlay != null)
            gamePlay.SetActive(false);
        if (gameOver != null)
            gameOver.SetActive(true);

        // Time.timeScale = 0f;
    }

    public void GameWin()
    {
        isGameWin = true;
        WinConfetti();
        if (gamePlay != null)
            gamePlay.SetActive(false);
        if (gameWin != null)
            gameWin.SetActive(true);
        MG_MGStatus.Instance.GamePassed("SSPlayed");

    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index); //tutaj skrypt do prze³adowania sceny :)
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void WinConfetti()
    {
        if (winParticleSystem == null)
            return;

        // Ustaw pozycjê na œrodek ekranu
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 3f, 0f);
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);

        GameObject explosion = Instantiate(winParticleSystem, worldCenter, Quaternion.identity);


        // Zmiana rotacji obiektu na -90 stopni wzd³u¿ osi X
        explosion.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        Destroy(explosion, 4f);
        if (audioSource != null && winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }
    }
}
