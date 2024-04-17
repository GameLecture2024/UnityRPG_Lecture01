using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieDeadState : EnemyState
{
    Enemy_Zombie enemy;

    public ZombieDeadState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _animName, Enemy_Zombie _enemy) : base(_enemybase, _stateMachine, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.isStopped = true;
        stateTimer = 5;         // 숫자 2를 deadTime 변수로 바꿀 것
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
            GameObject.Destroy(enemy.gameObject);   // HP가 0이 됬을 때 오브젝트를 파괴한다.
    }
}
