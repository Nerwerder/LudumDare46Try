using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public List<PlantPhase> plantPhases = null;
    public Fruit fruit = null;

    //Fertilizer
    public bool requiresFertilizer = false;
    public float fertilizerBoost = 2f;
    public float fertilizerConsumption = 1f;

    //Water
    public bool requiresWater = true;
    public float waterBoost = 1f;
    public float waterConsumption = 5f;

    //Live
    public float livePoints = 10f;
    public Material deadPlantMaterial = null;

    //Harvest
    public bool multipleHarvests = false;
    public int afterHarvestStage = 0;
    public float harvestableDecayReduction = 3f;

    private int currentPlantPhase = 0;
    private GameObject currentPlant = null;

    public enum PlantState { NotReady, Harvestable, Dead}
    private PlantState currentPlantState = PlantState.NotReady;
    private float plantGrowth = 0f;
    private float requiredGrowth = 0f;

    public void Start() {
        Debug.Assert(plantPhases != null);
        Debug.Assert(plantPhases.Count >= 2);

        currentPlant = Instantiate(plantPhases[currentPlantPhase].gameObject, transform);
        requiredGrowth = plantPhases[currentPlantPhase].requiredGrowth;
    }

    public void Grow(float g) {
        if (currentPlantState != PlantState.NotReady) { //A dead of fully grown Plant can no longer grow
            return;
        }

        plantGrowth += g;
        if (plantPhases.Count > currentPlantPhase && plantGrowth >= plantPhases[currentPlantPhase+1].requiredGrowth) {
            Destroy(currentPlant);

            ++currentPlantPhase;
            currentPlant = Instantiate(plantPhases[currentPlantPhase].gameObject, transform);

            //This was the last Phase -> Harvestable
            if (currentPlantPhase >= (plantPhases.Count - 1)) {
                currentPlantState = PlantState.Harvestable;
            }
        }
    }

    public void Decay(float d) {
        if(currentPlantState == PlantState.Dead) {
            return;
        }

        float decay = d;
        if(currentPlantState == PlantState.Harvestable) {
            decay /= harvestableDecayReduction;
        }

        livePoints -= decay;
        if (livePoints <= 0) {
            currentPlantState = PlantState.Dead;
            SetDeadMaterialForAllChildren(transform);
        }
    }

    private void SetDeadMaterialForAllChildren(Transform t) {
        foreach (Transform child in t) {
            SetDeadMaterialForAllChildren(child);
            var renderer = child.GetComponent<Renderer>();
            if (renderer) {
                renderer.material = deadPlantMaterial;
            }
        }
    }

    public InteractionResult Harvest(Transform parent) {
        switch (currentPlantState) {
            case PlantState.NotReady:
                return new InteractionResult(null, false, false);
            case PlantState.Harvestable:
                Destroy(currentPlant);
                bool destroyed = true;
                if (multipleHarvests) {
                    currentPlantState = PlantState.NotReady;
                    currentPlantPhase = afterHarvestStage;
                    plantGrowth = plantPhases[currentPlantPhase].requiredGrowth;
                    currentPlant = Instantiate(plantPhases[currentPlantPhase].gameObject, transform);
                    destroyed = false;
                } else {
                    Destroy(gameObject);
                    destroyed = true;
                    currentPlant = null;
                }
                return new InteractionResult(Instantiate<Fruit>(fruit, parent), true, destroyed);
            case PlantState.Dead:
                Destroy(currentPlant);
                currentPlant = null;
                return new InteractionResult(null, true, true); ;
        }
        return new InteractionResult(null, false, false);
    }
}
