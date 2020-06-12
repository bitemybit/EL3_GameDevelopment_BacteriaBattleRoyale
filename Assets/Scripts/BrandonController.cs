using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BrandonController : BaseController
{
    public bool travelling;
    private NavMeshAgent _navMeshAgent;

    public float wallTolerance = 50;
    
    public Text healthT;
    public Text armorT;
    public Text energyT;
    public Text metabolismT;
    public Text ammoT;
    public Text nameT;
    public Text StateT;
    
    public List<GameObject> boundaries;

    public SphereCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        boundaries.Add(GameObject.Find("TopBoundary"));
        boundaries.Add(GameObject.Find("BotBoundary"));
        boundaries.Add(GameObject.Find("LeftBoundary"));
        boundaries.Add(GameObject.Find("RightBoundary"));
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        //();
        if (_navMeshAgent == null)
        {
            Debug.LogError("nav mesh component is not attached");
        }
        else
        {
            SwitchState();
            //GoToCenter();
            if (transform.position.x < -5 || transform.position.z > 37f || transform.position.x > 30f || transform.position.z < 5f)
            {
               
            }
        }
        
        if (travelling && _navMeshAgent.isStopped)
        {
            travelling = false;
            SwitchState();
        }
        
        healthT.text = "Health: " + Mathf.RoundToInt(health).ToString();
        armorT.text = "Armor: " + Mathf.RoundToInt(armor).ToString();
        energyT.text = "Energy: " + Mathf.RoundToInt(energy).ToString();
        metabolismT.text = "Metabolism: " + metabolism.ToString();
        ammoT.text = "Ammo: " + Mathf.RoundToInt(ammo).ToString();

        if (!alive)
        {
            nameT.color = Color.red;
            Destroy(gameObject);
        }
    }

    private void GoToCenter()
    {
        StateT.text = "Avoiding Walls...";
        Vector3 center = new Vector3(0, 0, 0);
        _navMeshAgent.SetDestination(center);
    }

    void Rays()
    {
        RaycastHit hit1;
        RaycastHit hit2;
        RaycastHit hit3;
        RaycastHit hit4;
        
        var rayOriginX1 = new Vector3(transform.position.x + collider.radius * 2, transform.position.y, transform.position.z);
        var rayOriginX2 = new Vector3(transform.position.x - collider.radius * 2, transform.position.y, transform.position.z);
        var rayOriginZ1 = new Vector3(transform.position.x, transform.position.y, transform.position.z  + collider.radius * 2);
        var rayOriginZ2 = new Vector3(transform.position.x, transform.position.y, transform.position.z  - collider.radius * 2);
        
        if (Physics.Raycast(rayOriginX1, transform.TransformDirection(Vector3.right), out hit1, wallTolerance))
        {
            Debug.DrawRay(rayOriginX1, transform.TransformDirection(Vector3.right)*wallTolerance, Color.black);
            if (hit1.collider.gameObject.GetComponent<BoundaryController>())
            {
                Debug.DrawRay(rayOriginX1, transform.TransformDirection(Vector3.right)*wallTolerance, Color.red);
                GoToCenter();
            }
        }
        
        if (Physics.Raycast(rayOriginX2, transform.TransformDirection(Vector3.right), out hit2, wallTolerance))
        {
            Debug.DrawRay(rayOriginX2, transform.TransformDirection(-Vector3.right)*wallTolerance, Color.black);
            if (hit2.collider.gameObject.GetComponent<BoundaryController>())
            {
                Debug.DrawRay(rayOriginX1, transform.TransformDirection(Vector3.right)*wallTolerance, Color.red);
                GoToCenter();
            }
        }
        
        if (Physics.Raycast(rayOriginZ1, transform.TransformDirection(Vector3.right), out hit3, wallTolerance))
        {
            Debug.DrawRay(rayOriginZ1, transform.TransformDirection(Vector3.forward)*wallTolerance, Color.black);
            if (hit3.collider.gameObject.GetComponent<BoundaryController>())
            {
                Debug.DrawRay(rayOriginX1, transform.TransformDirection(Vector3.right)*wallTolerance, Color.red);
                GoToCenter();
            }
        }
        
        if (Physics.Raycast(rayOriginZ2, transform.TransformDirection(Vector3.right), out hit4, wallTolerance))
        {
            Debug.DrawRay(rayOriginZ2, transform.TransformDirection(-Vector3.forward)*wallTolerance, Color.black);
            if (hit4.collider.gameObject.GetComponent<BoundaryController>())
            {
                Debug.DrawRay(rayOriginX1, transform.TransformDirection(Vector3.right)*wallTolerance, Color.red);
                GoToCenter();
            }
        }
    }

    void SwitchState()
    {
        if (transform.position.x <= boundaries[3].transform.position.x - wallTolerance &&
            transform.position.x > boundaries[2].transform.position.x + wallTolerance &&
            transform.position.x >= boundaries[1].transform.position.z + wallTolerance &&
            transform.position.z <= boundaries[0].transform.position.z - wallTolerance)
        {
            FindFood();
        }
        else
        {
            GoToCenter();
            /*var lowestDist = Mathf.Infinity;
            
            for (int i = 0; i < boundaries.Count; i++)
            {
                var closest = boundaries[i].GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                var dist = Vector3.Distance(transform.position, closest);
                if (dist < lowestDist)
                {
                    dist = lowestDist;
                }
            }

            StateT.text = "" + lowestDist;

            if (lowestDist > wallTolerance)
            {
                FindFood();
            }
            else
            {
               
            }*/
        }
    }

    void FindFood()
    {
        StateT.text = "Searching Food..";
        float closestRange = Mathf.Infinity;
        GameObject closestFood = null;
        
        GameObject[] ammo_food = GameObject.FindGameObjectsWithTag("Ammo");
        GameObject[] armor_food = GameObject.FindGameObjectsWithTag("Armor");

        GameObject[] metaPlus_food = GameObject.FindGameObjectsWithTag("MetaPlus");

        if (health >= maxHealth / 2)
        {
            GameObject[] energy_food = GameObject.FindGameObjectsWithTag("Energy");
            for (var i = 0; i < energy_food.Length; i++)
            {
                float dist = Vector3.Distance(player.transform.position, energy_food[i].transform.position);
                if (dist < closestRange)
                {
                    closestRange = dist;
                    closestFood = energy_food[i];
                }
                Vector3 targetVector = closestFood.transform.position;
                _navMeshAgent.SetDestination(targetVector);
                travelling = true;
            }
        }

        if (energy > 200)
        {
            GameObject[] metaMin_food = GameObject.FindGameObjectsWithTag("MetaMin");
            for (var i = 0; i < metaMin_food.Length; i++)
            {
                float dist = Vector3.Distance(player.transform.position, metaMin_food[i].transform.position);
                if (dist < closestRange)
                {
                    closestRange = dist;
                    closestFood = metaMin_food[i];
                }
                Vector3 targetVector = closestFood.transform.position;
                _navMeshAgent.SetDestination(targetVector);
                travelling = true;
            }

            if (metabolism < 5)
            {
                GameObject[] health_food = GameObject.FindGameObjectsWithTag("Health");
                
                for (var i = 0; i < health_food.Length; i++)
                {
                    float dist = Vector3.Distance(player.transform.position, health_food[i].transform.position);
                    if (dist < closestRange)
                    {
                        closestRange = dist;
                        closestFood = health_food[i];
                    }
                    Vector3 targetVector = closestFood.transform.position;
                    _navMeshAgent.SetDestination(targetVector);
                    travelling = true;
                }
            }
        }

        if (health < maxHealth / 2)
        {
            GameObject[] health_food = GameObject.FindGameObjectsWithTag("Health");

            for (var i = 0; i < health_food.Length; i++)
            {
                float dist = Vector3.Distance(player.transform.position, health_food[i].transform.position);
                if (dist < closestRange)
                {
                    closestRange = dist;
                    closestFood = health_food[i];
                }
                Vector3 targetVector = closestFood.transform.position;
                _navMeshAgent.SetDestination(targetVector);
                travelling = true;
            }
        }
    }
}
