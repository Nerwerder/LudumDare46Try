using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public List<PlantState> plantStates = null;

    private GameObject currentPlant = null;
    private int currentState = 0;
    private float plantGrowth = 0f;

    public void Start()
    {
        Debug.Assert(plantStates != null);
        Debug.Assert(plantStates.Count >= 2);

        currentPlant = Instantiate(plantStates[currentState].gameObject, transform);
    }

    public void grow(float g)
    {
        plantGrowth += g;
        if (plantStates.Count > currentState && plantGrowth >= plantStates[currentState].requiredTime)
        {
            plantGrowth -= plantStates[currentState].requiredTime;
            Destroy(currentPlant);
            currentPlant = Instantiate(plantStates[currentState].gameObject, transform);
            currentState++;
        }
    }
}
