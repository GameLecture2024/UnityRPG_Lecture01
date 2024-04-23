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

    // ctrl + K + S : �ڵ� ���α� - region ������
    #region ������
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

    // Slot�� �������� �̵���ų �� �ִ��� �Ǻ��ϴ� �Լ� true�̸� Slot �̵� ���� false�̸� �Ұ���
    public bool CanPlaceInSlot(ItemObject itemObject)
    {
        // ��ü ���� �� ��� �ִ� ������ ã�� ����
        if(allowItems.Length <= 0 || itemObject == null || itemObject.data.id < 0)
        {
            return true;
        }
        // ���� �κ��丮���� ���� Ÿ���� �����۸� �̵� �ϴ� ����
        foreach(ItemType type in allowItems)
        {
            if (itemObject.type == type) return true;
        }

        return false;
    }
}
