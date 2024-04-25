using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PlasticGui.Configuration;
using System.IO;

public class TSVtoJSONWindow : EditorWindow
{
    [MenuItem("Tools/JsonUtility/TSVtoJSONWindow")]
    public static void CreateWindow()
    {
        GetWindow<TSVtoJSONWindow>("Tsv to Json");
    }

    bool _ParseNumber = false;

    string _savePath = "";    
    string _tsvCopyValue;
    Vector2 _ScrollPos;

    private void OnGUI() 
    {
        _ParseNumber = EditorGUILayout.Toggle("Parse Number", _ParseNumber);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Tsv Text", EditorStyles.boldLabel);
        _ScrollPos = EditorGUILayout.BeginScrollView(_ScrollPos);
        _tsvCopyValue = EditorGUILayout.TextArea(_tsvCopyValue, GUILayout.MinHeight(300));
        EditorGUILayout.EndScrollView();

        if(GUILayout.Button("Parse", GUILayout.Height(50)))
        {
            // TSV를 JSON으로 파싱하는 기능 구현
            Parse(_tsvCopyValue);
            _tsvCopyValue = "";
            Repaint();
        }

        void Parse(string tsv)
        {
            string json = ""; // json변환을 위한 Utility Class 하나 구현을 해야 합니다.

            if(!string.IsNullOrEmpty(json))
            {
                _savePath = EditorUtility.SaveFilePanel("Save Json", Application.dataPath, "default Name", "json");
                
                File.WriteAllText(_savePath ,json);
                AssetDatabase.Refresh();

                EditorUtility.DisplayDialog("Complete", "변환이 완료되었습니다.\n" + _savePath, "Ok");
            }
            else
            {
                 EditorUtility.DisplayDialog("Fail", "변환이 실패했습니다.\n" + _savePath, "OK");
            }

        }
    }
}
