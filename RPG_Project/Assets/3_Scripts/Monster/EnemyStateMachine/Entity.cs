using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���͸� ��ӹ��� ���� �ƴ϶� �÷��̾ ����� �� �ְ�.
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
