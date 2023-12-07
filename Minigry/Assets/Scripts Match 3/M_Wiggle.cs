using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Wiggle : MonoBehaviour
{
    private RectTransform rectTransform;
    private float originalRotation;
    public float wiggleAmount = 10f;
    public float wiggleSpeed = 5f;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalRotation = rectTransform.rotation.eulerAngles.z;
        StartWiggle();
    }

    private void Update()
    {
        // StartWiggle();
    }

    public void StartWiggle()
    {
        // Funkcja uruchamiaj¹ca efekt kiwania
        StartCoroutine(WiggleAnimation());
    }

    private System.Collections.IEnumerator WiggleAnimation()
    {
        Debug.Log("StartWiggleEffect2 called");
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * wiggleSpeed;

            float rotationOffset = Mathf.Sin(elapsedTime * Mathf.PI * 2);
            rectTransform.rotation = Quaternion.Euler(0f, 0f, originalRotation + rotationOffset);

            yield return null;
        }

        rectTransform.rotation = Quaternion.Euler(0f, 0f, originalRotation); 
    }
}
