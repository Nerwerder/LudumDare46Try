using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [HideInInspector] public PlayerRobot player = null;

    public void Start() {
        player = FindObjectOfType<PlayerRobot>();
        Debug.Assert(player != null);
    }

    void OnMouseDown() {
        if (player.CanInteract(transform)) {
            player.InteractWithMe(this);
        }
    }
}
