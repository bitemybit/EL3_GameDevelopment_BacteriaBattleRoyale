using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameManager", order = 1)]
[SerializeField]
public class GameManager : ScriptableObject
{
    public int foodCount = 300;

    public List<GameObject> foodObjects;

    // Start is called before the first frame update
    void Start()
    {
        foodObjects = new List<GameObject>();
        //foodGeneration();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void foodGeneration()
    {
        for (int i = 0; i< foodCount / foodObjects.Count; i++)
        {
            for (int e = 0; e < foodObjects.Count; e++)
            {
                var foodPos = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0);
                Instantiate(foodObjects[e], foodPos, Quaternion.identity);
            }
        }
    }
}
