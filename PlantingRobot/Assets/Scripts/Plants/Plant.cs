using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public Material deadPlantMaterial = null;
    public List<PlantPhase> plantPhases = null;
    public Fruit fruit;
    private int currentPlantPhase = 0;

    public int price = 7;
    public int value = 21;
    public float livePoints = 100f;

    private GameObject currentPlant = null;

    public PlantState currentPlantState = PlantState.NotReady;
    private float plantGrowth = 0f;

    public enum PlantState { NotReady, Harvestable, Dead }

    public void Start() {
        Debug.Assert(plantPhases != null);
        Debug.Assert(plantPhases.Count >= 2);

        currentPlant = Instantiate(plantPhases[currentPlantPhase].gameObject, transform);
    }

    public void Decay(float d) {
        if (currentPlantState != PlantState.NotReady) {
            return;
        }

        livePoints -= d;
        if (livePoints <= 0) {
            currentPlantState = PlantState.Dead;
            currentPlant.GetComponent<Renderer>().material = deadPlantMaterial;
        }
    }

    public void Grow(float g) {
        if (currentPlantState != PlantState.NotReady) {
            return;
        }

        plantGrowth += g;
        if (plantPhases.Count > currentPlantPhase && plantGrowth >= plantPhases[currentPlantPhase].requiredTime) {
            plantGrowth -= plantPhases[currentPlantPhase].requiredTime;
            Destroy(currentPlant);
            currentPlant = Instantiate(plantPhases[currentPlantPhase].gameObject, transform);
            currentPlantPhase++;

            if (currentPlantPhase >= plantPhases.Count - 1) {
                currentPlantState = PlantState.Harvestable;
            }
        }
    }

    public Carryable Harvest() {
        switch(currentPlantState) {
            case PlantState.NotReady:
                return null;
            case PlantState.Harvestable:
                var ret = Instantiate<Fruit>(fruit);
                ret.value = value;
                Destroy(currentPlant);
                currentPlant = null;
                return ret;
            case PlantState.Dead:
                Destroy(currentPlant);
                currentPlant = null;
                return null;
            default:
                return null;
        }
    }
}
