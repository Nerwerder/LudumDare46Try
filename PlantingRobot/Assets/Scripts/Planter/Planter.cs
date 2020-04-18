using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    public Material dry;
    public Material wet;

    public PlayerRobot player;
    public GameObject potato; //TODO: remove
    private bool planted = false;
    private GameObject plant = null;

    //Water
    public float maxWater = 40;
    public float waterConsumption = 10;
    public float water = 0;

    void OnMouseDown()
    {
        if (player.CanInteract(transform))
        {
            switch (player.GetState())
            {
                case PlayerRobot.PlayerState.water:
                    water = 40;    //TODO: Remove water from wateringcan
                                   //Change Material
                    gameObject.GetComponent<MeshRenderer>().material = wet;
                    break;

                case PlayerRobot.PlayerState.seed:
                    if (!planted)
                    {
                        plant = Instantiate(potato, transform);
                        planted = true;
                    }
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (water > 0 && planted)
        {
            float consumedWater = waterConsumption * Time.deltaTime;
            float growth = Mathf.Min(consumedWater / waterConsumption, water);

            plant.GetComponent<Plant>().grow(growth);
            water -= growth;

            if (water <= 0)
            {
                water = 0;
                gameObject.GetComponent<MeshRenderer>().material = dry;
            }
        }
    }
}
