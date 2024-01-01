using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyWave : MonoBehaviour
{
    internal int Length;
    
    public int NumOfEnemies()
    {
        // Pobierz wszystkie obiekty klasy Enemy, które s¹ dzieci¹ tego obiektu EnemyWave.
        S_Enemy[] enemies = GetComponentsInChildren<S_Enemy>();
        Debug.Log("enemies in wave " + enemies.Length);
        // Zwróæ liczbê obiektów klasy Enemy w tablicy.
        return enemies.Length;

    }

    public void DestroyWave()
    {
        Destroy(gameObject);
    }

}
