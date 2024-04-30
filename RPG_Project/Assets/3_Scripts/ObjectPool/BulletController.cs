using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolTest
{
	public class BulletController : MonoBehaviour
	{
		[SerializeField] private GameObject projectile;

        private ObjectPool objectPool;
        public KeyCode bulletSpawnKey;  // 원하는 생성 키 할당

        private void Awake()
        {
            objectPool = new ObjectPool(projectile); // 5개의 프리팹 생성 후 비활성화
        }

        private void Update()
        {
            if (Input.GetKey(bulletSpawnKey)) // " " 키 입력 시
            {
                //GameObject clone = Instantiate(projectile, transform.position, Quaternion.identity);
                // 생성할 때 -> ActivatePoolitem이용해서 미리 만들어 둔 오브젝트 풀 사용
                GameObject clone = objectPool.ActivatePoolItem();

                clone.GetComponent<Projectile>().SetUp(objectPool);
            }

            if (Input.GetKeyDown(KeyCode.A)) // A키 입력 시 전체 오브젝트 비활성화
            {
                objectPool.DeActivateAllPoolItem();
            }
        }
    } 
}
