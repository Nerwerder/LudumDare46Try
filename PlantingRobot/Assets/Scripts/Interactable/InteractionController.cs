using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public const string interactableTag = "Interactable";
    public const string carryableTag = "Carryable";
    public const string playerTag = "Player";

    private PlayerRobot player = null;

    public void Start() {
        player = FindObjectOfType<PlayerRobot>();
    }


    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            foreach(RaycastHit hit in hits) {
                if(player.CanInteract(hit.transform)) {
                    bool success = false;
                    switch (hit.transform.tag) {
                        case interactableTag:
                            success = player.InteractWithInteractable(hit.transform.gameObject.GetComponent<Interactable>());
                            break;
                        case carryableTag:
                            success = player.InteractWithCarryable(hit.transform.gameObject.GetComponent<Carryable>());
                            break;
                        case playerTag:
                            success = player.InteractWithYourself(hit.transform.gameObject.GetComponent<PlayerRobot>());
                            break;
                    }
                    if (success) {
                        break;
                    }
                }
            }
        }
    }
}
