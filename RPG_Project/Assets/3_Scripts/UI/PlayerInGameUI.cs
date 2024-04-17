using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// player의 HP, Mp, Stamina 등의 UI변경 됬을 때 적용시켜주는 클래스
/// </summary>
public class PlayerInGameUI : MonoBehaviour
{
    public PlayerManager player;

    public Image hpSlider;
    public Image mpSlider;

    public GameObject gameOverUI;


    public void InitializeSlider()
    {
        hpSlider.fillAmount = (float)player.HP / (float)player.MAXHP;
        mpSlider.fillAmount = (float)player.MP / (float)player.MAXMP;
    }


    private void OnEnable()
    {
        player.OnChangedStats += OnChangedValue;
    }

    private void OnDisable()
    {
        player.OnChangedStats -= OnChangedValue;
    }

    private void OnChangedValue()
    {
        hpSlider.fillAmount = (float)player.HP / (float)player.MAXHP;
        mpSlider.fillAmount = (float)player.MP / (float)player.MAXMP;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }
}
