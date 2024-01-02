using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Manages the health of an object and handles health-related events.
 */
public class S_HealthManager : MonoBehaviour
{
    private int currentHealth; /** The current health of the object. */
    [SerializeField] private int minHealth = 0, maxHealth = 100; /** The minimum and maximum health values. */
    [SerializeField] private Image healthFill; /** UI image representing the health fill. */
    [SerializeField] private Text healthText; /** UI text displaying the current health. */
    [SerializeField] private GameObject explosionEffectPrefab; /** Prefab of the explosion effect upon death. */
    [SerializeField] private AudioClip deathSound; /** Sound played upon death. */
    private AudioSource audioSource; /** Audio source component for playing sounds. */
    private bool isDead = false;  /** Flag indicating whether the object is dead. */

    /**
     * Increases the health of the object.
     * @param amount The amount by which to increase the health (default is 1).
     */
    public void IncreaseHealth(int amount = 1)
    {
        if (currentHealth < maxHealth)
            currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
    }

    /**
     * Decreases the health of the object.
     * @param amount The amount by which to decrease the health (default is 1).
     */
    public void DecreaseHealth(int amount = 1)
    {
        if (currentHealth < minHealth)
            Kill();
        if (currentHealth > minHealth)
            currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
    }

    /**
     * Initiates the explosion effect and plays the death sound.
     */
    private void Explode()
    {
        audioSource.PlayOneShot(deathSound);
        if (explosionEffectPrefab == null)
            return;

        GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

        Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
    }

    /**
     * Plays the specified sound.
     * @param clip The audio clip to be played.
     */
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    /**
     * Handles the death of the object.
     * Initiates the explosion, updates scores, and triggers game over if the player dies.
     */
    private void Kill()
    {
        if (isDead) return; 
        isDead = true; 
        Explode();
        if (GetComponent<S_Enemy>())
        {
            S_ScoreManager.Instance.IncreaseScore(GetComponent<S_Enemy>().ScoreToIncrease);
            S_EnemySpawner.enemiesDefeated++;
        }
        else if (GetComponent<S_PlayerController>())
        {
            if (S_ScoreManager.Instance != null)
                S_ScoreManager.Instance.SetHighScore();
            GameObject.FindFirstObjectByType<S_GameOverManager>().GameOver();
        }
        Destroy(gameObject);
    }

    /**
     * Start is called before the first frame update.
     */
    void Start()
    {
        currentHealth = maxHealth;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 1.0f;
    }

    /**
     * Calculates the fill amount for the health bar.
     * @param cur The current health value.
     * @param max The maximum health value.
     * @return The fill amount as a float.
     */
    private float FillAmount(int cur, int max)
    {
        return (float)((float)cur / (float)max);
    }

    /**
     * Update is called once per frame.
     */
    void Update()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
        if (healthFill != null)
        {
            healthFill.fillAmount = FillAmount(currentHealth, maxHealth);
        }
        if (currentHealth <= minHealth)
        {
            PlaySound(deathSound);
            Invoke("Kill", 0.1f);
        }
    }
}
