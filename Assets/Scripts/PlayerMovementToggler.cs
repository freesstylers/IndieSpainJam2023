using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementToggler : MonoBehaviour
{
    public void EnableMove()
    {
        GameObject player = DialogueManager.instance_.player;
        player.GetComponent<Player.PlayerMovement>().SetInteracting(false);
    }

    public void DisableMove()
    {
        GameObject player = DialogueManager.instance_.player;
        player.GetComponent<Player.PlayerMovement>().SetInteracting(true);
    }
}
