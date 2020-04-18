using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Carryable
{
    [HideInInspector] public int value = 0;
    public override CarryableInteractionResult InteractWith(Interactable i) {
        if(i is Container) {
            switch (((Container)i).type) {
                case Container.ContainerType.DropOffContainer:
                    player.Earn(value);
                    Destroy(gameObject);
                    return new CarryableInteractionResult(null, false);
                default:
                    return new CarryableInteractionResult(this, false);
            }
        }
        return new CarryableInteractionResult(this, false);
    }
}
