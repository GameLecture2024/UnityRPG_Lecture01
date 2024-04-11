using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TitleUIManager : MonoBehaviour
{

    public void GameStart()
    {
        // 저장 Application.persistantDataPath 경로에 데이터를 List화 해서. maxCount 지정을 해줘야 합니다.

        // 00 ~ 마지막 번호 탐색을. 비어 있는 Slot이 있으면, 해당 Slot 버튼의 이름으로 변경한다.

        LoadingUI.LoadScene("CameraSettingScene");
    }

    
    public void GameQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
