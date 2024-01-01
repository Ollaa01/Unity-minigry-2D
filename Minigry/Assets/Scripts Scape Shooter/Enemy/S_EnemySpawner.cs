using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawner : MonoBehaviour
{
    [SerializeField] private int maxEnemies = 3;
    [SerializeField] private GameObject[] enemiesToSpawn = new GameObject[2];
    [SerializeField] private int interval = 10;
    [SerializeField] private int enemyDeathsToSpawnBoss = 4;
    [SerializeField] private GameObject enemyBoss;
    public static S_EnemySpawner Instance;

    public static int enemiesDefeated = 0;

    private bool IsBossAlreadySpawned()
    {
        // Sprawdü, czy istnieje juø przeciwnik typu boss na scenie
        S_Enemy[] temp = GameObject.FindObjectsOfType<S_Enemy>();
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].isBoss)
            {
                return true;
            }
        }
        return false;
    }
    private int GetNumberOfEnemies()
    {
        int num = 0;
        S_Enemy[] temp = GameObject.FindObjectsOfType<S_Enemy>();
        Debug.Log("temp " + temp.Length);
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].enemyType == S_Enemy.EnemyType.Individual)
                num++;
            if (temp[i].enemyType == S_Enemy.EnemyType.Wave)
                num += GameObject.FindObjectOfType<S_EnemyWave>().NumOfEnemies();
            Debug.Log("a " + num);
        }
        // EnemyWave[] temp2 = GameObject.FindObjectOfType<EnemyWave>();
        //  if (GameObject.FindObjectOfType<EnemyWave>() == true)
        //    num++;
        //num += GameObject.FindObjectOfType<EnemyWave>().NumOfEnemies();
        return num;
    }
    private void SpawnEnemies()
    {
        
        if (GetNumberOfEnemies() >= maxEnemies)
            return;
        int n = Random.Range(0, enemiesToSpawn.Length);
        Debug.Log(n);
        // int i = GetNumberOfEnemies();
        //Debug.Log("Bleble " + i);
        Instantiate(enemiesToSpawn[n]);
        /*
        for (int i = GetNumberOfEnemies(); i <= maxEnemies; i++)
        {
            Debug.Log("num " + i);
            if(enemiesToSpawn[n] != null)
                 Instantiate(enemiesToSpawn[n]);
        } */

    }
    // Start is called before the first frame update
    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemyBoss()
    {
        if (!IsBossAlreadySpawned() && enemyBoss != null)
            Instantiate(enemyBoss);
    }

    private bool exec = false;

    public void SetExec(bool type)
    {
        exec = type;
    }
    // Update is called once per frame
    private void Update()
    {
        if (enemiesDefeated >= enemyDeathsToSpawnBoss)
        {
            if (!exec)
            {
                SpawnEnemyBoss();
                exec = true;
                enemiesDefeated = 0;
               
                exec = false;
            }
        }
        else
        {
            if (Time.frameCount % interval == 0)
                SpawnEnemies();
        }
    }
}
