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
        // 1. 충돌 검사
        // 조건문 사용해서 GroundItem을 검색하여
        // Debug로 해당 클래스의 오브젝트 이름을 출력하도록 작성해보세요.
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            // itemObject의 data에 접근하여 Buff 클래스에 -> StatType + playerData 비교 해주는 코드를 작성해야합니다.
            
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

            // 2. Debug테스트한 내부에 기능 추가
            // Item 클래스를 만들어서 조건 안에서 호출하도록 구현

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
