using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDialogueData : SingletonMonoBehaviour<WorldDialogueData>
{
    public string jsonName;

    // 외부 클래스에서 이 클래스의 데이터를 참조하기 위한 콜렉션 : Dictionary
    public Dictionary<int, List<DialogueSystem.DialogData>> dialogueDatabase 
        = new Dictionary<int, List<DialogueSystem.DialogData>>();

    protected override void Awake()
    {
        base.Awake();  // SingletonMonoBehaviour의 Awake함수 실행
        LoadJson(); 
    }

    void LoadJson()
    {
        string jsonString = JSONLoader.LoadData(jsonName);

        JArray jArr = JArray.Parse(jsonString);

        int IndexID = 0;

        foreach (JObject jObj in jArr)
        {           
            DialogueSystem.DialogData dialogData =
            JsonConvert.DeserializeObject<DialogueSystem.DialogData>(jObj.ToString());

            // 1. 현재 번호 0 == 0(인덱스 번호) 일치 한다.
            if (IndexID == int.Parse((string)jObj["IndexNumber"])) 
            {
                // Database Key값에 해당 하는 List 메모리가 할당되어 있지 않다면 할당해줘라.
                if (!dialogueDatabase.ContainsKey(IndexID))
                {
                    dialogueDatabase[IndexID] = new List<DialogueSystem.DialogData>();
                }

                dialogueDatabase[IndexID].Add(dialogData);
            }
            else
            {
                // 현재 Index번호와 Excel의 Index번호가 다르다면
                IndexID = int.Parse((string)jObj["IndexNumber"]);
                dialogueDatabase[IndexID] = new List<DialogueSystem.DialogData>
                { 
                    dialogData 
                };                
            }
        }

    }
}
