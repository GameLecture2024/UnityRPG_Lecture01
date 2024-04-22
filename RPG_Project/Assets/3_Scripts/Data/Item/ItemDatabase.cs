using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName ="Data/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public ItemObject[] itemObjects;
}
