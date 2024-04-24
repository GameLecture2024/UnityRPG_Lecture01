using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTest : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private IEnumerator Start()
    {
        FadeEffect fadeEffect = fadeImage.GetComponent<FadeEffect>();
        fadeEffect.OnFade(FadeState.FadeOut);

        yield return new WaitForSeconds(3f);
        ShakeCamera.Instance.OnShakeCamera(1,0.1f,false);

        yield return new WaitForSeconds(3f);
        fadeEffect.OnFade(FadeState.FadeIn);
    }
}
