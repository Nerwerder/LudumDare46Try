using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Carryable : MonoBehaviour
{
    protected PlayerRobot player = null;

    public void Start() {
        player = FindObjectOfType<PlayerRobot>();
        Debug.Assert(player != null);
        Debug.Assert(gameObject.GetComponent<Rigidbody>() != null);
    }

    void OnMouseDown() {
        if (player.CanInteract(transform)) {
            player.PickMeUp(this);
        }
    }

    /// <summary>
    /// Interact with Interactible
    /// </summary>
    /// <param name="i">The Interactible to interact with</param>
    /// <returns>Return the InteractionResult - was the Interaction successfull and is the Object still the same (or was Destroyed)</returns>
    public abstract InteractionResult InteractWith(Interactable i);
}
