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

    void OnMouseDown() {
        if (player.CanInteract(transform)) {
            switch (player.GetState()) {
                case PlayerRobot.PlayerState.normal:
                    if (plant != null) {
                        switch (plant.GetComponent<Plant>().Harvest()) {
                            case Plant.PlantState.NotReady:
                                //Nothing
                                break;
                            case Plant.PlantState.Harvestable:
                                player.Earn(plant.GetComponent<Plant>().value);
                                Destroy(plant);
                                plant = null;
                                break;
                            case Plant.PlantState.Dead:
                                //Sad biep
                                Destroy(plant);
                                plant = null;
                                break;
                            default:
                                Debug.Assert(false);
                                break;
                        }
                    }
                    break;

                case PlayerRobot.PlayerState.water:
                    water = 40;    //TODO: Remove water from wateringcan
                                   //Change Material
                    gameObject.GetComponent<MeshRenderer>().material = wet;
                    break;

                case PlayerRobot.PlayerState.seed:
                    if (plant == null) {
                        plant = Instantiate(potato, transform);
                    }
                    break;
            }
        }
    }

    void FixedUpdate() {
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
