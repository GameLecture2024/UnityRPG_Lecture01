using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMove : TutorialBase
{
    [SerializeField] RectTransform rectTransform;       // UI의 Transform 이동 구혀
    [SerializeField] Vector3 targetPos;                 // UI가 최종 목적지
    [SerializeField] private float moveTime = 0.5f;     // UI가 이동할 시간
    private bool isCompleted = false;


    public override void Enter()
    {
        ShakeCamera.Instance.OnShakeCamera();
        StartCoroutine(UILerpMove());
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted) controller.SetNextTutorial();
    }

    public override void Exit()
    {
       
    }

    IEnumerator UILerpMove()
    {
        float currentTime = 0;
        float percent = 0;

        Vector3 startPos = rectTransform.anchoredPosition;   // 시작 위치 , target 위치, percent => Lerp

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / moveTime;                // currentTime이 moveTime이 되면 종료

            rectTransform.anchoredPosition = Vector3.Lerp(startPos, targetPos, percent);

            yield return null;
        }

        isCompleted = true;
    }
}
