using CameraSetting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    [Header("Common Player Data")]
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;

    [Header("플레이어 제약 조건")]
    public bool isPerformingAction = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool canCombo = false;

    [Header("Player Manager Script")]
    [HideInInspector] public PlayerAnimationManager playerAnimationManager;
    [HideInInspector] public PlayerMovementManager playerMovementManager;
    [HideInInspector] public PlayerAudioManager playerAudioManager;
    private void Awake()
    {
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        playerAudioManager = GetComponent<PlayerAudioManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
}
