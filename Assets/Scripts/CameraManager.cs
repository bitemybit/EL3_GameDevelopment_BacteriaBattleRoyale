using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera brandonCamera;
    public Canvas brandonCanvas;
    public Camera kaanCamera;
    public Canvas kaanCanvas;
    public Camera gokhanCamera;
    public Canvas gokhanCanvas;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        brandonCamera.enabled = false;
        brandonCanvas.enabled = false;
        kaanCamera.enabled = false;
        kaanCanvas.enabled = false;
        gokhanCamera.enabled = false;
        gokhanCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCam(int id)
    {
        switch (id)
        {
            case 0:
                {
                    brandonCamera.enabled = false;
                    brandonCanvas.enabled = false;
                    kaanCamera.enabled = false;
                    kaanCanvas.enabled = false;
                    gokhanCamera.enabled = false;
                    gokhanCanvas.enabled = false;
                    mainCamera.enabled = true;
                    break;
                }

            case 1:
                {
                    mainCamera.enabled = false;
                    brandonCamera.enabled = false;
                    brandonCanvas.enabled = false;
                    gokhanCamera.enabled = false;
                    gokhanCanvas.enabled = false;
                    kaanCamera.enabled = true;
                    kaanCanvas.enabled = true;
                    break;
                }

            case 2:
                {
                    mainCamera.enabled = false;
                    gokhanCamera.enabled = false;
                    gokhanCanvas.enabled = false;
                    kaanCamera.enabled = false;
                    kaanCanvas.enabled = false;
                    brandonCamera.enabled = true;
                    brandonCanvas.enabled = true;
                    break;
                }

            case 3:
                {
                    mainCamera.enabled = false;
                    kaanCamera.enabled = false;
                    kaanCanvas.enabled = false;
                    brandonCamera.enabled = false;
                    brandonCanvas.enabled = false;
                    gokhanCamera.enabled = true;
                    gokhanCanvas.enabled = true;
                    break;
                }

            default:
                {
                    brandonCamera.enabled = false;
                    brandonCanvas.enabled = false;
                    kaanCamera.enabled = false;
                    kaanCanvas.enabled = false;
                    gokhanCamera.enabled = false;
                    gokhanCanvas.enabled = false;
                    mainCamera.enabled = true;
                    break;
                }
        }
    }
}
