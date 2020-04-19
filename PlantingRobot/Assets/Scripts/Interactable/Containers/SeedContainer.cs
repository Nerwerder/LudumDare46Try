using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedContainer : Container
{
    public enum SeedType { Potato, Tomato, Beet}
    public SeedType seedType;
    public GameObject seed;
    public int seedCost = 0;

    public InteractionResult BuySeed() {
        if(player.Pay(seedCost)) {
            var res = Instantiate(seed).GetComponent<Seed>();
            return new InteractionResult(res, true);
        }
        return new InteractionResult(null, false);
    }
}
