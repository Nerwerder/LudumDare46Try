using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : Interactable
{
    public float maxFlightHeight = 0f;
    public float minFlightHeight = 0f;
    public float upDownSpeed = 2f;

    /// <summary>
    /// Insects have to check on a regular basis if there is a better Target (for example if someone removes there current Plant Target)
    /// </summary>
    public float targetUpdateTime = 1f;
    private float targetUpdateTimer = 0f;
    public float movementSpeed = 2f;

    public float arriveDistance = 2f;
    public float circleDistance = 5f;

    public float attackPower = 3f;

    private bool up = false;
    private Vector3 target;
    private Plant plantTarget = null;
    private Planter planterTarget = null;
    private PlanterRegistry planterReg = null;
    private bool circleMode = false;
    private Vector3 circleTarget;

    public new void Start() {
        base.Start();

        //Set the Incect to a Random Height
        Vector3 curPos = transform.position;
        curPos.y = Random.Range(minFlightHeight, maxFlightHeight);
        transform.position = curPos;

        //Decide if the Insect starts flying up or down
        up = (Random.value > 0.5f);

        planterReg = FindObjectOfType<PlanterRegistry>();
        SearchNewTarget();
        
    }

    public void Update() {
        MoveUpAndDown();

        //Every targetUpdateTime search for a new Target
        targetUpdateTimer += Time.deltaTime;
        if(targetUpdateTimer > targetUpdateTime) {
            SearchBetterTarget();
        }

        if(circleMode) {
            CircleAroundTarget();
        } else {
            MoveToTarget();
        }
    }

    public InteractionResult ShootAt(LaserGun l) {
        Destroy(gameObject);
        return new InteractionResult(l, true, true);
    }

    private void SearchBetterTarget() {
        if(plantTarget == null) {
            plantTarget = planterReg.GetRandomPlant();
            if (plantTarget != null) {
                target = GetTargetTransform();
                circleMode = false;
            }
        }    
    }

    private void SearchNewTarget() {
        plantTarget = planterReg.GetRandomPlant();
        if(plantTarget == null) {
            planterTarget = planterReg.GetRandomPlanter();
        }
        target = GetTargetTransform();
        circleMode = false;
    }

    private Vector3 GetTargetTransform() {
        if(plantTarget == null) {
            return new Vector3(planterTarget.transform.position.x, planterTarget.transform.position.y, planterTarget.transform.position.z);
        }
        return new Vector3(plantTarget.transform.position.x, plantTarget.transform.position.y, plantTarget.transform.position.z);
    }

    private void MoveUpAndDown() {
        //Get Vector from Insect to InsectHeightTarget
        Vector3 curPos = transform.position;
        Vector3 targetHeight = new Vector3(curPos.x, ((up) ? (maxFlightHeight) : (minFlightHeight)), curPos.z);
        Vector3 heightChange = (targetHeight - curPos).normalized * upDownSpeed * Time.deltaTime;
        transform.Translate(heightChange, Space.World);

        if (up && transform.position.y > maxFlightHeight) {
            up = false;
        } else if (!up && transform.position.y < minFlightHeight) {
            up = true;
        }
    }

    private void MoveToTarget() {
        Vector3 curPos = transform.position;
        Vector3 targetPos = new Vector3(target.x, curPos.y, target.z);
        Vector3 posChange = (targetPos - curPos).normalized * movementSpeed * Time.deltaTime;
        transform.Translate(posChange, Space.World);
        transform.forward = -(targetPos - curPos).normalized;   //TODO: Fix Model Direction

        if ((targetPos - curPos).magnitude <= arriveDistance) {
            if (plantTarget == null) {
                SearchNewTarget();
            } else {
                circleMode = true;
                circleTarget = GetCircleTarget();
            }
        }
    }

    private void CircleAroundTarget() {
        Vector3 curPos = transform.position;
        Vector3 targetPos = new Vector3(circleTarget.x, curPos.y, circleTarget.z);
        Vector3 posChange = (targetPos - curPos).normalized * movementSpeed * Time.deltaTime;
        transform.Translate(posChange, Space.World);
        transform.forward = -(targetPos - curPos).normalized;   //TODO: Fix Model Direction

        if ((targetPos - curPos).magnitude <= (arriveDistance / 2)) {
            circleTarget = GetCircleTarget();
        }

        if(plantTarget != null) {
            plantTarget.InsectAttack(attackPower * Time.deltaTime);
        }
    }

    private Vector3 GetCircleTarget() {
        return target + RandomPointOnCircleEdge(circleDistance);
    }

    private Vector3 RandomPointOnCircleEdge(float radius) {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        return new Vector3(vector2.x, 0, vector2.y);
    }
}
