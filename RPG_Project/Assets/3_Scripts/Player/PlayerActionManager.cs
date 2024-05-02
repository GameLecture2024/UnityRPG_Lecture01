using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerActionManager : MonoBehaviour, ISaveManager
{
    PlayerManager player;

    public float maxDistance = 50f;

    [SerializeField] private InventoryObject equipment;
    [SerializeField] private InventoryObject inventory;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    private void Start()
    {
        inventory.OnUseItem += OnUseItem;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. 충돌 검사
        // 조건문 사용해서 GroundItem을 검색하여
        // Debug로 해당 클래스의 오브젝트 이름을 출력하도록 작성해보세요.
        if (other.CompareTag("Item"))
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            interactable.Interact(gameObject);

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Quest"))
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            interactable.Interact(gameObject);
        }
    }

    private void OnUseItem(ItemObject itemObject)
    {
        // 포션 사용 시 기능
        if(itemObject.type == ItemType.Potion)
        {
            Debug.Log("Potion User!");
            return;
        }

        // 버프 적용 시 기능 
        foreach (Buff buff in itemObject.data.buffs)
        {
            foreach (Stat stat in player.playerData.stats)
            {
                if (stat.type == buff.type)
                {
                    stat.value.Addmodifier(buff);
                }
            }
        }
    }

    public bool PickupItem(ItemObject itemObject, int amount = 1)
    {
        if (itemObject != null)
        {
            return inventory.AddItem(new Item(itemObject), amount);
        }

        return false;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.inventory = inventory.container;
    }

    public void LoadData(GameData gameData)
    {
        //inventory.container = gameData.inventory;
    }
}
