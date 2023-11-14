using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectedPiece;
    int OIL = 1;
    void Start()
    {
          
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Lewy przycisk myszy
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.CompareTag("Puzzle")) 
            {
                if (!hit.transform.GetComponent<PiecesScrpit>().InRightPosition)
                {
                    SelectedPiece = hit.transform.gameObject;
                    SelectedPiece.GetComponent<PiecesScrpit>().Selected = true;
                    SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                    OIL++;
                }
            }
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            if (SelectedPiece != null)
            {
                SelectedPiece.GetComponent<PiecesScrpit>().Selected = false;
                SelectedPiece = null;
            }
        }
        if (SelectedPiece != null) 
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);
        }
    }
}
