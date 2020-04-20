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

            List<Interactable> interactables = new List<Interactable>();
            List<Carryable> carryables = new List<Carryable>();

            foreach(RaycastHit hit in hits) {
                if(player.CanInteract(hit.transform)) {
                    switch (hit.transform.tag) {
                        case interactableTag:
                            interactables.Add(hit.transform.gameObject.GetComponent<Interactable>());
                            break;
                        case carryableTag:
                            carryables.Add(hit.transform.gameObject.GetComponent<Carryable>());
                            break;
                    }
                }
            }

            //Check all the Found Thing in a logical Order
            if(player.HasLaserGun()) {  //If the Player has a LaserGun, the most important entity are Insects
                foreach(Interactable i in interactables) {
                    if(i is Insect) {
                        if (player.InteractWithInteractable(i)) {
                            goto End;
                        }
                    }
                }
            }

            //In most cases Carryables have priority (the Bucket on top of  a Planter)
            foreach(Carryable c in carryables) {
                if(player.InteractWithCarryable(c)) {
                    goto End;
                }
            }

            //If there was nothing else interesting, try to interact with the interactable
            foreach(Interactable i in interactables) {
                if (player.InteractWithInteractable(i)) {
                    goto End;
                }
            }

        End:;
        }
    }
}
