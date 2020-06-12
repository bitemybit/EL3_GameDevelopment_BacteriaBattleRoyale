using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGeneration : MonoBehaviour
{
    public int foodCount;
    public List<GameObject> foodObjects;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        foodGeneration();
    }

    private void foodGeneration()
    {
        for (int i = 0; i < foodCount / foodObjects.Count; i++)
        {
            for (int j = 0; j < foodObjects.Count; j++)
            {

                GameObject food = Instantiate(foodObjects[j], new Vector3(Random.Range(-100, 100), 0f, Random.Range(-60, 60)), Quaternion.identity);
                food.transform.SetParent(GameObject.FindGameObjectWithTag("Level").transform);
            }
        }
    }
}
