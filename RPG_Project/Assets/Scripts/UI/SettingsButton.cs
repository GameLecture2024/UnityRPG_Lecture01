using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    public Button ConfirmBtn;
    public Button BackButton;
    public Button ReturnToLobbyButton;

    [SerializeField] private GamePlayUIManager gamePlayUIManager;

    public void Awake()
    {
        BackButton.onClick.AddListener(() => gamePlayUIManager.CloseUIMenu());
        ReturnToLobbyButton.onClick.AddListener(()=> gamePlayUIManager.ReturnToTitleScene());
        ConfirmBtn.onClick.AddListener(()=> SaveManager.Instance.SaveGame());
    }
}
