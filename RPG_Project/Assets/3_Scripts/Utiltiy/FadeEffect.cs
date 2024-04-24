using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum FadeState { FadeIn = 0, FadeOut = 1, FadeInOut, FadeLoop }

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private float fadeTime;               // fade가 진행될 시간
    [SerializeField] private AnimationCurve fadeCurve;     // percent의 변화를 그래프로 적용할 수 있다.
    public FadeState fadeState;
    private Image image => GetComponent<Image>();          // Ui.Image 오브젝트 위에 클래스 컴포넌트로 존재해야한다.

    private void Start()
    {
        // UI에 있는 Image에 접근해서. Color 접근 알파 값을 0으로 만들면 된다. 1로 만들면 된다.
        // 코루틴 실행, 함수 실행 
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
                timevalue += Time.deltaTime;               // 직관적으로 시간이 더해지는 변수
                percent = timevalue / fadeTime;            // 0 ~ 1 사이 값을 갖고, fade의 Lerp 시간을 결정하는 percent 값

                Color color = image.color;
                color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));  // 애니메이션 커브에 따른 페이드 효과 구현
                image.color = color;

                yield return null;
            }
        }

        IEnumerator FadeInOut()
        {
            while (true)
            {
                yield return StartCoroutine(Fade(1, 0)); // 페이드 인

                yield return StartCoroutine(Fade(0, 1)); // 페이드 아웃

                if(fadeState == FadeState.FadeInOut)
                {
                    break;
                }
            }
        }

    }
}