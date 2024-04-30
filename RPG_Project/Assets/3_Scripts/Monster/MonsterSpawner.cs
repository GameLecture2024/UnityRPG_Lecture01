using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // JSON�� ��θ� �޾ƿͼ� �������� �����ϴ� ��
    public string MonsterName;
    private string MonsterPath = "Prefabs/Monster/";

    private ObjectPool objectPool;

    private void Awake()
    {
        GameObject asset = Resources.Load<GameObject>($"{MonsterPath + MonsterName}");
        objectPool = new ObjectPool(asset);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Resources<GameObject>(���) 
        // Instantiate �Լ��� ����ؼ� Ű �ڵ� ������ �� ���͸� �����ϴ� �ڵ�

        //GameObject asset = Resources.Load<GameObject>($"{MonsterPath + MonsterName}");
        //GameObject monsterPrefab = Instantiate(asset) as GameObject;


        // �����ϴ� �ڵ带 ObjectPool�� �����ؼ� �ҷ����� �� ����
        // �ӽ� �ڵ带 �ۼ��ؼ� �ı����� �ѹ� �����غ��� ��
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newObject = objectPool.ActivatePoolItem();

            //Destroy(newObject, 2f);
            StartCoroutine(DeActivateMonster(newObject));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            objectPool.DeActivateAllPoolItem();
        }
    }

    IEnumerator DeActivateMonster(GameObject newObject)
    {
        yield return new WaitForSeconds(2f);
        objectPool.DeActivatePoolItem(newObject);
    }
}
