using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectRegistry : MonoBehaviour
{
    public Transform insectParent;

    private List<Insect> insects = new List<Insect>();

    public Transform GetInsectParent() {
        return insectParent;
    }

    public void RegisteerInsect(Insect i) {
        insects.Add(i);
    }

    public void DeregisterInsect(Insect i) {
        insects.Remove(i);
    }

    public List<Insect> GetNearInsects(Insect i, float range) {
        List<Insect> ret = new List<Insect>();
        foreach(Insect it in insects) {
            if(it == i) {
                continue;
            }
            if((it.transform.position-i.transform.position).magnitude <= range) {
                ret.Add(it);
            }
        }
        return ret;
    }
}
