using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
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
        if (collision.gameObject.GetComponent<PlayerController>() != null) 
        {
            Debug.Log("tu");

            if (powerUpType == PowerUpType.Projectile)
            {
                if (GameStatsManager.Instance != null)
                    GameStatsManager.Instance.AddMissilesByAmount(numToGive);
            }
            else
            {
                if (collision.gameObject.GetComponent<HealthManager>())
                    collision.gameObject.GetComponent<HealthManager>().IncreaseHealth(numToGive);
            }
            Destroy(gameObject);
        }

    }
}
