using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public Material material;
    public PlayerRobot player;
    public PlayerRobot.PlayerState state;

    void OnMouseDown()
    {
        if (player.CanInteract(gameObject.transform))
        {
            player.ChangePlayerState(state, material);
        }
    }
}
