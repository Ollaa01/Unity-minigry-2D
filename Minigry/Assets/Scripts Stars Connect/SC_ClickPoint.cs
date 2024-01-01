using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Dodaj ten namespace

public class SC_ClickPoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color clickedColor = Color.blue;
    public GameObject clickEffectPrefab;
    private GameObject currentClickEffect;
    private GameObject clickEffect;
    // Przechowuje aktualny efekt Particle System

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void ChangePointColor(Transform point)
    {
        if (spriteRenderer != null)
        {
            //point.GetComponent<SpriteRenderer>().color = clickedColor;
            //spriteRenderer.color = clickedColor;


            // Dodaj efekt Particle System w miejscu klikniêcia
            if (clickEffectPrefab != null)
            {
                clickEffect = Instantiate(clickEffectPrefab, point.position, Quaternion.identity);
                // Dodaj skrypt do efektu, który zniszczy go po klikniêciu innego obiektu
                SC_ClickEffectDestroyer destroyer = clickEffect.AddComponent<SC_ClickEffectDestroyer>();
                destroyer.Initialize(point.gameObject);
            }
        }
    }

    // Nowa metoda do dezaktywacji efektu Particle System
    public void DeactivateClickEffect()
    {
        if (clickEffect != null)
        {
            clickEffect.SetActive(false);
        }
    }
}
