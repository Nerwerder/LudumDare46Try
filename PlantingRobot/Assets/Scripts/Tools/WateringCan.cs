using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : Tool
{
    public float maxWater = 0f;
    public float wateringAmount = 0f;
    private float curWater = 0f;
    void Start() {
        Debug.Assert(maxWater > 0);
        Debug.Assert(wateringAmount > 0);
    }

    public override void InteractWith(Container c)  {
        switch(c.type) {
            case Container.ContainerType.WaterHole:
                if(curWater < maxWater && player.Pay(c.interactionCost)) {
                    curWater = maxWater;
                }       
                break;
            default:
                //Nothing: Every Tool only interacts with relevant other Objects
                break;
        }
    }

    public override void InteractWith(Planter p) {
        //Water the planter
        float water = Mathf.Min(wateringAmount, curWater);
        p.Water(water);
        curWater -= water;
    }
}
