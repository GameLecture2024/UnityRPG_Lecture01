using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class UI_VolumeSlider : MonoBehaviour
{
    // UI 슬라이어의 Value와 AudioMixer Parameter를 연결시켜주기 위한 클래스

    public Slider slider;
    public string parameter;           // AudioMixer에서 파라미터의 이름이 여러가지가 있기 때문에, 이를 매칭해줄 문자열 변수를 저장.

    [SerializeField] private AudioMixer audioMixer;       // AudioMixer를 사용하기 위한 변수
    [SerializeField] private float multiplier;            // 슬라이더 값의 크기를 제어하는 변수
    
    public void SliderValue(float _value)
    {
        audioMixer.SetFloat(parameter, Mathf.Log10(_value) * multiplier);  // Mathf.Log10 : 정수 보다는 작게, 유연하게 값의 변화를 표현
    }

    public void LoadSlider(float _value)
    {
        if(_value >= 0.001f)                     // min값인 0.001f 작은 값은 불러올 수 없도록.
            slider.value = _value;
    }
}
