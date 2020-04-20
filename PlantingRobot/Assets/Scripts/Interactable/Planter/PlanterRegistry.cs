using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanterRegistry : MonoBehaviour
{
    public List<Planter> planters = new List<Planter>();
    public List<Plant> plants = new List<Plant>();

    public void RegisterPlanter(Planter p) {
        planters.Add(p);
    }

    public void DeregisterPlanter(Planter p) {
        planters.Remove(p);
    }

    public void RegisterPlant(Plant p) {
        plants.Add(p);
    }

    public void DeregisterPlant(Plant p) {
        plants.Remove(p);
    }

    public Planter GetRandomPlanter() {
        if(planters.Count == 0) {
            return null;
        }

        int index = Random.Range(0, (planters.Count - 1));
        return planters[index];
    }

    public Plant GetRandomPlant() {
        if(plants.Count == 0) {
            return null;
        }

        int index = Random.Range(0, (plants.Count - 1));
        return plants[index];
    }

    public List<Planter> GetNearPlanters(Planter p, float range) {
        List<Planter> ret = new List<Planter>();
        foreach(Planter pt in planters) {
            if(pt == p) {
                continue;
            }
            if((pt.transform.position-p.transform.position).magnitude <= range) {
                ret.Add(pt);
            }
        }
        return ret;
    }
}
