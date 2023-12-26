using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_RandomPuzzle : MonoBehaviour
{
    public GameObject Canvas;
    public Image[] puzzleImages; 

    void Start()
    {
        Canvas.SetActive(false);

        SetPuzzles(puzzleImages[Random.Range(0, puzzleImages.Length)]);
    }
    public void SetPuzzles(Image Photo)
    {
        for (int i = 0; i < 36; i++)
        {
            GameObject.Find("Piece (" + i + ")").transform.Find("Image").GetComponent<SpriteRenderer>().sprite = Photo.sprite;
        }
    }


}
