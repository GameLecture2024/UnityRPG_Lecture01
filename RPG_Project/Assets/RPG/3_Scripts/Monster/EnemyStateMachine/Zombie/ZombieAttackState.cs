using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : EnemyState
{
    Enemy_Zombie enemy;
    public ZombieAttackState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _animName, Enemy_Zombie _enemy) : base(_enemybase, _stateMachine, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()      // 공격 상태로 진입!
    {
        base.Enter();
        Debug.Log("Attack 상태 진입");

        if(!enemy.IsAvailableAttack)   // true이면 공격 가능하다. false이면 공격을 못한다.
        {
            enemy.stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {

    }
}
