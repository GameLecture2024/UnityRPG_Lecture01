using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Helmet = 0,
    Boots,
    Weapon,
    Default
}


[CreateAssetMenu(fileName = "New Item", menuName = "Data/New Item")]
public class ItemObject : ScriptableObject
{
    public ItemType type;             // 장비 아이템 구분
    public bool stackable;            // 중첩 가능 여부

    public Sprite icon;               // 인벤토리 아이템 아이콘
    public GameObject modelPrefab;    // 모델 프리팹 데이터

    public Item data = new Item();
}
