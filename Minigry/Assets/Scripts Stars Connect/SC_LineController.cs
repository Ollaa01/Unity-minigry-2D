using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SC_LineController : MonoBehaviour
{
    private LineRenderer lr;
    public List<Transform> points = new List<Transform>();
    public Transform lastPoints;
    public List<Transform> targetPoints = new List<Transform>();
    public SC_Paths[] allPaths;
    public SC_ClickPoint clickPoint;
    private bool isPatternCompleted = false;
    public GameObject errorTextObject;
    public GameObject winTextObject;
    public GameObject replayButton;
    public GameObject starsNames;

    void Start()
    {
        winTextObject.SetActive(false);
        errorTextObject.SetActive(false);
        replayButton.SetActive(true);
        starsNames.SetActive(false);
    }

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void makeLine(Transform finalPoint)
    {
        if(lastPoints == null)
        {
            lastPoints = finalPoint;
            points.Add(lastPoints);
            clickPoint.ChangePointColor(lastPoints);
        }
        else
        {
            points.Add(finalPoint);
            lr.enabled = true;
            SetupLine();
            clickPoint.ChangePointColor(finalPoint);
        }
        
    }

    private void SetupLine()
    {
        int pointLength = points.Count;
        lr.positionCount = pointLength;
        for (int i = 0; i < pointLength; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }

    private bool IsNextTarget(Transform clickedPoint)
    {
        if (targetPoints.Count == 0)
            return false;  
        return clickedPoint == targetPoints[0];
    }

    private void ResetPattern()
    {
        isPatternCompleted = false;
        targetPoints.Clear();
    }

    void Update()
    {
        if (!isPatternCompleted && Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                makeLine(hit.collider.transform);
                print(hit.collider.name);
                bool anyPathEqual = false;

                foreach (SC_Paths paths in allPaths)
                {
                    bool areEqual = points.SequenceEqual(paths.paths);

                    if (areEqual)
                    {
                        anyPathEqual = true;
                        break; 
                    }
                }
                foreach (Transform point in allPaths[0].paths)
                {
                    Debug.Log("T " + point.name); 
                }
                foreach (Transform point in points)
                {
                    Debug.Log("P " + point.name); 
                }
                if (anyPathEqual)
                {
                    isPatternCompleted = true;
                    winTextObject.SetActive(true);
                    starsNames.SetActive(true);
                }
                if (points.Count >= targetPoints.Count && anyPathEqual == false) 
                {
                    isPatternCompleted = true;
                    errorTextObject.SetActive(true);

                }

            }
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
