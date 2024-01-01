using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_GameStatsManager : MonoBehaviour
{
    public static S_GameStatsManager Instance;
    [SerializeField]
    private int numOfLasers = 100, numOfMissiles = 100;
    [SerializeField]
    private Text lasersText, missilesText;

    [SerializeField] private bool infiniteMissiles = false, infiniteLasers = false;

    [Header("Audio")]
    [SerializeField] private AudioClip laserSound;
    [SerializeField] private AudioClip missileSound;
    [SerializeField] private AudioClip backgroundMusic;
    private AudioSource audioSource;
    public bool CheckIfCanShootLaser(int amount)
    {
        if (infiniteLasers)
            return true;
        bool p = false;
        if (numOfLasers - amount >= 0)
            p = true;
        return p;
    }
    public bool CheckIfCanShootMissiles(int amount)
    {
        if (infiniteMissiles)
            return true;
        bool p = false;
        if (numOfMissiles - amount >= 0)
            p = true;
        return p;
    }
    public void ShootLasersByAmount(int amount)
    {
        if (infiniteLasers)
        {
            PlaySound(laserSound);
            return;
        }
        if (numOfLasers - amount >= 0)
        {
            numOfLasers -= amount;
            PlaySound(laserSound);
        }
            
    }
    public void ShootMissilesByAmount(int amount)
    {
        if (infiniteMissiles)
        {
            PlaySound(missileSound);
            return;
        }
        if (numOfMissiles - amount >= 0)
        {
            numOfMissiles -= amount;
            PlaySound(missileSound);
        }
    }
    public void AddMissilesByAmount(int amount)
    {
        if (infiniteMissiles)
            return;
        if (numOfMissiles >= 0)
            numOfMissiles += amount;
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f; // Dostosuj g³oœnoœæ wed³ug potrzeb
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.Play();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (lasersText != null)
            lasersText.text = "Lasers: " + numOfLasers.ToString();
        if (missilesText != null)
            missilesText.text = "Missiles: " + numOfMissiles.ToString();
    }
}
