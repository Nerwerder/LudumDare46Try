using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Tool
{
    public override InteractionResult InteractWith(Interactable i) {
        return new InteractionResult(this, false);
    }

    public void ShootAt(RaycastHit h) {

    }
}
