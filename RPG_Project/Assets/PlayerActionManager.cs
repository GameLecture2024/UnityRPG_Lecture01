using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어의 공격 로직 구현하는 스크립트, Awake와 Update를 하나의 스크립트에서 모아서 처리하도록 개선 필요
/// </summary>
public class PlayerActionManager : MonoBehaviour
{
    PlayerManager player;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        HandleComboAttack();
        HandleActionInput();
    }


    private void HandleActionInput()
    {
        if (player.isPerformingAction) return;

        if (Input.GetMouseButtonDown(0))
        {
            HandleAttackAction();
        }
    }

    private void HandleAttackAction()
    {
        player.playerAnimationManager.PlayerTargetActionAnimation("ATK0", true);
        player.canCombo = true;                                                    // canCombo True일 대만 콤보 어택을 할 수 있게 제어 변수 선언
    }

    private void HandleComboAttack()
    {
        if (!player.canCombo) return; // 예외 사항 처리

        // 콤보 어택을 사용할 입력 키 설정

        if (Input.GetMouseButtonDown(0))
        {
            player.anim.SetTrigger("doAttack");
        }
    }
}
