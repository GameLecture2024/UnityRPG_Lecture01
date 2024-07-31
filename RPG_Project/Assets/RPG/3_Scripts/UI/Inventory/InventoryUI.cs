using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI Event�� �߻����� �� ���콺�� ������ �����ϴ� Ŭ����
/// </summary>
public static class MouseData
{
    public static InventoryUI interfaceMouseIsOver;
    public static GameObject tempItemDrag;            // �巡�� ���� ������ ������ �����ϴ� ������Ʈ
    public static GameObject slotHoveredOver; 
}

public abstract class InventoryUI : MonoBehaviour
{
    public InventoryObject inventoryObject;

    public Dictionary<GameObject, InventorySlot> slotUIs = new Dictionary<GameObject, InventorySlot>();

    private void Awake()
    {
        CreateSlotUIs();

        for(int i =0; i < inventoryObject.Slots.Length; i++)
        {
            inventoryObject.Slots[i].parent = inventoryObject;
            inventoryObject.Slots[i].OnPostUpdate += OnPostUpdate;
        }
        // �������̽��� ���콺�� �ö������ �ʱ�ȭ ����� ����
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject);});
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }



    private void Start()
    {
        for (int i = 0; i < inventoryObject.Slots.Length; i++)
        {
            inventoryObject.Slots[i].AddItem(inventoryObject.Slots[i].item, inventoryObject.Slots[i].amount);
        }
    }

    public abstract void CreateSlotUIs();

    protected void AddEvent(GameObject go, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = go.GetComponent<EventTrigger>();
        if (!trigger)
        {
            Debug.LogWarning("�̺�Ʈ Ʈ���Ű� �����ϴ�.");
            return;
        }

        EventTrigger.Entry eventTrigger = new EventTrigger.Entry { eventID = type };
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    // Inventory Panel UI �̺�Ʈ �Լ�
    private void OnEnterInterface(GameObject gameObject)
    {
        MouseData.interfaceMouseIsOver = gameObject.GetComponent<InventoryUI>();
    }

    private void OnExitInterface(GameObject gameObject)
    {
        MouseData.interfaceMouseIsOver = null;
    }

    // Slot UI �̺�Ʈ �Լ�
    public void OnEnter(GameObject gameObject)
    {
        MouseData.slotHoveredOver = gameObject;
        MouseData.interfaceMouseIsOver = GetComponentInParent<InventoryUI>();
    }

    public void OnExit(GameObject gameObject)
    {
        MouseData.slotHoveredOver = null;
    }

    public void OnStartDrag(GameObject go)
    {
        // Slot�� �ִ� ���ӿ�����Ʈ�� ���� DragImage GameObject ����
        MouseData.tempItemDrag = CreateDragImage(go);
    }

    private GameObject CreateDragImage(GameObject go)
    {
        if (slotUIs[go].item.id < 0) return null;

        GameObject dragImage = new GameObject();

        RectTransform rect = dragImage.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(100 / 2, 100 / 2); // �巡�� �� ���� ���� ũ���� ���� ũ��� ����
        dragImage.transform.SetParent(transform.parent);
        Image image = dragImage.AddComponent<Image>();
        image.sprite = slotUIs[go].itemObject.icon;
        image.raycastTarget = false;

        dragImage.name = "Drag Image";

        return dragImage;
    }

    public void OnDrag(GameObject go)
    {
        if (MouseData.tempItemDrag == null) return;

        MouseData.tempItemDrag.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void OnEndDrag(GameObject go)
    {
        Destroy(MouseData.tempItemDrag);

        // �κ��丮 UI�� �ƴ� �������̽����� �巡�� ���� �� ���� ������ ����
        if(MouseData.interfaceMouseIsOver == null)
        {
            slotUIs[go].RemoveItem();
        }
        // �ι��׷� UI���� �ٸ� ������ ��� �ش� ���԰� ��ü
        else if (MouseData.slotHoveredOver)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotUIs[MouseData.slotHoveredOver];

            inventoryObject.SwapItems(slotUIs[go], mouseHoverSlotData);
        }
    }

    public void OnClick(GameObject go, PointerEventData data)
    {
        InventorySlot slot = slotUIs[go];
        if(slot == null)
        {
            Debug.Log("������ �����ϴ�.");
            return;
        }

        if(data.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick(slot);
        }
        else if(data.button == PointerEventData.InputButton.Right)
        {
            OnRightClick(slot);
        }
    }

    protected virtual void OnRightClick(InventorySlot slot) { }

    protected virtual void OnLeftClick(InventorySlot slot) { }

    public void OnPostUpdate(InventorySlot slot)
    {
        if (slot == null || slot.slotUI == null) return;

        Image slotImage = slot.slotUI.transform.GetChild(0).GetComponent<Image>();

        // InventoryItem���� Slot UI �̹����� �ҷ��´�.
        slotImage.sprite = slot.item.id < 0 ? null : slot.itemObject.icon;

        // SlotUIGameObject�� Image������Ʈ�� Alpha�� 0 -> 1
        slotImage.color = slot.item.id < 0 ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);

        // Ÿ���� ������ ��쿡�� Text�� Amount ���� ��ŭ ����Ѵ�.
        slot.slotUI.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.id < 0 ? string.Empty : (slot.amount == 1 ? string.Empty : "x" + slot.amount.ToString("n0"));
    }
}
