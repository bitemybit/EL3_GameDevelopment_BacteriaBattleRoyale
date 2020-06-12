using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (cam.orthographicSize > 10f)
        {
            var temp = cam.orthographicSize;
            cam.orthographicSize = temp - 0.001f;
        }
    }
}
