using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONLoader
{
    public static string LoadData(string jsonName)
    {
        TextAsset jText = Resources.Load<TextAsset>($"JsonData/{jsonName}");

        if (jText != null)
        {
            string jString = jText.ToString();
            return jString;
        }
        else
        {
            Debug.LogError("Json 경로가 잘못 지정되었습니다.");
            return string.Empty;
        }
    }
}
