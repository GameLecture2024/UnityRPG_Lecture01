using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeadState : EnemyState
{
    Enemy_Zombie enemy;

    public ZombieDeadState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _animName, Enemy_Zombie _enemy) : base(_enemybase, _stateMachine, _animName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 3.0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
            GameObject.Destroy(enemy.gameObject);
        }
    }


}
