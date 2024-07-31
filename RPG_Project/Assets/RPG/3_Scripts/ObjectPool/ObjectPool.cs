using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    // ������Ʈ Ǯ��, ����ؾ� �ϴ� ����?
    // ����Ƽ ���� ������Ʈ ���� �� �ı��� �� GC�� ���� �޸� ������ �߻��ȴ�.
    // ���� �ı��Ǵ� ���� ������Ʈ ( �Ѿ�, �뷮�� ���� �ı� ��, ����Ʈ) ������Ʈ Ǯ�� ������� �޸� ���� ����.

    // �ı��� ��.. ���ҽ� ���� �߻� -> ���� ����� ��, ���� ����� �� 1���� ����.
    // ������ ������Ʈ ���� �÷��ǿ� �����ϰ�, �ʿ��� �� ���� ������ ������Ʈ�� Ȱ��ȭ
    // �ʿ� ���� ������Ʈ�� ��Ȱ��ȭ �ϴ� ����� ����մϴ�

    // ���� ������Ʈ Ǯ��, ���� ������Ʈ Ǯ��
    // ���� : �ѹ� ������Ʈ Ǯ���� �����͸� ���� �� �� ���� ������ ��
    // ���� : ���ʿ� ������Ʈ Ǯ�� �� Ȱ��ȭ�� �����Ͱ� �����ϸ� �� �� ���� ���ο� ������Ʈ�� �߰����ִ� ���

    public int maxCount = int.MaxValue; // ������Ʈ Ǯ���� �ִ�ġ ����
    private int allocateCount;          // ���(Ȱ��ȭ) ������ ����
    private int activeCount;            // ���� Ȱ��ȭ ���� ����
    private int increaseCount = 5;      // Ǯ���� ������Ʈ ���� ������ �� �߰��� �������� ��

    private GameObject poolObject;
    private List<GameObject> poolObjectList;     // ���� Ȱ��ȭ�� ������Ʈ + ��Ȱ���� ������Ʈ
    // private Stack<GameObject> PushedObject;   // ��Ȱ��ȭ�� ������Ʈ ����
    


    private static Transform Container;     // ������ Instant���� ������ �����̳� ���ӿ�����Ʈ ��ġ

    public ObjectPool() { } // �⺻ ������

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

        // increaseCount �� ��ŭ ������Ʈ�� ����
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

    // �� ���� �Ǵ� ���� ���� �ÿ� �ѹ��� �����Ѵ�.
    public void DestroyObject() // List�� �ִ� ��� ������Ʈ�� �ı��ϰ� ����Ʈ Clear
    {
        if(poolObjectList == null)
        {
            Debug.Log("������ ������Ʈ Ǯ�� �����ϴ�.");
            return;
        }

        for(int i=0; i<poolObjectList.Count; i++)
        {
            GameObject.Destroy(poolObjectList[i].gameObject);
        }

        poolObjectList.Clear();
    }

    public GameObject ActivatePoolItem() // ��Ȱ��ȭ�� ������Ʈ�� Ȱ��ȭ �����ش�.
    {
        if (poolObjectList == null) return null;

        // ��� ������ ���ڿ� Ȱ��ȭ�� ���� ���� ��
        // ������Ʈ ���� Ǯ���� ��
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

    public void DeActivatePoolItem(GameObject removeObject) // ������Ʈ�� �ı��Ǵ� ��� ��Ȱ��ȭ �����ش�.
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
    public void DeActivateAllPoolItem() // ��� ������Ʈ�� ��Ȱ��ȭ �����ִ� �Լ�.
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
