using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �÷��̾��� ���� ���� �����ϴ� ��ũ��Ʈ, Awake�� Update�� �ϳ��� ��ũ��Ʈ���� ��Ƽ� ó���ϵ��� ���� �ʿ�
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
        player.canCombo = true;                                                    // canCombo True�� �븸 �޺� ������ �� �� �ְ� ���� ���� ����
    }

    private void HandleComboAttack()
    {
        if (!player.canCombo) return; // ���� ���� ó��

        // �޺� ������ ����� �Է� Ű ����

        if (Input.GetMouseButtonDown(0))
        {
            player.anim.SetTrigger("doAttack");
        }
    }
}
