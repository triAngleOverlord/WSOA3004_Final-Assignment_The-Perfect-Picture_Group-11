using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_lineController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform[] points;

    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
        
    }
}
