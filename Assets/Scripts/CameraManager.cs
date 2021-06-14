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

    public JaniBaseController janiBaseController;
    public KBaseController kaanBaseController;
    public BrandonController brandonBaseController;
    public GBaseController gokhanBaseController;

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
    {// Feels like this could've been done with an array of all the bacteria and you just use a for loop to see how many are alive.
        // I would also just have the name in the bacteria and you get it when it's the last one alive. You can fill the winner text like this:
        // winnerText.text = $"{winnerName}'s Bacteria Wins!";
        // Winnername is then taken from the bacteria
        // If the positioning in the arrays is always the same (it seems like it), you could've also used an enum in combination with a dictionary
        // That way you can also remove without using an array.
        if (!kaanAlive)
        {
            janiBaseController.RemoveEnemy(0);
            brandonBaseController.RemoveEnemy(0);
            gokhanBaseController.RemoveEnemy(0);
        }

        if (!gokhanAlive)
        {
            janiBaseController.RemoveEnemy(1);
            brandonBaseController.RemoveEnemy(1);
            kaanBaseController.RemoveEnemy(0);
        }

        if (!brandonAlive)
        {
            janiBaseController.RemoveEnemy(2);
            gokhanBaseController.RemoveEnemy(1);
            kaanBaseController.RemoveEnemy(1);
        }

        if (!janiAlive)
        {
            brandonBaseController.RemoveEnemy(2);
            gokhanBaseController.RemoveEnemy(2);
            kaanBaseController.RemoveEnemy(2);
        }

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
        {// Apparently you did write parts of it, but I would've rewritten this to a struct that contains two variables (camera & canvas) and then just use a dictionary with the aforementioned enum
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
