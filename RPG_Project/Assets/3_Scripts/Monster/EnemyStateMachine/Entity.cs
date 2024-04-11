using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Status")]
    public int HP;
    public int AttackPower;
    public float AttackRange;

    public Rigidbody rigidbody;
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
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

}
