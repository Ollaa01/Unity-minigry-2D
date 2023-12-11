using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Dodaj ten namespace

public class SC_ClickPoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color clickedColor = Color.blue;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void ChangePointColor(Transform point)
    {
        if (spriteRenderer != null)
        {
            point.GetComponent<SpriteRenderer>().color = clickedColor;
            spriteRenderer.color = clickedColor;
        }
    }
}
