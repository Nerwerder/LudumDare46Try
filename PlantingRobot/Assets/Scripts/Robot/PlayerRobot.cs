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
    public float resetArmsTimer = 0.2f;

    private bool requestArmReset = false;
    private float armRequestTimer = 0f;

    public float toolSummonTime = 3f;
    public float toolSummonRadius = 5f;
    public float toolSommunUp = 3f;
    private ToolsRegistry toolsReg = null;
    private float toolSummonTimer = 0f;

    private bool audioPlaying = false;
    private AudioSource audioSource;
    public AudioClip summonChanting;
    

    public void Start() {
        feedbackLamp = gameObject.GetComponent<MoodLight>();
        arms = gameObject.GetComponent<ArmMovement>();
        toolsReg = FindObjectOfType<ToolsRegistry>();
        audioSource = gameObject.GetComponent<AudioSource>();
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

        if(Input.GetKey(KeyCode.Q) && curCarrying==null) {
            if(!audioPlaying) {
                audioSource.clip = summonChanting;
                audioSource.Play();
                audioSource.volume = 0.6f;
                audioPlaying = true;
            }
            toolSummonTimer += Time.deltaTime;

            if(toolSummonTimer > toolSummonTime) {
                SummonTools();
                toolSummonTimer = 0f;
            }
        } else if(toolSummonTimer > 0) {
            toolSummonTimer = 0;
            if(audioPlaying) {
                audioSource.Stop();
                audioPlaying = false;
            }
        }

        if(requestArmReset) {
            armRequestTimer += Time.deltaTime;
            if(armRequestTimer >= resetArmsTimer) {
                requestArmReset = false;
                armRequestTimer = 0f;
                ControlArms();
            }
        }
    }

    private Vector3 RandomPointOnCircleEdge(float radius) {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        return new Vector3(vector2.x, 0, vector2.y);
    }

    private void SummonTools() {
        var tools = toolsReg.GetAllTools();
        foreach(Tool t in tools) {
            t.GetComponent<Rigidbody>().velocity = Vector3.zero;
            t.transform.position = transform.position + RandomPointOnCircleEdge(toolSummonRadius) + Vector3.up * toolSommunUp;
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
        if (curCarrying) {
            curCarrying.DeactivateThrownMode();
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
        requestArmReset = true;
        armRequestTimer = 0f;
    }
}
