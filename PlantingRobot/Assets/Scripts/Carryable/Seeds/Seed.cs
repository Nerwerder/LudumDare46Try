using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Carryable
{
    public int price = 0;
    public Plant plant = null;

    public new void Start() {
        base.Start();
        Debug.Assert(plant != null);
        Debug.Assert(price > 0);
    }

    public override InteractionResult InteractWith(Interactable i) {
        if (i is Planter) {
            Planter p = (Planter)i;
            var res = p.Plant(this);
            if (res.carryable == null) {
                Destroy(gameObject);
            }
            return res;
        }
        return new InteractionResult(this, false);
    }
}
