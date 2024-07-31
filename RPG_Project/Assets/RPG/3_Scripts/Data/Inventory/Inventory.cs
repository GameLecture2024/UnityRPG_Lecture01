using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 인벤토리의 데이터를 비우거나, 인벤토리안에 데이터가 있는지 확인하는 클래스
/// </summary>

[System.Serializable]
public class Inventory
{
    public InventorySlot[] slots = new InventorySlot[16];

    public void Clear() // 인벤토리 안의 모든 데이터를 비우는 함수. 버튼과 연결
    {
        foreach(var slot in slots)
        {
            slot.RemoveItem();
        }
    }

    // Helper Class
    public bool IsContain(ItemObject itemObject)
    {
        return IsContain(itemObject.data.id);
    }

    public bool IsContain(int id)
    {
        return slots.FirstOrDefault(slot => slot.item.id == id) != null;
    }
}
