using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedContainer : Container
{
    public Seed seed = null;

    public new void Start() {
        base.Start();
        Debug.Assert(seed != null);
    }

    public InteractionResult BuySeed() {
        if(player.Pay(seed.price)) {
            return new InteractionResult(Instantiate(seed), true);
        }
        return new InteractionResult(null, false);
    }
}
