using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;




public class Food : MonoBehaviour
{

    public enum FoodType
    {
        METABOLISM,
        HEALTH,
        AMMO,
        ARMOUR,
        ENERGY
    }
    public FoodType foodType;
    public float foodValue = 5f;
    public bool negative = false;
    private Renderer renderer;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        renderer = GetComponent<Renderer>();
        
        //foodType = (FoodType) Random.Range(0, 4);
        /*
        switch (foodType)
        {
            case FoodType.Ammo:
                renderer.material.color = Color.cyan;
                break;
            case FoodType.Armour:
                renderer.material.color = Color.blue;
                break;
            case FoodType.Energy:
                renderer.material.color = Color.yellow;
                break;
            case FoodType.Health:
                renderer.material.color = Color.black;
                break;
            case FoodType.Metabolism:
                renderer.material.color = Color.red;
                var rand = Mathf.RoundToInt(Random.Range(0, 100));
                if (rand > 50)
                {
                    negative = true;
                }
                break;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("GameManager").GetComponent<GameManager>().constrainZ(transform);
    }
}
