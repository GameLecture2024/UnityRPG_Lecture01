using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager 
{
    // 인터페이스를 사용하는 목적
    // 1. 저장이 필요한 클래스를 ISaveManager를 상속시켜 한 번에 관리할 수 있다.
    // 2. 같은 이름의 함수를 사용하여 각 클래스에서 다른 기능으로 데이터를 저장 또는 로드 할 수 있다.


    public void SaveData(ref GameData gameData);
    public void LoadData(GameData gameData);
}
