using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [Header("Enemy Stat")]
    public float idleTime;                 // idle 상태 시간
    public float moveTime;                 // 추적 시간
    public float chaseSpeed;               // 추적 속도

    [Header("Components")]
    public Rigidbody rigidbody;
    public Animator animator;
    public EnemyStateMachine stateMachine { get; private set; }
    public NavMeshAgent agent;
    public EnemyAttackManager enemyAttackManager; 

    [Header("Search Target")]
    private FieldOfView fov;
    public LayerMask targetMask;           // 타겟 지정 레이어 마스크
    public Transform target;

    [Header("Patrol")]
    [SerializeField] private Transform[] wayPoints;               // 탐색할 위치 경로
    [HideInInspector] public Transform targetWayPoint = null;
    private int wayPointIndex = 0;

    public Transform hitSpawnPos;

    [Header("Battle UI")]
    [SerializeField] protected NPCBattleUI battleUI;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();

        if (battleUI)
        {
            battleUI.MinimumValue = 0.0f;
            battleUI.MaximumValue = MaxHP;
            battleUI.Value = HP;
        }
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public override void OnLoadComponents()
    {
        base.OnLoadComponents();
        fov = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        enemyAttackManager = GetComponent<EnemyAttackManager>();
    }

    public bool IsAvailableAttack
    {
        get
        {
            if (!target)
            {
                return false;
            }

            if((target.position - transform.position).sqrMagnitude < AttackRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public Transform SearchTarget()
    {
        target = fov.NearesetTarget;
        return target;
    }

    /// <summary>
    /// true이면 남은 거리가 있다. false이면 남은 거리가 없으므로 다음 대상을 찾는다.
    /// </summary>
    
    public bool CheckRemainDistance()
    {
        if((wayPoints[wayPointIndex].transform.position - transform.position).sqrMagnitude < 0.1)
        {
            if (wayPointIndex < wayPoints.Length - 1)
                wayPointIndex++;
            else
                wayPointIndex = 0;

            return false;
        }
        return true;
    }

    public Transform FindnextWayPoint()
    {
        targetWayPoint = null;
        if(wayPoints.Length > 0)
        {
            targetWayPoint = wayPoints[wayPointIndex];
        }

        return targetWayPoint;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ViewRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
