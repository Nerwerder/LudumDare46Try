using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedContainer : Container
{
    public enum SeedType { Potato, Tomato, Beet}
    public SeedType seedType;
    public GameObject Seed;

    public Seed BuySeed() {
        if(player.Pay(interactionCost)) {
            return Instantiate(Seed).GetComponent<Seed>();
        }
        return null;
    }
}
