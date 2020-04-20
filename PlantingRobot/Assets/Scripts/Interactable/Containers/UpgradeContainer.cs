using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeContainer : Container
{
    public List<WateringCan> wateringCans;
    public List<LaserGun> laserGuns;
    public List<FertilizerBox> fertilizers;

    public new void Start() {
        base.Start();
    }

    public InteractionResult UpgradeMe(Carryable c) {
        if(c is WateringCan) {
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
        }  else if (c is LaserGun) {
            LaserGun oldLaserGun = (LaserGun)c;
            int upgradeLevel = oldLaserGun.level + 1;
            LaserGun newLaserGun = null;
            foreach (LaserGun l in laserGuns) {
                if (l.level == upgradeLevel) {
                    newLaserGun =l;
                    break;
                }
            }

            if(newLaserGun && player.Pay(newLaserGun.cost)) {
                Destroy(oldLaserGun.gameObject);
                return new InteractionResult(Instantiate(newLaserGun, oldLaserGun.oldParent), true, true);
            }
        } else if (c is FertilizerBox) {
            FertilizerBox oldFertilizer = (FertilizerBox)c;
            int upgradeLevel = oldFertilizer.level + 1;
            FertilizerBox newFertilizer = null;
            foreach (FertilizerBox f in fertilizers) {
                if (f.level == upgradeLevel) {
                    newFertilizer = f;
                    break;
                }
            }

            if (newFertilizer && player.Pay(newFertilizer.cost)) {
                Destroy(oldFertilizer.gameObject);
                return new InteractionResult(Instantiate(newFertilizer, oldFertilizer.oldParent), true, true);
            }
        }

        return new InteractionResult(c, false, false);
    }
}
