using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public PlayerRobot player;
    public ContainerType type;

    public enum ContainerType { WaterContainer, SeedContainer, DropOffContainer }
    public int interactionCost = 0;

    void OnMouseDown() {
        if (player.CanInteract(transform) && player.CanAfford(interactionCost)) {
            player.InteractWithMe(this);
        }
    }
}
