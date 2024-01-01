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
        // SprawdŸ, czy zosta³ wykonany klik mysz¹
        if (Input.GetMouseButtonDown(0))
        {
            // Przekonwertuj pozycjê myszy na œwiatow¹
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Utwórz promieñ od pozycji myszy
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            // SprawdŸ, czy klikniêto na inny obiekt ni¿ ten, który wywo³a³ efekt Particle System
            if (hit.collider != null && hit.collider.gameObject != this.gameObject)
            {
                // Zniszcz efekt Particle System
                Destroy(gameObject);
            }
        }
    }
}

