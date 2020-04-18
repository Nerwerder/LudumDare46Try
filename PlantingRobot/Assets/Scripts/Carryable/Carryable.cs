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
    /// Interact with the Container c
    /// </summary>
    /// <param name="c">The Container to interact with</param>
    /// <returns>Return the InteractionResult - was the Interaction successfull and is the Object still the same (or was Destroyed)</returns>
    public abstract CarryableInteractionResult InteractWith(Container c);

    /// <summary>
    /// Interact with the Planter p
    /// </summary>
    /// <param name="p">The Planter to Interact with</param>
    /// <returns>Return the InteractionResult - was the Interaction successfull and is the Object still the same (or was Destroyed)</returns>
    public abstract CarryableInteractionResult InteractWith(Planter p);
}
