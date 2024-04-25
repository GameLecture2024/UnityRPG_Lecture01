using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTest : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    public DialogueSystem dialogueSystem1;
    public DialogueSystem dialogueSystem2;

    private IEnumerator Start()
    {
        FadeEffect fadeEffect = fadeImage.GetComponent<FadeEffect>();
        fadeEffect.OnFade(FadeState.FadeOut);

        yield return new WaitForSeconds(3f);

        yield return new WaitUntil( () => dialogueSystem1.UpdateDialog());

        yield return new WaitForSeconds(1f);
        ShakeCamera.Instance.OnShakeCamera(1,0.1f,false);

        yield return new WaitUntil(()=> dialogueSystem2.UpdateDialog());

        fadeEffect.OnFade(FadeState.FadeIn);
    }
}
