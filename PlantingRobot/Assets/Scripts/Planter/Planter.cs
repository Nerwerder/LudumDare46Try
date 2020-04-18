using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    public Material dry;
    public Material wet;

    public PlayerRobot player;
    public GameObject potato; //TODO: remove
    private GameObject plant = null;

    //Water
    public float maxWater = 40;
    public float waterConsumption = 10;
    public float water = 0;

    public void OnMouseDown() {
        if (player.CanInteract(transform)) {
            player.InteractWithMe(this);
        }
    }

    public void Harvest() {
        if (plant != null) {
            switch (plant.GetComponent<Plant>().currentPlantState) {
                case global::Plant.PlantState.NotReady:
                    //Nothing
                    break;
                case global::Plant.PlantState.Harvestable:
                    player.Earn(plant.GetComponent<Plant>().value);
                    Destroy(plant);
                    plant = null;
                    break;
                case global::Plant.PlantState.Dead:
                    //Sad biep
                    Destroy(plant);
                    plant = null;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }
    }


    public void Water(float w) {
        water = Mathf.Min(water + w, maxWater);
        gameObject.GetComponent<MeshRenderer>().material = wet;
    }

    public bool IsFree() {
        return (plant == null);
    }

    public bool Plant(Seed s) {
        if (plant == null) {
            plant = Instantiate(potato, transform);
            return true;
        }
        return false;
    }

    public void FixedUpdate() {
        if (plant != null) {
            float requiredWater = waterConsumption * Time.deltaTime;

            if (water > 0) {
                float consumedWater = Mathf.Min(requiredWater, water);

                float growth = consumedWater / waterConsumption;
                plant.GetComponent<Plant>().Grow(growth);

                water -= consumedWater;
                if (water <= 0) {
                    water = 0;
                    gameObject.GetComponent<MeshRenderer>().material = dry;
                }
            } else {
                float decay = requiredWater / waterConsumption;
                plant.GetComponent<Plant>().Decay(decay);
            }
        }
    }
}
