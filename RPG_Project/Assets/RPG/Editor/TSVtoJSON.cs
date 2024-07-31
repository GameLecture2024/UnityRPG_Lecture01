using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PlasticGui.Configuration;
using System.IO;
using System;

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
            // TSV�� JSON���� �Ľ��ϴ� ��� ����
            if (_tsvCopyValue.EndsWith(Environment.NewLine))  // ������ �������� ������ �ٿ� ���ο� ������ ���� ���
            {
                _tsvCopyValue = _tsvCopyValue.Substring(0, _tsvCopyValue.Length - 1);  // ������ �迭 ���� ���ش�.
            }

            Parse(_tsvCopyValue);
            _tsvCopyValue = "";
            Repaint();
        }

        void Parse(string tsv)
        {
            string json = TsvtoJsonUtility.ParseTsv(_tsvCopyValue, _ParseNumber); // json��ȯ�� ���� Utility Class �ϳ� ������ �ؾ� �մϴ�.

            if(!string.IsNullOrEmpty(json))
            {
                _savePath = EditorUtility.SaveFilePanel("Save Json", Application.dataPath, "default Name", "json");
                
                File.WriteAllText(_savePath ,json);
                AssetDatabase.Refresh();

                EditorUtility.DisplayDialog("Complete", "��ȯ�� �Ϸ�Ǿ����ϴ�.\n" + _savePath, "Ok");
            }
            else
            {
                 EditorUtility.DisplayDialog("Fail", "��ȯ�� �����߽��ϴ�.\n" + _savePath, "OK");
            }

        }
    }
}
