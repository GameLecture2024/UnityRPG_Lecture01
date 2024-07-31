using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolTest
{
    public class Projectile : MonoBehaviour
    {
        private ObjectPool objectPool; // ObjectPool Data µî·Ï

        private void Awake()
        {

        }

        public void SetUp(ObjectPool objectPool)
        {
            this.objectPool = objectPool;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DestoryZone"))
            {
                //Destroy(gameObject);
                objectPool.DeActivatePoolItem(gameObject);
            }
        }
    } 
}
