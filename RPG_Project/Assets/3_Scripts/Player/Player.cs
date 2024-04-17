using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public ManualCollider manualCollider;

    public PlayerData playerData;
    public Action OnChangedStats;

    protected override void Awake()
    {
        base.Awake();
        manualCollider = GetComponentInChildren<ManualCollider>();
    }

    protected override void Start()
    {
        InitPlayerData();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void InitPlayerData()
    {
        MAXHP = playerData.MaxHp;
        MAXMP = playerData.MaxMp;
        AttackPower = playerData.AttackPower;
    }

    public override void TakeDamage(int damage, Vector3 contactPos, GameObject hitEffectPrefabs = null)
    {
        base.TakeDamage(damage, contactPos, hitEffectPrefabs);
    }
}
