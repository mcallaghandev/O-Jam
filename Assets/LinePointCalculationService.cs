using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LinePointCalculationService : MonoBehaviour
{
    [SerializeField]
    GameObject line;

    private LineRenderer lineRenderer;

    [SerializeField]
    GameObject circle;

    private ScoreManager scoreManager;

    [SerializeField]
    private float minimumDistanceFromCircle;

    [SerializeField]
    private float lineEndsTolerance;

    void Start()
    {
        lineRenderer = line.GetComponent<LineRenderer>();
        scoreManager = circle.GetComponent<ScoreManager>();
        scoreManager.ClearScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer.positionCount > 0)
        {
            StartCoroutine(CalculatePercentDistance());
            IsLineTooClose();
            IsCircleComplete();
        }
        else
        {
            scoreManager.ClearScoreText();
        }
    }

    private void IsCircleComplete()
    {
        if (!Input.GetMouseButtonUp(0))
        {
            return;
        }

        var startPoint = lineRenderer.GetPosition(0);
        var endPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        var distance = Vector3.Distance(startPoint, endPoint);

        if(distance > lineEndsTolerance)
        {
            scoreManager.SetDebugText($"Not a circle! :O");
        }
    }

    private void IsLineTooClose()
    {
        var allLinePoints = new List<Vector3>();
        List<float> distances = new List<float>();

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            allLinePoints.Add(lineRenderer.GetPosition(i));
        }

        if (allLinePoints.Count > 250)
        {
            foreach (var linePoint in allLinePoints)
            {
                distances.Add(Vector3.Distance(linePoint, circle.transform.position));
            }

            var average = distances.Average();

            if(average < minimumDistanceFromCircle)
            {
                scoreManager.SetDebugText($"Too close! :)");
            }
        }
    }

    private IEnumerator CalculatePercentDistance()
    {
        var allLinePoints = new List<Vector3>();
        List<float> distances = new List<float>();

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            allLinePoints.Add(lineRenderer.GetPosition(i));
        }

        if (allLinePoints.Count > 5)
        {

            foreach (var linePoint in allLinePoints)
            {
                distances.Add(Vector3.Distance(linePoint, circle.transform.position));
            }

            var average = distances.Average();
            var max = distances.Max();

            var percent = Math.Round(average / max * 100, 2);

            scoreManager.SetScoreText(percent);
        }

        yield return null;
    }
}
