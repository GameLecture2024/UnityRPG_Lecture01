using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public ManualCollider manualCollider;

    protected override void Awake()
    {
        base.Awake();
        manualCollider = GetComponentInChildren<ManualCollider>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
