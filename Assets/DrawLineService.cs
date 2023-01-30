using System.Collections;
using UnityEngine;

public class DrawLineService : MonoBehaviour
{
    Coroutine drawing;

    [SerializeField]
    GameObject line;

    private LineRenderer lineRenderer;

    [SerializeField]
    int lineDeleteSecondsDelay;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = line.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartLine();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(EndLine());
        }
    }

    private IEnumerator EndLine()
    {
        StopCoroutine(drawing);

        yield return new WaitForSeconds(lineDeleteSecondsDelay);

        lineRenderer.SetPositions(new Vector3[] { });
        lineRenderer.positionCount = 0;
    }

    private void StartLine()
    {
        if (drawing != null)
        {
            EndLine();
        }

        drawing = StartCoroutine(DrawLine());
    }

    private IEnumerator DrawLine()
    {
        lineRenderer.positionCount = 0;

        while (true)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePosition);
            lineRenderer.sortingOrder = 1;
            yield return null;
        }
    }
}
