using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveManager : SingletonMonoBehaviour<SaveManager>
{
    // Singleton Pattern : 인스턴스가 없을 경우 인스턴스를 생성하고, 이미 존재할 경우 인스턴스를 반환

    DataHandler dataHandler;
    GameData gameData;                   // 플레이어의 정보를 저장할 클래스
    List<ISaveManager> saveManagers;     // 하이어라키창에 있는 ISaveManager를 상속하는 클래스를 저장할 리스트

    [Header("저장할 데이터 변수 정보")]
    public string fileName;
    public string originName;
    public SaveGameSlot nowSlot;         // 현재 선택된 슬롯을 저장하는 변수

    protected override void Awake()         // ctrl + M + M 
    {
        base.Awake();
        originName = fileName;           // 시작할 때 저장데이터 이름의 원본을 기억해둔다.    
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneloaded;    // 콜백으로 호출하게 함수를 등록한다.
    }

    private void OnSceneloaded(Scene scene, LoadSceneMode mode) // 매개 변수로 scene, Scene호출 방식을 매개 변수로 작성을 해줘야 한다.
    {
        dataHandler = new DataHandler(Application.persistentDataPath, fileName);  //  장점 : 플랫폼에 상관없이 데이터를 쉽게 저장할 수 있다. 단점 : 특정 플랫폼의 경우에는 저장된 데이터를 확인할 수 없다.
        saveManagers = FindAllSaveManagers();                                     // 하이어라키창에 있는 ISaveManager를 상속하는 클래스를 찾아서 리스트에 저장한다.
        LoadGame();
    }

    public void ChangeSaveFileNameBySelectSlot(SaveGameSlot slot)   // SaveSlot 클래스에서 호출하는 함수
    {
        nowSlot = slot;

        fileName = originName;
        fileName += slot.ToString() + ".txt";
    }

    public void SaveGame()
    {
        // 1. 저장할 데이터를 이 함수에 호출한 뒤에 gameData에 저장한다.
        foreach(var saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        // 2. 저장한 gameData를 외부 폴더에 저장시킨다.
        dataHandler.DataSave(gameData);
        Debug.Log("게임이 저장되었습니다.");
    }

    public void LoadGame()
    {
        // 외부에 저장된 게임 데이터 파일을 불러온다.
        gameData = dataHandler.DataLoad();  // dataHandler.DataLoad() 함수를 호출하여 데이터가 있으면 해당 데이터를 gameData 형식으로 반환하고, 없으면 null을 반환하다.

        if (gameData == null)               // 만약 외부에 게임 데이터가 없다면 새로운 게임을 호출한다.
        {
            NewGame();
        }
        // ------------------------------------------------------------------------------------------
        // GameData 클래스에 있는 데이터를 게임에 필요한 클래스에 각각 데이터를 전달해준다.
        foreach(var saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }
        
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }
}
