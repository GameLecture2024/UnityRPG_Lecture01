using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public PlayerManager player;

    [SerializeField] private List<TutorialBase> tutorials; // 인스팩터 창에서 Tutorial Class를 받아와서 저장할 List

    private TutorialBase currentTutorial = null;
    private int currentIndex = -1;

    private void Start()
    {
        SetNextTutorial();
        player.canMove = false;
        player.isPerformingAction = true;
    }

    private void Update()
    {
        currentTutorial?.Execute(this);    // 튜토리얼 고유의 로직을 Update으로 실행하라.
    }

    public void SetNextTutorial() // currentTutorial 있는 기능을 실행하라.
    {
        if (currentTutorial != null) currentTutorial.Exit(); // 진행 중인 튜토리얼이 있으면 종료하고

        if(currentIndex >= tutorials.Count - 1)                   // 튜토리얼이 끝인지 검사
        {
            CompleteTutorial(); // 튜토리얼 종료 로직을 시작
            return;
        }

        // 다음 튜토리얼 설정
        currentIndex++;
        Debug.Log($"현재 인덱스 번호{currentIndex}");
        currentTutorial = tutorials[currentIndex];

        currentTutorial.Enter();                            // 새로운 튜토리얼을 시작한다.
    }

    void CompleteTutorial()
    {
        Debug.Log("튜토리얼 끝");
        currentTutorial = null;
        LoadingUI.LoadScene("BattleScene");                // 로딩 씬으로 이동 후, 로딩이 끝나면 nextScene으로 이동
    }
}
