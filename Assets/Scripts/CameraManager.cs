using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera brandonCamera;
    public Canvas brandonCanvas;
    public Camera kaanCamera;
    public Canvas kaanCanvas;
    public Camera gokhanCamera;
    public Canvas gokhanCanvas;
    public Camera janiCamera;
    public Canvas janiCanvas;

    public bool brandonAlive;
    public bool kaanAlive;
    public bool gokhanAlive;
    public bool janiAlive;

    public Text winnerText;

    public bool winnerDisplayed;

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
        janiCamera.enabled = false;
        janiCanvas.enabled = false;

        brandonAlive = true;
        kaanAlive = true;
        gokhanAlive = true;
        janiAlive = true;

        winnerDisplayed = false;

        winnerText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!brandonAlive && !kaanAlive && !gokhanAlive && janiAlive && !winnerDisplayed)
        {
            winnerDisplayed = true;
            winnerText.enabled = true;
            winnerText.color = new Color(255, 0, 183);
            winnerText.text = "Jani's Bacteria Wins!";
        }
        else if (!brandonAlive && !kaanAlive && gokhanAlive && !janiAlive && !winnerDisplayed)
        {
            winnerDisplayed = true;
            winnerText.enabled = true;
            winnerText.color = new Color(255, 255, 0);
            winnerText.text = "Gokhan's Bacteria Wins!";
        }
        else if (!brandonAlive && kaanAlive && !gokhanAlive && !janiAlive && !winnerDisplayed)
        {
            winnerDisplayed = true;
            winnerText.enabled = true;
            winnerText.color = Color.blue;
            winnerText.text = "Kaan's Bacteria Wins!";
        }
        else if (brandonAlive && !kaanAlive && !gokhanAlive && !janiAlive && !winnerDisplayed)
        {
            winnerDisplayed = true;
            winnerText.enabled = true;
            winnerText.color = new Color(255, 255, 255);
            winnerText.text = "Brandon's Bacteria Wins!";
        }
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
                    janiCamera.enabled = false;
                    janiCanvas.enabled = false;
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
                    janiCamera.enabled = false;
                    janiCanvas.enabled = false;
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
                    janiCamera.enabled = false;
                    janiCanvas.enabled = false;
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
                    janiCamera.enabled = false;
                    janiCanvas.enabled = false;
                    gokhanCamera.enabled = true;
                    gokhanCanvas.enabled = true;
                    break;
                }

            case 4:
                {
                    mainCamera.enabled = false;
                    kaanCamera.enabled = false;
                    kaanCanvas.enabled = false;
                    brandonCamera.enabled = false;
                    brandonCanvas.enabled = false;
                    gokhanCamera.enabled = false;
                    gokhanCanvas.enabled = false;
                    janiCamera.enabled = true;
                    janiCanvas.enabled = true;
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
