using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour
{
    // 1-1. Resources 경로에 저장되어 있는 JsonData를 불러오는 실습.
    // 1-2. String data로 이미지를 로드하는 방법

    // 2. Json을 GameData로 변환 후 DialoguSystem의 dialogue Data로 Deserialize(역직렬화)하여 사용해보기

    // Start is called before the first frame update

    public Image textUI;
    public string jsonName;

    void Start()
    {
        //Resources.Load함수는 Assets/Resources 경로까지 포함.. ()안에 Resources 이 후의 폴더 또는 파일 이름 경로를 지정해주면 됩니다.

        TextAsset jsonData = Resources.Load<TextAsset>("JsonData/Dialogue");
        // JsonData -> Unity Load 함수를 사용을 한다.

        Sprite sprite = Resources.Load<Sprite>("UI/G02B_sword");
        textUI.sprite = sprite;

        //GameObject zombie = Resources.Load<GameObject>("Prefabs/Monster/Zombie");
        //
        //if(null == zombie as GameObject)
        //{
        //    Debug.LogError("경로가 잘못되었습니다.");
        //}
        //else
        //{
        //    Instantiate(zombie);
        //}

        LoadJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadJson()
    {
        string jsonString = JSONLoader.LoadData(jsonName);

        JArray jArr = JArray.Parse(jsonString);

        int count = 0;

        foreach(JObject jObj in jArr)
        {
            count++;
            DialogueSystem.DialogData dialogData = 
            JsonConvert.DeserializeObject<DialogueSystem.DialogData>(jObj.ToString());

            Debug.Log($"현재 카운트{count} : {dialogData.name}, {dialogData.dialogue}");
        }

    }
}
