using CameraSetting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Player, ISaveManager
{
    [Header("Common Player Data")]
    [HideInInspector] public CharacterController characterController;

    [Header("플레이어 제약 조건")]
    public bool isPerformingAction = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool canCombo = false;
    public bool isDead = false;

    [Header("Player Manager Script")]
    [HideInInspector] public PlayerAnimationManager playerAnimationManager;
    [HideInInspector] public PlayerMovementManager playerMovementManager;
    [HideInInspector] public PlayerAudioManager playerAudioManager;
    [HideInInspector] public PlayerActionManager playerActionManager;
    [HideInInspector] public PlayerEffectManager playerEffectManager;

    [Header("delegate")]
    public Action OnChangedStats;

    private void Awake()
    {
        OnLoadComponents();
        LoadPlayerData(playerData);
    }

    protected override void Start()
    {
        base.Start();
        InitializeStats();
    }

    public void SaveData(ref GameData gameData)   // GameData 클래스에 플레이어의 현재 좌표를 저장
    {
        gameData.x = transform.position.x;
        gameData.y = transform.position.y;
        gameData.z = transform.position.z;
    }

    public void LoadData(GameData gameData)      // GameData 클래스에 저장된 정보를 플레이어 데이터로 호출
    {
        Vector3 loadPlayerPos = new Vector3(gameData.x, gameData.y, gameData.z);

        transform.position = loadPlayerPos;
    }

    private void InitializeStats()
    {
        HP = MaxHP;
        Stamina = MaxStmaina;
    }

    private void LoadPlayerData(PlayerData playerData)
    {
        MaxHP = playerStats.maxHealth.baseValue =  playerData.HP;
        MaxStmaina = playerStats.maxStamina.baseValue = playerData.Stamina;
        AttackPower = playerStats.damage.baseValue = playerData.Attack;
    }

    public override void OnLoadComponents()
    {
        base.OnLoadComponents();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        playerAudioManager = GetComponent<PlayerAudioManager>();
        playerActionManager = GetComponent<PlayerActionManager>();
        characterController = GetComponent<CharacterController>();
        playerEffectManager = GetComponent<PlayerEffectManager>();
    }

    public override void TakeDamage(int damage, Vector3 contactPos, GameObject hitEffectPrefabs = null)
    {
        base.TakeDamage(damage, contactPos, hitEffectPrefabs);
        playerStats.currentHealth -= damage;
        playerAnimationManager.PlayerTargetActionAnimation("Hit", true);
        OnChangedStats?.Invoke();

        if(HP < 0)
        {
            OnDie();
        }
    }

    public void OnDie()
    {
        anim.CrossFade("Dead", 0.2f);
        isDead = true;
    }
}
