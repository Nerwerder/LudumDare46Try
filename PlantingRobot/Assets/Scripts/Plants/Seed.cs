using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public PlayerRobot player;

    public void OnMouseDown() {
        if (player.CanInteract(transform)) {
            player.PickMeUp(this);
        }
    }

    public void Start() {
        player = FindObjectOfType<PlayerRobot>();
    }

    public Seed InteractWith(Planter p) {
        if(p.IsFree()) {
            p.Plant(this);
            Destroy(gameObject);
            return null;
        } else {
            return this;
        }
    }
}
