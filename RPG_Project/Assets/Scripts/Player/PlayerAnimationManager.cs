using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    PlayerManager player;

    [Header("애니메이션 제어 변수")]
    private Animator animator;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>();
    }

    // 애니메이션 클립의 이름을 호출하여 각 playerManager에서 애니메이션을 쉽게 호출할 수 있게 캡슐화한 함수
    public void PlayerTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false) 
    {
        animator.CrossFade(targetAnimation, 0.2f);
        player.isPerformingAction = isPerformingAction;
        player.applyRootMotion = applyRootMotion;
        player.canRotate = canRotate;
        player.canMove = canMove;
    }


    public void AnimationTest() // Attack에 있는 애니메이션 이벤트를 삭제한 후 에 이 함수를 삭제하세요
    {
        // 애니메이션 공격 이팩트 실행

        Debug.Log(" 공격 첫번째 애니메이션이 실행되었다.");
    }

}
