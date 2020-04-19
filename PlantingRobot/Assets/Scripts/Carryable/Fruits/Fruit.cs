using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Carryable
{
    public int value = 0;

    public override InteractionResult InteractWith(Interactable i) {
        if(i is Container) {
            switch (((Container)i).type) {
                case Container.ContainerType.DropOffContainer:
                    player.Earn(value);
                    Destroy(gameObject);
                    return new InteractionResult(null, true);
                default:
                    return new InteractionResult(this, false);
            }
        }
        return new InteractionResult(this, false);
    }
}
