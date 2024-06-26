/**
 * SC_ClickEffectDestroyer.cs
 * Destroys the click effect after clicking another object.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * SC_ClickEffectDestroyer class.
 * Destroys the click effect after clicking another object.
 */
public class SC_ClickEffectDestroyer : MonoBehaviour
{
    private GameObject targetObject; /** Reference to the target object that initiated the click effect. */

    /**
     * Initializes the target object.
     * @param target The target object that initiated the click effect.
     */
    public void Initialize(GameObject target)
    {
        targetObject = target;
    }

    /**
     * Update is called once per frame.
     */
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject != this.gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
}
