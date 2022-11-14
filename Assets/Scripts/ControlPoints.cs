using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoints : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            addSphere();
        }
    }

    private void addSphere()
    {

        var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
        pos.y = 0;
        var sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sp.transform.SetPositionAndRotation(pos, Quaternion.identity);
        sp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}