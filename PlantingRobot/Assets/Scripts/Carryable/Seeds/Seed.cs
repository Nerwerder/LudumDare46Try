using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Carryable
{
    public override InteractionResult InteractWith(Interactable i) {
        if(i is Planter) {
            Planter p = (Planter)i;
            if (p.IsFree()) {
                var res = p.Plant(this);
                if(res.carryable == null) {
                    Destroy(gameObject);
                }
                return res;
            }
        }
        return new InteractionResult(this, false);
    }
}
