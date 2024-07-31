using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// EnemySate의 현재 상태를 반환, 다른 상태로 전환 하는 기능을 하는 클래스, EnemyState 클래스에 부착하여 사용
/// </summary>
public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }

    public void Initilize(EnemyState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
