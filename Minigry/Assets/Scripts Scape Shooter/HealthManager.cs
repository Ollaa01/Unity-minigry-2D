using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private int currentHealth;
    [SerializeField] private int minHealth = 0, maxHealth = 100;
    [SerializeField] private Image healthFill;
    [SerializeField] private Text healthText;
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private AudioClip deathSound; 
    private AudioSource audioSource;

    public void IncreaseHealth(int amount = 1)
    {
        if (currentHealth < maxHealth)
            currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
    }
    public void DecreaseHealth(int amount = 1)
    {
        //Debug.Log("tutaj");
        if (currentHealth < minHealth)
            Kill();
        if (currentHealth > minHealth)
            currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
    }

    private void Explode()
    {
        audioSource.PlayOneShot(deathSound);
        if (explosionEffectPrefab == null)
            return;

        GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

        // Zniszcz efekt eksplozji po zakoñczeniu
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
            Debug.Log("Playing sound: " + clip.name);
            Debug.Log("AudioSource volume: " + audioSource.volume);
        }
    }
    private bool isDead = false;

    private void Kill()
    {
        if (isDead) return; // Jeœli ju¿ umar³, zakoñcz funkcjê
        isDead = true; // Ustaw flagê na true, ¿eby nie wywo³ywaæ funkcji wiêcej ni¿ raz
        Explode();
        if (GetComponent<Enemy>())
        {
            S_ScoreManager.Instance.IncreaseScore(GetComponent<Enemy>().ScoreToIncrease);
            if (GetComponent<Enemy>().isBoss)
            {
                // EnemySpawner.Instance.SetExec(false);
            }
            EnemySpawner.enemiesDefeated++;
        }
        else if (GetComponent<PlayerController>())
        {
            if (S_ScoreManager.Instance != null)
                S_ScoreManager.Instance.SetHighScore();
            GameObject.FindFirstObjectByType<S_GameOverManager>().GameOver();
        }
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 1.0f;
    }

    private float FillAmount(int cur, int max)
    {
        return (float)((float)cur / (float)max);
    }
    // Update is called once per frame
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
            //PlaySound(deathSound);
            //StartCoroutine(ExampleCoroutine());
            //Kill();
            //PlaySound(deathSound);
        }
    }
    private IEnumerator ExampleCoroutine()
    {
        Debug.Log("Coroutine started");

        // Czekaj 2 sekundy
        yield return new WaitForSeconds(0.1f);

        Debug.Log("Coroutine resumed after 2 seconds");
    }
}
