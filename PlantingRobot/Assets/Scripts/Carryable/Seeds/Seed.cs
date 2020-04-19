using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Carryable
{
    public override InteractionResult InteractWith(Interactable i) {
        if(i is Planter) {
            Planter p = (Planter)i;
            if (p.IsFree()) {
                p.Plant(this);
                Destroy(gameObject);
                return new InteractionResult(null, true);
            }
        }
        return new InteractionResult(this, false);
    }
}
