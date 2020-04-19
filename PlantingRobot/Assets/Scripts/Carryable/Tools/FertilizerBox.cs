using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizerBox : Tool
{
    public int maxUses = 5;
    public int costPerUse = 2;
    public float fertilizerPerUse = 10f;

    private int uses = 0;

    public new void Start() {
        base.Start();
        Debug.Assert(maxUses > 0);
        Debug.Assert(costPerUse > 0);
        Debug.Assert(fertilizerPerUse > 0);
        uses = maxUses;
    }

    public override InteractionResult InteractWith(Interactable i) {
        if(i is Container) {
            switch (((Container)i).type) {
                case Container.ContainerType.FertilizerContainer:
                    player.Earn(uses * costPerUse);
                    Destroy(gameObject);
                    return new InteractionResult(null, true);
            }
        } else if(i is Planter) {
            Debug.Assert(uses > 0);
            ((Planter)i).Fertilize(fertilizerPerUse);
            --uses;
            if(uses == 0) {
                Destroy(gameObject);
                return new InteractionResult(null, true);
            }
            return new InteractionResult(this, true);
        }
        return new InteractionResult(this, false);
    }
}
