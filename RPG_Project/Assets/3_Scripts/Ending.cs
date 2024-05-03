using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public static Ending Instance;

    [Header("Step 1")]
    public GameObject[] endingTexts;
    public float textDelay = 0.2f;

    [Header("Step 2")]
    public GameObject stepTwoObject;
    public TextMeshProUGUI stepTwoText;
    public string context = "Made By Kim";
    public float textMessageDelay = 0.15f;

    [Header("Credit")]
    public GameObject credit;

    [Header("FadeEffect")]
    public FadeEffect fadeEffect;

    public AudioClip bgm;
    public AudioSource audioSource;
    public Animator animator;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameObject.SetActive(false);
    }

    public void PlayEndingScene()
    {
        gameObject.SetActive(true);
        audioSource.PlayOneShot(bgm);
        StartCoroutine(ShowTextAnimation());
    }

    IEnumerator ShowTextAnimation()
    {
        // 순차적으로 오브젝트 활성화 하는 방식
        foreach(var text in endingTexts)
        {
            text.gameObject.SetActive(true);
            yield return new WaitForSeconds(textDelay);
        }
        // 모든 텍스트 활성화 이 후에 FadeOut 진행
        fadeEffect.gameObject.SetActive(true);
        fadeEffect.OnFade(FadeState.FadeOut, StepTwo);
    }

    IEnumerator ShowMessageAnimation()
    {
        stepTwoText.text = "";

        for (int i=0; i<context.Length; i++)
        {
            stepTwoText.text += context[i];
            yield return new WaitForSeconds(textMessageDelay);
        }

        fadeEffect.gameObject.SetActive(true);
        fadeEffect.OnFade(FadeState.FadeOut, ShowEndingCredit);
    }

    void StepTwo()
    {
        // stepTwoText.parent.gameObject.SetActive(true);
        stepTwoObject.SetActive(true);
        fadeEffect.gameObject.SetActive(false);
        StartCoroutine(ShowMessageAnimation());
    }

    void ShowEndingCredit()
    {
        fadeEffect.gameObject.SetActive(false);
        credit.SetActive(true);
        animator.CrossFade("ShowCredit", 0.1f);
    }

    public void CreditEnd()
    {
        LoadingUI.LoadScene("TitleScene");
    }
}
