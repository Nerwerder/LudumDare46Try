using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : Tool
{
    public float maxWater = 0f;
    public float wateringAmount = 0f;
    private float curWater = 0f;

    public new void Start() {
        base.Start();
        Debug.Assert(maxWater > 0);
        Debug.Assert(wateringAmount > 0);
    }

    public override CarryableInteractionResult InteractWith(Container c)  {
        switch (c.type) {
            case Container.ContainerType.WaterContainer:
                if(curWater < maxWater && player.Pay(c.interactionCost)) {
                    curWater = maxWater;
                    return new CarryableInteractionResult(this, true);
                }       
                break;
            default:
                //Nothing: Every Tool only interacts with relevant other Objects
                break;
        }
        return new CarryableInteractionResult(this, false);
    }

    public override CarryableInteractionResult InteractWith(Planter p) {
        //Water the planter
        float water = Mathf.Min(wateringAmount, curWater);
        if(water > 0) {
            p.Water(water);
            curWater -= water;
            return new CarryableInteractionResult(this, true);
        }
        return new CarryableInteractionResult(this, false);
    }
}
