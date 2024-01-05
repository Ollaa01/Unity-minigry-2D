/**
 * P_PiecesScript.cs
 * Handles the behavior of individual puzzle pieces.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/**
 * P_PiecesScript class.
 * Handles the behavior of individual puzzle pieces.
 */
public class P_PiecesScript : MonoBehaviour
{
    private Vector3 RightPosition; /** The correct position for the puzzle piece. */
    public bool InRightPosition; /** Flag indicating whether the puzzle piece is in the correct position. */
    public bool Selected; /** Flag indicating whether the puzzle piece is selected by the player. */
    private static int piecesInRightPosition = 0; /** Counter for puzzle pieces in the correct position. */
    private static bool hasWon = false; /** Flag indicating whether the player has won the puzzle. */

    /**
     * Checks if the player has won the puzzle.
     * @return True if the player has won, otherwise false.
     */
    public static bool CheckIfWon()
    {
        return hasWon;
    }

    /**
     * Start is called before the first frame update.
     */
    void Start()
    {
        piecesInRightPosition = 0;
        hasWon = false;
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(2f, 13f), Random.Range(-5f, 5f));
       
    }

    /**
     * Update is called once per frame. Checking if all puzles are in right position.
     */
    void Update()
    {
        if (Vector3.Distance(transform.position, RightPosition) < 0.5f)
        {
            if (!Selected)
            {
                if (InRightPosition == false)
                {
                    transform.position = RightPosition;
                    InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                    piecesInRightPosition++;
                    Debug.Log(piecesInRightPosition);
                }
                if (piecesInRightPosition == 36)
                {
                    if (!hasWon)
                    {
                        hasWon = true;
                        Debug.Log("You win");
                        MG_MGStatus.Instance.GamePassed("PuzzlePlayed");
                    }
                }
            }
        }
    }
}
