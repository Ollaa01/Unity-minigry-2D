using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Laser : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy = 1f;
    [SerializeField] private int damage = 25;
    private bool collided = false;

    private float timer = 0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        if (collision.gameObject.GetComponent<S_HealthManager>())
            collision.gameObject.GetComponent<S_HealthManager>().DecreaseHealth(damage);
        S_PlayerController.Instance.ReleaseLaser(gameObject);
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       timer += Time.deltaTime;
       if((timer >= timeToDestroy) && !collided)
        {
            timer = 0f;
            S_PlayerController.Instance.ReleaseLaser(gameObject);
        }
    }
}
