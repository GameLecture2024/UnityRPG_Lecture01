using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName ="Data/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public ItemObject[] itemObjects;

    public void OnValidate()
    {
        // 인스팩터 창에서 변경사항이 발생했을 때 이 함수를 실행한다.
        for(int i =0; i< itemObjects.Length; i++)
        {
            itemObjects[i].data.id = i;
        }
    }
}
