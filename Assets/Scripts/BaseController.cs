using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.AI;

public class BaseController : MonoBehaviour
{
    public float shootForce;
    public float shootRange;
    public bool inRange;
    public Transform player;
    public Transform firePoint;
    public Transform targetE;
    public Transform[] enemiesList;
    public GameObject bullet;
    public float metabolism;
    public float maxHealth;
    public float health;
    public float energy = 10f;
    public float energyDepletion;
    public float speed = 7f;
    public float armor;
    public float ammo;
    public float scaleModifier = 0.5f;
    public float maxSize = 30f;
    public bool idle = true;
    public Rigidbody rb;
    public Transform target;
    public SphereCollider awarenessSphere;
    public Camera camera;
    private IEnumerator currentMoveCoroutine;
    public GameObject gameManager;
    private float initHealth;

    public bool alive = true;
    //public List<FoodType> foodTypes = new List<FoodType>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();
        initHealth = health;
        energyDepletion = (1 / metabolism) * 10;
        StartCoroutine(_loseEnergy());
        //gameManager.GetComponent<CameraManager>().agentCameras.Add(camera);
        //StartCoroutine(_idle());
        inRange = false;
        InvokeRepeating("ShootEnemy", 0f, 0.5f);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
 

        StartCoroutine(_chooseTarget());
        //GameObject.Find("GameManager").GetComponent<GameManager>().constrainZ(transform);
        energyDepletion = metabolism / 10;
        speed = (metabolism / 2) * scaleModifier;
        maxHealth = (1 / metabolism) * 1000;

        if (currentMoveCoroutine != null)
        {
            if (currentMoveCoroutine == _loseHealth())
            {
                if (energy > energyDepletion)
                {
                    StopCoroutine(currentMoveCoroutine);
                    StartCoroutine(_loseEnergy());
                }
            }
        }

        if (health <= 0)
        {
            alive = false;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Food>() != null)
        {
            target = other.transform;
            // Debug.Log("Found Food");
        }
    }

    private void OnMouseDown()
    {
        camera.gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        var otherObj = other.collider.gameObject;
        if (otherObj.GetComponent<Food>() != null)
        {
            var food = otherObj.GetComponent<Food>();
            switch (food.foodType)
            {
                case Food.FoodType.AMMO:

                    ammo += food.foodValue;
                    break;
                case Food.FoodType.ARMOUR:
                    armor += food.foodValue;
                    break;
                case Food.FoodType.ENERGY:
                    energy += food.foodValue;
                    break;
                case Food.FoodType.HEALTH:
                    if (health < maxHealth)
                    {
                        health += food.foodValue;
                        if (metabolism < 10)
                        {
                            var ratio = (health / maxHealth);
                            scaleModifier = (1 / metabolism) * 10 * ratio;
                            gameObject.transform.localScale += new Vector3(scaleModifier, scaleModifier, scaleModifier);

                            if (gameObject.transform.localScale.x < maxSize / 4)
                            {
                                //camera.transform.position += new Vector3(0, 0.5f, 0);
                            }
                        }
                    }
                    break;
                case Food.FoodType.METABOLISM:
                    if (food.negative)
                    {
                        if (metabolism > 1)
                        {
                            metabolism -= food.foodValue;
                        }
                        //gameObject.transform.localScale += new Vector3(scaleModifier, scaleModifier,scaleModifier);
                    }
                    else
                    {
                        if (metabolism < 20)
                        {
                            metabolism += food.foodValue;
                        }
                        //gameObject.transform.localScale = new Vector3(transform.localScale.x - scaleModifier, transform.localScale.y - scaleModifier,transform.localScale.z - scaleModifier);
                    }
                    break;
            }

            Destroy(other.collider.gameObject);
            target = null;
        }
    }



    public IEnumerator _loseEnergy()
    {
        while (energy > 0)
        {
            energy -= energyDepletion;
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Energy 0");
        currentMoveCoroutine = _loseHealth();
        StartCoroutine(currentMoveCoroutine);
    }

    public IEnumerator _loseHealth()
    {
        while (health > 0 && energy < energyDepletion)
        {
            health -= 1f;
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Health 0");
        StartCoroutine(_loseEnergy());
    }

    public void move(Vector3 target)
    {
        GetComponent<NavMeshAgent>().speed = speed;
        GetComponent<NavMeshAgent>().destination = target;
    }

    public void ShootEnemy()
    {
        if (inRange == true && ammo > 0)
        {
            GameObject shot = Instantiate(bullet, firePoint.position, firePoint.rotation);
            shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * shootForce);
            Destroy(shot, 1.2f);
            ammo -= 1;
        }
    }

    public IEnumerator _chooseTarget()
    {
        float shortestRange = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Transform enemy in enemiesList)
        {
            float distanceToEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestRange)
            {
                shortestRange = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && shortestRange <= shootRange)
        {
            inRange = true;
            target = closestEnemy.transform;
            firePoint.LookAt(target);
        }
        else
        {
            inRange = false;
            target = null;
        }
        yield return new WaitForSeconds(1f);
    }

   
        /*
        public IEnumerator _idle()
        {
            while (idle)
            {
                var pos = new Vector3(0, 0, 0);
                var timeTo = 0f;

                if (target != null)
                {
                    pos = target.transform.position;
                    if (currentMoveCoroutine != null)
                    {
                        StopCoroutine(currentMoveCoroutine);
                    }
                    currentMoveCoroutine = _move(pos);
                    yield return StartCoroutine(currentMoveCoroutine);
                }
                else
                {
                    pos = new Vector3(Random.Range(-10, 30), Random.Range(-10, 30), 0);
                    timeTo = (Vector3.Distance(transform.position, pos) / speed);

                    if (currentMoveCoroutine != null)
                    {
                        StopCoroutine(currentMoveCoroutine);
                    }
                    currentMoveCoroutine = _move(pos);
                    StartCoroutine(currentMoveCoroutine);

                    yield return new WaitForSeconds(timeTo);
                }
            }
        }
        */
}




