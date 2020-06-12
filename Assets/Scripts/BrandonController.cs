using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BrandonController : BaseController
{
    public bool travelling;
    private NavMeshAgent _navMeshAgent;
    
    public Text healthT;
    public Text armorT;
    public Text energyT;
    public Text metabolismT;
    public Text ammoT;
    public Text nameT;

    public GameObject boundary1;
    public GameObject boundary2;
    public GameObject boundary3;
    public GameObject boundary4;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        
        if (_navMeshAgent == null)
        {
            Debug.LogError("nav mesh component is not attached");
        }
        else
        {
            FindFood();
            //GoToCenter();
            if (transform.position.x < -5 || transform.position.z > 37f || transform.position.x > 30f || transform.position.z < 5f)
            {
               
            }
        }
        
        if (travelling && _navMeshAgent.isStopped)
        {
            travelling = false;
            FindFood();
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

    void GoToCenter()
    {
        Vector3 center = new Vector3(11f, -48.22035f, 23.2f);
        _navMeshAgent.SetDestination(center);
    }

    void FindFood()
    {
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
