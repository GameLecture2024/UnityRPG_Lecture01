using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumUtil<T>
{
   // Enum 데이터 타입의 string data를 반환하는 유틸리티 클래스 
    public static T Parse(string s)
    {
        return (T)Enum.Parse(typeof(T), s);
    }
}