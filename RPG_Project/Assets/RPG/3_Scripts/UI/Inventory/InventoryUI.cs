using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI Event가 발생햇을 때 마우스의 정보를 저장하는 클래스
/// </summary>
public static class MouseData
{
    public static InventoryUI interfaceMouseIsOver;
    public static GameObject tempItemDrag;            // 드래그 중인 아이템 정보를 포함하는 오브젝트
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
        // 인터페이스에 마우스가 올라왔을때 초기화 기능을 구현
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
            Debug.LogWarning("이벤트 트리거가 없습니다.");
            return;
        }

        EventTrigger.Entry eventTrigger = new EventTrigger.Entry { eventID = type };
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    // Inventory Panel UI 이벤트 함수
    private void OnEnterInterface(GameObject gameObject)
    {
        MouseData.interfaceMouseIsOver = gameObject.GetComponent<InventoryUI>();
    }

    private void OnExitInterface(GameObject gameObject)
    {
        MouseData.interfaceMouseIsOver = null;
    }

    // Slot UI 이벤트 함수
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
        // Slot에 있는 게임오브젝트로 부터 DragImage GameObject 생성
        MouseData.tempItemDrag = CreateDragImage(go);
    }

    private GameObject CreateDragImage(GameObject go)
    {
        if (slotUIs[go].item.id < 0) return null;

        GameObject dragImage = new GameObject();

        RectTransform rect = dragImage.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(100 / 2, 100 / 2); // 드래그 할 때는 슬롯 크기의 절반 크기로 설정
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

        // 인벤토리 UI가 아닌 인터페이스에서 드래그 끝날 때 슬롯 아이템 제거
        if(MouseData.interfaceMouseIsOver == null)
        {
            slotUIs[go].RemoveItem();
        }
        // 인번테로 UI에서 다른 슬롯일 경우 해당 슬롯과 교체
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
            Debug.Log("슬롯이 없습니다.");
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

        // InventoryItem에서 Slot UI 이미지를 불러온다.
        slotImage.sprite = slot.item.id < 0 ? null : slot.itemObject.icon;

        // SlotUIGameObject에 Image컴포넌트의 Alpha를 0 -> 1
        slotImage.color = slot.item.id < 0 ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);

        // 타입이 수량일 경우에는 Text에 Amount 갯수 만큼 출력한다.
        slot.slotUI.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.id < 0 ? string.Empty : (slot.amount == 1 ? string.Empty : "x" + slot.amount.ToString("n0"));
    }
}
