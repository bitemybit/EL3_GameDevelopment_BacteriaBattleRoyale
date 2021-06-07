using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Security.Cryptography;

public class GBaseController : BaseController
{
    public bool travelling;
    private NavMeshAgent navMeshAgent;
    public Text healthT;
    public Text armorT;
    public Text energyT;
    public Text metabolismT;
    public Text ammoT;
    public Text nameT;
    public Text StateT;

    public Text healthTT;
    public Text armorTT;
    public Text energyTT;
    public Text metabolismTT;
    public Text ammoTT;
    public Text nameTT;
    public Text StateTT;

    public GameObject explosionVFX;

    public List<GameObject> boundaries;
    private int FoodEnemyWalls = 0;

    protected override void Start()
    {
        base.Start();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        boundaries.Add(GameObject.Find("TopBoundary"));
        boundaries.Add(GameObject.Find("BotBoundary"));
        boundaries.Add(GameObject.Find("LeftBoundary"));
        boundaries.Add(GameObject.Find("RightBoundary"));
    }

    protected override void Update()
    {
        base.Update();

        if (navMeshAgent == null)
        {
            Debug.LogError("nav mesh component is not attached");
        }
        else
        {
            SwitchState();
        }

        if (travelling && navMeshAgent.isStopped == true)
        {
            travelling = false;
            SwitchState();
        }

        healthT.text = "Health: " + Mathf.RoundToInt(health).ToString();
        armorT.text = "Armor: " + Mathf.RoundToInt(armor).ToString();
        energyT.text = "Energy: " + Mathf.RoundToInt(energy).ToString();
        metabolismT.text = "Metabolism: " + metabolism.ToString();
        ammoT.text = "Ammo: " + Mathf.RoundToInt(ammo).ToString();

        healthTT.text = "Health: " + Mathf.RoundToInt(health).ToString();
        armorTT.text = "Armor: " + Mathf.RoundToInt(armor).ToString();
        energyTT.text = "Energy: " + Mathf.RoundToInt(energy).ToString();
        metabolismTT.text = "Metabolism: " + metabolism.ToString();
        ammoTT.text = "Ammo: " + Mathf.RoundToInt(ammo).ToString();

        //UI SHOWS STATES
        if (FoodEnemyWalls == 1)
        {
            StateT.text = "Searching Food..";
            StateTT.text = "Searching Food..";
        }
        if (FoodEnemyWalls == 2)
        {
            StateT.text = "Searching Enemies..";
            StateTT.text = "Searching Enemies..";
        }
        if (FoodEnemyWalls == 3)
        {
            StateT.text = "Avoiding Walls..";
            StateTT.text = "Avoiding Walls..";
        }


        if (!alive)
        {
            GameObject tempEplosionFVX = Instantiate(explosionVFX);
            tempEplosionFVX.transform.position = gameObject.transform.position;
            tempEplosionFVX.GetComponentInChildren<ParticleSystem>().Play();
            nameT.color = Color.red;
            nameTT.color = Color.red;
            Destroy(tempEplosionFVX, 02f);
            Destroy(gameObject);
        }
    }

    private void SwitchState()
    {
        if (transform.position.x <= boundaries[3].transform.position.x - 70 && transform.position.x > boundaries[2].transform.position.x + 70 && transform.position.x >= boundaries[1].transform.position.z + 70 && transform.position.z <= boundaries[0].transform.position.z - 70)
        {
            if (health >= 90 && armor >= 30 && ammo >= 10 && energy >= 70 && metabolism >= 8 && metabolism <= 13)
            {
                Debug.Log("Searching Enemies");
                SearchForEnemies();
            }
            else
            {
                Debug.Log("Finding Food");
                FindFood();
            }
        }
        else
        {
            IgnoreWalls();
        }
        
    }

    private void FindFood()
    {
        FoodEnemyWalls = 1;
        float closestRange = Mathf.Infinity;
        GameObject closestFood = null;

            //health
            if (health < 90)
            {
                GameObject[] hFood = GameObject.FindGameObjectsWithTag("Health");

                for (var i = 0; i < hFood.Length; i++)
                {
                    float dist = Vector3.Distance(player.transform.position, hFood[i].transform.position);
                    if (dist < closestRange)
                    {
                        closestRange = dist;
                        closestFood = hFood[i];
                    }
                    Vector3 targetVector = closestFood.transform.position;
                    navMeshAgent.SetDestination(targetVector);
                    travelling = true;
                }
            }

            //armor
            if (health >= 90 && armor < 30)
            {
                GameObject[] aFood = GameObject.FindGameObjectsWithTag("Armor");

                for (var i = 0; i < aFood.Length; i++)
                {
                    float dist = Vector3.Distance(player.transform.position, aFood[i].transform.position);
                    if (dist < closestRange)
                    {
                        closestRange = dist;
                        closestFood = aFood[i];
                    }
                    Vector3 targetVector = closestFood.transform.position;
                    navMeshAgent.SetDestination(targetVector);
                    travelling = true;
                }
            }

            //ammo
            if (health >= 90 && armor >= 30 && ammo < 10)
            {
                GameObject[] amFood = GameObject.FindGameObjectsWithTag("Ammo");

                for (var i = 0; i < amFood.Length; i++)
                {
                    float dist = Vector3.Distance(player.transform.position, amFood[i].transform.position);
                    if (dist < closestRange)
                    {
                        closestRange = dist;
                        closestFood = amFood[i];
                    }
                    Vector3 targetVector = closestFood.transform.position;
                    navMeshAgent.SetDestination(targetVector);
                    travelling = true;
                }
            }

            //Energy
            if (health >= 70 && energy < 70)
            {
                GameObject[] eFood = GameObject.FindGameObjectsWithTag("Energy");

                for (var i = 0; i < eFood.Length; i++)
                {
                    float dist = Vector3.Distance(player.transform.position, eFood[i].transform.position);
                    if (dist < closestRange)
                    {
                        closestRange = dist;
                        closestFood = eFood[i];
                    }
                    Vector3 targetVector = closestFood.transform.position;
                    navMeshAgent.SetDestination(targetVector);
                    travelling = true;
                }
            }

            //MetaPlus
            if (health >= 90 && armor >= 30 && metabolism < 8)
            {
                GameObject[] mFood = GameObject.FindGameObjectsWithTag("MetaPlus");

                for (var i = 0; i < mFood.Length; i++)
                {
                    float dist = Vector3.Distance(player.transform.position, mFood[i].transform.position);
                    if (dist < closestRange)
                    {
                        closestRange = dist;
                        closestFood = mFood[i];
                    }
                    Vector3 targetVector = closestFood.transform.position;
                    navMeshAgent.SetDestination(targetVector);
                    travelling = true;
                }
            }

            //MetaMin
            if (health >= 90 && armor >= 30 && metabolism >= 12)
            {
                GameObject[] m2Food = GameObject.FindGameObjectsWithTag("MetaMin");

                for (var i = 0; i < m2Food.Length; i++)
                {
                    float dist = Vector3.Distance(player.transform.position, m2Food[i].transform.position);
                    if (dist < closestRange)
                    {
                        closestRange = dist;
                        closestFood = m2Food[i];
                    }
                    Vector3 targetVector = closestFood.transform.position;
                    navMeshAgent.SetDestination(targetVector);
                    travelling = true;
                }
            }
    }

    private void SearchForEnemies()
    {
        FoodEnemyWalls = 2;
        Vector3 targetVector = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
        navMeshAgent.SetDestination(targetVector);
    }
    private void IgnoreWalls()
    {
        FoodEnemyWalls = 3;
        Debug.Log("Not Going");
        navMeshAgent.SetDestination(new Vector3(0, 0, 0));
    }
}
