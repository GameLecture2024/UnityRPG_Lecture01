using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum FadeState { FadeIn = 0, FadeOut = 1, FadeInOut, FadeLoop }

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private float fadeTime;               // fade�� ����� �ð�
    [SerializeField] private AnimationCurve fadeCurve;     // percent�� ��ȭ�� �׷����� ������ �� �ִ�.
    public FadeState fadeState;
    private Image image => GetComponent<Image>();          // Ui.Image ������Ʈ ���� Ŭ���� ������Ʈ�� �����ؾ��Ѵ�.

    private void Start()
    {
        // UI�� �ִ� Image�� �����ؼ�. Color ���� ���� ���� 0���� ����� �ȴ�. 1�� ����� �ȴ�.
        // �ڷ�ƾ ����, �Լ� ���� 
        OnFade(fadeState);
    }

    public void OnFade(FadeState state)
    {
        fadeState = state;

        switch (fadeState)
        {
            case FadeState.FadeIn:
                StartCoroutine(Fade(1, 0));
                break;
            case FadeState.FadeOut:
                StartCoroutine(Fade(0, 1));
                break;
            case FadeState.FadeInOut:
            case FadeState.FadeLoop:
                StartCoroutine(FadeInOut());
                break;
        }

        IEnumerator Fade(float start, float end)
        {
            float timevalue = 0.0f;
            float percent = 0.0f;

            while (percent < 1)
            {
                timevalue += Time.deltaTime;               // ���������� �ð��� �������� ����
                percent = timevalue / fadeTime;            // 0 ~ 1 ���� ���� ����, fade�� Lerp �ð��� �����ϴ� percent ��

                Color color = image.color;
                color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));  // �ִϸ��̼� Ŀ�꿡 ���� ���̵� ȿ�� ����
                image.color = color;

                yield return null;
            }
        }

        IEnumerator FadeInOut()
        {
            while (true)
            {
                yield return StartCoroutine(Fade(1, 0)); // ���̵� ��

                yield return StartCoroutine(Fade(0, 1)); // ���̵� �ƿ�

                if(fadeState == FadeState.FadeInOut)
                {
                    break;
                }
            }
        }

    }
}