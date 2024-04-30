using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // JSON의 경로를 받아와서 프리팹을 생성하는 것
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
        // Resources<GameObject>(경로) 
        // Instantiate 함수를 사용해서 키 코드 눌렀을 때 몬스터를 생성하는 코드

        //GameObject asset = Resources.Load<GameObject>($"{MonsterPath + MonsterName}");
        //GameObject monsterPrefab = Instantiate(asset) as GameObject;


        // 생성하는 코드를 ObjectPool로 저장해서 불러오는 것 까지
        // 임시 코드를 작성해서 파괴까지 한번 구현해보는 것
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
