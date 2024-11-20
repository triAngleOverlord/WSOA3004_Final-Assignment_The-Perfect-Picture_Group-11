using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_testting : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private lr_lineController line;
    private void Start()
    {
        line.SetUpLine(points);
    }

    
}
