using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumUtil<T>
{
   // Enum ������ Ÿ���� string data�� ��ȯ�ϴ� ��ƿ��Ƽ Ŭ���� 
    public static T Parse(string s)
    {
        return (T)Enum.Parse(typeof(T), s);
    }
}