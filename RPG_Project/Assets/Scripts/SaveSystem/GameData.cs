using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    [Header("Player Data")]
    // 플레이어의 위치 정보를 x,y,z 값으로 분할하여 저장
    public float x;
    public float y;
    public float z;

    public float timeValue;  // 플레이 타임을 저장하는 변수

    public string playerName; // 플레이어의 이름을 저장하는 변수

    public SerializableDictionary<string, float> volumeSettings; // 볼륨 설정을 Dictionary로 저장
}
