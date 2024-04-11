using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;


public enum SaveGameSlot
{
    _00,
    _01,
    _02,
    _03,
    No_Slot
}

public class SaveSlot : MonoBehaviour
{

    [Header("저장 슬롯 정보")]
    public SaveGameSlot saveGameSlot;
    [SerializeField] TMP_Text playername;
    [SerializeField] TMP_Text playTime;
    private string userName;

    [Header("플레이 시간 정보")]
    private float timeValue;
    private int min;
    private int sec;

    [Header("데이터 핸들러")]
    private DataHandler dataHandler;
    private GameData gameData;

    private void OnEnable()
    {
        // 로드 버튼 그룹이 활성화 됬을 때 실행 시키는 함수
        // 데이터 핸들러를 사용하여 외부에 저장된 파일이 존재하면 데이터를 갱신하고, 그렇지 않으면 기본 값을 출력한다.
        LoadSavedSlotData();
    }

    private void LoadSavedSlotData()   // Slot의 데이터 정보를 읽고 출력한다.
    {
        if (!SaveManager.Instance) return;

        string mySlotName = SaveManager.Instance.originName + saveGameSlot.ToString() + ".txt";
        dataHandler = new DataHandler(Application.persistentDataPath, mySlotName);        // SaveGameSlot 열거형 값에 따라 저장되는 파일 이름이 변경된다.

        if (dataHandler.CheckFileExists(Application.persistentDataPath, mySlotName))
        {
            // 해당 데이터에 있는 gameData를 gameData에 저장하고 데이터를 적용시켜 준다.
            gameData = dataHandler.DataLoad();
            LoadData();
        }
    }

    public void LoadGameData()
    {
        SaveManager.Instance.ChangeSaveFileNameBySelectSlot(saveGameSlot);

        // 씬을 로드 하는 기능
        LoadingUI.LoadScene("CameraSettingScene");
    }

    public void DeleteGameData()
    {
        dataHandler.DataDelete();

        playername.text = "No Data";
        playTime.text = "00 : 00";

        gameObject.SetActive(false);
    }

    private void LoadData()
    {
        playername.text = gameData.playerName;
        if(userName == "")
        {
            playername.text = "이름이 없음";
        }

        timeValue = gameData.timeValue;

        min = (int)timeValue / 60;
        sec = ((int)(timeValue - min) % 60);

        playTime.text = string.Format("{0:D2} : {1:D2}", min, sec);
    }


    private void Reset()
    {
        playername = transform.Find("CharacterName").GetComponent<TMP_Text>();
        playTime = transform.Find("PlayTimeText").GetComponent<TMP_Text>();
    }
}
