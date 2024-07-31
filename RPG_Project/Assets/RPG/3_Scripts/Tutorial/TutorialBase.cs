using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 튜토리얼로 사용할 모든 클래스의 추상 클래스로 사용, Enter, Execute, Exit
/// </summary>
public abstract class TutorialBase : MonoBehaviour
{
    public abstract void Enter();

    public abstract void Execute(TutorialController controller);

    public abstract void Exit();
}
