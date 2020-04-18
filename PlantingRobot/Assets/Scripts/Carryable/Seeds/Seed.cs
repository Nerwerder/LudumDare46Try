using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Carryable
{
    public override CarryableInteractionResult InteractWith(Interactable i) {
        if(i is Planter) {
            Planter p = (Planter)i;
            if (p.IsFree()) {
                p.Plant(this);
                Destroy(gameObject);
                return new CarryableInteractionResult(null, true);
            }
        }
        return new CarryableInteractionResult(this, false);
    }
}
