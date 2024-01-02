using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Manages game over and win conditions.
 */
public class S_GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gamePlay, gameOver, gameWin, winParticleSystem; /** References to game objects for gameplay, game over, and game win. */
    [SerializeField] private AudioClip winSound; /** The sound played upon winning. */
    private AudioSource audioSource;  /** The audio source component for playing sounds. */
    private bool isGameWin = false; /** Flag indicating whether the game has been won. */

    /**
     * Start is called before the first frame update.
     */
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

    /**
     * Activates the game over screen.
     */
    public void GameOver()
    {
        if (isGameWin == true)
            return;
        if (gamePlay != null)
            gamePlay.SetActive(false);
        if (gameOver != null)
            gameOver.SetActive(true);
    }

    /**
     * Activates the game win screen.
     */
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

    /**
     * Reloads the current scene.
     */
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /**
     * Loads the scene with the specified index.
     * @param index The index of the scene to be loaded.
     */
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index); 
    }

    /**
     * Spawns confetti particles and plays the win sound.
     */
    private void WinConfetti()
    {
        if (winParticleSystem == null)
            return;

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 3f, 0f);
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);

        GameObject explosion = Instantiate(winParticleSystem, worldCenter, Quaternion.identity);

        explosion.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        Destroy(explosion, 4f);
        if (audioSource != null && winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }
    }
}
