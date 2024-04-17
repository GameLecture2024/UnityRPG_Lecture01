using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    public int animationIndex;

    public int priority;

    public int damage;
    public float range = 3f;

    [SerializeField] protected float coolTime;
    protected float coolTimeValue = 0.0f;

    [HideInInspector] public LayerMask targetMask;

    private void Start()
    {
        coolTimeValue = coolTime;
    }

    private void Update()
    {
        if(coolTimeValue < coolTime)
        {
            coolTimeValue += Time.deltaTime;
        }
    }

    public abstract void ExecuteAttack(GameObject target = null, Transform startPoint = null);

    public bool IsAvailable => coolTimeValue >= coolTime;
}
