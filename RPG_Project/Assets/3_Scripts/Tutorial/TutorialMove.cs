using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMove : TutorialBase
{
    [SerializeField] RectTransform rectTransform;       // UI�� Transform �̵� ����
    [SerializeField] Vector3 targetPos;                 // UI�� ���� ������
    [SerializeField] private float moveTime = 0.5f;     // UI�� �̵��� �ð�
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

        Vector3 startPos = rectTransform.anchoredPosition;   // ���� ��ġ , target ��ġ, percent => Lerp

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / moveTime;                // currentTime�� moveTime�� �Ǹ� ����

            rectTransform.anchoredPosition = Vector3.Lerp(startPos, targetPos, percent);

            yield return null;
        }

        isCompleted = true;
    }
}
