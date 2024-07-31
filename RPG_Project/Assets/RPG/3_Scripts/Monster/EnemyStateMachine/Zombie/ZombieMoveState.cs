using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMoveState : EnemyState
{
    public Enemy_Zombie enemy;
    public ZombieMoveState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _animName, Enemy_Zombie _enemy) : base(_enemybase, _stateMachine, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.moveTime;   // Idle 상태를 지속하는 시간을 초기화
    }

    public override void Exit()
    {
        base.Exit();
        // Exit할 때 agent의 남은 거리가 있으면 해당 위치로 이동한다.
        enemy.agent.ResetPath();
    }

    public override void Update()
    {
        base.Update();

        //enemy. NaveMeshAgnet 플레이어를 쫓는 기능
        Transform target = enemy.SearchTarget();

        if (target)
        {
            enemy.agent.SetDestination(target.position);
        }
        else
        {
            stateMachine.ChangeState(enemy.IdleState);
        }

        if (enemy.IsAvailableAttack)
            stateMachine.ChangeState(enemy.attackState);

    }
}
