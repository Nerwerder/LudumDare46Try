using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : Interactable
{
    public Material dryMat;
    public Material wetMat;
    public Material fertMat;
    public Material wetFertMat;

    //Water
    public float maxWater = 40f;
    public float water = 0f;

    //Fertilizer
    public float maxFertilizer = 20f;
    public float fertilizer = 0f;

    private Plant plant = null;
    private new Renderer renderer = null;

    public new void Start() {
        base.Start();
        renderer = gameObject.GetComponent<Renderer>();
    }

    public InteractionResult Harvest() {
        if (plant != null) {
            InteractionResult res = plant.Harvest();
            if(res.success) {
                Destroy(plant.gameObject);
                plant = null;
            }
            return res;
        }
        return new InteractionResult(null, false);
    }

    public InteractionResult Plant(Seed s) {
        if (plant == null) {
            plant = Instantiate<Plant>(s.plant, transform);
            return new InteractionResult(null, true);
        }
        return new InteractionResult(s, false);
    }

    public void Water(float w) {
        water = Mathf.Min(water + w, maxWater);
        ChangeMaterial();
    }

    public void Fertilize(float f) {
        fertilizer = Mathf.Min(fertilizer + f, maxFertilizer);
        ChangeMaterial();
    }

    public bool IsFree() {
        return (plant == null);
    }

    private void ChangeMaterial() {
        if(water > 0 && fertilizer > 0) {
            renderer.material = wetFertMat;
        } else if(water > 0) {
            renderer.material = wetMat;
        } else if(fertilizer > 0) {
            renderer.material = fertMat;
        } else {
            renderer.material = dryMat;
        }
    }

    public void FixedUpdate() {
        if (plant != null) {

            //Water
            float requiredWater = plant.waterConsumption * Time.deltaTime;
            float consumedWater = Mathf.Min(requiredWater, water);
            bool waterOK = (plant.requiresWater) ? (consumedWater > 0) : (true); 

            //Fertilizer
            float requiredFertilizer = plant.fertilizerConsumption * Time.deltaTime;
            float consumedFertilizer = Mathf.Min(requiredFertilizer, fertilizer);
            bool fertilizerOK = (plant.requiresFertilizer) ? (consumedFertilizer > 0) : (true);

            if (waterOK && fertilizerOK) {
                float growth = (consumedWater / plant.waterConsumption) * plant.waterBoost + (consumedFertilizer / plant.fertilizerConsumption) * plant.fertilizerBoost;
                plant.Grow(growth);
                water -= consumedWater;
                fertilizer -= consumedFertilizer;
                if(water <= 0f || fertilizer <= 0f) {
                    ChangeMaterial();
                }
            } else {
                float decay = 0f;
                decay += (plant.requiresWater) ? (requiredWater) : (0f);
                decay += (plant.requiresFertilizer) ? (requiredFertilizer) : (0f);
                plant.Decay(decay);
            }
        }
    }
}
