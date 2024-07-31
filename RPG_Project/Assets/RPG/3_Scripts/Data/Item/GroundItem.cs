using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour, IInteractable
{
    public ItemObject itemObject;

    public void Interact(GameObject go)
    {
        PlayerActionManager player = go.GetComponent<PlayerActionManager>();

        player.PickupItem(itemObject);
    }

}
