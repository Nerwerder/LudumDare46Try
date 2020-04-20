using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Carryable : MonoBehaviour
{
    protected PlayerRobot player = null;
    [HideInInspector] public Transform oldParent = null;

    /// <summary>
    /// A Object that was thrown by the Player is in thrown Mode
    /// </summary>
    private bool thrownMode = false;
    private float throwModeMaxTimer = 5f;
    private float thrownTimer = 0f;
    private const string interactableTag = "Interactable";

    public void Start() {
        player = FindObjectOfType<PlayerRobot>();
        Debug.Assert(player != null);
        Debug.Assert(gameObject.GetComponent<Rigidbody>() != null);
    }

    public void ActivateThrownMode() {
        thrownMode = true;
    }

    public void DeactivateThrownMode() {
        thrownMode = false;
        thrownTimer = 0f;
    }

    public void Update() {
        if(thrownMode) {
            thrownTimer += Time.deltaTime;
            if(thrownTimer >= throwModeMaxTimer) {
                DeactivateThrownMode()
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(thrownMode && other.gameObject.tag == interactableTag) {
            var res = InteractWith(other.GetComponent<Interactable>());
            //TODO: Do something with the Result?
        }
        thrownMode = false;
    }


    /// <summary>
    /// Interact with Interactible
    /// </summary>
    /// <param name="i">The Interactible to interact with</param>
    /// <returns>Return the InteractionResult - was the Interaction successfull and is the Object still the same (or was Destroyed)</returns>
    public abstract InteractionResult InteractWith(Interactable i);
}
