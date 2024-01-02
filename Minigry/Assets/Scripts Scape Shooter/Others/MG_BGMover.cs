using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controls the scrolling movement of the background.
 */
public class MG_BGMover : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.3f; /** The speed at which the background scrolls. */
    private Vector2 offset; /** The offset for scrolling. */

    /**
     * Update is called once per frame.
     */
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
