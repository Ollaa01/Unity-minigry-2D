using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    private float explosionEffectLength = 10f;
    [SerializeField]
    private float timeToDestroy = 1f;
    private bool collided = false;
    [SerializeField] private int damage = 100;
    private float timer = 0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation) as GameObject;
            Destroy(explosion, explosionEffectLength);
        }
        if (collision.gameObject.GetComponent<HealthManager>())
            collision.gameObject.GetComponent<HealthManager>().DecreaseHealth(damage);
        PlayerController.Instance.ReleaseMissile(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            timer += Time.deltaTime;
            if ((timer >= timeToDestroy) && !collided)
            {
                timer = 0f;
                Debug.Log("Im here");
                PlayerController.Instance.ReleaseMissile(gameObject);
            }
    }
}
