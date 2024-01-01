using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class P_PiecesScript : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;

    private static int piecesInRightPosition = 0; 
    private static bool hasWon = false; 

    public static bool CheckIfWon()
    {
        return hasWon;
    }
    void Start()
    {
        hasWon = false;
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(2f, 13f), Random.Range(-5f, 5f));
       
    }

    // Update is called once per frame
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
