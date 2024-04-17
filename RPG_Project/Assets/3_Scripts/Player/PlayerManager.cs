using CameraSetting;
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
    public PlayerInGameUI playerInGameUI;
    protected override void Awake()
    {
        base.Awake();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        playerAudioManager = GetComponent<PlayerAudioManager>();
        characterController = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        base.Start();
        playerInGameUI.InitializeSlider();
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

    public override void TakeDamage(int damage, Vector3 contactPos, GameObject hitEffectPrefabs = null)
    {
        base.TakeDamage(damage, contactPos, hitEffectPrefabs);
        OnChangedStats?.Invoke();

        if (hitEffectPrefabs) // 피격 위치에 피격 이팩트 생성
        {
            Instantiate(hitEffectPrefabs, Vector3.zero, Quaternion.identity); // 오브젝트 풀링 교체
        }

        if (IsAlive)
        {
            // Hit
            playerAnimationManager.PlayerTargetActionAnimation("Hit", false);
            // VFX - Hit Effect manager로 색 변경
        }
        else
        {
            // Dead
            isDead = true;
            playerInGameUI.GameOver();
            animator.CrossFade("Dead", 0.2f);
        }
    }
}
