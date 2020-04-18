using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public PlayerRobot player = null;
    public int interactionCost = 0;

    public void Start() {
        player = FindObjectOfType<PlayerRobot>();
        Debug.Assert(player != null);
    }

    void OnMouseDown() {
        if (player.CanInteract(transform) && player.CanAfford(interactionCost)) {
            player.InteractWithMe(this);
        }
    }
}
