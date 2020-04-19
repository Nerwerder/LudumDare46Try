using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected PlayerRobot player = null;

    public void Start() {
        player = FindObjectOfType<PlayerRobot>();
        Debug.Assert(player != null);
    }
}
