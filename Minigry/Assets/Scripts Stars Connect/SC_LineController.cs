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
    //public List<Transform> targetPoints = new List<Transform>();
    public SC_Paths[] allPaths;
    public SC_ClickPoint clickPoint;
    private bool isPatternCompleted = false;
    public GameObject errorTextObject;
    public GameObject winTextObject;
    public GameObject replayButton;
    public GameObject starsNames;
    public GameObject moveButton;
    public string moveToScene = null;
    private int maxCount;

    void Start()
    {
        winTextObject.SetActive(false);
        errorTextObject.SetActive(false);
        replayButton.SetActive(true);
        starsNames.SetActive(false);
        moveButton.SetActive(false);
        maxCount = MaxCountOfPaths(allPaths);
        Debug.Log(maxCount);
        Debug.Log(allPaths.Count());
    }

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void makeLine(Transform finalPoint)
    {

        if (lastPoints == null)
        {
            lastPoints = finalPoint;
            points.Add(lastPoints);
            clickPoint.ChangePointColor(lastPoints);
        }
        else
        {
            if (points[points.Count - 1].name != finalPoint.name)
            {
                points.Add(finalPoint);
                lr.enabled = true;
                SetupLine();
                clickPoint.ChangePointColor(finalPoint);
            }

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
                foreach (Transform point in allPaths[1].paths)
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
                    moveButton.SetActive(true);
                }
                if (points.Count >= maxCount && anyPathEqual == false)
                {
                    isPatternCompleted = true;
                    errorTextObject.SetActive(true);

                }

            }
        }
    }
    public void MoveNext()
    {
        if (moveToScene != null)  SceneManager.LoadScene(moveToScene);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

    public int MaxCountOfPaths (SC_Paths[] all)
    {
        int max = 0;

        foreach (SC_Paths scPaths in all)
        {
            int count = scPaths.paths.Count;

            if (count > max)
            {
                max = count;
            }
        }

        return max;
    }
}
