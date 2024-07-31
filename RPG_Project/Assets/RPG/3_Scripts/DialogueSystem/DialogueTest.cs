using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour
{
    // 1-1. Resources ��ο� ����Ǿ� �ִ� JsonData�� �ҷ����� �ǽ�.
    // 1-2. String data�� �̹����� �ε��ϴ� ���

    // 2. Json�� GameData�� ��ȯ �� DialoguSystem�� dialogue Data�� Deserialize(������ȭ)�Ͽ� ����غ���

    // Start is called before the first frame update

    public Image textUI;
    public string jsonName;

    void Start()
    {
        //Resources.Load�Լ��� Assets/Resources ��α��� ����.. ()�ȿ� Resources �� ���� ���� �Ǵ� ���� �̸� ��θ� �������ָ� �˴ϴ�.

        TextAsset jsonData = Resources.Load<TextAsset>("JsonData/Dialogue");
        // JsonData -> Unity Load �Լ��� ����� �Ѵ�.

        Sprite sprite = Resources.Load<Sprite>("UI/G02B_sword");
        textUI.sprite = sprite;

        //GameObject zombie = Resources.Load<GameObject>("Prefabs/Monster/Zombie");
        //
        //if(null == zombie as GameObject)
        //{
        //    Debug.LogError("��ΰ� �߸��Ǿ����ϴ�.");
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

            Debug.Log($"���� ī��Ʈ{count} : {dialogData.name}, {dialogData.dialogue}");
        }

    }
}
