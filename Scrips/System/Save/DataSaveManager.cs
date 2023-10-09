using System;
using System.Collections;
using System.IO;
using UnityEngine;
using static AreaStatement;
using static MissionCompleteManager;
using static UpgradeDataSystem;
using static WeaponUnlockManager;
using static PlanetCashAssetDatas;
using static ScoreManager;
using static SaveDateAndShips;

public class DataSaveManager : MonoBehaviour
{
    public static DataSaveManager instance = null;

    SceneSaveStart SceneSaveStart;
    SaveInput SaveInput;
    LoadDataManager LoadDataManager;
    SceneLoad1 SceneLoad1;

    //MonoBehaviours 클래스 목록
    AreaStatement AreaStatement;

    //노멀 클래스 데이터 목록
    [SerializeField] private SerializableBattleSave SerializableBattleSave;
    [SerializeField] private SerializableAreaStatement SerializableAreaStatement;
    [SerializeField] private SerializableMissionCompleteManager SerializableMissionCompleteManager;
    [SerializeField] private SerializableWeaponUnlockManager SerializableWeaponUnlockManager;
    [SerializeField] private SerializableUpgradeDataSystem SerializableUpgradeDataSystem;
    [SerializeField] private SerializablePlanetCashAssetDatas SerializablePlanetCashAssetDatas;
    [SerializeField] private SerializableScoreManager SerializableScoreManager;
    [SerializeField] private SerializableSaveDateAndShips SerializableSaveDateAndShips;

    //저장하기 전, 각 데이터 스크립트의 정보를 MonoBehaviours 클래스에서 노멀 클래스로 가져온다.
    void OnBeforeSerialize(string data)
    {
        if (BattleSave.Save1 != null)
            SerializableBattleSave = BattleSave.Save1.GetSerializable();
        if (AreaStatement != null)
            SerializableAreaStatement = AreaStatement.GetSerializable();
        if (MCMInstance != null)
            SerializableMissionCompleteManager = MCMInstance.GetSerializable();
        if (WeaponUnlockManager.instance != null)
            SerializableWeaponUnlockManager = WeaponUnlockManager.instance.GetSerializable();
        if (UpgradeDataSystem.instance != null)
            SerializableUpgradeDataSystem = UpgradeDataSystem.instance.GetSerializable();
        if (PlanetCashAssetDatas.instance != null)
            SerializablePlanetCashAssetDatas = PlanetCashAssetDatas.instance.GetSerializable();
        if (ScoreManager.instance != null)
            SerializableScoreManager = ScoreManager.instance.GetSerializable();
        File.WriteAllText(data, JsonUtility.ToJson(this));
    }

    //불러오기를 한 후, 노멀 클래스의 데이터를 MonoBehaviours 클래스의 데이터에 덮어씌운다.
    void OnAfterDeserialize()
    {
        if (BattleSave.Save1 != null)
            BattleSave.Save1.GetData(SerializableBattleSave);
        if (AreaStatement != null)
            AreaStatement.GetData(SerializableAreaStatement);
        if (MCMInstance != null)
            MCMInstance.GetData(SerializableMissionCompleteManager);
        if (WeaponUnlockManager.instance != null)
            WeaponUnlockManager.instance.GetData(SerializableWeaponUnlockManager);
        if (UpgradeDataSystem.instance != null)
            UpgradeDataSystem.instance.GetData(SerializableUpgradeDataSystem);
        if (PlanetCashAssetDatas.instance != null)
            PlanetCashAssetDatas.instance.GetData(SerializablePlanetCashAssetDatas);
        if (ScoreManager.instance != null)
            ScoreManager.instance.GetData(SerializableScoreManager);
    }

    //날짜 및 보유 함대수 정보를 저장한다.
    public void SaveDate(int number)
    {
        //총 함대수
        int FormationShip = 0;
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            FormationShip += ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count;
        }
        SaveDateAndShips.instance.TotalFlagships = ShipManager.instance.FlagShipList.Count;
        SaveDateAndShips.instance.TotalShips = FormationShip;

        //저장 날짜
        DateTime currentDate = DateTime.Now;
        string dateString = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
        SaveDateAndShips.instance.SavedDate = dateString;

        if (SaveDateAndShips.instance != null)
            SerializableSaveDateAndShips = SaveDateAndShips.instance.GetSerializable();

        string dataPath = Path.Combine(Application.persistentDataPath, "UCCIS Battle Log Date" + number + ".json");
        File.WriteAllText(dataPath, JsonUtility.ToJson(SerializableSaveDateAndShips));
    }

    //저장 날짜와 보유 함대수 정보를 가져온다.
    public void GetSavedDate(int number)
    {
        string dataPath = Path.Combine(Application.persistentDataPath, "UCCIS Battle Log Date" + number + ".json");
        if (File.Exists(dataPath))
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(dataPath), SerializableSaveDateAndShips);

            if (SaveDateAndShips.instance != null)
                SaveDateAndShips.instance.GetData(SerializableSaveDateAndShips);
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        AreaStatement = FindObjectOfType<AreaStatement>();
    }

    private void OnEnable()
    {
        if (LoadDataManager == null)
            LoadDataManager = FindObjectOfType<LoadDataManager>();

        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                string DataPath = Path.Combine(Application.persistentDataPath, "UCCIS Battle Log.json"); //파일 확인
                if (File.Exists(DataPath))
                {
                    LoadDataManager.GetSaveDataList();
                    break;
                }
            }
            else
            {
                string DataPath = Path.Combine(Application.persistentDataPath, "UCCIS Battle Log" + i + ".json"); //파일 확인
                if (File.Exists(DataPath))
                {
                    LoadDataManager.GetSaveDataList();
                    break;
                }
                else //파일이 없으면 새로 시작하는 것을 의미하며, 튜토리얼을 시작한다.
                {
                    if (i == 9)
                    {
                        string StartPath = Path.Combine(Application.persistentDataPath, "Start UCCIS Battle Log.json");
                        LoadSaveData(StartPath);

                        if (SceneLoad1 == null)
                            SceneLoad1 = FindObjectOfType<SceneLoad1>();
                        SceneLoad1.ButtonBackground.raycastTarget = false;
                        SceneLoad1.progressbar.gameObject.SetActive(true);
                        StartCoroutine(SceneLoad1.MissionStart());
                        NewGame();
                        break;
                    }
                }
            }
        }
    }

    //세이브 데이터 불러오기
    public void LoadSaveData(string data)
    {
        BattleSave.Save1.GroundBattleCount = 1;
        JsonUtility.FromJsonOverwrite(File.ReadAllText(data), this); //파일이 존재할 때에만 파일 불러오기, 이미 튜토리얼을 끝낸 이후의 저장파일들만 받는다.
        OnAfterDeserialize();
    }

    //새 게임 시작하기
    public void NewGame()
    {
        BattleSave.Save1.FirstStart = true;
        BattleSave.Save1.GroundBattleCount = 0;
    }

    //데이터를 저장하기
    public IEnumerator SaveStart(string data, int number)
    {
        SaveDate(number);
        if (LoadDataManager == null)
            LoadDataManager = FindObjectOfType<LoadDataManager>();
        if (SceneSaveStart == null)
            SceneSaveStart = FindObjectOfType<SceneSaveStart>();
        SceneSaveStart.isJson = true;
        SceneSaveStart.enabled = true;

        yield return new WaitForSecondsRealtime(0.1f);
        SceneSaveStart.isJson = false;
        SceneSaveStart.enabled = false;
        OnBeforeSerialize(data);
        DataDelete();
        LoadDataManager.GetSaveDataList();
        //Debug.Log("저장 완료");
    }

    //세이브 데이터 삭제
    public void DeleteData(string data)
    {
        File.Delete(data);
        if (LoadDataManager == null)
            LoadDataManager = FindObjectOfType<LoadDataManager>();
        LoadDataManager.GetSaveDataList();
    }

    //각 리스트의 데이터를 먼저 초기화 
    void DataDelete()
    {
        SaveInput = FindObjectOfType<SaveInput>();
        SaveInput.DeleteData();
    }

    //새 게임용 데이터 저장
    public void NewGameDataSave()
    {
        string StartPath = Path.Combine(Application.persistentDataPath, "Start UCCIS Battle Log.json");
        if (!File.Exists(StartPath))
            OnBeforeSerialize(StartPath);
    }
}