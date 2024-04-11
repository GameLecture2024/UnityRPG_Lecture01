using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleZombieController : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;

    [Header("좀비가 쫓아갈 대상")]
    public Transform target;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        TraceTarget(target.position);
    }

    /// <summary>
    /// 타겟의 위치를 통해 navMeshAgent를 타겟을 추적하는 함수
    /// </summary>
    private void TraceTarget(Vector3 des)
    {
        agent.SetDestination(des);
    }
}
