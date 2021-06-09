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

public class JaniBaseController : BaseController
{
    private int currentState = 0;
    private NavMeshAgent navMeshAgent;
    private Vector3 levelCenter;

    public List<GameObject> boundaries;
    public bool travelling;

    [Space(10)]
    [Header("UI Variables")]
    // Main Camera UI, Text Variables
    public Text healthT;
    public Text armorT;
    public Text energyT;
    public Text metabolismT;
    public Text ammoT;
    public Text nameT;
    public Text StateT;

    // Main Camera UI, Player Button
    public Text camBtnTxt;
    public Button camBtn;

    // Player Camera UI, Text Variables
    public Text healthTT;
    public Text armorTT;
    public Text energyTT;
    public Text metabolismTT;
    public Text ammoTT;
    public Text nameTT;
    public Text StateTT;

    [Space(10)]
    [Header("Prefabs")]
    // Explosion Particle Effect Prefab
    public GameObject explosionVFX;

    [Space(10)]
    [Header("External Scripts")]
    // External Scripts
    public AudioController audioController;
    public CameraManager cameraManager;

    protected override void Start()
    {
        base.Start();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        FindBoundaries();
        levelCenter = new Vector3(0, 0, 0);
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
            NextAction();
        }

        if (travelling && navMeshAgent.isStopped == true)
        {
            travelling = false;
            NextAction();
        }

        // Main Camera UI Text Updates
        healthT.text = "Health: " + Mathf.RoundToInt(health).ToString();
        armorT.text = "Armor: " + Mathf.RoundToInt(armor).ToString();
        energyT.text = "Energy: " + Mathf.RoundToInt(energy).ToString();
        metabolismT.text = "Metabolism: " + metabolism.ToString("F2"); //
        ammoT.text = "Ammo: " + Mathf.RoundToInt(ammo).ToString();

        // Player Camera UI Text Updates
        healthTT.text = "Health: " + Mathf.RoundToInt(health).ToString();
        armorTT.text = "Armor: " + Mathf.RoundToInt(armor).ToString();
        energyTT.text = "Energy: " + Mathf.RoundToInt(energy).ToString();
        metabolismTT.text = "Metabolism: " + metabolism.ToString("F2");
        ammoTT.text = "Ammo: " + Mathf.RoundToInt(ammo).ToString();

        // UI Current State Update
        if (currentState == 1)
        {
            StateT.text = "Searching Food..";
            StateTT.text = "Searching Food..";
        }
        if (currentState == 2)
        {
            StateT.text = "Searching Enemies..";
            StateTT.text = "Searching Enemies..";
        }
        if (currentState == 3)
        {
            StateT.text = "Avoiding Walls..";
            StateTT.text = "Avoiding Walls..";
        }

        // On Bacteria Death
        if (!alive)
        {
            cameraManager.janiAlive = false;
            cameraManager.SwitchCam(0); // Switch to Main Camera

            // UI Update & Disable Player Camera Button
            nameT.color = Color.red;
            nameTT.color = Color.red;
            camBtnTxt.color = Color.red;
            camBtn.interactable = false;

            audioController.BacteriaDied(); // Play Death Sound FX

            // Play Death Particle FX
            GameObject tempEplosionFVX = Instantiate(explosionVFX);
            tempEplosionFVX.transform.position = gameObject.transform.position;
            tempEplosionFVX.GetComponentInChildren<ParticleSystem>().Play();
            Destroy(tempEplosionFVX, 02f);
            
            this.gameObject.SetActive(false); // Disable Game Object
        }
    }

    // Find Level Boundaries
    public void FindBoundaries()
    {
        boundaries.Add(GameObject.Find("TopBoundary"));
        boundaries.Add(GameObject.Find("BotBoundary"));
        boundaries.Add(GameObject.Find("LeftBoundary"));
        boundaries.Add(GameObject.Find("RightBoundary"));
    }

    // Remove Dead Bacteria from List of Enemies
    public void RemoveEnemy(int id)
    {
        enemiesList[id] = null;
    }

    // Determine Next Move
    private void NextAction()
    {
        // Check Distance to Boundaries, If Below 45 -> Avoid Walls
        if (transform.position.x <= boundaries[3].transform.position.x - 65 && transform.position.x > boundaries[2].transform.position.x + 65 && 
            transform.position.x >= boundaries[1].transform.position.z + 65 && transform.position.z <= boundaries[0].transform.position.z - 65) 
        {
            // If Food Needs Met -> Find Nearby Enemies
            if (ammo >= 30 && armor >= 20 && energy >= 65 && health >= 75 && metabolism >= 9)
            {
                SearchForEnemies();
            }
            else // Find Nearby Food
            {
                SearchForFood();
            }
        }
        else // Go to Game Area Center
        {
            SearchForCenter();
        }

    }

    // Find Specific Food Types based on Current Values
    private void SearchForFood()
    {
        currentState = 1;
        float closestRange = Mathf.Infinity;
        GameObject closestFood = null;

        // Search for Type Ammo Food
        if (ammo < 30)
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

        // Search for Type Armor Food
        if (ammo >= 30 && armor < 20)
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

        // Search for Type Energy Food
        if (ammo >= 30 && armor >= 20 && energy < 65)
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

        // Search for Type Health Food
        if (ammo >= 30 && armor >= 20 && energy >= 65 && health < 75)
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

        // Search for Type + Metabolism Food
        if (ammo >= 30 && armor >= 20 && energy >= 65 && health >= 75 && metabolism < 9)
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
    }

    // Find Nearby Enemy Bacteria
    private void SearchForEnemies()
    {
        currentState = 2;
        Vector3 targetVector = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
        navMeshAgent.SetDestination(targetVector);
    }

    // Travel to Center of Game Area
    private void SearchForCenter()
    {
        currentState = 3;
        navMeshAgent.SetDestination(levelCenter);
    }
}


