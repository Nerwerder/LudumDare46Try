using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public PlayerRobot player = null;
    public float distance = 0f;
    public float heightOffset = 0f;

    void Start() {
        Debug.Assert(player != null);
    }

    void OnMouseDown() {
        if(player.CanInteract(transform)) {
            player.PickMeUp(this);
        }
    }

    public abstract void InteractWith(Container c);
    public abstract void InteractWith(Planter p);
}
