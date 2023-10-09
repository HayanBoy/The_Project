using System.Collections.Generic;
using System;
using UnityEngine;

public class BattleSave : MonoBehaviour
{
    public static BattleSave Save1 = null;

    [Header("설정")]
    public float BGMSliderValue;
    public float UniverseSoundEffectSliderValue;
    public float BSSoundEffectSliderValue;

    [Header("자원 목록")]
    public float NarihaUnionGlopaoros; //나리하 인류연합 화폐 Glopaoros
    public float NarihaUnionGlopaoroslimit; //Glopaoros 한도량
    public float ConstructionResource; //건설 재료
    public float ConstructionResourcelimit; //건설 재료 한도량
    public float Taritronic; //타리트로닉 연구재료

    [Header("기타")]
    public int GroundBattleCount; //SaveInput의 데이터 불러오기 위한 용도
    public int SelectedshipNumber; //선택된 기함
    public int LanguageType; //언어
    public int BattleLoadScene; //0 = UCCIS창, 1 = 허리케인 작전 준비 창, 2 = 허리케인 작전 도중
    public bool FlagshipMode = false; //기함모드 여부
    public bool GoBackTitle; //타이틀 화면으로 돌아갈 때에만 사용

    [Header("튜토리얼")]
    public bool FirstStart = false; //게임을 최초로 시작할 때에만 발동하는 튜토리얼(함대전 이동~사타리우스 글래시아 해방)
    public int Tutorial = 0;
    public int PlanetTutorial = 0; //튜토리얼 행성전. 0 = 행성전 미진행, 1 = 행성전 진행중(도중에 취소할 때 감지용), 2 = 행성전 임무 완료

    public bool FleetWeaponTutorial = false;
    public bool FleetFormationTutorial = false;
    public bool FlagshipManagementTutorial = false;
    public bool LabTutorial = false;
    public bool MBCA79Tutorial = false;

    [Header("미션 데이터")]
    public int MissionLevel;
    public int MissionArea;
    public int MissionType;
    public int MissionPlanetNumber; //미션을 시작한 행성 및 항성이나 지정된 지역.
    public int FinishSpawnNumber;
    public int SelectedMissionTableNumber; //선택한 미션 테이블 번호. 완료 후, 해당 테이블의 미션이 사라지도록 조취
    public bool MissionSuccessed; //미션이 성공했는지에 대한 여부.
    public int FlagshipMissionSuccessed; //기함 침투전 미션 성공 여부. 1 = 기함 무력화 성공, 2 = 기함 폭파 성공

    [Header("사이트")]
    public List<Vector3> RandomSiteTransform = new List<Vector3>();
    public List<bool> RandomSiteFirstSpawn = new List<bool>();
    public List<int> RandomSiteNumber = new List<int>();

    [Header("행성")]
    public List<int> PlanetNumber = new List<int>(); //행성 번호
    public List<bool> FirstFreePlanet = new List<bool>(); //해방된 행성 목록
    public List<bool> BattleVictoryPlanet = new List<bool>(); //전투에서 승전한 행성 목록

    [Header("함선 목록")]
    public int NarihaFlagShipList; //나리하 인류연합 기함 목록
    public int EnemiesFlagShipList; //적 모든 기함 목록
    public int EnemiesFormationShipList; //적 모든 편대함 목록

    [Header("나리하 기함 위치")]
    public int CountOfFlagship;
    public Vector3 CameraLocation;
    public List<Vector3> FlagshipLocation = new List<Vector3>();
    public List<Quaternion> FlagshipRotanion = new List<Quaternion>();

    [Header("나리하 기함 선체 상태")]
    public List<bool> FlagshipShieldDown = new List<bool>();
    public List<float> FlagshipHull = new List<float>();
    public List<int> FormationStorage = new List<int>(); //기함 당 총 편대수
    public List<Vector3> FlagshipFormationsCodex = new List<Vector3>(); //기함당 편대함선들의 배열 위치 좌표

    [Header("나리하 스킬 상태")]
    public List<int> FirstSkillType = new List<int>();
    public List<int> FirstSkillNumber = new List<int>();
    public List<int> SecondSkillType = new List<int>();
    public List<int> SecondSkillNumber = new List<int>();
    public List<int> ThirdSkillType = new List<int>();
    public List<int> ThirdSkillNumber = new List<int>();

    [Header("나리하 기함 부위별 체력")]
    public List<float> Main1Left1HPFlagship = new List<float>();
    public List<float> Main1Left2HPFlagship = new List<float>();
    public List<float> Main1Right1HPFlagship = new List<float>();
    public List<float> Main1Right2HPFlagship = new List<float>();
    public List<float> Main2Left1HPFlagship = new List<float>();
    public List<float> Main2Left2HPFlagship = new List<float>();
    public List<float> Main2Right1HPFlagship = new List<float>();
    public List<float> Main2Right2HPFlagship = new List<float>();
    public List<float> Main3Left1HPFlagship = new List<float>();
    public List<float> Main3Right1HPFlagship = new List<float>();

    [Header("나리하 기함 부위 무력화 여부")]
    public List<bool> Main1Left1DownFlagship = new List<bool>();
    public List<bool> Main1Left2DownFlagship = new List<bool>();
    public List<bool> Main1Right1DownFlagship = new List<bool>();
    public List<bool> Main1Right2DownFlagship = new List<bool>();
    public List<bool> Main2Left1DownFlagship = new List<bool>();
    public List<bool> Main2Left2DownFlagship = new List<bool>();
    public List<bool> Main2Right1DownFlagship = new List<bool>();
    public List<bool> Main2Right2DownFlagship = new List<bool>();
    public List<bool> Main3Left1DownFlagship = new List<bool>();
    public List<bool> Main3Right1DownFlagship = new List<bool>();

    [Header("편대 함선 정보")]
    public List<int> CountOfFormationShip = new List<int>(); //기함별 보유 함선 수
    public List<int> FormationType = new List<int>(); //기함별 보유한 함선의 각 유형

    [Header("편대 함선 위치")]
    public List<Vector3> FormationShipLocation = new List<Vector3>();
    public List<Quaternion> FormationShipRotanion = new List<Quaternion>();

    [Header("편대 함선 선체 상태")]
    public List<bool> FormationShieldDown = new List<bool>();
    public List<float> FormationShipHull = new List<float>();

    [Header("편대 함선 부위별 체력")]
    public List<float> Main1Left1HPFormationShip = new List<float>();
    public List<float> Main1Right1HPFormationShip = new List<float>();
    public List<float> Main2Left1HPFormationShip = new List<float>();
    public List<float> Main2Right1HPFormationShip = new List<float>();
    public List<float> Main3Left1HPFormationShip = new List<float>();
    public List<float> Main3Right1HPFormationShip = new List<float>();

    [Header("편대 함선 부위 무력화 여부")]
    public List<bool> Main1Left1DownFormationShip = new List<bool>();
    public List<bool> Main1Right1DownFormationShip = new List<bool>();
    public List<bool> Main2Left1DownFormationShip = new List<bool>();
    public List<bool> Main2Right1DownFormationShip = new List<bool>();
    public List<bool> Main3Left1DownFormationShip = new List<bool>();
    public List<bool> Main3Right1DownFormationShip = new List<bool>();

    [Header("함대 기동 상태")]
    public List<Vector3> DestinationArea = new List<Vector3>(); //함대 목적지
    public List<float> WarpStartTime = new List<float>(); //워프 시작 시간
    public List<bool> WarpDriveReady = new List<bool>();
    public List<bool> WarpDriveActive = new List<bool>();

    [Header("함대 포탑 정보")]
    public List<int> CannonType = new List<int>(); //무기 타입
    public List<float> AttackTime = new List<float>(); //사격 시간 상태
    public List<int> FlagshipTurretDownList = new List<int>(); //무력화된 기함 함포 리스트
    public List<int> FormationTurretDownList = new List<int>(); //무력화된 편대함 함포 리스트

    [Header("적 함대 상태")]
    public List<bool> EnemyIsBattleSite = new List<bool>(); //전투 지역에 있는지
    public List<int> EnemyBattleSiteNumber = new List<int>(); //해당 소속된 전투 지역 번호

    [Header("적 기함 위치")]
    public List<bool> SelectedEnemyFlagship = new List<bool>(); //보병전에 선택된 기함
    public int CountOfEnemyFlagship;
    public List<int> EnemyNationType = new List<int>(); //적 종족별 구분, 1 = 나리하 인류연합 아군, 2 = 슬로리어스, 3 = 칸타크리
    public List<Vector3> EnemyFlagshipLocation = new List<Vector3>();
    public List<Quaternion> EnemyFlagshipRotanion = new List<Quaternion>();

    [Header("적 기함 선체 상태")]
    public List<bool> EnemyFlagshipDown = new List<bool>(); //기함이 침투전으로 인해 무력화 되었을 경우
    public List<bool> EnemyFlagshipShieldDown = new List<bool>();
    public List<float> EnemyFlagshipShield = new List<float>();
    public List<float> EnemyFlagshipHull = new List<float>();

    [Header("적 기함 부위별 체력")]
    public List<float> Main1Left1HPEnemyFlagship = new List<float>();
    public List<float> Main1Left2HPEnemyFlagship = new List<float>();
    public List<float> Main1Right1HPEnemyFlagship = new List<float>();
    public List<float> Main1Right2HPEnemyFlagship = new List<float>();
    public List<float> Main2Left1HPEnemyFlagship = new List<float>();
    public List<float> Main2Left2HPEnemyFlagship = new List<float>();
    public List<float> Main2Right1HPEnemyFlagship = new List<float>();
    public List<float> Main2Right2HPEnemyFlagship = new List<float>();
    public List<float> Main3Left1HPEnemyFlagship = new List<float>();
    public List<float> Main3Right1HPEnemyFlagship = new List<float>();

    [Header("적 기함 부위 무력화 여부")]
    public List<bool> Main1Left1DownEnemyFlagship = new List<bool>();
    public List<bool> Main1Left2DownEnemyFlagship = new List<bool>();
    public List<bool> Main1Right1DownEnemyFlagship = new List<bool>();
    public List<bool> Main1Right2DownEnemyFlagship = new List<bool>();
    public List<bool> Main2Left1DownEnemyFlagship = new List<bool>();
    public List<bool> Main2Left2DownEnemyFlagship = new List<bool>();
    public List<bool> Main2Right1DownEnemyFlagship = new List<bool>();
    public List<bool> Main2Right2DownEnemyFlagship = new List<bool>();
    public List<bool> Main3Left1DownEnemyFlagship = new List<bool>();
    public List<bool> Main3Right1DownEnemyFlagship = new List<bool>();

    [Header("적 편대 함선 정보")]
    public List<int> CountOfEnemyFormationShip = new List<int>(); //기함별 보유 함선 수
    public List<int> EnemyFormationType = new List<int>(); //기함별 보유한 함선의 각 유형

    [Header("적 편대 함선 위치")]
    public List<Vector3> EnemyFormationShipLocation = new List<Vector3>();
    public List<Quaternion> EnemyFormationShipRotanion = new List<Quaternion>();

    [Header("적 편대 함선 선체 상태")]
    public List<bool> EnemyFormationShieldDown = new List<bool>();
    public List<float> EnemyFormationShield = new List<float>();
    public List<float> EnemyFormationShipHull = new List<float>();

    [Header("적 편대 함선 부위별 체력")]
    public List<float> Main1Left1HPEnemyFormationShip = new List<float>();
    public List<float> Main1Right1HPEnemyFormationShip = new List<float>();
    public List<float> Main2Left1HPEnemyFormationShip = new List<float>();
    public List<float> Main2Right1HPEnemyFormationShip = new List<float>();
    public List<float> Main3Left1HPEnemyFormationShip = new List<float>();
    public List<float> Main3Right1HPEnemyFormationShip = new List<float>();

    [Header("적 편대 함선 부위 무력화 여부")]
    public List<bool> Main1Left1DownEnemyFormationShip = new List<bool>();
    public List<bool> Main1Right1DownEnemyFormationShip = new List<bool>();
    public List<bool> Main2Left1DownEnemyFormationShip = new List<bool>();
    public List<bool> Main2Right1DownEnemyFormationShip = new List<bool>();
    public List<bool> Main3Left1DownEnemyFormationShip = new List<bool>();
    public List<bool> Main3Right1DownEnemyFormationShip = new List<bool>();

    [Header("적 함대 기동 상태")]
    public List<Vector3> EnemyDestinationArea = new List<Vector3>(); //함대 목적지
    public List<float> EnemyWarpStartTime = new List<float>(); //워프 시작 시간
    public List<bool> EnemyWarpDriveActive = new List<bool>();

    [Header("적 함대 포탑 정보")]
    public List<int> EnemyCannonType = new List<int>(); //무기 타입
    public List<float> EnemyAttackTime = new List<float>(); //사격 시간 상태
    public List<int> EnemyFlagshipTurretDownList = new List<int>(); //무력화된 기함 함포 리스트
    public List<int> EnemyFormationTurretDownList = new List<int>(); //무력화된 편대함 함포 리스트

    void Awake()
    {
        if (Save1 == null)
            Save1 = this;
        else if (Save1 != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void GetData(SerializableBattleSave values)
    {
        BGMSliderValue = values.BGMSliderValue;
        UniverseSoundEffectSliderValue = values.UniverseSoundEffectSliderValue;
        BSSoundEffectSliderValue = values.BSSoundEffectSliderValue;

        NarihaUnionGlopaoros = values.NarihaUnionGlopaoros;
        NarihaUnionGlopaoroslimit = values.NarihaUnionGlopaoroslimit;
        ConstructionResource = values.ConstructionResource;
        ConstructionResourcelimit = values.ConstructionResourcelimit;
        Taritronic = values.Taritronic;

        SelectedshipNumber = values.SelectedshipNumber;
        FlagshipMode = values.FlagshipMode;
        LanguageType = values.LanguageType;

        FirstStart = values.FirstStart;
        FleetWeaponTutorial = values.FleetWeaponTutorial;
        FleetFormationTutorial = values.FleetFormationTutorial;
        FlagshipManagementTutorial = values.FlagshipManagementTutorial;
        LabTutorial = values.LabTutorial;
        MBCA79Tutorial = values.MBCA79Tutorial;

        MissionLevel = values.MissionLevel;
        MissionArea = values.MissionArea;
        MissionType = values.MissionType;
        MissionPlanetNumber = values.MissionPlanetNumber;
        FinishSpawnNumber = values.FinishSpawnNumber;
        SelectedMissionTableNumber = values.SelectedMissionTableNumber;
        MissionSuccessed = values.MissionSuccessed;
        FlagshipMissionSuccessed = values.FlagshipMissionSuccessed;

        RandomSiteTransform = values.RandomSiteTransform;
        RandomSiteFirstSpawn = values.RandomSiteFirstSpawn;
        RandomSiteNumber = values.RandomSiteNumber;

        PlanetNumber = values.PlanetNumber;
        FirstFreePlanet = values.FirstFreePlanet;
        BattleVictoryPlanet = values.BattleVictoryPlanet;

        NarihaFlagShipList = values.NarihaFlagShipList;
        EnemiesFlagShipList = values.EnemiesFlagShipList;
        EnemiesFormationShipList = values.EnemiesFormationShipList;

        CountOfFlagship = values.CountOfFlagship;
        CameraLocation = values.CameraLocation;
        FlagshipLocation = values.FlagshipLocation;
        FlagshipRotanion = values.FlagshipRotanion;

        FlagshipShieldDown = values.FlagshipShieldDown;
        FlagshipHull = values.FlagshipHull;
        FormationStorage = values.FormationStorage;
        FlagshipFormationsCodex = values.FlagshipFormationsCodex;

        FirstSkillType = values.FirstSkillType;
        FirstSkillNumber = values.FirstSkillNumber;
        SecondSkillType = values.SecondSkillType;
        SecondSkillNumber = values.SecondSkillNumber;
        ThirdSkillType = values.ThirdSkillType;
        ThirdSkillNumber = values.ThirdSkillNumber;

        Main1Left1HPFlagship = values.Main1Left1HPFlagship;
        Main1Left2HPFlagship = values.Main1Left2HPFlagship;
        Main1Right1HPFlagship = values.Main1Right1HPFlagship;
        Main1Right2HPFlagship = values.Main1Right2HPFlagship;
        Main2Left1HPFlagship = values.Main2Left1HPFlagship;
        Main2Left2HPFlagship = values.Main2Left2HPFlagship;
        Main2Right1HPFlagship = values.Main2Right1HPFlagship;
        Main2Right2HPFlagship = values.Main2Right2HPFlagship;
        Main3Left1HPFlagship = values.Main3Left1HPFlagship;
        Main3Right1HPFlagship = values.Main3Right1HPFlagship;

        Main1Left1DownFlagship = values.Main1Left1DownFlagship;
        Main1Left2DownFlagship = values.Main1Left2DownFlagship;
        Main1Right1DownFlagship = values.Main1Right1DownFlagship;
        Main1Right2DownFlagship = values.Main1Right2DownFlagship;
        Main2Left1DownFlagship = values.Main2Left1DownFlagship;
        Main2Left2DownFlagship = values.Main2Left2DownFlagship;
        Main2Right1DownFlagship = values.Main2Right1DownFlagship;
        Main2Right2DownFlagship = values.Main2Right2DownFlagship;
        Main3Left1DownFlagship = values.Main3Left1DownFlagship;
        Main3Right1DownFlagship = values.Main3Right1DownFlagship;

        CountOfFormationShip = values.CountOfFormationShip;
        FormationType = values.FormationType;

        FormationShipLocation = values.FormationShipLocation;
        FormationShipRotanion = values.FormationShipRotanion;

        FormationShieldDown = values.FormationShieldDown;
        FormationShipHull = values.FormationShipHull;

        Main1Left1HPFormationShip = values.Main1Left1HPFormationShip;
        Main1Right1HPFormationShip = values.Main1Right1HPFormationShip;
        Main2Left1HPFormationShip = values.Main2Left1HPFormationShip;
        Main2Right1HPFormationShip = values.Main2Right1HPFormationShip;
        Main3Left1HPFormationShip = values.Main3Left1HPFormationShip;
        Main3Right1HPFormationShip = values.Main3Right1HPFormationShip;

        Main1Left1DownFormationShip = values.Main1Left1DownFormationShip;
        Main1Right1DownFormationShip = values.Main1Right1DownFormationShip;
        Main2Left1DownFormationShip = values.Main2Left1DownFormationShip;
        Main2Right1DownFormationShip = values.Main2Right1DownFormationShip;
        Main3Left1DownFormationShip = values.Main3Left1DownFormationShip;
        Main3Right1DownFormationShip = values.Main3Right1DownFormationShip;

        DestinationArea = values.DestinationArea;
        WarpStartTime = values.WarpStartTime;
        WarpDriveReady = values.WarpDriveReady;
        WarpDriveActive = values.WarpDriveActive;

        CannonType = values.CannonType;
        AttackTime = values.AttackTime;
        FlagshipTurretDownList = values.FlagshipTurretDownList;
        FormationTurretDownList = values.FormationTurretDownList;

        EnemyIsBattleSite = values.EnemyIsBattleSite;
        EnemyBattleSiteNumber = values.EnemyBattleSiteNumber;

        SelectedEnemyFlagship = values.SelectedEnemyFlagship;
        CountOfEnemyFlagship = values.CountOfEnemyFlagship;
        EnemyNationType = values.EnemyNationType;
        EnemyFlagshipLocation = values.EnemyFlagshipLocation;
        EnemyFlagshipRotanion = values.EnemyFlagshipRotanion;

        EnemyFlagshipDown = values.EnemyFlagshipDown;
        EnemyFlagshipShieldDown = values.EnemyFlagshipShieldDown;
        EnemyFlagshipShield = values.EnemyFlagshipShield;
        EnemyFlagshipHull = values.EnemyFlagshipHull;

        Main1Left1HPEnemyFlagship = values.Main1Left1HPEnemyFlagship;
        Main1Left2HPEnemyFlagship = values.Main1Left2HPEnemyFlagship;
        Main1Right1HPEnemyFlagship = values.Main1Right1HPEnemyFlagship;
        Main1Right2HPEnemyFlagship = values.Main1Right2HPEnemyFlagship;
        Main2Left1HPEnemyFlagship = values.Main2Left1HPEnemyFlagship;
        Main2Left2HPEnemyFlagship = values.Main2Left2HPEnemyFlagship;
        Main2Right1HPEnemyFlagship = values.Main2Right1HPEnemyFlagship;
        Main2Right2HPEnemyFlagship = values.Main2Right2HPEnemyFlagship;
        Main3Left1HPEnemyFlagship = values.Main3Left1HPEnemyFlagship;
        Main3Right1HPEnemyFlagship = values.Main3Right1HPEnemyFlagship;

        Main1Left1DownEnemyFlagship = values.Main1Left1DownEnemyFlagship;
        Main1Left2DownEnemyFlagship = values.Main1Left2DownEnemyFlagship;
        Main1Right1DownEnemyFlagship = values.Main1Right1DownEnemyFlagship;
        Main1Right2DownEnemyFlagship = values.Main1Right2DownEnemyFlagship;
        Main2Left1DownEnemyFlagship = values.Main2Left1DownEnemyFlagship;
        Main2Left2DownEnemyFlagship = values.Main2Left2DownEnemyFlagship;
        Main2Right1DownEnemyFlagship = values.Main2Right1DownEnemyFlagship;
        Main2Right2DownEnemyFlagship = values.Main2Right2DownEnemyFlagship;
        Main3Left1DownEnemyFlagship = values.Main3Left1DownEnemyFlagship;
        Main3Right1DownEnemyFlagship = values.Main3Right1DownEnemyFlagship;

        CountOfEnemyFormationShip = values.CountOfEnemyFormationShip;
        EnemyFormationType = values.EnemyFormationType;

        EnemyFormationShipLocation = values.EnemyFormationShipLocation;
        EnemyFormationShipRotanion = values.EnemyFormationShipRotanion;

        EnemyFormationShieldDown = values.EnemyFormationShieldDown;
        EnemyFormationShield = values.EnemyFormationShield;
        EnemyFormationShipHull = values.EnemyFormationShipHull;

        Main1Left1HPEnemyFormationShip = values.Main1Left1HPEnemyFormationShip;
        Main1Right1HPEnemyFormationShip = values.Main1Right1HPEnemyFormationShip;
        Main2Left1HPEnemyFormationShip = values.Main2Left1HPEnemyFormationShip;
        Main2Right1HPEnemyFormationShip = values.Main2Right1HPEnemyFormationShip;
        Main3Left1HPEnemyFormationShip = values.Main3Left1HPEnemyFormationShip;
        Main3Right1HPEnemyFormationShip = values.Main3Right1HPEnemyFormationShip;

        Main1Left1DownEnemyFormationShip = values.Main1Left1DownEnemyFormationShip;
        Main1Right1DownEnemyFormationShip = values.Main1Right1DownEnemyFormationShip;
        Main2Left1DownEnemyFormationShip = values.Main2Left1DownEnemyFormationShip;
        Main2Right1DownEnemyFormationShip = values.Main2Right1DownEnemyFormationShip;
        Main3Left1DownEnemyFormationShip = values.Main3Left1DownEnemyFormationShip;
        Main3Right1DownEnemyFormationShip = values.Main3Right1DownEnemyFormationShip;

        EnemyDestinationArea = values.EnemyDestinationArea;
        EnemyWarpStartTime = values.EnemyWarpStartTime;
        EnemyWarpDriveActive = values.EnemyWarpDriveActive;

        EnemyCannonType = values.EnemyCannonType;
        EnemyAttackTime = values.EnemyAttackTime;
        EnemyFlagshipTurretDownList = values.EnemyFlagshipTurretDownList;
        EnemyFormationTurretDownList = values.EnemyFormationTurretDownList;
    }

    public SerializableBattleSave GetSerializable()
    {
        var output = new SerializableBattleSave();

        output.BGMSliderValue = this.BGMSliderValue;
        output.UniverseSoundEffectSliderValue = this.UniverseSoundEffectSliderValue;
        output.BSSoundEffectSliderValue = this.BSSoundEffectSliderValue;

        output.NarihaUnionGlopaoros = this.NarihaUnionGlopaoros;
        output.NarihaUnionGlopaoroslimit = this.NarihaUnionGlopaoroslimit;
        output.ConstructionResource = this.ConstructionResource;
        output.ConstructionResourcelimit = this.ConstructionResourcelimit;
        output.Taritronic = this.Taritronic;

        output.SelectedshipNumber = this.SelectedshipNumber;
        output.FlagshipMode = this.FlagshipMode;
        output.LanguageType = this.LanguageType;

        output.FirstStart = this.FirstStart;
        output.FleetWeaponTutorial = this.FleetWeaponTutorial;
        output.FleetFormationTutorial = this.FleetFormationTutorial;
        output.FlagshipManagementTutorial = this.FlagshipManagementTutorial;
        output.LabTutorial = this.LabTutorial;
        output.MBCA79Tutorial = this.MBCA79Tutorial;

        output.MissionLevel = this.MissionLevel;
        output.MissionArea = this.MissionArea;
        output.MissionType = this.MissionType;
        output.MissionPlanetNumber = this.MissionPlanetNumber;
        output.FinishSpawnNumber = this.FinishSpawnNumber;
        output.SelectedMissionTableNumber = this.SelectedMissionTableNumber;
        output.MissionSuccessed = this.MissionSuccessed;
        output.FlagshipMissionSuccessed = this.FlagshipMissionSuccessed;

        output.RandomSiteTransform = this.RandomSiteTransform;
        output.RandomSiteFirstSpawn = this.RandomSiteFirstSpawn;
        output.RandomSiteNumber = this.RandomSiteNumber;

        output.PlanetNumber = this.PlanetNumber;
        output.FirstFreePlanet = this.FirstFreePlanet;
        output.BattleVictoryPlanet = this.BattleVictoryPlanet;

        output.NarihaFlagShipList = this.NarihaFlagShipList;
        output.EnemiesFlagShipList = this.EnemiesFlagShipList;
        output.EnemiesFormationShipList = this.EnemiesFormationShipList;

        output.CountOfFlagship = this.CountOfFlagship;
        output.CameraLocation = this.CameraLocation;
        output.FlagshipLocation = this.FlagshipLocation;
        output.FlagshipRotanion = this.FlagshipRotanion;

        output.FlagshipShieldDown = this.FlagshipShieldDown;
        output.FlagshipHull = this.FlagshipHull;
        output.FormationStorage = this.FormationStorage;
        output.FlagshipFormationsCodex = this.FlagshipFormationsCodex;

        output.FirstSkillType = this.FirstSkillType;
        output.FirstSkillNumber = this.FirstSkillNumber;
        output.SecondSkillType = this.SecondSkillType;
        output.SecondSkillNumber = this.SecondSkillNumber;
        output.ThirdSkillType = this.ThirdSkillType;
        output.ThirdSkillNumber = this.ThirdSkillNumber;

        output.Main1Left1HPFlagship = this.Main1Left1HPFlagship;
        output.Main1Left2HPFlagship = this.Main1Left2HPFlagship;
        output.Main1Right1HPFlagship = this.Main1Right1HPFlagship;
        output.Main1Right2HPFlagship = this.Main1Right2HPFlagship;
        output.Main2Left1HPFlagship = this.Main2Left1HPFlagship;
        output.Main2Left2HPFlagship = this.Main2Left2HPFlagship;
        output.Main2Right1HPFlagship = this.Main2Right1HPFlagship;
        output.Main2Right2HPFlagship = this.Main2Right2HPFlagship;
        output.Main3Left1HPFlagship = this.Main3Left1HPFlagship;
        output.Main3Right1HPFlagship = this.Main3Right1HPFlagship;

        output.Main1Left1DownFlagship = this.Main1Left1DownFlagship;
        output.Main1Left2DownFlagship = this.Main1Left2DownFlagship;
        output.Main1Right1DownFlagship = this.Main1Right1DownFlagship;
        output.Main1Right2DownFlagship = this.Main1Right2DownFlagship;
        output.Main2Left1DownFlagship = this.Main2Left1DownFlagship;
        output.Main2Left2DownFlagship = this.Main2Left2DownFlagship;
        output.Main2Right1DownFlagship = this.Main2Right1DownFlagship;
        output.Main2Right2DownFlagship = this.Main2Right2DownFlagship;
        output.Main3Left1DownFlagship = this.Main3Left1DownFlagship;
        output.Main3Right1DownFlagship = this.Main3Right1DownFlagship;

        output.CountOfFormationShip = this.CountOfFormationShip;
        output.FormationType = this.FormationType;

        output.FormationShipLocation = this.FormationShipLocation;
        output.FormationShipRotanion = this.FormationShipRotanion;

        output.FormationShieldDown = this.FormationShieldDown;
        output.FormationShipHull = this.FormationShipHull;

        output.Main1Left1HPFormationShip = this.Main1Left1HPFormationShip;
        output.Main1Right1HPFormationShip = this.Main1Right1HPFormationShip;
        output.Main2Left1HPFormationShip = this.Main2Left1HPFormationShip;
        output.Main2Right1HPFormationShip = this.Main2Right1HPFormationShip;
        output.Main3Left1HPFormationShip = this.Main3Left1HPFormationShip;
        output.Main3Right1HPFormationShip = this.Main3Right1HPFormationShip;

        output.Main1Left1DownFormationShip = this.Main1Left1DownFormationShip;
        output.Main1Right1DownFormationShip = this.Main1Right1DownFormationShip;
        output.Main2Left1DownFormationShip = this.Main2Left1DownFormationShip;
        output.Main2Right1DownFormationShip = this.Main2Right1DownFormationShip;
        output.Main3Left1DownFormationShip = this.Main3Left1DownFormationShip;
        output.Main3Right1DownFormationShip = this.Main3Right1DownFormationShip;

        output.DestinationArea = this.DestinationArea;
        output.WarpStartTime = this.WarpStartTime;
        output.WarpDriveReady = this.WarpDriveReady;
        output.WarpDriveActive = this.WarpDriveActive;

        output.CannonType = this.CannonType;
        output.AttackTime = this.AttackTime;
        output.FlagshipTurretDownList = this.FlagshipTurretDownList;
        output.FormationTurretDownList = this.FormationTurretDownList;

        output.EnemyIsBattleSite = this.EnemyIsBattleSite;
        output.EnemyBattleSiteNumber = this.EnemyBattleSiteNumber;

        output.SelectedEnemyFlagship = this.SelectedEnemyFlagship;
        output.CountOfEnemyFlagship = this.CountOfEnemyFlagship;
        output.EnemyNationType = this.EnemyNationType;
        output.EnemyFlagshipLocation = this.EnemyFlagshipLocation;
        output.EnemyFlagshipRotanion = this.EnemyFlagshipRotanion;

        output.EnemyFlagshipDown = this.EnemyFlagshipDown;
        output.EnemyFlagshipShieldDown = this.EnemyFlagshipShieldDown;
        output.EnemyFlagshipShield = this.EnemyFlagshipShield;
        output.EnemyFlagshipHull = this.EnemyFlagshipHull;

        output.Main1Left1HPEnemyFlagship = this.Main1Left1HPEnemyFlagship;
        output.Main1Left2HPEnemyFlagship = this.Main1Left2HPEnemyFlagship;
        output.Main1Right1HPEnemyFlagship = this.Main1Right1HPEnemyFlagship;
        output.Main1Right2HPEnemyFlagship = this.Main1Right2HPEnemyFlagship;
        output.Main2Left1HPEnemyFlagship = this.Main2Left1HPEnemyFlagship;
        output.Main2Left2HPEnemyFlagship = this.Main2Left2HPEnemyFlagship;
        output.Main2Right1HPEnemyFlagship = this.Main2Right1HPEnemyFlagship;
        output.Main2Right2HPEnemyFlagship = this.Main2Right2HPEnemyFlagship;
        output.Main3Left1HPEnemyFlagship = this.Main3Left1HPEnemyFlagship;
        output.Main3Right1HPEnemyFlagship = this.Main3Right1HPEnemyFlagship;

        output.Main1Left1DownEnemyFlagship = this.Main1Left1DownEnemyFlagship;
        output.Main1Left2DownEnemyFlagship = this.Main1Left2DownEnemyFlagship;
        output.Main1Right1DownEnemyFlagship = this.Main1Right1DownEnemyFlagship;
        output.Main1Right2DownEnemyFlagship = this.Main1Right2DownEnemyFlagship;
        output.Main2Left1DownEnemyFlagship = this.Main2Left1DownEnemyFlagship;
        output.Main2Left2DownEnemyFlagship = this.Main2Left2DownEnemyFlagship;
        output.Main2Right1DownEnemyFlagship = this.Main2Right1DownEnemyFlagship;
        output.Main2Right2DownEnemyFlagship = this.Main2Right2DownEnemyFlagship;
        output.Main3Left1DownEnemyFlagship = this.Main3Left1DownEnemyFlagship;
        output.Main3Right1DownEnemyFlagship = this.Main3Right1DownEnemyFlagship;

        output.CountOfEnemyFormationShip = this.CountOfEnemyFormationShip;
        output.EnemyFormationType = this.EnemyFormationType;

        output.EnemyFormationShipLocation = this.EnemyFormationShipLocation;
        output.EnemyFormationShipRotanion = this.EnemyFormationShipRotanion;

        output.EnemyFormationShieldDown = this.EnemyFormationShieldDown;
        output.EnemyFormationShield = this.EnemyFormationShield;
        output.EnemyFormationShipHull = this.EnemyFormationShipHull;

        output.Main1Left1HPEnemyFormationShip = this.Main1Left1HPEnemyFormationShip;
        output.Main1Right1HPEnemyFormationShip = this.Main1Right1HPEnemyFormationShip;
        output.Main2Left1HPEnemyFormationShip = this.Main2Left1HPEnemyFormationShip;
        output.Main2Right1HPEnemyFormationShip = this.Main2Right1HPEnemyFormationShip;
        output.Main3Left1HPEnemyFormationShip = this.Main3Left1HPEnemyFormationShip;
        output.Main3Right1HPEnemyFormationShip = this.Main3Right1HPEnemyFormationShip;

        output.Main1Left1DownEnemyFormationShip = this.Main1Left1DownEnemyFormationShip;
        output.Main1Right1DownEnemyFormationShip = this.Main1Right1DownEnemyFormationShip;
        output.Main2Left1DownEnemyFormationShip = this.Main2Left1DownEnemyFormationShip;
        output.Main2Right1DownEnemyFormationShip = this.Main2Right1DownEnemyFormationShip;
        output.Main3Left1DownEnemyFormationShip = this.Main3Left1DownEnemyFormationShip;
        output.Main3Right1DownEnemyFormationShip = this.Main3Right1DownEnemyFormationShip;

        output.EnemyDestinationArea = this.EnemyDestinationArea;
        output.EnemyWarpStartTime = this.EnemyWarpStartTime;
        output.EnemyWarpDriveActive = this.EnemyWarpDriveActive;

        output.EnemyCannonType = this.EnemyCannonType;
        output.EnemyAttackTime = this.EnemyAttackTime;
        output.EnemyFlagshipTurretDownList = this.EnemyFlagshipTurretDownList;
        output.EnemyFormationTurretDownList = this.EnemyFormationTurretDownList;

        return output;
    }
}

[Serializable]
public class SerializableBattleSave
{
    [Header("설정")]
    public float BGMSliderValue;
    public float UniverseSoundEffectSliderValue;
    public float BSSoundEffectSliderValue;

    [Header("자원 목록")]
    public float NarihaUnionGlopaoros; //나리하 인류연합 화폐 Glopaoros
    public float NarihaUnionGlopaoroslimit; //Glopaoros 한도량
    public float ConstructionResource; //건설 재료
    public float ConstructionResourcelimit; //건설 재료 한도량
    public float Taritronic; //타리트로닉 연구재료

    [Header("기타")]
    public int SelectedshipNumber; //선택된 기함
    public bool FlagshipMode = false; //기함모드 여부
    public int LanguageType; //언어

    [Header("튜토리얼")]
    public bool FirstStart = false; //게임을 최초로 시작할 때에만 발동하는 튜토리얼(함대전 이동~사타리우스 글래시아 해방)
    public bool FleetWeaponTutorial = false;
    public bool FleetFormationTutorial = false;
    public bool FlagshipManagementTutorial = false;
    public bool LabTutorial = false;
    public bool MBCA79Tutorial = false;

    [Header("미션 데이터")]
    public int MissionLevel;
    public int MissionArea;
    public int MissionType;
    public int MissionPlanetNumber; //미션을 시작한 행성 및 항성이나 지정된 지역.
    public int FinishSpawnNumber;
    public int SelectedMissionTableNumber; //선택한 미션 테이블 번호. 완료 후, 해당 테이블의 미션이 사라지도록 조취
    public bool MissionSuccessed; //미션이 성공했는지에 대한 여부.
    public int FlagshipMissionSuccessed; //기함 침투전 미션 성공 여부. 1 = 기함 무력화 성공, 2 = 기함 폭파 성공

    [Header("사이트")]
    public List<Vector3> RandomSiteTransform = new List<Vector3>();
    public List<bool> RandomSiteFirstSpawn = new List<bool>();
    public List<int> RandomSiteNumber = new List<int>();

    [Header("행성")]
    public List<int> PlanetNumber = new List<int>(); //행성 번호
    public List<bool> FirstFreePlanet = new List<bool>(); //해방된 행성 목록
    public List<bool> BattleVictoryPlanet = new List<bool>(); //전투에서 승전한 행성 목록

    [Header("함선 목록")]
    public int NarihaFlagShipList; //나리하 인류연합 기함 목록
    public int EnemiesFlagShipList; //적 모든 기함 목록
    public int EnemiesFormationShipList; //적 모든 편대함 목록

    [Header("나리하 기함 위치")]
    public int CountOfFlagship;
    public Vector3 CameraLocation;
    public List<Vector3> FlagshipLocation = new List<Vector3>();
    public List<Quaternion> FlagshipRotanion = new List<Quaternion>();

    [Header("나리하 기함 선체 상태")]
    public List<bool> FlagshipShieldDown = new List<bool>();
    public List<float> FlagshipHull = new List<float>();
    public List<int> FormationStorage = new List<int>(); //기함 당 총 편대수
    public List<Vector3> FlagshipFormationsCodex = new List<Vector3>(); //기함당 편대함선들의 배열 위치 좌표

    [Header("나리하 스킬 상태")]
    public List<int> FirstSkillType = new List<int>();
    public List<int> FirstSkillNumber = new List<int>();
    public List<int> SecondSkillType = new List<int>();
    public List<int> SecondSkillNumber = new List<int>();
    public List<int> ThirdSkillType = new List<int>();
    public List<int> ThirdSkillNumber = new List<int>();

    [Header("나리하 기함 부위별 체력")]
    public List<float> Main1Left1HPFlagship = new List<float>();
    public List<float> Main1Left2HPFlagship = new List<float>();
    public List<float> Main1Right1HPFlagship = new List<float>();
    public List<float> Main1Right2HPFlagship = new List<float>();
    public List<float> Main2Left1HPFlagship = new List<float>();
    public List<float> Main2Left2HPFlagship = new List<float>();
    public List<float> Main2Right1HPFlagship = new List<float>();
    public List<float> Main2Right2HPFlagship = new List<float>();
    public List<float> Main3Left1HPFlagship = new List<float>();
    public List<float> Main3Right1HPFlagship = new List<float>();

    [Header("나리하 기함 부위 무력화 여부")]
    public List<bool> Main1Left1DownFlagship = new List<bool>();
    public List<bool> Main1Left2DownFlagship = new List<bool>();
    public List<bool> Main1Right1DownFlagship = new List<bool>();
    public List<bool> Main1Right2DownFlagship = new List<bool>();
    public List<bool> Main2Left1DownFlagship = new List<bool>();
    public List<bool> Main2Left2DownFlagship = new List<bool>();
    public List<bool> Main2Right1DownFlagship = new List<bool>();
    public List<bool> Main2Right2DownFlagship = new List<bool>();
    public List<bool> Main3Left1DownFlagship = new List<bool>();
    public List<bool> Main3Right1DownFlagship = new List<bool>();

    [Header("편대 함선 정보")]
    public List<int> CountOfFormationShip = new List<int>(); //기함별 보유 함선 수
    public List<int> FormationType = new List<int>(); //기함별 보유한 함선의 각 유형

    [Header("편대 함선 위치")]
    public List<Vector3> FormationShipLocation = new List<Vector3>();
    public List<Quaternion> FormationShipRotanion = new List<Quaternion>();

    [Header("편대 함선 선체 상태")]
    public List<bool> FormationShieldDown = new List<bool>();
    public List<float> FormationShipHull = new List<float>();

    [Header("편대 함선 부위별 체력")]
    public List<float> Main1Left1HPFormationShip = new List<float>();
    public List<float> Main1Right1HPFormationShip = new List<float>();
    public List<float> Main2Left1HPFormationShip = new List<float>();
    public List<float> Main2Right1HPFormationShip = new List<float>();
    public List<float> Main3Left1HPFormationShip = new List<float>();
    public List<float> Main3Right1HPFormationShip = new List<float>();

    [Header("편대 함선 부위 무력화 여부")]
    public List<bool> Main1Left1DownFormationShip = new List<bool>();
    public List<bool> Main1Right1DownFormationShip = new List<bool>();
    public List<bool> Main2Left1DownFormationShip = new List<bool>();
    public List<bool> Main2Right1DownFormationShip = new List<bool>();
    public List<bool> Main3Left1DownFormationShip = new List<bool>();
    public List<bool> Main3Right1DownFormationShip = new List<bool>();

    [Header("함대 기동 상태")]
    public List<Vector3> DestinationArea = new List<Vector3>(); //함대 목적지
    public List<float> WarpStartTime = new List<float>(); //워프 시작 시간
    public List<bool> WarpDriveReady = new List<bool>();
    public List<bool> WarpDriveActive = new List<bool>();

    [Header("함대 포탑 정보")]
    public List<int> CannonType = new List<int>(); //무기 타입
    public List<float> AttackTime = new List<float>(); //사격 시간 상태
    public List<int> FlagshipTurretDownList = new List<int>(); //무력화된 기함 함포 리스트
    public List<int> FormationTurretDownList = new List<int>(); //무력화된 편대함 함포 리스트

    [Header("적 함대 상태")]
    public List<bool> EnemyIsBattleSite = new List<bool>(); //전투 지역에 있는지
    public List<int> EnemyBattleSiteNumber = new List<int>(); //해당 소속된 전투 지역 번호

    [Header("적 기함 위치")]
    public List<bool> SelectedEnemyFlagship = new List<bool>(); //보병전에 선택된 기함
    public int CountOfEnemyFlagship;
    public List<int> EnemyNationType = new List<int>(); //적 종족별 구분, 1 = 나리하 인류연합 아군, 2 = 슬로리어스, 3 = 칸타크리
    public List<Vector3> EnemyFlagshipLocation = new List<Vector3>();
    public List<Quaternion> EnemyFlagshipRotanion = new List<Quaternion>();

    [Header("적 기함 선체 상태")]
    public List<bool> EnemyFlagshipDown = new List<bool>(); //기함이 침투전으로 인해 무력화 되었을 경우
    public List<bool> EnemyFlagshipShieldDown = new List<bool>();
    public List<float> EnemyFlagshipShield = new List<float>();
    public List<float> EnemyFlagshipHull = new List<float>();

    [Header("적 기함 부위별 체력")]
    public List<float> Main1Left1HPEnemyFlagship = new List<float>();
    public List<float> Main1Left2HPEnemyFlagship = new List<float>();
    public List<float> Main1Right1HPEnemyFlagship = new List<float>();
    public List<float> Main1Right2HPEnemyFlagship = new List<float>();
    public List<float> Main2Left1HPEnemyFlagship = new List<float>();
    public List<float> Main2Left2HPEnemyFlagship = new List<float>();
    public List<float> Main2Right1HPEnemyFlagship = new List<float>();
    public List<float> Main2Right2HPEnemyFlagship = new List<float>();
    public List<float> Main3Left1HPEnemyFlagship = new List<float>();
    public List<float> Main3Right1HPEnemyFlagship = new List<float>();

    [Header("적 기함 부위 무력화 여부")]
    public List<bool> Main1Left1DownEnemyFlagship = new List<bool>();
    public List<bool> Main1Left2DownEnemyFlagship = new List<bool>();
    public List<bool> Main1Right1DownEnemyFlagship = new List<bool>();
    public List<bool> Main1Right2DownEnemyFlagship = new List<bool>();
    public List<bool> Main2Left1DownEnemyFlagship = new List<bool>();
    public List<bool> Main2Left2DownEnemyFlagship = new List<bool>();
    public List<bool> Main2Right1DownEnemyFlagship = new List<bool>();
    public List<bool> Main2Right2DownEnemyFlagship = new List<bool>();
    public List<bool> Main3Left1DownEnemyFlagship = new List<bool>();
    public List<bool> Main3Right1DownEnemyFlagship = new List<bool>();

    [Header("적 편대 함선 정보")]
    public List<int> CountOfEnemyFormationShip = new List<int>(); //기함별 보유 함선 수
    public List<int> EnemyFormationType = new List<int>(); //기함별 보유한 함선의 각 유형

    [Header("적 편대 함선 위치")]
    public List<Vector3> EnemyFormationShipLocation = new List<Vector3>();
    public List<Quaternion> EnemyFormationShipRotanion = new List<Quaternion>();

    [Header("적 편대 함선 선체 상태")]
    public List<bool> EnemyFormationShieldDown = new List<bool>();
    public List<float> EnemyFormationShield = new List<float>();
    public List<float> EnemyFormationShipHull = new List<float>();

    [Header("적 편대 함선 부위별 체력")]
    public List<float> Main1Left1HPEnemyFormationShip = new List<float>();
    public List<float> Main1Right1HPEnemyFormationShip = new List<float>();
    public List<float> Main2Left1HPEnemyFormationShip = new List<float>();
    public List<float> Main2Right1HPEnemyFormationShip = new List<float>();
    public List<float> Main3Left1HPEnemyFormationShip = new List<float>();
    public List<float> Main3Right1HPEnemyFormationShip = new List<float>();

    [Header("적 편대 함선 부위 무력화 여부")]
    public List<bool> Main1Left1DownEnemyFormationShip = new List<bool>();
    public List<bool> Main1Right1DownEnemyFormationShip = new List<bool>();
    public List<bool> Main2Left1DownEnemyFormationShip = new List<bool>();
    public List<bool> Main2Right1DownEnemyFormationShip = new List<bool>();
    public List<bool> Main3Left1DownEnemyFormationShip = new List<bool>();
    public List<bool> Main3Right1DownEnemyFormationShip = new List<bool>();

    [Header("적 함대 기동 상태")]
    public List<Vector3> EnemyDestinationArea = new List<Vector3>(); //함대 목적지
    public List<float> EnemyWarpStartTime = new List<float>(); //워프 시작 시간
    public List<bool> EnemyWarpDriveActive = new List<bool>();

    [Header("적 함대 포탑 정보")]
    public List<int> EnemyCannonType = new List<int>(); //무기 타입
    public List<float> EnemyAttackTime = new List<float>(); //사격 시간 상태
    public List<int> EnemyFlagshipTurretDownList = new List<int>(); //무력화된 기함 함포 리스트
    public List<int> EnemyFormationTurretDownList = new List<int>(); //무력화된 편대함 함포 리스트
}