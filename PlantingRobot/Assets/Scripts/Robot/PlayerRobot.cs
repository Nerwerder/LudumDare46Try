using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRobot : MonoBehaviour
{
    public Material playerMaterial = null;
    public float interactiveDistance = 3.0f;
    public int money = 20;

    public enum PlayerState { normal, water, seed };
    private PlayerState state = PlayerState.normal;

    public void ChangePlayerState(PlayerState s, Material m) {
        this.state = s;
        this.gameObject.GetComponent<Renderer>().material = m;
    }

    public bool CanInteract(Transform other) {
        if ((gameObject.transform.position - other.position).magnitude < interactiveDistance) {
            return true;
        }
        return false;
    }

    public PlayerState GetState() {
        return state;
    }

    void OnMouseDown() {
        ChangePlayerState(PlayerState.normal, playerMaterial);
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
}
