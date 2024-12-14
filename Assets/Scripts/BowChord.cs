using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
public class BowChord : MonoBehaviour
{
    [SerializeField]
    private Transform startAnchor; // dawniej endpoint1
    [SerializeField]
    private Transform endAnchor;   // dawniej endpoint2

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void GenerateString(Vector3? midPoint)
    {
        Vector3[] pointInLine = new Vector3[midPoint.HasValue ? 3 : 2];
        pointInLine[0] = startAnchor.localPosition;

        if (midPoint.HasValue)
        {
            pointInLine[1] = transform.InverseTransformPoint(midPoint.Value);
        }

        pointInLine[^1] = endAnchor.localPosition;

        lineRenderer.positionCount = pointInLine.Length;
        lineRenderer.SetPositions(pointInLine);
    }

    private void Start()
    {
        GenerateString(null);
    }

}
