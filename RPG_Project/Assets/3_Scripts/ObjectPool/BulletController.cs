using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolTest
{
	public class BulletController : MonoBehaviour
	{
		[SerializeField] private GameObject projectile;

        private ObjectPool objectPool;
        public KeyCode bulletSpawnKey;  // ���ϴ� ���� Ű �Ҵ�

        private void Awake()
        {
            objectPool = new ObjectPool(projectile); // 5���� ������ ���� �� ��Ȱ��ȭ
        }

        private void Update()
        {
            if (Input.GetKey(bulletSpawnKey)) // " " Ű �Է� ��
            {
                //GameObject clone = Instantiate(projectile, transform.position, Quaternion.identity);
                // ������ �� -> ActivatePoolitem�̿��ؼ� �̸� ����� �� ������Ʈ Ǯ ���
                GameObject clone = objectPool.ActivatePoolItem();

                clone.GetComponent<Projectile>().SetUp(objectPool);
            }

            if (Input.GetKeyDown(KeyCode.A)) // AŰ �Է� �� ��ü ������Ʈ ��Ȱ��ȭ
            {
                objectPool.DeActivateAllPoolItem();
            }
        }
    } 
}
