using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCluster : FoodGeneration
{
    public SphereCollider collider;
    public float clusterRadius = 5f;
    public bool randomRadius;
    public float minRadius = 5f;
    public float maxRadius = 10f;
    public bool randomAmount;
    public float minAmount = 5f;
    public float maxAmount = 35f;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<SphereCollider>();
        if (randomRadius)
        {
            collider.radius = Random.Range(minRadius, maxRadius);
        }
        else
        {
            collider.radius = clusterRadius;
        }
        foodGeneration();
    }
    
    private void foodGeneration()
    {
        if (randomAmount)
        {
            foodCount = Mathf.RoundToInt(Random.Range(minAmount, maxAmount));
        }
        
        for (int i = 0; i < foodCount / foodObjects.Count; i++)
        {
            for (int j = 0; j < foodObjects.Count; j++)
            {
              
                Instantiate(foodObjects[j], Utility.RandomPointInBounds(collider.bounds), Quaternion.identity);
            }
        }
    }
}
