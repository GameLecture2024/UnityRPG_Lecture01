using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Rendering;
using UnityEngine;

public enum InterfaceType
{
    Equipment, Invenotry, QuickSlot
}

[CreateAssetMenu(fileName = "new Inventory", menuName = "Data/Inventory")]
[System.Serializable]
public class InventoryObject : ScriptableObject
{
    public ItemDatabase database;
    public InterfaceType type;

    // Equipment의 인벤토리 슬롯 갯수, Inventory 슬롯 개수 등의 정보가 다르기 때문에, 각각의 정보를 담는 Container 역할
    [SerializeField] public Inventory container = new Inventory();

    public InventorySlot[] Slots => container.slots;
    public Action<ItemObject> OnUseItem;

    // Slot이 비어 있는지 아닌지 판단을 해야합니다.
    public int EmptySlotCount
    {
        get
        {
            int count = 0;    // 비어있는 Slot의 수
            foreach(var slot in Slots)
            {
                if(slot.item.id < 0)  // id가 -1이면 데이터가 없는 것  
                {
                    count++;
                }
            }
            return count;
        }
    }

    public bool AddItem(Item item, int amount)
    {
        // 비어 있는 슬롯이 없으면 AddItem return해줘야 한다.
        if (EmptySlotCount <= 0) return false;

        // 현재 같은 아이템이 있는지 확인하는 기능
        InventorySlot slot = FindItemInventory(item);
        if (!database.itemObjects[item.id].stackable || slot == null)
        {
            if (EmptySlotCount <= 0) return false;

            // 비어 있는 아이템 슬롯에 아이템 추가 하는 기능
            GetEmptySlot().AddItem(item, amount);
        }
        else
        {
            // 아이템에 스택을 쌓아 주는 기능
            slot.AddAmount(amount);
        }

        return true;
    }

    // Helper Class, Inventory 기능을 도와주는 함수
    private InventorySlot FindItemInventory(Item item)
    {
        return Slots.FirstOrDefault(slot => slot.item.id == item.id);      
    }

    private InventorySlot GetEmptySlot()
    {
        return Slots.FirstOrDefault(slot => slot.item.id < 0);
    }

    public bool IsContainItem(ItemObject itemObject)
    {
        return Slots.FirstOrDefault(slot => slot.item.id == itemObject.data.id) != null;
    }
    
    public void SwapItems(InventorySlot slot1, InventorySlot slot2)
    {
        if (slot1 == slot2) return;

        if(slot2.CanPlaceInSlot(slot1.itemObject) && slot1.CanPlaceInSlot(slot2.itemObject))
        {
            InventorySlot tempSlot = new InventorySlot(slot2.item, slot2.amount);
            slot2.AddItem(slot1.item, slot1.amount);
            slot1.AddItem(tempSlot.item, tempSlot.amount);
        }
    }

    public void UseItem(InventorySlot slotToUse)
    {
        // 인벤토리 슬롯에 있는 아이템을 사용
        if(slotToUse.itemObject == null || slotToUse.item.id < 0 || slotToUse.amount <= 0)
        {
            return;
        }

        ItemObject itemObject = slotToUse.itemObject;
        slotToUse.AddItem(slotToUse.item, slotToUse.amount - 1);

        OnUseItem.Invoke(itemObject);
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        container.Clear();
    }

    public string savePath;

    [ContextMenu("Save")]
    public void SaveData()
    {     
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.persistentDataPath + "/" + savePath + ".txt", FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, container);
        stream.Close();
    }
    [ContextMenu("Load")]
    public void LoadData()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.persistentDataPath + "/" + savePath + ".txt", FileMode.Open, FileAccess.Read);
        Inventory newContainer = formatter.Deserialize(stream) as Inventory;

        for(int i =0; i< Slots.Length; i++)
        {
            Slots[i].AddItem(newContainer.slots[i].item, newContainer.slots[i].amount);
        }
        stream.Close();
    }
}
