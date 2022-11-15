using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
class BezierCreator : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    public GameObject controlPoint1;
    public GameObject controlPoint2;

    //Vector3 p1;
    //Vector3 p2;
    //Vector3 c1;
    //Vector3 c2;

    List<Vector3> inputPoints = new List<Vector3>();
    List<Vector3> controlPoints = new List<Vector3>();


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
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Add point");
            var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            pos.y = 0;
            inputPoints.Add(pos);
            DrawBezier();
        }

        //p1 = point1.transform.position;
        //p2 = point2.transform.position;

        //c1 = controlPoint1.transform.position;
        //c2 = controlPoint2.transform.position;
        //lineRenderer.positionCount = stepSize + 1;
        //for (int i = 0; i <= stepSize; i++)
        //{
        //    var t = i / (float)stepSize;
        //    var point = Mathf.Pow((1 - t), 3) * p1 + 3 * Mathf.Pow((1 - t), 2) * t * c1 + 3 * (1 - t) * Mathf.Pow(t, 2) * c2 + Mathf.Pow(t, 3) * p2;
        //    lineRenderer.SetPosition(i, point);
        //}
    }

    private void DrawBezier()
    {
        if(inputPoints.Count >= 2)
        {
            lineRenderer.positionCount = stepSize + 1;
            for (int j = 0; j < inputPoints.Count; j++)
            {
                var p1 = inputPoints[j];
                var p2 = inputPoints[j + 1];

                var c1 = Vector3.Cross((p2 - p1), Vector3.up).normalized * 10;
                var c2 = Vector3.Cross((p1 - p2), Vector3.up).normalized * 10;

                controlPoints.Add(c1);
                controlPoints.Add(c2);
                var sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sp.transform.SetPositionAndRotation(c1, Quaternion.identity);

                sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sp.transform.SetPositionAndRotation(c2, Quaternion.identity);

                for (int i = 0; i <= stepSize; i++)
                {
                    var t = i / (float)stepSize;
                    var point = Mathf.Pow((1 - t), 3) * p1 + 3 * Mathf.Pow((1 - t), 2) * t * c1 + 3 * (1 - t) * Mathf.Pow(t, 2) * c2 + Mathf.Pow(t, 3) * p2;
                    lineRenderer.SetPosition(i, point);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        try
        {
            for (int i = 0; i < inputPoints.Count; i++)
            {
                Gizmos.DrawSphere(inputPoints[i], 0.5f);
                if (inputPoints.Count >= 2)
                {
                    Gizmos.DrawLine(inputPoints[i], inputPoints[i + 1]);
                }
            }

            for (int i = 0; i < controlPoints.Count; i++)
            {
                Gizmos.DrawSphere(controlPoints[i], 0.5f);
                //if (inputPoints.Count >= 2)
                //{
                //    Gizmos.DrawLine(inputPoints[i], inputPoints[i + 1]);
                //}
            }
        }
        catch (System.Exception)
        {
        }
    }
}