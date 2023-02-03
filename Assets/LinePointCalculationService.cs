using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private float lineEndsToleranceMax;

    void Start()
    {
        lineRenderer = line.GetComponent<LineRenderer>();
        scoreManager = circle.GetComponent<ScoreManager>();
        scoreManager.ClearScoreText();
        scoreManager.ClearLineInfoText();
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer.positionCount > 0)
        {
            StartCoroutine(CalculatePercentDistance());

            if (Input.GetMouseButtonUp(0))
            {
                IsCircleComplete();
            }
            else
            {
                IsLineTooClose();
            }
        }
        else
        {
            scoreManager.ClearScoreText();
        }
    }

    private IEnumerator EndIsLineInfoCheckComplete()
    {
        yield return new WaitForSeconds(2);

        scoreManager.ClearLineInfoText();
    }

    private void IsCircleComplete()
    {
        var startPoints = GetRangeOfLinePositions(lineRenderer, 0, 50);
        var endPoints = GetRangeOfLinePositions(lineRenderer, lineRenderer.positionCount - 1 - 50, lineRenderer.positionCount - 1);

        if(AreAllOfTheEndPointsAwayFromEachOther(startPoints, endPoints))
        {
            scoreManager.SetLineInfoText($"Not a circle! :O");
            StartCoroutine(EndIsLineInfoCheckComplete());
        }
    }

    private IEnumerable<Vector3> GetRangeOfLinePositions(LineRenderer line, int startIndex, int count)
    {
        var positions = new List<Vector3>();

        for (int i = startIndex; i < count; i++)
        {
            positions.Add(line.GetPosition(i));
        }

        return positions;
    }

    private bool AreAllOfTheEndPointsAwayFromEachOther(IEnumerable<Vector3> startPoints, IEnumerable<Vector3> endPoints)
    {
        var distances = new List<float>();

        foreach(var startPoint in startPoints)
        {
            foreach(var endPoint in endPoints)
            {
                distances.Add(Vector3.Distance(startPoint, endPoint));
            }
        }

        return distances.All(e => e > lineEndsToleranceMax);
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
                scoreManager.SetLineInfoText($"Too close! :)");
                StartCoroutine(EndIsLineInfoCheckComplete());
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
