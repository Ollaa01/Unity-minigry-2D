using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMover : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.3f;
    private Vector2 offset;
    // Update is called once per frame
    private void Update()
    {
        if (!GetComponent<MeshRenderer>())
            return;
        float scrollAmount = Time.time * scrollSpeed;
        offset.x = 0f;
        offset.y = scrollAmount;
        GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", offset);
    }
}
