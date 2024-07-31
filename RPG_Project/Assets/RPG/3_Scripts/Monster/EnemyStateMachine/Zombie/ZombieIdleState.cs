using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : EnemyState
{
    Enemy_Zombie enemy;

    public ZombieIdleState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _animName, Enemy_Zombie _enemy) : base(_enemybase, _stateMachine, _animName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;   // Idle 상태를 지속하는 시간을 초기화
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        Transform target = enemy.SearchTarget();
        if (target)
        {
            if (enemy.IsAvailableAttack)
            {
                stateMachine.ChangeState(enemy.attackState);
            }
            else
            {
                stateMachine.ChangeState(enemy.moveState);
            }
        }
        else
        {
            if (stateTimer <= 0)
            {
                stateMachine.ChangeState(enemy.patrolState);
            }
        }
    }
}
