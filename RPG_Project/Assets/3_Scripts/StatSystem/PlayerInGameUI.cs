using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInGameUI : MonoBehaviour
{
    public PlayerManager player;
    public PlayerStats playerStats;

    public TextMeshProUGUI levelText;
    public Image hpSlider;
    public Image apSlider;


    private void Start()
    {
        //levelText.text = statsObject.level.ToString();
        hpSlider.fillAmount = playerStats.currentHealth;
        apSlider.fillAmount = playerStats.currentStamina;
    }

    private void OnEnable()
    {
        player.OnChangedStats += OnChangedStats;
    }

    private void OnDisable()
    {
        player.OnChangedStats -= OnChangedStats;
    }

    private void OnChangedStats()
    {
        hpSlider.fillAmount = playerStats.HealthPercentage;
        apSlider.fillAmount = playerStats.StaminaPercentage;
    }
}
