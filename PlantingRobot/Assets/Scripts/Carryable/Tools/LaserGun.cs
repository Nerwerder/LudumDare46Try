using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Tool
{
    public Transform lasterStart = null;
    public float cooldown = 1f;
    public float laserDuration = 0.2f;
    private LineRenderer lRen = null;
    private float coolTimer = 0f;
    private bool readyToFire = true;

    public float upgradeDistance = 15f;

    public bool destroyMultipleTargets = false;
    public float destructionRange = 0f;

    private InsectRegistry insectReg = null;

    public new void Start() {
        base.Start();
        lRen = gameObject.GetComponent<LineRenderer>();
        insectReg = FindObjectOfType<InsectRegistry>();
    }

    private float distance(Transform t1, Transform t2) {
        return (t1.position - t2.position).magnitude;
    }

    public override InteractionResult InteractWith(Interactable i) {
        if(i is UpgradeContainer && (distance(transform, i.transform) <= upgradeDistance)) {
            return ((UpgradeContainer)i).UpgradeMe(this);
        }

        if (!readyToFire) { 
            return new InteractionResult(this, false); 
        }

        ShootAt(i.transform);

        if(i is Insect) {
            Insect insect = (Insect)i;
            if(destroyMultipleTargets) {
                foreach(Insect it in insectReg.GetNearInsects(insect, destructionRange)) {
                    it.ShootAt(this);
                }
            }
            return insect.ShootAt(this);
        }

        return new InteractionResult(this, false, false);
    }

    public void ShootAt(Transform t) {
        lRen.enabled = true;
        lRen.SetPosition(0, lasterStart.position);
        lRen.SetPosition(1, t.position);
        StartCoroutine(DisableLaser());
        readyToFire = false;
    }

    public void Update() {
        if (lRen.enabled) {
            lRen.SetPosition(0, lasterStart.position);
        }

        if (!readyToFire) {
            coolTimer += Time.deltaTime;
            if (coolTimer >= cooldown) {
                readyToFire = true;
                coolTimer = 0f;
            }
        }
    }

    IEnumerator DisableLaser() {
        yield return new WaitForSeconds(laserDuration);
        lRen.enabled = false;
    }
}
