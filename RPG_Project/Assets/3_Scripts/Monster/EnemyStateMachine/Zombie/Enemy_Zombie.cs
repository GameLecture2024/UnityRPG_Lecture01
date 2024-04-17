using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldOfView))]
public class Enemy_Zombie : Enemy
{
    #region State
    public ZombieIdleState IdleState { get; private set; }
    public ZombieMoveState moveState { get; private set; }
    public ZombieAttackState attackState { get; private set; }
    public ZombiePatrolState patrolState { get; private set; }
    public ZombieDeadState deadState { get; private set; }
    #endregion

    public ZombieData zombieData;

    protected override void Awake()
    {
        base.Awake();
        OnLoadComponents();

        IdleState = new ZombieIdleState(this, stateMachine, "Idle", this);
        moveState = new ZombieMoveState(this, stateMachine, "Walk", this);
        attackState = new ZombieAttackState(this, stateMachine, "Attack", this);
        patrolState = new ZombiePatrolState(this, stateMachine, "Walk", this);
        deadState = new ZombieDeadState(this, stateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initilize(patrolState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void OnLoadComponents()
    {
        base.OnLoadComponents();
        HP = zombieData.HP;
        AttackPower = zombieData.Attack;
        AttackRange = zombieData.attackRange;
        ViewRange = zombieData.viewRange;
    }

    public override void TakeDamage(int damage, Vector3 contactPos, GameObject hitEffectPrefabs = null)
    {
        base.TakeDamage(damage, contactPos, hitEffectPrefabs);

        if (IsAlive)
        {
            animator?.CrossFade("Hit", 0.2f);
            transform.forward = contactPos;
            stateMachine.ChangeState(IdleState);
        }
        else
        {
            stateMachine.ChangeState(deadState);
        }
    }
}
