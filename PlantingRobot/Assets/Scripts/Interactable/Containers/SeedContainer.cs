using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedContainer : Container
{
    public Seed seed = null;
    public Transform seedParent = null;
    public new void Start() {
        base.Start();
        Debug.Assert(seed != null);
        Debug.Assert(seedParent != null);
    }

    public InteractionResult BuySeed() {
        if(player.Pay(seed.price)) {
            return new InteractionResult(Instantiate(seed, seedParent), true);
        }
        return new InteractionResult(null, false);
    }
}
