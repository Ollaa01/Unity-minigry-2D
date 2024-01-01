using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ClickEffectDestroyer : MonoBehaviour
{
    private GameObject targetObject;

    // Inicjalizacja obiektu docelowego
    public void Initialize(GameObject target)
    {
        targetObject = target;
    }

    private void Update()
    {
        // Sprawd�, czy zosta� wykonany klik mysz�
        if (Input.GetMouseButtonDown(0))
        {
            // Przekonwertuj pozycj� myszy na �wiatow�
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Utw�rz promie� od pozycji myszy
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            // Sprawd�, czy klikni�to na inny obiekt ni� ten, kt�ry wywo�a� efekt Particle System
            if (hit.collider != null && hit.collider.gameObject != this.gameObject)
            {
                // Zniszcz efekt Particle System
                Destroy(gameObject);
            }
        }
    }
}

