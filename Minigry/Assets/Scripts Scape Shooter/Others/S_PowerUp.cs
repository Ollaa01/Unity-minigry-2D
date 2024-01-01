using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Projectile, Health
    }
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private int numToGive = 10;

    private void Start()
    {
        Debug.Log("powerSpawn");
        transform.position = new Vector2(Random.Range(-7f, 7f), 4.4f);
        //.position = new Vector2(1f, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<S_PlayerController>() != null) 
        {
            Debug.Log("tu");

            if (powerUpType == PowerUpType.Projectile)
            {
                if (S_GameStatsManager.Instance != null)
                    S_GameStatsManager.Instance.AddMissilesByAmount(numToGive);
            }
            else
            {
                if (collision.gameObject.GetComponent<S_HealthManager>())
                    collision.gameObject.GetComponent<S_HealthManager>().IncreaseHealth(numToGive);
            }
            Destroy(gameObject);
        }

    }
}
