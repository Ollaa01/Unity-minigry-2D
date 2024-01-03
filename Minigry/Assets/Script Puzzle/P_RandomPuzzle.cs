/**
 * P_RandomPuzzle.cs
 * Sets up a random puzzle by assigning a chosen image to puzzle pieces.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * P_RandomPuzzle class.
 * Sets up a random puzzle by assigning a chosen image to puzzle pieces.
 */
public class P_RandomPuzzle : MonoBehaviour
{
    public GameObject Canvas; /** Reference to the canvas containing the puzzle pieces. */
    public Image[] puzzleImages; /** Array of puzzle images to choose from. */

    /**
     * Start is called before the first frame update.
     */

    void Start()
    {
        Canvas.SetActive(false);

        SetPuzzles(puzzleImages[Random.Range(0, puzzleImages.Length)]);
    }

    /**
     * Sets the puzzle pieces with the provided image.
     * @param Photo The image to assign to puzzle pieces.
     */
    public void SetPuzzles(Image Photo)
    {
        for (int i = 0; i < 36; i++)
        {
            GameObject.Find("Piece (" + i + ")").transform.Find("Image").GetComponent<SpriteRenderer>().sprite = Photo.sprite;
        }
    }


}
