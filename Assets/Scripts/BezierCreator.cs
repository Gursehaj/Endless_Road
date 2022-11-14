using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
class BezierCreator : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    public GameObject controlPoint1;
    public GameObject controlPoint2;

    Vector3 p1;
    Vector3 p2;
    Vector3 c1;
    Vector3 c2;

    private List<Vector3> bezPoints;

    private LineRenderer lineRenderer;

    [Range(1, 50)]
    public int stepSize;

    private void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    private void Update()
    {
        p1 = point1.transform.position;
        p2 = point2.transform.position;

        c1 = controlPoint1.transform.position;
        c2 = controlPoint2.transform.position;
        lineRenderer.positionCount = stepSize + 1;
        for (int i = 0; i <= stepSize; i++)
        {
            var t = i / (float)stepSize;
            var point = Mathf.Pow((1 - t), 3) * p1 + 3 * Mathf.Pow((1 - t), 2) * t * c1 + 3 * (1 - t) * Mathf.Pow(t, 2) * c2 + Mathf.Pow(t, 3) * p2;
            lineRenderer.SetPosition(i, point);
        }
    }
}