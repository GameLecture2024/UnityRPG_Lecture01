using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    PlayerManager player;

    public Camera camera;
    public float maxDistance = 50f;
    public LayerMask actionMask;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
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
        // 1. �浹 �˻�
        // ���ǹ� ����ؼ� GroundItem�� �˻��Ͽ�
        // Debug�� �ش� Ŭ������ ������Ʈ �̸��� ����ϵ��� �ۼ��غ�����.
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            // itemObject�� data�� �����Ͽ� Buff Ŭ������ -> StatType + playerData �� ���ִ� �ڵ带 �ۼ��ؾ��մϴ�.
            
            foreach(Buff buff in item.itemObject.data.buffs)
            {
                foreach(Stat stat in player.playerData.stats)
                {
                    if(stat.type == buff.type)
                    {
                        stat.value.Addmodifier(buff);
                    }
                }
            }

            // 2. Debug�׽�Ʈ�� ���ο� ��� �߰�
            // Item Ŭ������ ���� ���� �ȿ��� ȣ���ϵ��� ����

            Destroy(other.gameObject);
        }
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
