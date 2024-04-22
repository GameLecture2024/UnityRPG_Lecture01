using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerActionManager : MonoBehaviour
{
    PlayerManager player;

    public Camera camera;
    public float maxDistance = 50f;
    public LayerMask actionMask;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            SearchInteractObject();
        }
    }

    private void SearchInteractObject()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance, actionMask))
        {

        }
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
    }

    private void OnUseItem(ItemObject itemObject)
    {
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

    private void OnDrawGizmos()
    {
        RaycastHit hit;

        bool isHit = Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance, actionMask);



        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(camera.transform.position, camera.transform.forward * hit.distance);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(camera.transform.position, camera.transform.forward * maxDistance);
        }

    }
}
