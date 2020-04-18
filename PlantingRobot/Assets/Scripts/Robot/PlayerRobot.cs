using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRobot : MonoBehaviour
{
    public Material playerMaterial = null;
    public GameObject colorChanger = null;
    public float interactiveDistance = 3.0f;
    public int money = 20;

    private Tool currentTool = null;
    private Seed currentSeed = null;

    public void Update() {

        if(currentTool) {
            currentTool.transform.position = transform.position + transform.forward * currentTool.distance + Vector3.up * currentTool.heightOffset;
            currentTool.transform.forward = -transform.forward;
        }

        if(currentSeed) {
            currentSeed.transform.position = transform.position + Vector3.up * 5;
        }

        if(Input.GetMouseButton(1)) {
            if(currentTool) {
                DropTool();
            }
            if(currentSeed) {
                DropSeeed();
            }
        }
    }

    public void InteractWithMe(Container c) {
        if(currentTool) {
            currentTool.InteractWith(c);
        } 

        if(!currentSeed && !currentTool) {
            switch(c.type) {
                case Container.ContainerType.SeedContainer:
                    currentSeed = ((SeedContainer)c).BuySeed();
                    break;
                default:
                    //Nothing
                    break;
            }
        }
    }

    public void InteractWithMe(Planter p) {
        if (currentTool) {
            currentTool.InteractWith(p);
        } else if (currentSeed) {
            currentSeed = currentSeed.InteractWith(p);
        } else {
            p.Harvest();//TODO: Make Robot the acttive component in this interaction
        }
    }

    public bool CanInteract(Transform other) {
        if ((gameObject.transform.position - other.position).magnitude < interactiveDistance) {
            return true;
        }
        return false;
    }

    public bool CanAfford(int m) {
        return money >= m;
    }

    public bool Pay(int m) {
        if (money > m) {
            money -= m;
            return true;
        }
        return false;
    }

    public void Earn(int m) {
        money += m;
    }

    //TODO Create Parent for Tool - Seed - Fruit
    public bool PickMeUp(Tool t) {
        if(currentTool || currentSeed) {
            return false;
        }

        currentTool = t;
        currentTool.GetComponent<Rigidbody>().isKinematic = true;
        
        return true;
    }

    public bool PickMeUp(Seed s) {
        if (currentSeed || currentTool) {
            return false;
        }

        currentSeed = s;
        currentSeed.GetComponent<Rigidbody>().isKinematic = true;

        return true;
    }

    public bool DropTool() {
        if(!currentTool) {
            return false;
        }

        currentTool.GetComponent<Rigidbody>().isKinematic = false;
        currentTool = null;
        return true;
    }

    public bool DropSeeed() {
        if(!currentSeed) {
            return false;
        }

        currentSeed.GetComponent<Rigidbody>().isKinematic = false;
        currentSeed = null;
        return true;
    }
}
