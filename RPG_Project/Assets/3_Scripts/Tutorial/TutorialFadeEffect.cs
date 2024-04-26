using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFadeEffect : TutorialBase
{
    [SerializeField] FadeEffect fadeEffect;        // 특정 이미지의 alpha 0 ~ 1 변환해주는 코드, 막을 내리고 여는 기능 호출
    [SerializeField] bool isFadeIn = true;         // true면 FadeIn, false면 FadeOut
    private bool isCompleted = false;              // 현재 코루틴이 종료됬는지 확인하는 bool
    public override void Enter()
    {
        // 코루틴이 완료되고 나서, 다음 튜토리얼을 실행 해야합니다.
        // 이를 구현 하기 위해 delegate 호출하도록 구현을 할겁니다.

        if (isFadeIn)
        {
            fadeEffect.OnFade(FadeState.FadeIn, OnAfterFadeEffect);
            isCompleted = true;
        }
        else
        {
            fadeEffect.OnFade(FadeState.FadeOut, OnAfterFadeEffect);
            isCompleted = true;
        }
    }

    private void OnAfterFadeEffect()
    {
        isCompleted = true;
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        
    }
}
