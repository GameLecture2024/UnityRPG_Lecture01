using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDialogueData : SingletonMonoBehaviour<WorldDialogueData>
{
    public string jsonName;

    // �ܺ� Ŭ�������� �� Ŭ������ �����͸� �����ϱ� ���� �ݷ��� : Dictionary
    public Dictionary<int, List<DialogueSystem.DialogData>> dialogueDatabase 
        = new Dictionary<int, List<DialogueSystem.DialogData>>();

    protected override void Awake()
    {
        base.Awake();  // SingletonMonoBehaviour�� Awake�Լ� ����
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

            // 1. ���� ��ȣ 0 == 0(�ε��� ��ȣ) ��ġ �Ѵ�.
            if (IndexID == int.Parse((string)jObj["IndexNumber"])) 
            {
                // Database Key���� �ش� �ϴ� List �޸𸮰� �Ҵ�Ǿ� ���� �ʴٸ� �Ҵ������.
                if (!dialogueDatabase.ContainsKey(IndexID))
                {
                    dialogueDatabase[IndexID] = new List<DialogueSystem.DialogData>();
                }

                dialogueDatabase[IndexID].Add(dialogData);
            }
            else
            {
                // ���� Index��ȣ�� Excel�� Index��ȣ�� �ٸ��ٸ�
                IndexID = int.Parse((string)jObj["IndexNumber"]);
                dialogueDatabase[IndexID] = new List<DialogueSystem.DialogData>
                { 
                    dialogData 
                };                
            }
        }

    }
}
