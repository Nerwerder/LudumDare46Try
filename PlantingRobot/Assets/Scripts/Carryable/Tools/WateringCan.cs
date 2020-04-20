using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : Tool
{
    public float maxWater = 0f;
    public float wateringAmount = 0f;
    private float curWater = 0f;

    public bool waterNearPlanters = false;
    public float extraWaterRange = 0f;

    private PlanterRegistry plantReg = null;
    public new void Start() {
        base.Start();
        Debug.Assert(maxWater > 0);
        Debug.Assert(wateringAmount > 0);

        plantReg = FindObjectOfType<PlanterRegistry>();
    }

    public override InteractionResult InteractWith(Interactable i) {
        if (i is Container) {
            Container c = (Container)i;
            switch (c.type) {
                case Container.ContainerType.WaterContainer:
                    if (curWater < maxWater) {
                        curWater = maxWater;
                        player.RequestArmMovement(PlayerRobot.ChangeOfArms.Dip);
                        return new InteractionResult(this, true);
                    }
                    break;
                case Container.ContainerType.UpgradeContainer:
                    return ((UpgradeContainer)c).UpgradeMe(this);
            }
        } else if (i is Planter) {
            float water = Mathf.Min(wateringAmount, curWater);
            if (water > 0) {
                curWater -= water;
                ((Planter)i).Water(water);

                if(waterNearPlanters) {
                    var nearPlanters = plantReg.GetNearPlanters((Planter)i, extraWaterRange);
                    foreach(Planter ep in nearPlanters) {
                        ep.Water(water);
                    }
                }
                
                return new InteractionResult(this, true);
            }
        }
        return new InteractionResult(this, false);
    }

    public bool hasWater() {
        return curWater > 0;
    }

    public new void OnDestroy() {
        base.OnDestroy();
    }
}
