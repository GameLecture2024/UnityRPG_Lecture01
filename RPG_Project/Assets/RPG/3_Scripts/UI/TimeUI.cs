using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour, ISaveManager
{
    [SerializeField] TMP_Text timeText;         // 시간을 표시할 텍스트

    private float timeValue;                    // 시간 증가를 저장할 변수  Time.deltaTime 매초 증가  
    private int min;                            // 분
    private int sec;                            // 초


    private void Update()
    {
        // 시간을 증가시키고 text에 출력시키는 기능
        SetTimeUI();
    }

    private void SetTimeUI()
    {
        timeValue += Time.deltaTime;

        min = (int)timeValue / 60;
        sec = ((int)(timeValue - min) % 60);

        timeText.text = string.Format("{0:D2} : {1:D2}", min, sec);
    }

    public void LoadData(GameData gameData)
    {
        timeValue = gameData.timeValue;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.timeValue = timeValue;
    }
}
