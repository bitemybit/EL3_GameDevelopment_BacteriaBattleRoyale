using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickMoveCharacter : MonoBehaviour
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
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit) 
            {
                GetComponent<NavMeshAgent>().speed = GetComponent<BaseController>().speed;
                GetComponent<NavMeshAgent>().acceleration = GetComponent<BaseController>().speed;
                GetComponent<NavMeshAgent>().destination = hitInfo.point;
            }
        } 
    }
}
