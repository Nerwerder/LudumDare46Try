using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : Interactable
{
    public new void Start() {
        base.Start();
    }

    public void Update() {
        
    }

    public InteractionResult ShootAt(LaserGun l) {
        Destroy(gameObject);
        return new InteractionResult(l, true, true);
    }
}
