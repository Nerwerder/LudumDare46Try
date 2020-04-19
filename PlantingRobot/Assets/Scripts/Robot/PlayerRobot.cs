using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRobot : MonoBehaviour
{
    public Material playerMaterial = null;
    public float interactiveDistance = 3.0f;
    public int money = 20;

    private Carryable curCarrying;
    private MoodLight feedbackLamp;
    public int feedbackBlinks = 2;

    //Special States for Color of Light
    private bool carryWateringCan;

    public void Start() {
        feedbackLamp = gameObject.GetComponent<MoodLight>();
        feedbackLamp.SetColor(Color.yellow);
    }

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
                FeedBack(Drop());
            }
        }
    }

    public bool CanInteract(Transform other) {
        if ((gameObject.transform.position - other.position).magnitude < interactiveDistance) {
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

    public InteractionResult InteractWith(Interactable i) {
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

    private void ChangeColor() {
        if(curCarrying) {
            if(carryWateringCan) {
                if(((WateringCan)curCarrying).hasWater()) {
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

    private void Carry (Carryable c) {
        curCarrying = c;
        if(curCarrying) {
            curCarrying.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public bool PickUp(Carryable c) {
        if(c == null || curCarrying != null) {
            return false;
        }

        curCarrying = c;
        curCarrying.GetComponent<Rigidbody>().isKinematic = true;

        if(curCarrying is WateringCan) {
            carryWateringCan = true;
        }
        ChangeColor();
        return true;
    }

    public bool Drop() {
        if(curCarrying == null) {
            return false;
        }

        curCarrying.GetComponent<Rigidbody>().isKinematic = false;
        curCarrying = null;
        carryWateringCan = false;
        ChangeColor();
        return true;
    }
}
