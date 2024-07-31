using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    // 오브젝트 풀링, 사용해야 하는 이유?
    // 유니티 게임 오브젝트 생성 후 파괴될 때 GC에 의한 메모리 누수가 발생된다.
    // 자주 파괴되는 게임 오브젝트 ( 총알, 대량의 몬스터 파괴 등, 이팩트) 오브젝트 풀링 방식으로 메모리 누수 방지.

    // 파괴될 때.. 리소스 누수 발생 -> 게임 종료될 때, 씬이 변경될 때 1번만 진행.
    // 생성한 오브젝트 들을 컬렉션에 보관하고, 필요할 때 마다 생성한 오브젝트를 활성화
    // 필요 없는 오브젝트는 비활성화 하는 방식을 사용합니다

    // 정적 오브젝트 풀링, 동적 오브젝트 풀링
    // 정적 : 한번 오브젝트 풀링할 데이터를 생성 후 그 수가 고정된 것
    // 동적 : 최초에 오브젝트 풀링 후 활성화할 데이터가 부족하면 그 때 마다 새로운 오브젝트를 추가해주는 방식

    public int maxCount = int.MaxValue; // 오브젝트 풀링의 최대치 설정
    private int allocateCount;          // 허용(활성화) 가능한 숫자
    private int activeCount;            // 현재 활성화 중인 숫자
    private int increaseCount = 5;      // 풀링할 오브젝트 수가 부족할 때 추가로 생성해줄 수

    private GameObject poolObject;
    private List<GameObject> poolObjectList;     // 현재 활성화된 오브젝트 + 비활성된 오브젝트
    // private Stack<GameObject> PushedObject;   // 비활성화된 오브젝트 모음
    


    private static Transform Container;     // 생성될 Instant들을 보관할 컨테이너 게임오브젝트 위치

    public ObjectPool() { } // 기본 생성자

    public ObjectPool(GameObject prefab)
    {
        allocateCount = 0;
        activeCount = 0;
        poolObject = prefab;
        poolObjectList = new List<GameObject>();

        if(Container == null)
        {
            GameObject container = new GameObject();
            container.name = "Container";
            Container = container.transform;
        }

        // increaseCount 수 만큼 오브젝트를 생성
        InstantiateObject();
    }

    public void InstantiateObject()
    {
        allocateCount += increaseCount;

        for(int i=0; i< allocateCount; i++)
        {
            GameObject newObject = GameObject.Instantiate(poolObject);
            newObject.SetActive(false);
            poolObjectList.Add(newObject);

            newObject.transform.parent = Container;
        }
    }

    // 씬 변경 또는 게임 종료 시에 한번만 실행한다.
    public void DestroyObject() // List에 있는 모든 오브젝트를 파괴하고 리스트 Clear
    {
        if(poolObjectList == null)
        {
            Debug.Log("생성된 오브젝트 풀이 없습니다.");
            return;
        }

        for(int i=0; i<poolObjectList.Count; i++)
        {
            GameObject.Destroy(poolObjectList[i].gameObject);
        }

        poolObjectList.Clear();
    }

    public GameObject ActivatePoolItem() // 비활성화된 오브젝트를 활성화 시켜준다.
    {
        if (poolObjectList == null) return null;

        // 허용 가능한 숫자와 활성화된 수가 같을 때
        // 오브젝트 최초 풀링할 때
        if(allocateCount == activeCount)
        {
            InstantiateObject();
        }

        for(int i=0; i< poolObjectList.Count; i++)
        {
            GameObject newGameObject = poolObjectList[i].gameObject;

            if(newGameObject.activeSelf == false)
            {
                activeCount++;
                newGameObject.SetActive(true);
                return newGameObject;
            }
        }

        return null;
    }

    public void DeActivatePoolItem(GameObject removeObject) // 오브젝트가 파괴되는 대신 비활성화 시켜준다.
    {
        if (poolObjectList == null || removeObject == null) return;

        for(int i=0; i< poolObjectList.Count; i++)
        {
            GameObject newGameObject = poolObjectList[i].gameObject;

            if (newGameObject == removeObject)
            {
                activeCount--;
                newGameObject.SetActive(false);
               
                return;
            }
        }
    }

    #region Helper 
    public void DeActivateAllPoolItem() // 모든 오브젝트를 비활성화 시켜주는 함수.
    {
        if (poolObjectList == null) return;

        for(int i=0; i< poolObjectList.Count; i++)
        {
            if(poolObjectList[i] != null && poolObjectList[i].activeSelf == true)
            {
                poolObjectList[i].SetActive(false);
            }
        }
        activeCount = 0;
    }

    #endregion

}
