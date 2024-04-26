using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : TutorialBase
{
    [SerializeField] PlayerManager player;

    [SerializeField] private Transform missionUI;
    
    public bool isTrigger = false;                     // Trigger�� Ȱ��ȭ �Ǹ� SetNextTutorial ����
    [SerializeField] Transform targetTrigger;          // Ÿ��Ʈ���� TutorialTrigger�� isTrigger�� True ��ȯ������ target ����
    public override void Enter()
    {
        player.isPerformingAction = false;
        player.canMove = true;

        missionUI.gameObject.SetActive(true);
    }

    public override void Execute(TutorialController controller)
    {
        if (isTrigger)
        {
            controller.SetNextTutorial();
            missionUI.gameObject.SetActive(false);
            // missionUI�� �̼� �Ϸ� ���·� ��ȭ�����ִ� �ڵ� �ۼ�
        }
    }

    public override void Exit()
    {
      
    }
}
