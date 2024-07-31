using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInventoryUI : InventoryUI
{
    public GameObject[] staticSlots = null;

    public override void CreateSlotUIs()
    {
        slotUIs = new Dictionary<GameObject, InventorySlot>();
        for(int i =0; i < inventoryObject.Slots.Length; i++)
        {
            GameObject go = staticSlots[i];

            AddEvent(go, EventTriggerType.PointerEnter, delegate { OnEnter(go); });
            AddEvent(go, EventTriggerType.PointerExit, delegate { OnExit(go); });
            AddEvent(go, EventTriggerType.BeginDrag, delegate { OnStartDrag(go); });
            AddEvent(go, EventTriggerType.Drag, delegate { OnDrag(go); });
            AddEvent(go, EventTriggerType.EndDrag, delegate { OnEndDrag(go); });

            inventoryObject.Slots[i].slotUI = go;
            slotUIs.Add(go, inventoryObject.Slots[i]);
        }
    }
}
