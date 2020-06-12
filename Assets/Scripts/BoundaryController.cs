using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    private int id;
    void Start()
    {
        if (gameObject.name == "TopBoundary")
        {
            id = 1;
        }
        else if (gameObject.name == "BotBoundary")
        {
            id = 2;
        }
        else if (gameObject.name == "LeftBoundary")
        {
            id = 3;
        }
        else if (gameObject.name == "RightBoundary")
        {
            id = 4;
        }
    }

    void Update()
    {
        switch (id)
        {
            case 1:
                {
                    Vector3 temp = transform.position;
                    temp.z -= 0.002f;
                    transform.position = temp;
                    break;
                }
            case 2:
                {
                    Vector3 temp = transform.position;
                    temp.z += 0.002f;
                    transform.position = temp;
                    break;
                }
            case 3:
                {
                    Vector3 temp = transform.position;
                    temp.x += 0.00259f;
                    transform.position = temp;
                    break;
                }
            case 4:
                {
                    Vector3 temp = transform.position;
                    temp.x -= 0.00259f;
                    transform.position = temp;
                    break;
                }
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseController>() != null)
        {
            other.GetComponent<BaseController>().alive = false;
        }
        else
        {
                    
            Destroy(other.gameObject);
        }
    }

   
}

