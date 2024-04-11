using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayUIManager : MonoBehaviour
{
    // 싱글톤 매니저

    [SerializeField] private GameObject UIGameObject;

    private bool isOpen = false; // True이면 UIManager를 호출하고, false이면 닫는다.


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC 버튼을 눌렀을 때 Setting 화면을 호출하고 닫는 코드
        {
            isOpen = !isOpen;
            CallSettingUI(isOpen);
        }
    }

    private void CallSettingUI(bool isOpen)
    {
        UIGameObject.SetActive(isOpen);
    }

    public void CloseUIMenu()
    {
        UIGameObject.SetActive(false);
    }

    public void ReturnToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
