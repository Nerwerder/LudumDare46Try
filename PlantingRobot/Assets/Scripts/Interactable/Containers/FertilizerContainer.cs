using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizerContainer : Container
{
    public FertilizerBox fert = null;
    public Transform toolParent = null;

    public new void Start() {
        base.Start();
        Debug.Assert(fert != null);
        Debug.Assert(toolParent != null);
    }

    public InteractionResult BuyFertilizer() {
        if(player.Pay(fert.maxUses * fert.costPerUse)) {
            return new InteractionResult(Instantiate(fert, toolParent), true);
        }
        return new InteractionResult(null, false);
    }
}
