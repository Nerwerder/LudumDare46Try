using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : Interactable
{
    public Material dryMat;
    public Material wetMat;
    public Material fertMat;
    public Material wetFertMat;
    public Renderer groundRenderer;

    //Water
    public float maxWater = 60f;
    public float defaultWaterConsumption = 1f;

    private float curWater = 0f;

    //Fertilizer
    public float maxFertilizer = 20f;
    private float curFertilizer = 0f;
    private float fertBonusPower = 0f;

    private Plant plant = null;
    private PlanterRegistry planterReg = null;

    private FruitRegistry fruitReg = null;
    private Transform fruitParent = null;

    public new void Start() {
        base.Start();
        planterReg = FindObjectOfType<PlanterRegistry>();
        Debug.Assert(planterReg != null);
        planterReg.RegisterPlanter(this);
        fruitReg = FindObjectOfType<FruitRegistry>();
        Debug.Assert(fruitReg != null);
        fruitParent = fruitReg.GetFruitParent();
        Debug.Assert(fruitParent != null);
    }
    ~Planter() {
        planterReg.DeregisterPlanter(this); //TODO: It could be a good Idea?
    }

    public InteractionResult Harvest() {
        if (plant != null) {
            InteractionResult res = plant.Harvest(fruitParent);
            if(res.destroyed) {
                planterReg.DeregisterPlant(plant);
                plant = null;
            }
            return res;
        }
        return new InteractionResult(null, false);
    }

    public InteractionResult Plant(Seed s) {
        if (plant == null) {
            plant = Instantiate<Plant>(s.plant, transform);
            planterReg.RegisterPlant(plant);
            return new InteractionResult(null, true);
        }
        return new InteractionResult(s, false);
    }

    public void Water(float w) {
        curWater = Mathf.Min(curWater + w, maxWater);
        ChangeMaterial();
    }

    public void Fertilize(float f, float fb) {
        curFertilizer = Mathf.Min(curFertilizer + f, maxFertilizer);
        fertBonusPower = fb;
        ChangeMaterial();
    }

    public bool IsFree() {
        return (plant == null);
    }

    private void ChangeMaterial() {
        if(curWater > 0 && curFertilizer > 0) {
            groundRenderer.material = wetFertMat;
        } else if(curWater > 0) {
            groundRenderer.material = wetMat;
        } else if(curFertilizer > 0) {
            groundRenderer.material = fertMat;
        } else {
            groundRenderer.material = dryMat;
        }
    }

    public void FixedUpdate() {

        //WaterConsumption
        float planterWaterRequirement = defaultWaterConsumption * Time.deltaTime;
        float plantWaterRequirement = ((plant) ? (plant.waterConsumption) : (0f)) * Time.deltaTime;
        float waterConsumption = Mathf.Min((planterWaterRequirement + plantWaterRequirement), curWater);

        if (plant != null) {

            //Fertilizer
            float plantFertilizerRequirement = plant.fertilizerConsumption * Time.deltaTime;
            float fertilizerConsumption = (plant.requiresFertilizer) ? (Mathf.Min(plantFertilizerRequirement, curFertilizer)) : (0f);

            if ((curWater > 0) && ((plant.requiresFertilizer) ? (fertilizerConsumption > 0) : (true))) {
                float growth = waterConsumption * plant.waterBoost + fertilizerConsumption * plant.fertilizerBoost + fertilizerConsumption * fertBonusPower;
                plant.Grow(growth);
            } else {
                float decay = ((plant.requiresWater) ? (planterWaterRequirement) : (0f)) + ((plant.requiresFertilizer) ? (plantFertilizerRequirement) : (0f));
                plant.Decay(decay);
            }

            curFertilizer -= fertilizerConsumption;
            if(curFertilizer <= 0f) {
                ChangeMaterial();
            }
        }

        if(curWater >= 0f) {
            curWater -= waterConsumption;
            if(curWater <= 0f) {
                ChangeMaterial();
            }
        }
    }
}
