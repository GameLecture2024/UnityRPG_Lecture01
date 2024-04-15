using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터만 상속받을 것이 아니라 플레이어도 상속할 수 있게.
/// </summary>
public class Entity : MonoBehaviour
{
    [Header("Status")]
    public int HP;
    public int AttackPower;

    public Animator animator;

    protected virtual void Awake()
    {
        OnLoadComponents();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    public virtual void OnLoadComponents()
    {
        animator = GetComponent<Animator>();
    }

}
