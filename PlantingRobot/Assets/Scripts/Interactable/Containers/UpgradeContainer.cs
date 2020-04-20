using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeContainer : Container
{
    public List<WateringCan> wateringCans;

    private PlayerRobot player = null;

    public new void Start() {
        base.Start();
        player = FindObjectOfType<PlayerRobot>();
        Debug.Assert(player != null);
    }

    public InteractionResult UpgradeMe(Carryable c) {

        if(c is WateringCan) {
            //Search for new WateringCan wit level x.level+1
            WateringCan oldWateringCan = (WateringCan)c;
            int upgradeLevel = oldWateringCan.level + 1;
            WateringCan newWateringCan = null;
            foreach(WateringCan w in wateringCans) {
                if(w.level == upgradeLevel) {
                    newWateringCan = w;
                    break;
                }
            }

            //If there is a better WateringCan and the player can afford it, upgrade.
            if(newWateringCan && player.Pay(newWateringCan.cost)) {
                Destroy(oldWateringCan.gameObject);
                return new InteractionResult(Instantiate(newWateringCan, oldWateringCan.oldParent), true, true);
            }
        }

        return new InteractionResult(c, false, false);
    }
}
