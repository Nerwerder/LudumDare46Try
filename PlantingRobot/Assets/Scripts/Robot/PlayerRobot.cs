using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRobot : MonoBehaviour
{
    public Material playerMaterial = null;
    public GameObject colorChanger = null;
    public float interactiveDistance = 3.0f;
    public int money = 20;

    private Carryable curCarrying;

    public void Update() {
        //Move the Payload
        if(curCarrying is Tool) {
            Tool t = (Tool)curCarrying;
            t.transform.position = transform.position + transform.forward * t.distance + Vector3.up * t.heightOffset;
            t.transform.forward = -transform.forward;
        } else if (curCarrying is Seed || curCarrying is Fruit) {
            curCarrying.transform.position = transform.position + Vector3.up * 5;
        }

        if(Input.GetMouseButton(1)) {
            if(curCarrying) {
                DropCarrying();
            }
        }
    }

    public void InteractWithMe(Container c) {
        if (curCarrying) {
            var res = curCarrying.InteractWith(c);
            Carry(res.carryable);    //TODO: Do something with success or failure
        } else {
            switch (c.type) {
                case Container.ContainerType.SeedContainer:
                    Carry(((SeedContainer)c).BuySeed());
                    break;
                default:
                    //Nothing
                    break;
            }
        }
    }

    public void InteractWithMe(Planter p) {
        if(curCarrying) {
            var res = curCarrying.InteractWith(p);
            Carry(res.carryable);    //TODO: Do something with success or failure
        } else {
            Carry(p.Harvest());
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

    private void Carry (Carryable c) {
        curCarrying = c;
        if(curCarrying) {
            curCarrying.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public bool PickMeUp(Carryable c) {
        if(c == null || curCarrying != null) {
            return false;
        }

        curCarrying = c;
        curCarrying.GetComponent<Rigidbody>().isKinematic = true;
        return true;
    }

    public bool DropCarrying() {
        if(curCarrying == null) {
            return false;
        }

        curCarrying.GetComponent<Rigidbody>().isKinematic = false;
        curCarrying = null;
        return true;
    }
}
