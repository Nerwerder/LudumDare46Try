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

    private int currentPlantPhase = 0;
    private GameObject currentPlant = null;

    public enum PlantState { NotReady, Harvestable, Dead }
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
        if (plantPhases.Count > currentPlantPhase && plantGrowth >= requiredGrowth) {
            Destroy(currentPlant);

            ++currentPlantPhase;
            currentPlant = Instantiate(plantPhases[currentPlantPhase].gameObject, transform);
            requiredGrowth += plantPhases[currentPlantPhase].requiredGrowth;

            //This was the last Phase -> Harvestable
            if (currentPlantPhase >= (plantPhases.Count - 1)) {
                currentPlantState = PlantState.Harvestable;
            }
        }
    }

    public void Decay(float d) {
        if (currentPlantState != PlantState.NotReady) {
            return;
        }

        livePoints -= d;
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

    public InteractionResult Harvest() {
        switch (currentPlantState) {
            case PlantState.NotReady:
                return new InteractionResult(null, false);
            case PlantState.Harvestable:
                var ret = Instantiate<Fruit>(fruit);
                Destroy(currentPlant);
                currentPlant = null;
                return new InteractionResult(ret, true);
            case PlantState.Dead:
                Destroy(currentPlant);
                currentPlant = null;
                return new InteractionResult(null, true); ;
        }
        return new InteractionResult(null, false);
    }
}
