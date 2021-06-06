using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera brandonCamera;
    public Camera kaanCamera;
    public Camera gokhanCamera;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        brandonCamera.enabled = false;
        kaanCamera.enabled = false;
        gokhanCamera.enabled = false;
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
                    kaanCamera.enabled = false;
                    gokhanCamera.enabled = false;
                    mainCamera.enabled = true;
                    break;
                }

            case 1:
                {
                    mainCamera.enabled = false;
                    brandonCamera.enabled = false;
                    gokhanCamera.enabled = false;
                    kaanCamera.enabled = true;
                    break;
                }

            case 2:
                {
                    mainCamera.enabled = false;
                    gokhanCamera.enabled = false;
                    kaanCamera.enabled = false;
                    brandonCamera.enabled = true;
                    break;
                }

            case 3:
                {
                    mainCamera.enabled = false;
                    kaanCamera.enabled = false;
                    brandonCamera.enabled = false;
                    gokhanCamera.enabled = true;
                    break;
                }

            default:
                {
                    brandonCamera.enabled = false;
                    kaanCamera.enabled = false;
                    gokhanCamera.enabled = false;
                    mainCamera.enabled = true;
                    break;
                }
        }
    }
}
