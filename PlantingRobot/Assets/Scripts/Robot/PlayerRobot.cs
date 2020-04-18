using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRobot : MonoBehaviour
{
    public float interactiveDistance = 3.0f;

    public enum PlayerState {normal, water, seed};
    private PlayerState state = PlayerState.normal;

    public void ChangePlayerState(PlayerState s, Material m)
    {
        this.state = s;
        this.gameObject.GetComponent<Renderer>().material = m;
    }

    public bool CanInteract(Transform other)
    {
        if((gameObject.transform.position - other.position).magnitude < interactiveDistance) {
            return true;
        }
        return false;
    }

    public PlayerState GetState()
    {
        return state;
    }
}
