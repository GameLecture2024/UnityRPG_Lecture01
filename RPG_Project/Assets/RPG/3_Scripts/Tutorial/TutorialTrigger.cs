using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : TutorialBase
{
    [SerializeField] PlayerManager player;

    [SerializeField] private Transform missionUI;
    
    public bool isTrigger = false;                     // Trigger가 활성화 되면 SetNextTutorial 실행
    [SerializeField] Transform targetTrigger;          // 타겟트리거 TutorialTrigger의 isTrigger를 True 변환시켜줄 target 지정
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
            // missionUI를 미션 완료 상태로 변화시켜주는 코드 작성
        }
    }

    public override void Exit()
    {
      
    }
}
