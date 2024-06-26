using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles the behavior of a laser projectile in the game.
 */
public class S_Laser : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy = 1f; /** The time it takes for the laser to self-destruct. */
    [SerializeField] private int damage = 25; /** The amount of damage the laser inflicts. */
    private bool collided = false; /** Flag indicating whether the laser has collided with an object. */
    private float timer = 0f; /** Timer to track the time since the laser was instantiated. */

    /**
     * Called when the laser collides with another 2D collider.
     * @param collision The collision data.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        if (collision.gameObject.GetComponent<S_HealthManager>())
            collision.gameObject.GetComponent<S_HealthManager>().DecreaseHealth(damage);
        S_PlayerController.Instance.ReleaseLaser(gameObject);
       
    }

    /**
     * Called once per frame to update the state of the laser.
     */
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
