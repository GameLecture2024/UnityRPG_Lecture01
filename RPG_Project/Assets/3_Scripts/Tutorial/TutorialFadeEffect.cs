using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFadeEffect : TutorialBase
{
    [SerializeField] FadeEffect fadeEffect;        // Ư�� �̹����� alpha 0 ~ 1 ��ȯ���ִ� �ڵ�, ���� ������ ���� ��� ȣ��
    [SerializeField] bool isFadeIn = true;         // true�� FadeIn, false�� FadeOut
    private bool isCompleted = false;              // ���� �ڷ�ƾ�� �������� Ȯ���ϴ� bool
    public override void Enter()
    {
        // �ڷ�ƾ�� �Ϸ�ǰ� ����, ���� Ʃ�丮���� ���� �ؾ��մϴ�.
        // �̸� ���� �ϱ� ���� delegate ȣ���ϵ��� ������ �Ұ̴ϴ�.

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
