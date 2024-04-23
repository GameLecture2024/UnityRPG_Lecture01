using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemType[] allowItems = new ItemType[0];

    [NonSerialized] public InventoryObject parent;
    [NonSerialized] public GameObject slotUI;

    public Action<InventorySlot> OnPreUpdate;
    public Action<InventorySlot> OnPostUpdate;

    public Item item;
    public int amount;

    public ItemObject itemObject
    {
        get
        {
            return item.id >= 0 ? parent.database.itemObjects[item.id] : null;
        }
    }

    // ctrl + K + S : 코드 감싸기 - region 생성자
    #region 생성자
    public InventorySlot() => UpdateSlot(new Item(), 0);
    public InventorySlot(Item item, int amount) => UpdateSlot(item, amount); 
    #endregion


    public void AddItem(Item item, int amount) => UpdateSlot(item, amount);
    public void AddAmount(int value) => UpdateSlot(item, amount += value);

    public void RemoveItem() => UpdateSlot(new Item(), 0);

    void UpdateSlot(Item item, int amount)
    {
        OnPreUpdate?.Invoke(this);    
        this.item = item;
        this.amount = amount;
        OnPostUpdate?.Invoke(this);
    }

    // Slot에 아이템을 이동시킬 수 있는지 판별하는 함수 true이면 Slot 이동 가능 false이면 불가능
    public bool CanPlaceInSlot(ItemObject itemObject)
    {
        // 전체 슬롯 중 비어 있는 슬롯을 찾는 로직
        if(allowItems.Length <= 0 || itemObject == null || itemObject.data.id < 0)
        {
            return true;
        }
        // 착용 인벤토리에서 같은 타입의 아이템만 이동 하는 로직
        foreach(ItemType type in allowItems)
        {
            if (itemObject.type == type) return true;
        }

        return false;
    }
}
