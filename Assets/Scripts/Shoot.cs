using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float shootForce;
    public float shootRange;
    public bool inRange;
    public Transform player;
    public Transform firePoint;
    public Transform target;
    public Transform[] enemiesList;
    public GameObject bullet;

    void Start()
    {
        inRange = false;
        InvokeRepeating("ShootEnemy", 0f, 0.5f);
    }

    public void ShootEnemy()
    {
        if (inRange == true)
        {
                GameObject shot = Instantiate(bullet, firePoint.position, firePoint.rotation);
                shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * shootForce);
                Destroy(shot, 1.2f);
        }
    }


    void ChooseTarget()
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
       
    }

    void Update()
    {
        ChooseTarget();  
    }
}
