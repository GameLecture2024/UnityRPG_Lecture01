using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBattleUI : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    /// <summary>
    /// Slider를 체력 바로 사용하기 위해서 최소, 최대, 현재 Value를 반환해줘야 한다.
    /// </summary>
    #region 프로퍼티
    public float MinValue
    {
        get => hpSlider.minValue;
        set => hpSlider.minValue = value;
    }

    public float MaxValue
    {
        get => hpSlider.maxValue;
        set => hpSlider.maxValue = value;
    }

    public float Value
    {
        get => hpSlider.value;
        set => hpSlider.value = value;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
