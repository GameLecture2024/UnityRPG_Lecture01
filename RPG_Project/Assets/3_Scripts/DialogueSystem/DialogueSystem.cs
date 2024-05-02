using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public struct Speaker
    {
        public Image portraitImage;
        public Image dialougeImage;
        public TextMeshProUGUI speakerName;
        public TextMeshProUGUI dialogue;
        public GameObject arrow;
    }

    [System.Serializable]
    public struct DialogData
    {
        public int speakerIndex;
        public string name;
        public string dialogue;
    }

    [SerializeField] private Speaker[] speakers;
    [SerializeField] private DialogData[] dialogs;

    [SerializeField] private bool isAuto = true; // true이면 시작하자마자 첫 대사 출력, false이면 플레이어의 입력을 대기
    private bool isFirst = true;

    private int currentDialogIndex = -1;        // 첫 번째 대사는 0번으로 출력된다. 따라서 시작 직전 넘버는 -1이 된다.
    private int currentSpeakerIndex = 0;        // 0번이면 캐릭터 이미지 ,1번이면 실루엣 이미지 

    public float textSpeed = 0.2f;
    public bool isTypeing = false;

    public int IndexNumber = 0;

    public void Setup()
    {
        for (int i = 0; i < speakers.Length; i++)
        {
            // 오브젝트 세팅
            SetActiveObjects(speakers[i], false);      // bool값이 true이면 해당 UI 출력 
            speakers[i].portraitImage.gameObject.SetActive(false);
        }
        isFirst = true;
        currentDialogIndex = -1;
        currentSpeakerIndex = 0;
        dialogs = WorldDialogueData.Instance.dialogueDatabase[IndexNumber].ToArray();
    }

    public bool UpdateDialog()
    {
        if (isFirst == true)
        {
            Setup();

            if (isAuto)
                SetNextDialog();

            isFirst = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(isTypeing) // 타이핑이 진행 중인 상태
            {
               isTypeing = false;

               //타이핑 기능 중지 시킨다.
                StopAllCoroutines();
                SoundManager.Instance.StopSFX();
                speakers[currentSpeakerIndex].dialogue.text = dialogs[currentDialogIndex].dialogue; // 대사 UI에 대사 출력
                speakers[currentSpeakerIndex].arrow.SetActive(true);   // 현재 대사가 끝났음을 알린다.
            }
            else if (dialogs.Length > currentDialogIndex + 1)             // 대사가 남아 있을 경우에는 다음 대사를 진행
            {
                SetNextDialog();

            }
            // 대사가 없을 경우 모든 오브젝트를 비활성화 하고 true를 반환
            else
            {
                for (int i = 0; i < speakers.Length; i++)
                {
                    SetActiveObjects(speakers[i], false);
                    speakers[i].portraitImage.gameObject.SetActive(false);
                }

                return true;                         
            }
        }

        return false;

    }


    void SetNextDialog()
    {
        SetActiveObjects(speakers[currentSpeakerIndex], false); // 대사 시작할 때 직전 대사 UI 비활성화

        currentDialogIndex++;  // 다음 dialogueData 검색

        currentSpeakerIndex = dialogs[currentSpeakerIndex].speakerIndex;

        SetActiveObjects(speakers[currentSpeakerIndex], true); // 현재 대사의 UI 활성화

        speakers[currentSpeakerIndex].speakerName.text = dialogs[currentDialogIndex].name; // 이름 끼리 매칭

        //speakers[currentSpeakerIndex].dialogue.text = dialogs[currentDialogIndex].dialogue; // 대사 UI에 대사 출력
        StartCoroutine(TypeSentence());
    }

    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.dialougeImage.gameObject.SetActive(visible);
        speaker.speakerName.gameObject.SetActive(visible);
        speaker.dialogue.gameObject.SetActive(visible);
        speaker.portraitImage.gameObject.SetActive(visible);

        speaker.arrow.SetActive(false);
    }

    IEnumerator TypeSentence()
    {
        int index = 0;
        isTypeing = true;

        while(index <= dialogs[currentDialogIndex].dialogue.Length)
        {
            speakers[currentSpeakerIndex].dialogue.text = dialogs[currentDialogIndex].dialogue.Substring(0, index); // String에 첫 문자 부터 마지막 문자까지 반복

            index++;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.typeText);
            yield return new WaitForSeconds(textSpeed);
            SoundManager.Instance.StopSFX();
        }

        isTypeing = false;

        speakers[currentSpeakerIndex].arrow.SetActive(true);   // 현재 대사가 끝났음을 알린다.
    }
}
