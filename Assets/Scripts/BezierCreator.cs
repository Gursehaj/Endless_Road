using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
class BezierCreator : MonoBehaviour
{
    [SerializeField]
    bool isDebugging;

    List<Vector3> inputPoints = new List<Vector3>();
    List<Vector3> controlPoints = new List<Vector3>();
    LineRenderer lineRenderer;

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

                var c1 = Vector3.Cross((p2 - p1), Vector3.up);
                var c2 = Vector3.Cross((p1 - p2), Vector3.up);

                Debug.DrawLine(p1, c1);
                Debug.DrawLine(p2, c2);

                //var ang1 = Vector3.SignedAngle(p1 , c1 , Vector3.up);
                //var ang2 = Vector3.SignedAngle(p2 , c2 , Vector3.up);

                //print(ang1);
                //print(ang2);

                controlPoints.Add(c1);
                controlPoints.Add(c2);

                

                //for (int i = 0; i <= stepSize; i++)
                //{
                //    var t = i / (float)stepSize;
                //    var point = Mathf.Pow((1 - t), 3) * p1 + 3 * Mathf.Pow((1 - t), 2) * t * c1 + 3 * (1 - t) * Mathf.Pow(t, 2) * c2 + Mathf.Pow(t, 3) * p2;
                //    lineRenderer.SetPosition(i, point);
                //}
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
            }
        }
        catch (System.Exception)
        {
        }
    }
}