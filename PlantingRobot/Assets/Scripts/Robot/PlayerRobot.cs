using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRobot : MonoBehaviour
{
    public Transform carryConnectioPoint;

    public float interactiveDistance = 3.0f;
    public int money = 20;

    private Carryable curCarrying;
    private MoodLight feedbackLamp;
    private ArmMovement arms;
    public int feedbackBlinks = 2;
    public float throwPower = 0f;

    public void Start() {
        feedbackLamp = gameObject.GetComponent<MoodLight>();
        arms = gameObject.GetComponent<ArmMovement>();

        ChangeColor();
        ControlArms();
    }

    public void Update() {
        if(Input.GetMouseButtonDown(1)) {
            FeedBack(Throw());      
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            FeedBack(Drop());
        }
    }

    public bool HasLaserGun() {
        return (curCarrying != null && curCarrying is LaserGun);
    }

    public bool CanInteract(Transform other) {
        float intDis = interactiveDistance;

        if(curCarrying && curCarrying is Tool) {
            intDis = ((Tool)curCarrying).toolRange;
        }

        if ((gameObject.transform.position - other.position).magnitude < intDis) {
            return true;
        }
        return false;
    }

    public bool InteractWithInteractable(Interactable i) {
        InteractionResult result = null;
        if (curCarrying) {
            result = curCarrying.InteractWith(i);
        } else {
            result = InteractWith(i);
        }
        Carry(result.carryable);
        FeedBack(result.success);
        ChangeColor();
        return true;
    }

    public bool InteractWithCarryable(Carryable c) {
        return PickUp(c);
    }

    public bool InteractWithYourself(PlayerRobot p) {
        return false;
    }

    private InteractionResult InteractWith(Interactable i) {
        if (i is Container) {
            var c = (Container)i;
            switch (c.type) {
                case Container.ContainerType.SeedContainer:
                    return ((SeedContainer)c).BuySeed();
                case Container.ContainerType.FertilizerContainer:
                    return ((FertilizerContainer)c).BuyFertilizer();
            }
        } else if (i is Planter) {
            return ((Planter)i).Harvest();
        }
        return new InteractionResult(null, false);
    }

    public bool CanAfford(int m) {
        return money >= m;
    }

    public bool Pay(int m) {
        if (money >= m) {
            money -= m;
            return true;
        }
        return false;
    }

    public void Earn(int m) {
        money += m;
    }

    public int GetMoney() {
        return money;
    }

    private void ChangeColor() {
        if (curCarrying) {
            if (curCarrying is WateringCan) {
                if (((WateringCan)curCarrying).hasWater()) {
                    feedbackLamp.SetColor(Color.blue);
                } else {
                    feedbackLamp.SetColor(Color.yellow);
                }
            }
        } else {
            feedbackLamp.SetColor(Color.yellow);
        }
    }

    private void FeedBack(bool s) {
        var blinkColor = s ? (Color.green) : (Color.red);
        feedbackLamp.SetColorBlink(blinkColor, feedbackBlinks);
    }

    private void Carry(Carryable c) {
        if(curCarrying == c) {
            return;
        }

        curCarrying = c;
        curCarrying.DeactivateThrownMode();
        if (curCarrying) {
            curCarrying.GetComponent<Rigidbody>().isKinematic = true;
            curCarrying.oldParent = curCarrying.transform.parent;
            curCarrying.transform.SetParent(carryConnectioPoint.transform);
            curCarrying.transform.position = carryConnectioPoint.transform.position;
            if(curCarrying is Tool) {
                curCarrying.transform.forward = -gameObject.transform.forward;  //TODO: Fix Tool-Models
                curCarrying.transform.position += ((Tool)curCarrying).carryOffset * curCarrying.transform.forward;
            }
        }

        ControlArms();
        ChangeColor();
    }

    public bool PickUp(Carryable c) {
        if (c == null || curCarrying != null) {
            return false;
        }

        Carry(c);
        return true;
    }

    public bool Drop() {
        if (curCarrying == null) {
            return false;
        }

        curCarrying.GetComponent<Rigidbody>().isKinematic = false;
        if (curCarrying.oldParent) {
            curCarrying.transform.SetParent(curCarrying.oldParent);
            curCarrying.oldParent = null;
        }

        curCarrying = null;
        ControlArms();
        ChangeColor();
        return true;
    }

    public bool Throw() {
        if(curCarrying == null) {
            return false;
        }

        curCarrying.GetComponent<Rigidbody>().isKinematic = false;
        if (curCarrying.oldParent) {
            curCarrying.transform.SetParent(curCarrying.oldParent);
            curCarrying.oldParent = null;
        }

        //Get VectorToTarget
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000)) {
            Vector3 ThrowVector = (hit.point - transform.position) * throwPower;
            curCarrying.GetComponent<Rigidbody>().AddForce(ThrowVector);
            curCarrying.ActivateThrownMode();
        }

        curCarrying = null;
        ControlArms();
        ChangeColor();
        return true;
    }

    private void ControlArms() {
        if(curCarrying && (curCarrying is Fruit || curCarrying is Seed)) {
            arms.ArmsUp();
        } else {
            arms.ArmsMid();
        }
    }

    public enum ChangeOfArms { Dip}
    public void RequestArmMovement(ChangeOfArms c) {
        arms.ArmsDown();
    }


}
