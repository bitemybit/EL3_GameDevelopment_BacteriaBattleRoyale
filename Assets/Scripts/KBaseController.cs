using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.AI;
using UnityEngine.UI;


public class KBaseController : BaseController
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

    private int FoodEnemyWalls;

    public AudioController audioController;
    public CameraManager cameraManager;

    public Text camBtnTxt;
    public Button camBtn;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (navMeshAgent == null)
        {
            Debug.LogError("nav mesh component is not attached");
        }
        else
        {

            DestinationF();
        }

        if (travelling && navMeshAgent.isStopped == true)
        {
            travelling = false;
            DestinationF();
        }

        healthT.text = "Health: " + Mathf.RoundToInt(health).ToString();
        armorT.text = "Armor: " + Mathf.RoundToInt(armor).ToString();
        energyT.text = "Energy: " + Mathf.RoundToInt(energy).ToString();
        metabolismT.text = "Metabolism: " + Mathf.RoundToInt(metabolism).ToString();
        ammoT.text = "Ammo: " + Mathf.RoundToInt(ammo).ToString();

        healthTT.text = "Health: " + Mathf.RoundToInt(health).ToString();
        armorTT.text = "Armor: " + Mathf.RoundToInt(armor).ToString();
        energyTT.text = "Energy: " + Mathf.RoundToInt(energy).ToString();
        metabolismTT.text = "Metabolism: " + Mathf.RoundToInt(metabolism).ToString();
        ammoTT.text = "Ammo: " + Mathf.RoundToInt(ammo).ToString();

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
            cameraManager.SwitchCam(0);
            cameraManager.kaanAlive = false;
            audioController.BacteriaDied();
            GameObject tempEplosionFVX = Instantiate(explosionVFX);
            tempEplosionFVX.transform.position = gameObject.transform.position;
            tempEplosionFVX.GetComponentInChildren<ParticleSystem>().Play();
            nameT.color = Color.red;
            nameTT.color = Color.red;
            camBtnTxt.color = Color.red;
            camBtn.interactable = false;
            Destroy(tempEplosionFVX, 02f);
            this.gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    private void DestinationF()
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
        if (health >= 90 && armor < 50)
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
        if (health >= 90 && armor >= 50 && ammo < 30)
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
        if (health >= 90 && armor >= 80 && ammo >= 50 && energy < 30)
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
        if (health >= 90 && armor >= 50  && metabolism < 8)
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
        if (health >= 90 && armor >= 50 && metabolism >= 12)
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
}
