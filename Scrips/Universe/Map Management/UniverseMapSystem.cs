using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class UniverseMapSystem : MonoBehaviour
{
    [Header("스크립트")]
    public CameraZoom CameraZoom;
    public CameraFollow CameraFollow;
    public RTSControlSystem RTSControlSystem;
    public UIControlSystem UIControlSystem;
    public WordPrintSystem WordPrintSystem;
    public MultiFlagshipSystem MultiFlagshipSystem;
    public HurricaneOperationMenu HurricaneOperationMenu;
    public MainMenuButtonSystem MainMenuButtonSystem;
    public TutorialSystem TutorialSystem;
    public LiveCommunicationSystem LiveCommunicationSystem;
    AreaStatement AreaStatement;

    [Header("에니메이션")]
    //부팅창
    public GameObject MenuBooting; //메뉴 부팅 애니메이션
    public GameObject MenuWallPrefab; //화면전환용 프리팹
    public GameObject MenuBackground; //메뉴 백그라운드 이미지 활성화
    public GameObject WallEffectUCCIS; //UCCIS메뉴 화면전환 애니메이션
    public Image UniverseMapButtonImage; //메뉴 부팅하는 시간 동안 UniverseMapButtonImage 터치 무력화

    //우주맵
    public GameObject UniverseMapPrefab; //우주맵 활성화에만 프리팹 켜기, 애니메이션 출력용
    public GameObject UniverseMapUI;

    //우주맵 진행바
    public GameObject UniverseProgressBarUI;
    public GameObject UniverseProgressBarUIActive; //UniverseProgressBarUI를 온오프하기 위한 용도
    public GameObject UniverseProgressBarButton;
    public GameObject UniverseProgressBarButtonActive; //UniverseProgressBarButton를 온오프하기 위한 용도
    public GameObject ShipSelectedIcon; //함선 선택 시 아이콘 활성화
    public bool UniverseMapMode = false; //우주지도 모드
    public bool UniverseMapCompleteOff = false; //우주지도 모드가 종료될 때 완전히 종료됨을 알리는 스위치(CameraFollow스크립트의 CameraZoom.CameraInitialize() 메서드를 우주맵이 완전히 종료되었을 때부터 활성화하기 위한 목적)
    public bool ClickMapMode = false;
    public bool ClickUniverseProgressBarButton = false;

    Coroutine MapOnline;
    Coroutine MapOffline;

    public GameObject LiveCommunication; //실시간 자막 및 워프 항법 자막

    [Header("UI 파티클 이펙트")]
    public GameObject UIEffect;
    public GameObject TeleEffect1; //행성 선택 이후 노멀 주파수 이펙트
    public GameObject TeleEffect2; //함선 선택시, 지역으로부터 주파수발신 이펙트
    public GameObject TeleEffect3; //워프 진행직후에 발생하는 주파수 이펙트

    [Header("미니맵 지도 조작")]
    public float MapZoom;

    [Header("미니맵 워프 절차")]
    public int WarpStep = 0;
    public Vector3 Destination; //최종 워프 장소
    public Vector3 DestinationFollow; //Destination 주변 워프 장소
    private Vector3 WarpDestination; //목표 장소를 저장하기 위한 지역
    private float MinusOfCenter; //워프 지역의 중심으로부터 떨어진 거리

    [Header("미니맵 UI 라인")]
    private RectTransform MapDestination; //함선 선택시 라인의 목적지 좌표

    [Header("참조")]
    public RectTransform MiniMapPoint1; //UI맵 우측 하단 모서리 위치
    public RectTransform MiniMapPoint2; //UI맵 좌측 상단 모서리 위치
    public Transform WorldMapPoint1; //실제맵 우측 하단 모서리 위치
    public Transform WorldMapPoint2; //실제맵 좌측 상단 모서리 위치

    [Header("미니맵 UI")]
    public GameObject TouchAreaUI; //우주지도에서 UI선택시 나타나는 UI
    public GameObject ShowUI; //지역 설명창

    [Header("함대 프리팹 목록")]
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject Player3Prefab;
    public GameObject Player4Prefab;
    public GameObject Player5Prefab;

    [Header("함대 위치 목록")]
    public bool StartMapping = false;
    public RectTransform Player1; //미니맵 위치
    public RectTransform Player2;
    public RectTransform Player3;
    public RectTransform Player4;
    public RectTransform Player5;
    public Transform WorldPlayer1; //실제 위치
    public Transform WorldPlayer2;
    public Transform WorldPlayer3;
    public Transform WorldPlayer4;
    public Transform WorldPlayer5;

    [Header("함대 버튼 및 소요 시간")]
    public Image Player1Image;
    public Image Player2Image;
    public Image Player3Image;
    public Image Player4Image;
    public Image Player5Image;
    float timeTaken; //워프 소요시간 계산

    [Header("미니맵 위치 항성 목록")]
    public RectTransform ToropioStar;
    public RectTransform Roro1Star; //Roro 항성계
    public RectTransform Roro2Star; //Roro 항성계
    public RectTransform SarisiStar;
    public RectTransform GarixStar;
    public RectTransform OctoKrasisPatoroSystemOrbit; //OctoKrasis Patoro 항성계 궤도
    public RectTransform SecrosStar; //OctoKrasis Patoro 항성계
    public RectTransform TeretosStar; //OctoKrasis Patoro 항성계
    public RectTransform MiniPopoStar; //OctoKrasis Patoro 항성계
    public RectTransform DeltaD31_402054SystemOrbit; //Delta D31-402054 항성계 궤도
    public RectTransform DeltaD31_4AStar; //Delta D31-402054 항성계
    public RectTransform DeltaD31_4BStar; //Delta D31-402054 항성계
    public RectTransform JeratoO95_99024SystemOrbit; //Jerato O95-99024 항성계 궤도
    public RectTransform JeratoO95_7Orbit;
    public RectTransform JeratoO95_7AStar; //Jerato O95-99024 항성계
    public RectTransform JeratoO95_7BStar; //Jerato O95-99024 항성계
    public RectTransform JeratoO95_14Orbit;
    public RectTransform JeratoO95_14CStar; //Jerato O95-99024 항성계
    public RectTransform JeratoO95_14DStar; //Jerato O95-99024 항성계
    public RectTransform JeratoO95_OmegaStar; //Jerato O95-99024 항성계

    [Header("미니맵 위치 행성 목록")]
    public RectTransform SatariusGlessia; //Toropio 항성계
    public RectTransform Aposis; //Toropio 항성계
    public RectTransform Torono; //Toropio 항성계
    public RectTransform Plopa2; //Toropio 항성계
    public RectTransform Vedes4; //Toropio 항성계
    public RectTransform AronPeri; //Roro 항성계
    public RectTransform Papatus2; //Roro 항성계
    public RectTransform Papatus3; //Roro 항성계
    public RectTransform Kyepotoros; //Roro 항성계
    public RectTransform Tratos; //Sarisi 항성계
    public RectTransform Oclasis; //Sarisi 항성계
    public RectTransform DeriousHeri; //Sarisi 항성계
    public RectTransform Veltrorexy; //Garix 항성계
    public RectTransform ErixJeoqeta; //Garix 항성계
    public RectTransform Qeepo; //Garix 항성계
    public RectTransform CrownYosere; //Garix 항성계
    public RectTransform Oros; //OctoKrasis Patoro 항성계
    public RectTransform JapetAgrone; //OctoKrasis Patoro 항성계
    public RectTransform Xacro042351; //OctoKrasis Patoro 항성계
    public RectTransform DeltaD31_2208; //Delta D31-402054 항성계
    public RectTransform DeltaD31_9523; //Delta D31-402054 항성계
    public RectTransform DeltaD31_12721; //Delta D31-402054 항성계
    public RectTransform JeratoO95_1125; //Jerato O95-99024 항성계
    public RectTransform JeratoO95_2252; //Jerato O95-99024 항성계
    public RectTransform JeratoO95_8510; //Jerato O95-99024 항성계

    [Header("랜덤 사이트 목록")]
    public RectTransform ToropioRandomSite1;
    public RectTransform ToropioRandomSite2;
    public RectTransform RoroRandomSite1;
    public RectTransform RoroRandomSite2;
    public RectTransform RoroRandomSite3;
    public RectTransform SarisiRandomSite1;
    public RectTransform SarisiRandomSite2;
    public RectTransform SarisiRandomSite3;
    public RectTransform GarixRandomSite1;
    public RectTransform GarixRandomSite2;
    public RectTransform GarixRandomSite3;
    public RectTransform OctoKrasisPatoroRandomSite1;
    public RectTransform OctoKrasisPatoroRandomSite2;
    public RectTransform OctoKrasisPatoroRandomSite3;
    public RectTransform OctoKrasisPatoroRandomSite4;
    public RectTransform DeltaD31_402054RandomSite1;
    public RectTransform DeltaD31_402054RandomSite2;
    public RectTransform DeltaD31_402054RandomSite3;
    public RectTransform DeltaD31_402054RandomSite4;
    public RectTransform DeltaD31_402054RandomSite5;
    public RectTransform JeratoO95_99024RandomSite1;
    public RectTransform JeratoO95_99024RandomSite2;
    public RectTransform JeratoO95_99024RandomSite3;
    public RectTransform JeratoO95_99024RandomSite4;
    public RectTransform JeratoO95_99024RandomSite5;

    [Header("미니맵 UI 항성 아이콘 클릭 활성화")]
    public GameObject ToropioStarPrefab;
    public GameObject Roro1StarPrefab;
    public GameObject Roro2StarPrefab;
    public GameObject SarisiStarPrefab;
    public GameObject GarixStarPrefab;
    public GameObject SecrosStarPrefab;
    public GameObject TeretosStarPrefab;
    public GameObject MiniPopoStarPrefab;
    public GameObject DeltaD31_4AStarPrefab;
    public GameObject DeltaD31_4BStarPrefab;
    public GameObject JeratoO95_7AStarPrefab;
    public GameObject JeratoO95_7BStarPrefab;
    public GameObject JeratoO95_14CStarPrefab;
    public GameObject JeratoO95_14DStarPrefab;
    public GameObject JeratoO95_OmegaStarPrefab;

    [Header("미니맵 UI 항성계 궤도 아이콘 클릭 활성화")]
    public GameObject ToropioStarOrbitPrefab;
    public GameObject RoroSystemOrbitPrefab;
    public GameObject SarisiStarOrbitPrefab;
    public GameObject GarixStarOrbitPrefab;
    public GameObject OctoKrasisPatoroSystemOrbitPrefab;
    public GameObject DeltaD31_402054SystemOrbitPrefab;
    public GameObject DeltaD31_4AStarOrbitPrefab;
    public GameObject DeltaD31_4BStarOrbitPrefab;
    public GameObject JeratoO95_99024SystemOrbitPrefab;
    public GameObject JeratoO95_7OrbitPrefab;
    public GameObject JeratoO95_14OrbitPrefab;

    [Header("미니맵 UI 행성 아이콘 클릭 활성화")]
    public GameObject SatariusGlessiaPrefab;
    public GameObject AposisPrefab;
    public GameObject ToronoPrefab;
    public GameObject Plopa2Prefab;
    public GameObject Vedes4Prefab;
    public GameObject AronPeriPrefab;
    public GameObject Papatus2Prefab;
    public GameObject Papatus3Prefab;
    public GameObject KyepotorosPrefab;
    public GameObject TratosPrefab;
    public GameObject OclasisPrefab;
    public GameObject DeriousHeriPrefab;
    public GameObject VeltrorexyPrefab;
    public GameObject ErixJeoqetaPrefab;
    public GameObject QeepoPrefab;
    public GameObject CrownYoserePrefab;
    public GameObject OrosPrefab;
    public GameObject JapetAgronePrefab;
    public GameObject Xacro042351Prefab;
    public GameObject DeltaD31_2208Prefab;
    public GameObject DeltaD31_9523Prefab;
    public GameObject DeltaD31_12721Prefab;
    public GameObject JeratoO95_1125Prefab;
    public GameObject JeratoO95_2252Prefab;
    public GameObject JeratoO95_8510Prefab;

    [Header("미니맵 UI 랜덤 사이트 아이콘 클릭 활성화")]
    public GameObject ToropioRandomSite1Prefab;
    public GameObject ToropioRandomSite2Prefab;
    public GameObject RoroRandomSite1Prefab;
    public GameObject RoroRandomSite2Prefab;
    public GameObject RoroRandomSite3Prefab;
    public GameObject SarisiRandomSite1Prefab;
    public GameObject SarisiRandomSite2Prefab;
    public GameObject SarisiRandomSite3Prefab;
    public GameObject GarixRandomSite1Prefab;
    public GameObject GarixRandomSite2Prefab;
    public GameObject GarixRandomSite3Prefab;
    public GameObject OctoKrasisPatoroRandomSite1Prefab;
    public GameObject OctoKrasisPatoroRandomSite2Prefab;
    public GameObject OctoKrasisPatoroRandomSite3Prefab;
    public GameObject OctoKrasisPatoroRandomSite4Prefab;
    public GameObject DeltaD31_402054RandomSite1Prefab;
    public GameObject DeltaD31_402054RandomSite2Prefab;
    public GameObject DeltaD31_402054RandomSite3Prefab;
    public GameObject DeltaD31_402054RandomSite4Prefab;
    public GameObject DeltaD31_402054RandomSite5Prefab;
    public GameObject JeratoO95_99024RandomSite1Prefab;
    public GameObject JeratoO95_99024RandomSite2Prefab;
    public GameObject JeratoO95_99024RandomSite3Prefab;
    public GameObject JeratoO95_99024RandomSite4Prefab;
    public GameObject JeratoO95_99024RandomSite5Prefab;

    [Header("UI 아이콘 색상")]
    public Color IconShipNormal;
    public Color IconNormal;
    public Color IconClick;
    public Color AreaInBattle;
    public Color AreaInOccupation;
    public Color AreaInInfection;

    [Header("실제 위치 항성 목록")]
    public Transform WorldToropioStar;
    public Transform WorldRoro1Star;
    public Transform WorldRoro2Star;
    public Transform WorldSarisiStar;
    public Transform WorldGarixStar;
    public Transform WorldOctoKrasisPatoroSystemOrbit;
    public Transform WorldSecrosStar;
    public Transform WorldTeretosStar;
    public Transform WorldMiniPopoStar;
    public Transform WorldDeltaD31_402054SystemOrbit;
    public Transform WorldDeltaD31_4AStar;
    public Transform WorldDeltaD31_4BStar;
    public Transform WorldJeratoO95_99024SystemOrbit;
    public Transform WorldJeratoO95_7Orbit;
    public Transform WorldJeratoO95_7AStar;
    public Transform WorldJeratoO95_7BStar;
    public Transform WorldJeratoO95_14Orbit;
    public Transform WorldJeratoO95_14CStar;
    public Transform WorldJeratoO95_14DStar;
    public Transform WorldJeratoO95_OmegaStar;

    [Header("실제 위치 행성 목록")]
    public Transform WorldSatariusGlessia;
    public Transform WorldAposis;
    public Transform WorldTorono;
    public Transform WorldPlopa2;
    public Transform WorldVedes4;
    public Transform WorldAronPeri;
    public Transform WorldPapatus2;
    public Transform WorldPapatus3;
    public Transform WorldKyepotoros;
    public Transform WorldTratos;
    public Transform WorldOclasis;
    public Transform WorldDeriousHeri;
    public Transform WorldVeltrorexy;
    public Transform WorldErixJeoqeta;
    public Transform WorldQeepo;
    public Transform WorldCrownYosere;
    public Transform WorldOros;
    public Transform WorldJapetAgrone;
    public Transform WorldXacro042351;
    public Transform WorldDeltaD31_2208;
    public Transform WorldDeltaD31_9523;
    public Transform WorldDeltaD31_12721;
    public Transform WorldJeratoO95_1125;
    public Transform WorldJeratoO95_2252;
    public Transform WorldJeratoO95_8510;

    [Header("실제 위치 랜덤 사이트 목록")]
    public Transform WorldToropioRandomSite1;
    public Transform WorldToropioRandomSite2;
    public Transform WorldRoroRandomSite1;
    public Transform WorldRoroRandomSite2;
    public Transform WorldRoroRandomSite3;
    public Transform WorldSarisiRandomSite1;
    public Transform WorldSarisiRandomSite2;
    public Transform WorldSarisiRandomSite3;
    public Transform WorldGarixRandomSite1;
    public Transform WorldGarixRandomSite2;
    public Transform WorldGarixRandomSite3;
    public Transform WorldOctoKrasisPatoroRandomSite1;
    public Transform WorldOctoKrasisPatoroRandomSite2;
    public Transform WorldOctoKrasisPatoroRandomSite3;
    public Transform WorldOctoKrasisPatoroRandomSite4;
    public Transform WorldDeltaD31_402054RandomSite1;
    public Transform WorldDeltaD31_402054RandomSite2;
    public Transform WorldDeltaD31_402054RandomSite3;
    public Transform WorldDeltaD31_402054RandomSite4;
    public Transform WorldDeltaD31_402054RandomSite5;
    public Transform WorldJeratoO95_99024RandomSite1;
    public Transform WorldJeratoO95_99024RandomSite2;
    public Transform WorldJeratoO95_99024RandomSite3;
    public Transform WorldJeratoO95_99024RandomSite4;
    public Transform WorldJeratoO95_99024RandomSite5;

    [Header("항성 목록")]
    public GameObject WorldMapToropioStar;
    public GameObject WorldMapRoro1Star;
    public GameObject WorldMapRoro2Star;
    public GameObject WorldMapSarisiStar;
    public GameObject WorldMapGarixStar;
    public GameObject WorldMapSecrosStar;
    public GameObject WorldMapTeretosStar;
    public GameObject WorldMapMiniPopoStar;
    public GameObject WorldMapDeltaD31_4AStar;
    public GameObject WorldMapDeltaD31_4BStar;
    public GameObject WorldMapJeratoO95_7AStar;
    public GameObject WorldMapJeratoO95_7BStar;
    public GameObject WorldMapJeratoO95_14CStar;
    public GameObject WorldMapJeratoO95_14DStar;
    public GameObject WorldMapJeratoO95_OmegaStar;

    [Header("행성 목록")]
    public GameObject WorldMapSatariusGlessia;
    public GameObject WorldMapAposis;
    public GameObject WorldMapTorono;
    public GameObject WorldMapPlopa2;
    public GameObject WorldMapVedes4;
    public GameObject WorldMapAronPeri;
    public GameObject WorldMapPapatus2;
    public GameObject WorldMapPapatus3;
    public GameObject WorldMapKyepotoros;
    public GameObject WorldMapTratos;
    public GameObject WorldMapOclasis;
    public GameObject WorldMapDeriousHeri;
    public GameObject WorldMapVeltrorexy;
    public GameObject WorldMapErixJeoqeta;
    public GameObject WorldMapQeepo;
    public GameObject WorldMapCrownYosere;
    public GameObject WorldMapOros;
    public GameObject WorldMapJapetAgrone;
    public GameObject WorldMapXacro042351;
    public GameObject WorldMapDeltaD31_2208;
    public GameObject WorldMapDeltaD31_9523;
    public GameObject WorldMapDeltaD31_12721;
    public GameObject WorldMapJeratoO95_1125;
    public GameObject WorldMapJeratoO95_2252;
    public GameObject WorldMapJeratoO95_8510;

    [Header("미니맵 항성 좌표 목록(라인추적용)")]
    public RectTransform ToropioStarLine;
    public RectTransform Roro1StarLine;
    public RectTransform Roro2StarLine;
    public RectTransform SarisiStarLine;
    public RectTransform GarixStarLine;
    public RectTransform SecrosStarLine;
    public RectTransform TeretosStarLine;
    public RectTransform MiniPopoStarLine;
    public RectTransform DeltaD31_4AStarLine;
    public RectTransform DeltaD31_4BStarLine;
    public RectTransform JeratoO95_7AStarLine;
    public RectTransform JeratoO95_7BStarLine;
    public RectTransform JeratoO95_14CStarLine;
    public RectTransform JeratoO95_14DStarLine;
    public RectTransform JeratoO95_OmegaStarLine;

    [Header("미니맵 행성 좌표 목록(라인추적용)")]
    public RectTransform SatariusGlessiaLine;
    public RectTransform AposisLine;
    public RectTransform ToronoLine;
    public RectTransform Plopa2Line;
    public RectTransform Vedes4Line;
    public RectTransform AronPeriLine;
    public RectTransform Papatus2Line;
    public RectTransform Papatus3Line;
    public RectTransform KyepotorosLine;
    public RectTransform TratosLine;
    public RectTransform OclasisLine;
    public RectTransform DeriousHeriLine;
    public RectTransform VeltrorexyLine;
    public RectTransform ErixJeoqetaLine;
    public RectTransform QeepoLine;
    public RectTransform CrownYosereLine;
    public RectTransform OrosLine;
    public RectTransform JapetAgroneLine;
    public RectTransform Xacro042351Line;
    public RectTransform DeltaD31_2208Line;
    public RectTransform DeltaD31_9523Line;
    public RectTransform DeltaD31_12721Line;
    public RectTransform JeratoO95_1125Line;
    public RectTransform JeratoO95_2252Line;
    public RectTransform JeratoO95_8510Line;

    [Header("미니맵 랜덤 사이트 좌표 목록(라인추적용)")]
    public RectTransform ToropioRandomSite1Line;
    public RectTransform ToropioRandomSite2Line;
    public RectTransform RoroRandomSite1Line;
    public RectTransform RoroRandomSite2Line;
    public RectTransform RoroRandomSite3Line;
    public RectTransform SarisiRandomSite1Line;
    public RectTransform SarisiRandomSite2Line;
    public RectTransform SarisiRandomSite3Line;
    public RectTransform GarixRandomSite1Line;
    public RectTransform GarixRandomSite2Line;
    public RectTransform GarixRandomSite3Line;
    public RectTransform OctoKrasisPatoroRandomSite1Line;
    public RectTransform OctoKrasisPatoroRandomSite2Line;
    public RectTransform OctoKrasisPatoroRandomSite3Line;
    public RectTransform OctoKrasisPatoroRandomSite4Line;
    public RectTransform DeltaD31_402054RandomSite1Line;
    public RectTransform DeltaD31_402054RandomSite2Line;
    public RectTransform DeltaD31_402054RandomSite3Line;
    public RectTransform DeltaD31_402054RandomSite4Line;
    public RectTransform DeltaD31_402054RandomSite5Line;
    public RectTransform JeratoO95_99024RandomSite1Line;
    public RectTransform JeratoO95_99024RandomSite2Line;
    public RectTransform JeratoO95_99024RandomSite3Line;
    public RectTransform JeratoO95_99024RandomSite4Line;
    public RectTransform JeratoO95_99024RandomSite5Line;

    [Header("항성 버튼")]
    public Image ToropioStarImage;
    public Image Roro1StarImage;
    public Image Roro2StarImage;
    public Image SarisiStarImage;
    public Image GarixStarImage;
    public Image SecrosStarImage;
    public Image TeretosStarImage;
    public Image MiniPopoStarImage;
    public Image DeltaD31_4AStarImage;
    public Image DeltaD31_4BStarImage;
    public Image JeratoO95_7AStarImage;
    public Image JeratoO95_7BStarImage;
    public Image JeratoO95_14CStarImage;
    public Image JeratoO95_14DStarImage;
    public Image JeratoO95_OmegaStarImage;

    [Header("행성 버튼")]
    public Image SatariusGlessiaImage;
    public Image AposisImage;
    public Image ToronoImage;
    public Image Plopa2Image;
    public Image Vedes4Image;
    public Image AronPeriImage;
    public Image Papatus2Image;
    public Image Papatus3Image;
    public Image KyepotorosImage;
    public Image TratosImage;
    public Image OclasisImage;
    public Image DeriousHeriImage;
    public Image VeltrorexyImage;
    public Image ErixJeoqetaImage;
    public Image QeepoImage;
    public Image CrownYosereImage;
    public Image OrosImage;
    public Image JapetAgroneImage;
    public Image Xacro042351Image;
    public Image DeltaD31_2208Image;
    public Image DeltaD31_9523Image;
    public Image DeltaD31_12721Image;
    public Image JeratoO95_1125Image;
    public Image JeratoO95_2252Image;
    public Image JeratoO95_8510Image;

    [Header("랜덤 사이트 버튼")]
    public Image ToropioRandomSite1Image;
    public Image ToropioRandomSite2Image;
    public Image RoroRandomSite1Image;
    public Image RoroRandomSite2Image;
    public Image RoroRandomSite3Image;
    public Image SarisiRandomSite1Image;
    public Image SarisiRandomSite2Image;
    public Image SarisiRandomSite3Image;
    public Image GarixRandomSite1Image;
    public Image GarixRandomSite2Image;
    public Image GarixRandomSite3Image;
    public Image OctoKrasisPatoroRandomSite1Image;
    public Image OctoKrasisPatoroRandomSite2Image;
    public Image OctoKrasisPatoroRandomSite3Image;
    public Image OctoKrasisPatoroRandomSite4Image;
    public Image DeltaD31_402054RandomSite1Image;
    public Image DeltaD31_402054RandomSite2Image;
    public Image DeltaD31_402054RandomSite3Image;
    public Image DeltaD31_402054RandomSite4Image;
    public Image DeltaD31_402054RandomSite5Image;
    public Image JeratoO95_99024RandomSite1Image;
    public Image JeratoO95_99024RandomSite2Image;
    public Image JeratoO95_99024RandomSite3Image;
    public Image JeratoO95_99024RandomSite4Image;
    public Image JeratoO95_99024RandomSite5Image;

    [Header("항성 진행바 프리팹")]
    public GameObject ToropioProgressPrefab;
    public GameObject Roro1ProgressPrefab;
    public GameObject Roro2ProgressPrefab;
    public GameObject SarisiProgressPrefab;
    public GameObject GarixProgressPrefab;
    public GameObject SecrosProgressPrefab;
    public GameObject TeretosProgressPrefab;
    public GameObject MiniPopoProgressPrefab;
    public GameObject DeltaD31_4AProgressPrefab;
    public GameObject DeltaD31_4BProgressPrefab;
    public GameObject JeratoO95_7AProgressPrefab;
    public GameObject JeratoO95_7BProgressPrefab;
    public GameObject JeratoO95_14CProgressPrefab;
    public GameObject JeratoO95_14DProgressPrefab;
    public GameObject JeratoO95_OmegaProgressPrefab;

    [Header("행성 진행바 프리팹")]
    public GameObject SatariusGlessiaProgressPrefab;
    public GameObject AposisProgressPrefab;
    public GameObject ToronoProgressPrefab;
    public GameObject Plopa2ProgressPrefab;
    public GameObject Vedes4ProgressPrefab;
    public GameObject AronPeriProgressPrefab;
    public GameObject Papatus2ProgressPrefab;
    public GameObject Papatus3ProgressPrefab;
    public GameObject KyepotorosProgressPrefab;
    public GameObject TratosProgressPrefab;
    public GameObject OclasisProgressPrefab;
    public GameObject DeriousHeriProgressPrefab;
    public GameObject VeltrorexyProgressPrefab;
    public GameObject ErixJeoqetaProgressPrefab;
    public GameObject QeepoProgressPrefab;
    public GameObject CrownYosereProgressPrefab;
    public GameObject OrosProgressPrefab;
    public GameObject JapetAgroneProgressPrefab;
    public GameObject Xacro042351ProgressPrefab;
    public GameObject DeltaD31_2208ProgressPrefab;
    public GameObject DeltaD31_9523ProgressPrefab;
    public GameObject DeltaD31_12721ProgressPrefab;
    public GameObject JeratoO95_1125ProgressPrefab;
    public GameObject JeratoO95_2252ProgressPrefab;
    public GameObject JeratoO95_8510ProgressPrefab;

    [Header("랜덤 사이트 진행바 프리팹")]
    public GameObject ToropioRandomSitePrefab;
    public GameObject RoroRandomSitePrefab;
    public GameObject SarisiRandomSitePrefab;
    public GameObject GarixRandomSitePrefab;
    public GameObject OctoKrasisPatoroRandomSitePrefab;
    public GameObject DeltaD31_402054RandomSitePrefab;
    public GameObject JeratoO95_99024RandomSitePrefab;

    public GameObject ToropioRandomSite1ProgressPrefab;
    public GameObject ToropioRandomSite2ProgressPrefab;
    public GameObject RoroRandomSite1ProgressPrefab;
    public GameObject RoroRandomSite2ProgressPrefab;
    public GameObject RoroRandomSite3ProgressPrefab;
    public GameObject SarisiRandomSite1ProgressPrefab;
    public GameObject SarisiRandomSite2ProgressPrefab;
    public GameObject SarisiRandomSite3ProgressPrefab;
    public GameObject GarixRandomSite1ProgressPrefab;
    public GameObject GarixRandomSite2ProgressPrefab;
    public GameObject GarixRandomSite3ProgressPrefab;
    public GameObject OctoKrasisPatoroRandomSite1ProgressPrefab;
    public GameObject OctoKrasisPatoroRandomSite2ProgressPrefab;
    public GameObject OctoKrasisPatoroRandomSite3ProgressPrefab;
    public GameObject OctoKrasisPatoroRandomSite4ProgressPrefab;
    public GameObject DeltaD31_402054RandomSite1ProgressPrefab;
    public GameObject DeltaD31_402054RandomSite2ProgressPrefab;
    public GameObject DeltaD31_402054RandomSite3ProgressPrefab;
    public GameObject DeltaD31_402054RandomSite4ProgressPrefab;
    public GameObject DeltaD31_402054RandomSite5ProgressPrefab;
    public GameObject JeratoO95_99024RandomSite1ProgressPrefab;
    public GameObject JeratoO95_99024RandomSite2ProgressPrefab;
    public GameObject JeratoO95_99024RandomSite3ProgressPrefab;
    public GameObject JeratoO95_99024RandomSite4ProgressPrefab;
    public GameObject JeratoO95_99024RandomSite5ProgressPrefab;

    [Header("기타 진행바 프리팹")]
    public GameObject UniverseProgressBarShipPrefab;
    public GameObject FlagshipProgressPrefab;
    public int AccountOfShip; //선택한 함대수

    private float MinimapRatio;

    //스위치 목록
    [Header("플레이어 워프 함대 선택")]
    public bool Player1Selet;
    public bool Player2Selet;
    public bool Player3Selet;
    public bool Player4Selet;
    public bool Player5Selet;
    private bool FirstWarpFleet; //제일 먼저 워프하는 함대
    public int WarpToPlayer; //아군에게 워프하기 위한 해당 아군함대 번호
    public Vector3 WarpToPlayerArea; //합류하려는 함대의 위치

    [Header("플레이어 함대 방문 번호")]
    public int Player1Number; //해당 함대가 방문한 행성 번호
    public int Player2Number;
    public int Player3Number;
    public int Player4Number;
    public int Player5Number;

    [Header("지역 번호")]
    public int AreaNumber; //지역 번호
    public int SystemDestinationNumber; //목적지 항성계 번호
    public int StateNumber; //천체 상태번호(AreaStatement에서 보내는 번호를 받는다.)

    //기타지역
    public bool UniverseMapEnabled = false; //터치가 먼저 활성화 된 이후에 행동 개시 가능하도록 만든 스위치

    [Header("튜토리얼")]
    public bool Tutorial = false;
    public int TutorialMapStep;
    public bool TutorialOnce = false; //딱 한번만 튜토리얼 창을 띄우게 하기 위함
    public bool FirstBattle = false; //튜토리얼 첫 전투
    public GameObject TutorialStar;
    public GameObject TutorialPlanet;
    public GameObject SatariusGlessiaTutorial;
    public RectTransform TutorialViewer;
    public Text TutorialText;
    public Text TutorialText2;

    [Header("사운드")]
    public AudioClip UniverseMapButtonAudio;
    public AudioClip UniverseMapOnlineAudio;
    public AudioClip UniverseMapOfflineAudio;
    public AudioClip SelectButtonAudio;
    public AudioClip DestinationAudio;
    public AudioClip FinalFleetSelectAudio;
    public AudioClip WarningFleetSelectAudio;
    public AudioClip WarpStartUIAudio;

    private void Start()
    {
        AreaStatement = FindObjectOfType<AreaStatement>();
    }

    //천체 상태 색상 가져오기
    private void StartToGetIconColor()
    {
        Color NormalColor = IconNormal;
        Color BattleColor = AreaInBattle;
        Color OccupationColor = AreaInOccupation;
        Color InfectionColor = AreaInInfection;

        //항성 상태 불러오기
        if (AreaStatement.ToropioStarState == 1)
            ToropioStarImage.color = NormalColor;
        else if (AreaStatement.ToropioStarState == 2)
            ToropioStarImage.color = BattleColor;
        else if (AreaStatement.ToropioStarState == 3)
            ToropioStarImage.color = OccupationColor;
        else if (AreaStatement.ToropioStarState == 4)
            ToropioStarImage.color = InfectionColor;

        if (AreaStatement.Roro1StarState == 1)
            Roro1StarImage.color = NormalColor;
        else if (AreaStatement.Roro1StarState == 2)
            Roro1StarImage.color = BattleColor;
        else if (AreaStatement.Roro1StarState == 3)
            Roro1StarImage.color = OccupationColor;
        else if (AreaStatement.Roro1StarState == 4)
            Roro1StarImage.color = InfectionColor;

        if (AreaStatement.Roro2StarState == 1)
            Roro2StarImage.color = NormalColor;
        else if (AreaStatement.Roro2StarState == 2)
            Roro2StarImage.color = BattleColor;
        else if (AreaStatement.Roro2StarState == 3)
            Roro2StarImage.color = OccupationColor;
        else if (AreaStatement.Roro2StarState == 4)
            Roro2StarImage.color = InfectionColor;

        if (AreaStatement.SarisiStarState == 1)
            SarisiStarImage.color = NormalColor;
        else if (AreaStatement.SarisiStarState == 2)
            SarisiStarImage.color = BattleColor;
        else if (AreaStatement.SarisiStarState == 3)
            SarisiStarImage.color = OccupationColor;
        else if (AreaStatement.SarisiStarState == 4)
            SarisiStarImage.color = InfectionColor;

        if (AreaStatement.GarixStarState == 1)
            GarixStarImage.color = NormalColor;
        else if (AreaStatement.GarixStarState == 2)
            GarixStarImage.color = BattleColor;
        else if (AreaStatement.GarixStarState == 3)
            GarixStarImage.color = OccupationColor;
        else if (AreaStatement.GarixStarState == 4)
            GarixStarImage.color = InfectionColor;

        if (AreaStatement.SecrosStarState == 1)
            SecrosStarImage.color = NormalColor;
        else if (AreaStatement.SecrosStarState == 2)
            SecrosStarImage.color = BattleColor;
        else if (AreaStatement.SecrosStarState == 3)
            SecrosStarImage.color = OccupationColor;
        else if (AreaStatement.SecrosStarState == 4)
            SecrosStarImage.color = InfectionColor;

        if (AreaStatement.TeretosStarState == 1)
            TeretosStarImage.color = NormalColor;
        else if (AreaStatement.TeretosStarState == 2)
            TeretosStarImage.color = BattleColor;
        else if (AreaStatement.TeretosStarState == 3)
            TeretosStarImage.color = OccupationColor;
        else if (AreaStatement.TeretosStarState == 4)
            TeretosStarImage.color = InfectionColor;

        if (AreaStatement.MiniPopoStarState == 1)
            MiniPopoStarImage.color = NormalColor;
        else if (AreaStatement.MiniPopoStarState == 2)
            MiniPopoStarImage.color = BattleColor;
        else if (AreaStatement.MiniPopoStarState == 3)
            MiniPopoStarImage.color = OccupationColor;
        else if (AreaStatement.MiniPopoStarState == 4)
            MiniPopoStarImage.color = InfectionColor;

        if (AreaStatement.DeltaD31_4AStarState == 1)
            DeltaD31_4AStarImage.color = NormalColor;
        else if (AreaStatement.DeltaD31_4AStarState == 2)
            DeltaD31_4AStarImage.color = BattleColor;
        else if (AreaStatement.DeltaD31_4AStarState == 3)
            DeltaD31_4AStarImage.color = OccupationColor;
        else if (AreaStatement.DeltaD31_4AStarState == 4)
            DeltaD31_4AStarImage.color = InfectionColor;

        if (AreaStatement.DeltaD31_4BStarState == 1)
            DeltaD31_4BStarImage.color = NormalColor;
        else if (AreaStatement.DeltaD31_4BStarState == 2)
            DeltaD31_4BStarImage.color = BattleColor;
        else if (AreaStatement.DeltaD31_4BStarState == 3)
            DeltaD31_4BStarImage.color = OccupationColor;
        else if (AreaStatement.DeltaD31_4BStarState == 4)
            DeltaD31_4BStarImage.color = InfectionColor;

        if (AreaStatement.JeratoO95_7AStarState == 1)
            JeratoO95_7AStarImage.color = NormalColor;
        else if (AreaStatement.JeratoO95_7AStarState == 2)
            JeratoO95_7AStarImage.color = BattleColor;
        else if (AreaStatement.JeratoO95_7AStarState == 3)
            JeratoO95_7AStarImage.color = OccupationColor;
        else if (AreaStatement.JeratoO95_7AStarState == 4)
            JeratoO95_7AStarImage.color = InfectionColor;

        if (AreaStatement.JeratoO95_7BStarState == 1)
            JeratoO95_7BStarImage.color = NormalColor;
        else if (AreaStatement.JeratoO95_7BStarState == 2)
            JeratoO95_7BStarImage.color = BattleColor;
        else if (AreaStatement.JeratoO95_7BStarState == 3)
            JeratoO95_7BStarImage.color = OccupationColor;
        else if (AreaStatement.JeratoO95_7BStarState == 4)
            JeratoO95_7BStarImage.color = InfectionColor;

        if (AreaStatement.JeratoO95_14CStarState == 1)
            JeratoO95_14CStarImage.color = NormalColor;
        else if (AreaStatement.JeratoO95_14CStarState == 2)
            JeratoO95_14CStarImage.color = BattleColor;
        else if (AreaStatement.JeratoO95_14CStarState == 3)
            JeratoO95_14CStarImage.color = OccupationColor;
        else if (AreaStatement.JeratoO95_14CStarState == 4)
            JeratoO95_14CStarImage.color = InfectionColor;

        if (AreaStatement.JeratoO95_14DStarState == 1)
            JeratoO95_14DStarImage.color = NormalColor;
        else if (AreaStatement.JeratoO95_14DStarState == 2)
            JeratoO95_14DStarImage.color = BattleColor;
        else if (AreaStatement.JeratoO95_14DStarState == 3)
            JeratoO95_14DStarImage.color = OccupationColor;
        else if (AreaStatement.JeratoO95_14DStarState == 4)
            JeratoO95_14DStarImage.color = InfectionColor;

        if (AreaStatement.JeratoO95_OmegaStarState == 1)
            JeratoO95_OmegaStarImage.color = NormalColor;
        else if (AreaStatement.JeratoO95_OmegaStarState == 2)
            JeratoO95_OmegaStarImage.color = BattleColor;
        else if (AreaStatement.JeratoO95_OmegaStarState == 3)
            JeratoO95_OmegaStarImage.color = OccupationColor;
        else if (AreaStatement.JeratoO95_OmegaStarState == 4)
            JeratoO95_OmegaStarImage.color = InfectionColor;

        //행성 상태 불러오기
        //토로피오 항성계
        if (AreaStatement.SatariusGlessiaState == 1)
            SatariusGlessiaImage.color = NormalColor;
        else if (AreaStatement.SatariusGlessiaState == 2)
            SatariusGlessiaImage.color = BattleColor;
        else if (AreaStatement.SatariusGlessiaState == 3)
            SatariusGlessiaImage.color = OccupationColor;
        else if (AreaStatement.SatariusGlessiaState == 4)
            SatariusGlessiaImage.color = InfectionColor;

        if (AreaStatement.AposisState == 1)
            AposisImage.color = NormalColor;
        else if (AreaStatement.AposisState == 2)
            AposisImage.color = BattleColor;
        else if (AreaStatement.AposisState == 3)
            AposisImage.color = OccupationColor;
        else if (AreaStatement.AposisState == 4)
            AposisImage.color = InfectionColor;

        if (AreaStatement.ToronoState == 1)
            ToronoImage.color = NormalColor;
        else if (AreaStatement.ToronoState == 2)
            ToronoImage.color = BattleColor;
        else if (AreaStatement.ToronoState == 3)
            ToronoImage.color = OccupationColor;
        else if (AreaStatement.ToronoState == 4)
            ToronoImage.color = InfectionColor;

        if (AreaStatement.Plopa2State == 1)
            Plopa2Image.color = NormalColor;
        else if (AreaStatement.Plopa2State == 2)
            Plopa2Image.color = BattleColor;
        else if (AreaStatement.Plopa2State == 3)
            Plopa2Image.color = OccupationColor;
        else if (AreaStatement.Plopa2State == 4)
            Plopa2Image.color = InfectionColor;

        if (AreaStatement.Vedes4State == 1)
            Vedes4Image.color = NormalColor;
        else if (AreaStatement.Vedes4State == 2)
            Vedes4Image.color = BattleColor;
        else if (AreaStatement.Vedes4State == 3)
            Vedes4Image.color = OccupationColor;
        else if (AreaStatement.Vedes4State == 4)
            Vedes4Image.color = InfectionColor;

        //로로 항성계
        if (AreaStatement.AronPeriState == 1)
            AronPeriImage.color = NormalColor;
        else if (AreaStatement.AronPeriState == 2)
            AronPeriImage.color = BattleColor;
        else if (AreaStatement.AronPeriState == 3)
            AronPeriImage.color = OccupationColor;
        else if (AreaStatement.AronPeriState == 4)
            AronPeriImage.color = InfectionColor;

        if (AreaStatement.Papatus2State == 1)
            Papatus2Image.color = NormalColor;
        else if (AreaStatement.Papatus2State == 2)
            Papatus2Image.color = BattleColor;
        else if (AreaStatement.Papatus2State == 3)
            Papatus2Image.color = OccupationColor;
        else if (AreaStatement.Papatus2State == 4)
            Papatus2Image.color = InfectionColor;

        if (AreaStatement.Papatus3State == 1)
            Papatus3Image.color = NormalColor;
        else if (AreaStatement.Papatus3State == 2)
            Papatus3Image.color = BattleColor;
        else if (AreaStatement.Papatus3State == 3)
            Papatus3Image.color = OccupationColor;
        else if (AreaStatement.Papatus3State == 4)
            Papatus3Image.color = InfectionColor;

        if (AreaStatement.KyepotorosState == 1)
            KyepotorosImage.color = NormalColor;
        else if (AreaStatement.KyepotorosState == 2)
            KyepotorosImage.color = BattleColor;
        else if (AreaStatement.KyepotorosState == 3)
            KyepotorosImage.color = OccupationColor;
        else if (AreaStatement.KyepotorosState == 4)
            KyepotorosImage.color = InfectionColor;

        //사리시 항성계
        if (AreaStatement.TratosState == 1)
            TratosImage.color = NormalColor;
        else if (AreaStatement.TratosState == 2)
            TratosImage.color = BattleColor;
        else if (AreaStatement.TratosState == 3)
            TratosImage.color = OccupationColor;
        else if (AreaStatement.TratosState == 4)
            TratosImage.color = InfectionColor;

        if (AreaStatement.OclasisState == 1)
            OclasisImage.color = NormalColor;
        else if (AreaStatement.OclasisState == 2)
            OclasisImage.color = BattleColor;
        else if (AreaStatement.OclasisState == 3)
            OclasisImage.color = OccupationColor;
        else if (AreaStatement.OclasisState == 4)
            OclasisImage.color = InfectionColor;

        if (AreaStatement.DeriousHeriState == 1)
            DeriousHeriImage.color = NormalColor;
        else if (AreaStatement.DeriousHeriState == 2)
            DeriousHeriImage.color = BattleColor;
        else if (AreaStatement.DeriousHeriState == 3)
            DeriousHeriImage.color = OccupationColor;
        else if (AreaStatement.DeriousHeriState == 4)
            DeriousHeriImage.color = InfectionColor;

        //가릭스 항성계
        if (AreaStatement.VeltrorexyState == 1)
            VeltrorexyImage.color = NormalColor;
        else if (AreaStatement.VeltrorexyState == 2)
            VeltrorexyImage.color = BattleColor;
        else if (AreaStatement.VeltrorexyState == 3)
            VeltrorexyImage.color = OccupationColor;
        else if (AreaStatement.VeltrorexyState == 4)
            VeltrorexyImage.color = InfectionColor;

        if (AreaStatement.ErixJeoqetaState == 1)
            ErixJeoqetaImage.color = NormalColor;
        else if (AreaStatement.ErixJeoqetaState == 2)
            ErixJeoqetaImage.color = BattleColor;
        else if (AreaStatement.ErixJeoqetaState == 3)
            ErixJeoqetaImage.color = OccupationColor;
        else if (AreaStatement.ErixJeoqetaState == 4)
            ErixJeoqetaImage.color = InfectionColor;

        if (AreaStatement.QeepoState == 1)
            QeepoImage.color = NormalColor;
        else if (AreaStatement.QeepoState == 2)
            QeepoImage.color = BattleColor;
        else if (AreaStatement.QeepoState == 3)
            QeepoImage.color = OccupationColor;
        else if (AreaStatement.QeepoState == 4)
            QeepoImage.color = InfectionColor;

        if (AreaStatement.CrownYosereState == 1)
            CrownYosereImage.color = NormalColor;
        else if (AreaStatement.CrownYosereState == 2)
            CrownYosereImage.color = BattleColor;
        else if (AreaStatement.CrownYosereState == 3)
            CrownYosereImage.color = OccupationColor;
        else if (AreaStatement.CrownYosereState == 4)
            CrownYosereImage.color = InfectionColor;

        //옥토크라시스 파토로 항성계
        if (AreaStatement.OrosState == 1)
            OrosImage.color = NormalColor;
        else if (AreaStatement.OrosState == 2)
            OrosImage.color = BattleColor;
        else if (AreaStatement.OrosState == 3)
            OrosImage.color = OccupationColor;
        else if (AreaStatement.OrosState == 4)
            OrosImage.color = InfectionColor;

        if (AreaStatement.JapetAgroneState == 1)
            JapetAgroneImage.color = NormalColor;
        else if (AreaStatement.JapetAgroneState == 2)
            JapetAgroneImage.color = BattleColor;
        else if (AreaStatement.JapetAgroneState == 3)
            JapetAgroneImage.color = OccupationColor;
        else if (AreaStatement.JapetAgroneState == 4)
            JapetAgroneImage.color = InfectionColor;

        if (AreaStatement.Xacro042351State == 1)
            Xacro042351Image.color = NormalColor;
        else if (AreaStatement.Xacro042351State == 2)
            Xacro042351Image.color = BattleColor;
        else if (AreaStatement.Xacro042351State == 3)
            Xacro042351Image.color = OccupationColor;
        else if (AreaStatement.Xacro042351State == 4)
            Xacro042351Image.color = InfectionColor;

        //델타 D31-402054 항성계
        if (AreaStatement.DeltaD31_2208State == 1)
            DeltaD31_2208Image.color = NormalColor;
        else if (AreaStatement.DeltaD31_2208State == 2)
            DeltaD31_2208Image.color = BattleColor;
        else if (AreaStatement.DeltaD31_2208State == 3)
            DeltaD31_2208Image.color = OccupationColor;
        else if (AreaStatement.DeltaD31_2208State == 4)
            DeltaD31_2208Image.color = InfectionColor;

        if (AreaStatement.DeltaD31_9523State == 1)
            DeltaD31_9523Image.color = NormalColor;
        else if (AreaStatement.DeltaD31_9523State == 2)
            DeltaD31_9523Image.color = BattleColor;
        else if (AreaStatement.DeltaD31_9523State == 3)
            DeltaD31_9523Image.color = OccupationColor;
        else if (AreaStatement.DeltaD31_9523State == 4)
            DeltaD31_9523Image.color = InfectionColor;

        if (AreaStatement.DeltaD31_12721State == 1)
            DeltaD31_12721Image.color = NormalColor;
        else if (AreaStatement.DeltaD31_12721State == 2)
            DeltaD31_12721Image.color = BattleColor;
        else if (AreaStatement.DeltaD31_12721State == 3)
            DeltaD31_12721Image.color = OccupationColor;
        else if (AreaStatement.DeltaD31_12721State == 4)
            DeltaD31_12721Image.color = InfectionColor;

        //제라토 O95-99024 항성계
        if (AreaStatement.JeratoO95_1125State == 1)
            JeratoO95_1125Image.color = NormalColor;
        else if (AreaStatement.JeratoO95_1125State == 2)
            JeratoO95_1125Image.color = BattleColor;
        else if (AreaStatement.JeratoO95_1125State == 3)
            JeratoO95_1125Image.color = OccupationColor;
        else if (AreaStatement.JeratoO95_1125State == 4)
            JeratoO95_1125Image.color = InfectionColor;

        if (AreaStatement.JeratoO95_2252State == 1)
            JeratoO95_2252Image.color = NormalColor;
        else if (AreaStatement.JeratoO95_2252State == 2)
            JeratoO95_2252Image.color = BattleColor;
        else if (AreaStatement.JeratoO95_2252State == 3)
            JeratoO95_2252Image.color = OccupationColor;
        else if (AreaStatement.JeratoO95_2252State == 4)
            JeratoO95_2252Image.color = InfectionColor;

        if (AreaStatement.JeratoO95_8510State == 1)
            JeratoO95_8510Image.color = NormalColor;
        else if (AreaStatement.JeratoO95_8510State == 2)
            JeratoO95_8510Image.color = BattleColor;
        else if (AreaStatement.JeratoO95_8510State == 3)
            JeratoO95_8510Image.color = OccupationColor;
        else if (AreaStatement.JeratoO95_8510State == 4)
            JeratoO95_8510Image.color = InfectionColor;
    }

    //시작직후, 스폰한 기함위치 목록 가져오기
    public void GetPlayerFlagship()
    {
        WorldPlayer1 = ShipManager.instance.FlagShipList[0].transform;
        if (ShipManager.instance.FlagShipList.Count > 1)
            WorldPlayer2 = ShipManager.instance.FlagShipList[1].transform;
        if (ShipManager.instance.FlagShipList.Count > 2)
            WorldPlayer3 = ShipManager.instance.FlagShipList[2].transform;
        if (ShipManager.instance.FlagShipList.Count > 3)
            WorldPlayer4 = ShipManager.instance.FlagShipList[3].transform;
        if (ShipManager.instance.FlagShipList.Count > 4)
            WorldPlayer5 = ShipManager.instance.FlagShipList[4].transform;
    }

    //우주 지도 모드 UI
    public void UniverseMapClick()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (!UniverseMapMode)
            {
                if (MapOffline != null)
                {
                    StopCoroutine(MapOffline);
                    MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveStart = false;
                    WallEffectUCCIS.GetComponent<Animator>().SetFloat("Menu wall effect1, Menu wall", 0);
                    UniverseMapUI.GetComponent<Animator>().SetFloat("Open, Universe Map Butten", 0);
                    UniverseMapUI.GetComponent<Animator>().SetFloat("Active ship, Universe Map Butten", 0);
                }
                UniverseMapEnabled = true;
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", UniverseMapOnlineAudio);
                MapOnline = StartCoroutine(UniverseMapOnAnime());
            }
            else
            {
                if (MapOnline != null)
                {
                    StopCoroutine(MapOnline);
                    MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveStart = false;
                }
                if (WarpStep != 4) //워프실시한 경우에는 강제로 0가 되지 않는다.(우주맵 버튼 누를 때에만 활성화)
                    WarpStep = 0;
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", UniverseMapOfflineAudio);
                MapOffline = StartCoroutine(UniverseMapOffAnime());
            }

            UniverseMapMode = !UniverseMapMode;
        }
    }
    public void UniverseMapDown()
    {
        ClickMapMode = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", UniverseMapButtonAudio);
        if (UniverseMapMode == false)
            UniverseMapUI.GetComponent<Animator>().SetBool("Click, Universe Map Butten", true);
        else
            UniverseMapUI.GetComponent<Animator>().SetBool("Click2, Universe Map Butten", true);
    }
    public void UniverseMapUp()
    {
        if (ClickMapMode == true)
        {
            if (UniverseMapMode == false)
                UniverseMapUI.GetComponent<Animator>().SetBool("Click, Universe Map Butten", false);
            else
                UniverseMapUI.GetComponent<Animator>().SetBool("Click2, Universe Map Butten", false);
        }
        ClickMapMode = false;
    }
    public void UniverseMapEnter()
    {
        if (ClickMapMode == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", UniverseMapButtonAudio);
            if (UniverseMapMode == false)
                UniverseMapUI.GetComponent<Animator>().SetBool("Click, Universe Map Butten", true);
            else
                UniverseMapUI.GetComponent<Animator>().SetBool("Click2, Universe Map Butten", true);
        }
    }
    public void UniverseMapExit()
    {
        if (ClickMapMode == true)
        {
            if (UniverseMapMode == false)
                UniverseMapUI.GetComponent<Animator>().SetBool("Click, Universe Map Butten", false);
            else
                UniverseMapUI.GetComponent<Animator>().SetBool("Click2, Universe Map Butten", false);
        }
    }

    //베틀 세이브 파일을 불러올 때, 적 함선 스폰 직후 기존에 소속된 전투 지역에 다시 소속시키기 
    public void BattleSiteGet(GameObject Ship, int Area)
    {
        //항성
        if (Area == 1)
        {
            WorldMapToropioStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapToropioStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapToropioStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapToropioStar;
        }
        else if (Area == 2)
        {
            WorldMapRoro1Star.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapRoro1Star.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapRoro1Star.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapRoro1Star;
        }
        else if (Area == 3)
        {
            WorldMapRoro2Star.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapRoro2Star.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapRoro2Star.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapRoro2Star;
        }
        else if (Area == 4)
        {
            WorldMapSarisiStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapSarisiStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapSarisiStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapSarisiStar;
        }
        else if (Area == 5)
        {
            WorldMapGarixStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapGarixStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapGarixStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapGarixStar;
        }
        else if (Area == 6)
        {
            WorldMapSecrosStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapSecrosStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapSecrosStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapSecrosStar;
        }
        else if (Area == 7)
        {
            WorldMapTeretosStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapTeretosStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapTeretosStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapTeretosStar;
        }
        else if (Area == 8)
        {
            WorldMapMiniPopoStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapMiniPopoStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapMiniPopoStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapMiniPopoStar;
        }
        else if (Area == 9)
        {
            WorldMapDeltaD31_4AStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapDeltaD31_4AStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapDeltaD31_4AStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapDeltaD31_4AStar;
        }
        else if (Area == 10)
        {
            WorldMapDeltaD31_4BStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapDeltaD31_4BStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapDeltaD31_4BStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapDeltaD31_4BStar;
        }
        else if (Area == 11)
        {
            WorldMapJeratoO95_7AStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapJeratoO95_7AStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapJeratoO95_7AStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapJeratoO95_7AStar;
        }
        else if (Area == 12)
        {
            WorldMapJeratoO95_7BStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapJeratoO95_7BStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapJeratoO95_7BStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapJeratoO95_7BStar;
        }
        else if (Area == 13)
        {
            WorldMapJeratoO95_14CStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapJeratoO95_14CStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapJeratoO95_14CStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapJeratoO95_14CStar;
        }
        else if (Area == 14)
        {
            WorldMapJeratoO95_14DStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapJeratoO95_14DStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapJeratoO95_14DStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapJeratoO95_14DStar;
        }
        else if (Area == 15)
        {
            WorldMapJeratoO95_OmegaStar.GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(Ship);
            WorldMapJeratoO95_OmegaStar.GetComponent<StarBattleSystem>().isInFight = true;
            WorldMapJeratoO95_OmegaStar.GetComponent<StarBattleSystem>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapJeratoO95_OmegaStar;
        }

        //행성
        else if(Area == 1001)
        {
            WorldMapSatariusGlessia.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapSatariusGlessia.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapSatariusGlessia.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapSatariusGlessia;
        }
        else if (Area == 1002)
        {
            WorldMapAposis.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapAposis.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapAposis.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapAposis;
        }
        else if (Area == 1003)
        {
            WorldMapTorono.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapTorono.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapTorono.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapTorono;
        }
        else if (Area == 1004)
        {
            WorldMapPlopa2.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapPlopa2.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapPlopa2.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapPlopa2;
        }
        else if (Area == 1005)
        {
            WorldMapVedes4.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapVedes4.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapVedes4.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapVedes4;
        }
        else if (Area == 1006)
        {
            WorldMapAronPeri.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapAronPeri.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapAronPeri.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapAronPeri;
        }
        else if (Area == 1007)
        {
            WorldMapPapatus2.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapPapatus2.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapPapatus2.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapPapatus2;
        }
        else if (Area == 1008)
        {
            WorldMapPapatus3.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapPapatus3.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapPapatus3.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapPapatus3;
        }
        else if (Area == 1009)
        {
            WorldMapKyepotoros.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapKyepotoros.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapKyepotoros.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapKyepotoros;
        }
        else if (Area == 1010)
        {
            WorldMapTratos.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapTratos.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapTratos.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapTratos;
        }
        else if (Area == 1011)
        {
            WorldMapOclasis.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapOclasis.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapOclasis.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapOclasis;
        }
        else if (Area == 1012)
        {
            WorldMapDeriousHeri.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapDeriousHeri.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapDeriousHeri.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapDeriousHeri;
        }
        else if (Area == 1013)
        {
            WorldMapVeltrorexy.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapVeltrorexy.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapVeltrorexy.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapVeltrorexy;
        }
        else if (Area == 1014)
        {
            WorldMapErixJeoqeta.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapErixJeoqeta.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapErixJeoqeta.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapErixJeoqeta;
        }
        else if (Area == 1015)
        {
            WorldMapQeepo.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapQeepo.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapQeepo.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapQeepo;
        }
        else if (Area == 1016)
        {
            WorldMapCrownYosere.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapCrownYosere.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapCrownYosere.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapCrownYosere;
        }
        else if (Area == 1017)
        {
            WorldMapOros.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapOros.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapOros.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapOros;
        }
        else if (Area == 1018)
        {
            WorldMapJapetAgrone.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapJapetAgrone.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapJapetAgrone.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapJapetAgrone;
        }
        else if (Area == 1019)
        {
            WorldMapXacro042351.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapXacro042351.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapXacro042351.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapXacro042351;
        }
        else if (Area == 1020)
        {
            WorldMapDeltaD31_2208.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapDeltaD31_2208.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapDeltaD31_2208.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapDeltaD31_2208;
        }
        else if (Area == 1021)
        {
            WorldMapDeltaD31_9523.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapDeltaD31_9523.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapDeltaD31_9523.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapDeltaD31_9523;
        }
        else if (Area == 1022)
        {
            WorldMapDeltaD31_12721.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapDeltaD31_12721.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapDeltaD31_12721.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapDeltaD31_12721;
        }
        else if (Area == 1023)
        {
            WorldMapJeratoO95_1125.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapJeratoO95_1125.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapJeratoO95_1125.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapJeratoO95_1125;
        }
        else if (Area == 1024)
        {
            WorldMapJeratoO95_2252.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapJeratoO95_2252.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapJeratoO95_2252.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapJeratoO95_2252;
        }
        else if (Area == 1025)
        {
            WorldMapJeratoO95_8510.GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(Ship);
            WorldMapJeratoO95_8510.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
            WorldMapJeratoO95_8510.GetComponent<PlanetOurForceShipsManager>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldMapJeratoO95_8510;
        }

        //전투 지역
        else if(Area == 10000)
        {
            WorldToropioRandomSite1.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldToropioRandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldToropioRandomSite1.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldToropioRandomSite1.gameObject;
            WorldToropioRandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10001)
        {
            WorldToropioRandomSite2.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldToropioRandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldToropioRandomSite2.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldToropioRandomSite2.gameObject;
            WorldToropioRandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10002)
        {
            WorldRoroRandomSite1.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldRoroRandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldRoroRandomSite1.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldRoroRandomSite1.gameObject;
            WorldRoroRandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10003)
        {
            WorldRoroRandomSite2.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldRoroRandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldRoroRandomSite2.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldRoroRandomSite2.gameObject;
            WorldRoroRandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10004)
        {
            WorldRoroRandomSite3.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldRoroRandomSite3.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldRoroRandomSite3.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldRoroRandomSite3.gameObject;
            WorldRoroRandomSite3.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10005)
        {
            WorldSarisiRandomSite1.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldSarisiRandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldSarisiRandomSite1.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldSarisiRandomSite1.gameObject;
            WorldSarisiRandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10006)
        {
            WorldSarisiRandomSite2.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldSarisiRandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldSarisiRandomSite2.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldSarisiRandomSite2.gameObject;
            WorldSarisiRandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10007)
        {
            WorldSarisiRandomSite3.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldSarisiRandomSite3.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldSarisiRandomSite3.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldSarisiRandomSite3.gameObject;
            WorldSarisiRandomSite3.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10008)
        {
            WorldGarixRandomSite1.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldGarixRandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldGarixRandomSite1.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldGarixRandomSite1.gameObject;
            WorldGarixRandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10009)
        {
            WorldGarixRandomSite2.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldGarixRandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldGarixRandomSite2.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldGarixRandomSite2.gameObject;
            WorldGarixRandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10010)
        {
            WorldGarixRandomSite3.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldGarixRandomSite3.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldGarixRandomSite3.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldGarixRandomSite3.gameObject;
            WorldGarixRandomSite3.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10011)
        {
            WorldOctoKrasisPatoroRandomSite1.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldOctoKrasisPatoroRandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldOctoKrasisPatoroRandomSite1.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldOctoKrasisPatoroRandomSite1.gameObject;
            WorldOctoKrasisPatoroRandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10012)
        {
            WorldOctoKrasisPatoroRandomSite2.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldOctoKrasisPatoroRandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldOctoKrasisPatoroRandomSite2.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldOctoKrasisPatoroRandomSite2.gameObject;
            WorldOctoKrasisPatoroRandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10013)
        {
            WorldOctoKrasisPatoroRandomSite3.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldOctoKrasisPatoroRandomSite3.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldOctoKrasisPatoroRandomSite3.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldOctoKrasisPatoroRandomSite3.gameObject;
            WorldOctoKrasisPatoroRandomSite3.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10014)
        {
            WorldOctoKrasisPatoroRandomSite4.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldOctoKrasisPatoroRandomSite4.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldOctoKrasisPatoroRandomSite4.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldOctoKrasisPatoroRandomSite4.gameObject;
            WorldOctoKrasisPatoroRandomSite4.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10015)
        {
            WorldDeltaD31_402054RandomSite1.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldDeltaD31_402054RandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldDeltaD31_402054RandomSite1.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldDeltaD31_402054RandomSite1.gameObject;
            WorldDeltaD31_402054RandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10016)
        {
            WorldDeltaD31_402054RandomSite2.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldDeltaD31_402054RandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldDeltaD31_402054RandomSite2.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldDeltaD31_402054RandomSite2.gameObject;
            WorldDeltaD31_402054RandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10017)
        {
            WorldDeltaD31_402054RandomSite3.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldDeltaD31_402054RandomSite3.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldDeltaD31_402054RandomSite3.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldDeltaD31_402054RandomSite3.gameObject;
            WorldDeltaD31_402054RandomSite3.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10018)
        {
            WorldDeltaD31_402054RandomSite4.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldDeltaD31_402054RandomSite4.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldDeltaD31_402054RandomSite4.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldDeltaD31_402054RandomSite4.gameObject;
            WorldDeltaD31_402054RandomSite4.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10019)
        {
            WorldDeltaD31_402054RandomSite5.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldDeltaD31_402054RandomSite5.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldDeltaD31_402054RandomSite5.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldDeltaD31_402054RandomSite5.gameObject;
            WorldDeltaD31_402054RandomSite5.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10020)
        {
            WorldJeratoO95_99024RandomSite1.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldJeratoO95_99024RandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldJeratoO95_99024RandomSite1.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldJeratoO95_99024RandomSite1.gameObject;
            WorldJeratoO95_99024RandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10021)
        {
            WorldJeratoO95_99024RandomSite2.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldJeratoO95_99024RandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldJeratoO95_99024RandomSite2.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldJeratoO95_99024RandomSite2.gameObject;
            WorldJeratoO95_99024RandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10022)
        {
            WorldJeratoO95_99024RandomSite3.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldJeratoO95_99024RandomSite3.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldJeratoO95_99024RandomSite3.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldJeratoO95_99024RandomSite3.gameObject;
            WorldJeratoO95_99024RandomSite3.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10023)
        {
            WorldJeratoO95_99024RandomSite4.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldJeratoO95_99024RandomSite4.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldJeratoO95_99024RandomSite4.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldJeratoO95_99024RandomSite4.gameObject;
            WorldJeratoO95_99024RandomSite4.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
        else if (Area == 10024)
        {
            WorldJeratoO95_99024RandomSite5.gameObject.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(Ship);
            WorldJeratoO95_99024RandomSite5.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            WorldJeratoO95_99024RandomSite5.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
            Ship.GetComponent<EnemyShipLevelInformation>().Zone = WorldJeratoO95_99024RandomSite5.gameObject;
            WorldJeratoO95_99024RandomSite5.gameObject.GetComponent<OurForceGet>().enabled = true;
        }
    }

    //전투 지역으로 워프시, 적 함대 불러오기
    public void BattleSiteEnemyGet(int Area)
    {
        //항성
        if (Area == 1)
        {
            if (WorldMapToropioStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.ToropioStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapToropioStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapToropioStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapToropioStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapToropioStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 2)
        {
            if (WorldMapRoro1Star.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.Roro1StarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapRoro1Star.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapRoro1Star.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapRoro1Star.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapRoro1Star.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 3)
        {
            if (WorldMapRoro2Star.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.Roro2StarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapRoro2Star.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapRoro2Star.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapRoro2Star.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapRoro2Star.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 4)
        {
            if (WorldMapSarisiStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.SarisiStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapSarisiStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapSarisiStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapSarisiStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapSarisiStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 5)
        {
            if (WorldMapGarixStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.GarixStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapGarixStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapGarixStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapGarixStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapGarixStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 6)
        {
            if (WorldMapSecrosStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.SecrosStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapSecrosStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapSecrosStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapSecrosStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapSecrosStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 7)
        {
            if (WorldMapTeretosStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.TeretosStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapTeretosStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapTeretosStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapTeretosStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapTeretosStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 8)
        {
            if (WorldMapMiniPopoStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.MiniPopoStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapMiniPopoStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapMiniPopoStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapMiniPopoStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapMiniPopoStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 9)
        {
            if (WorldMapDeltaD31_4AStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.DeltaD31_4AStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapDeltaD31_4AStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapDeltaD31_4AStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapDeltaD31_4AStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapDeltaD31_4AStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 10)
        {
            if (WorldMapDeltaD31_4BStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.DeltaD31_4BStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapDeltaD31_4BStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapDeltaD31_4BStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapDeltaD31_4BStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapDeltaD31_4BStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 11)
        {
            if (WorldMapJeratoO95_7AStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.JeratoO95_7AStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapJeratoO95_7AStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapJeratoO95_7AStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapJeratoO95_7AStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapJeratoO95_7AStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 12)
        {
            if (WorldMapJeratoO95_7BStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.JeratoO95_7BStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapJeratoO95_7BStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapJeratoO95_7BStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapJeratoO95_7BStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapJeratoO95_7BStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 13)
        {
            if (WorldMapJeratoO95_14CStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.JeratoO95_14CStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapJeratoO95_14CStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapJeratoO95_14CStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapJeratoO95_14CStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapJeratoO95_14CStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 14)
        {
            if (WorldMapJeratoO95_14DStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.JeratoO95_14DStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapJeratoO95_14DStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapJeratoO95_14DStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapJeratoO95_14DStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapJeratoO95_14DStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }
        else if (Area == 15)
        {
            if (WorldMapJeratoO95_OmegaStar.gameObject.GetComponent<StarBattleSystem>().isInFight == false && AreaStatement.JeratoO95_OmegaStarState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapJeratoO95_OmegaStar.gameObject.GetComponent<StarBattleSystem>().isInFight = true;
                WorldMapJeratoO95_OmegaStar.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapJeratoO95_OmegaStar.gameObject.GetComponent<EnemyGet>().WarpFleetDestination = Destination;
                WorldMapJeratoO95_OmegaStar.gameObject.GetComponent<EnemyGet>().enabled = true;
            }
        }

        //행성
        else if(Area == 1001)
        {
            if (WorldMapSatariusGlessia.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.SatariusGlessiaState > 1)
            {
                WorldMapSatariusGlessia.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapSatariusGlessia.gameObject.GetComponent<EnemyGet>().WarpControsType = 2;
                WorldMapSatariusGlessia.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1002)
        {
            if (WorldMapAposis.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.AposisState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapAposis.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapAposis.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapAposis.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1003)
        {
            if (WorldMapTorono.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.ToronoState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapTorono.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapTorono.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapTorono.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1004)
        {
            if (WorldMapPlopa2.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.Plopa2State > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapPlopa2.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapPlopa2.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapPlopa2.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1005)
        {
            if (WorldMapVedes4.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.Vedes4State > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapVedes4.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapVedes4.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapVedes4.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1006)
        {
            if (WorldMapAronPeri.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.AronPeriState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapAronPeri.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapAronPeri.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapAronPeri.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1007)
        {
            if (WorldMapPapatus2.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.Papatus2State > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapPapatus2.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapPapatus2.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapPapatus2.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1008)
        {
            if (WorldMapPapatus3.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.Papatus3State > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapPapatus3.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapPapatus3.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapPapatus3.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1009)
        {
            if (WorldMapKyepotoros.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.KyepotorosState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapKyepotoros.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapKyepotoros.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapKyepotoros.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1010)
        {
            if (WorldMapTratos.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.TratosState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapTratos.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapTratos.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapTratos.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1011)
        {
            if (WorldMapOclasis.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.OclasisState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapOclasis.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapOclasis.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapOclasis.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1012)
        {
            if (WorldMapDeriousHeri.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.DeriousHeriState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapDeriousHeri.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapDeriousHeri.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapDeriousHeri.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1013)
        {
            if (WorldMapVeltrorexy.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.VeltrorexyState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapVeltrorexy.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapVeltrorexy.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapVeltrorexy.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1014)
        {
            if (WorldMapErixJeoqeta.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.ErixJeoqetaState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapErixJeoqeta.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapErixJeoqeta.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapErixJeoqeta.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1015)
        {
            if (WorldMapQeepo.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.QeepoState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapQeepo.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapQeepo.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapQeepo.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1016)
        {
            if (WorldMapCrownYosere.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.CrownYosereState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapCrownYosere.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapCrownYosere.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapCrownYosere.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1017)
        {
            if (WorldMapOros.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.OrosState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapOros.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapOros.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapOros.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1018)
        {
            if (WorldMapJapetAgrone.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.JapetAgroneState > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapJapetAgrone.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapJapetAgrone.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapJapetAgrone.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1019)
        {
            if (WorldMapXacro042351.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.Xacro042351State > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapXacro042351.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapXacro042351.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapXacro042351.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1020)
        {
            if (WorldMapDeltaD31_2208.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.DeltaD31_2208State > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapDeltaD31_2208.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapDeltaD31_2208.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapDeltaD31_2208.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1021)
        {
            if (WorldMapDeltaD31_9523.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.DeltaD31_9523State > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapDeltaD31_9523.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapDeltaD31_9523.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapDeltaD31_9523.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1022)
        {
            if (WorldMapDeltaD31_12721.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.DeltaD31_12721State > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapDeltaD31_12721.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapDeltaD31_12721.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapDeltaD31_12721.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1023)
        {
            if (WorldMapJeratoO95_1125.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.JeratoO95_1125State > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapJeratoO95_1125.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapJeratoO95_1125.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapJeratoO95_1125.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1024)
        {
            if (WorldMapJeratoO95_2252.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.JeratoO95_2252State > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapJeratoO95_2252.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapJeratoO95_2252.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapJeratoO95_2252.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }
        else if (Area == 1025)
        {
            if (WorldMapJeratoO95_8510.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight == false && AreaStatement.JeratoO95_8510State > 1)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldMapJeratoO95_8510.gameObject.GetComponent<PlanetOurForceShipsManager>().isInFight = true;
                WorldMapJeratoO95_8510.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldMapJeratoO95_8510.gameObject.GetComponent<EnemyGet>().enabled = true;
                StartCoroutine(LiveCommunicationSystem.MainCommunication(6.01f));
            }
        }

        //전투 지역
        else if (Area == 10000)
        {
            if (WorldToropioRandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldToropioRandomSite1.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                if (BattleSave.Save1.FirstStart == false)
                    WorldToropioRandomSite1.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                else
                    WorldToropioRandomSite1.gameObject.GetComponent<EnemyGet>().WarpControsType = 2;
                WorldToropioRandomSite1.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldToropioRandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
            else if(WorldToropioRandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldToropioRandomSite1.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldToropioRandomSite1.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldToropioRandomSite1.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldToropioRandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10001)
        {
            if (WorldToropioRandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldToropioRandomSite2.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldToropioRandomSite2.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldToropioRandomSite2.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldToropioRandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10002)
        {
            if (WorldRoroRandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldRoroRandomSite1.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldRoroRandomSite1.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldRoroRandomSite1.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldRoroRandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10003)
        {
            if (WorldRoroRandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldRoroRandomSite2.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldRoroRandomSite2.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldRoroRandomSite2.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldRoroRandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10004)
        {
            if (WorldRoroRandomSite3.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldRoroRandomSite3.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldRoroRandomSite3.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldRoroRandomSite3.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldRoroRandomSite3.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10005)
        {
            if (WorldSarisiRandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldSarisiRandomSite1.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldSarisiRandomSite1.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldSarisiRandomSite1.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldSarisiRandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10006)
        {
            if (WorldSarisiRandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldSarisiRandomSite2.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldSarisiRandomSite2.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldSarisiRandomSite2.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldSarisiRandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10007)
        {
            if (WorldSarisiRandomSite3.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldSarisiRandomSite3.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldSarisiRandomSite3.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldSarisiRandomSite3.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldSarisiRandomSite3.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10008)
        {
            if (WorldGarixRandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldGarixRandomSite1.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldGarixRandomSite1.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldGarixRandomSite1.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldGarixRandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10009)
        {
            if (WorldGarixRandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldGarixRandomSite2.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldGarixRandomSite2.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldGarixRandomSite2.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldGarixRandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10010)
        {
            if (WorldGarixRandomSite3.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldGarixRandomSite3.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldGarixRandomSite3.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldGarixRandomSite3.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldGarixRandomSite3.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10011)
        {
            if (WorldOctoKrasisPatoroRandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldOctoKrasisPatoroRandomSite1.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldOctoKrasisPatoroRandomSite1.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldOctoKrasisPatoroRandomSite1.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldOctoKrasisPatoroRandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10012)
        {
            if (WorldOctoKrasisPatoroRandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldOctoKrasisPatoroRandomSite2.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldOctoKrasisPatoroRandomSite2.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldOctoKrasisPatoroRandomSite2.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldOctoKrasisPatoroRandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10013)
        {
            if (WorldOctoKrasisPatoroRandomSite3.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldOctoKrasisPatoroRandomSite3.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldOctoKrasisPatoroRandomSite3.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldOctoKrasisPatoroRandomSite3.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldOctoKrasisPatoroRandomSite3.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10014)
        {
            if (WorldOctoKrasisPatoroRandomSite4.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldOctoKrasisPatoroRandomSite4.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldOctoKrasisPatoroRandomSite4.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldOctoKrasisPatoroRandomSite4.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldOctoKrasisPatoroRandomSite4.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10015)
        {
            if (WorldDeltaD31_402054RandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldDeltaD31_402054RandomSite1.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldDeltaD31_402054RandomSite1.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldDeltaD31_402054RandomSite1.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldDeltaD31_402054RandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10016)
        {
            if (WorldDeltaD31_402054RandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldDeltaD31_402054RandomSite2.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldDeltaD31_402054RandomSite2.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldDeltaD31_402054RandomSite2.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldDeltaD31_402054RandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10017)
        {
            if (WorldDeltaD31_402054RandomSite3.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldDeltaD31_402054RandomSite3.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldDeltaD31_402054RandomSite3.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldDeltaD31_402054RandomSite3.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldDeltaD31_402054RandomSite3.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10018)
        {
            if (WorldDeltaD31_402054RandomSite4.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldDeltaD31_402054RandomSite4.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldDeltaD31_402054RandomSite4.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldDeltaD31_402054RandomSite4.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldDeltaD31_402054RandomSite4.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10019)
        {
            if (WorldDeltaD31_402054RandomSite5.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldDeltaD31_402054RandomSite5.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldDeltaD31_402054RandomSite5.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldDeltaD31_402054RandomSite5.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldDeltaD31_402054RandomSite5.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10020)
        {
            if (WorldJeratoO95_99024RandomSite1.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldJeratoO95_99024RandomSite1.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldJeratoO95_99024RandomSite1.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldJeratoO95_99024RandomSite1.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldJeratoO95_99024RandomSite1.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10021)
        {
            if (WorldJeratoO95_99024RandomSite2.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldJeratoO95_99024RandomSite2.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldJeratoO95_99024RandomSite2.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldJeratoO95_99024RandomSite2.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldJeratoO95_99024RandomSite2.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10022)
        {
            if (WorldJeratoO95_99024RandomSite3.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldJeratoO95_99024RandomSite3.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldJeratoO95_99024RandomSite3.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldJeratoO95_99024RandomSite3.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldJeratoO95_99024RandomSite3.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10023)
        {
            if (WorldJeratoO95_99024RandomSite4.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldJeratoO95_99024RandomSite4.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldJeratoO95_99024RandomSite4.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldJeratoO95_99024RandomSite4.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldJeratoO95_99024RandomSite4.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
        else if (Area == 10024)
        {
            if (WorldJeratoO95_99024RandomSite5.gameObject.GetComponent<RandomSiteBattle>().isInFight == false)
            {
                int RandomEnemy = Random.Range(1, 4);
                WorldJeratoO95_99024RandomSite5.gameObject.GetComponent<RandomSiteBattle>().FlagshipHere = true;
                WorldJeratoO95_99024RandomSite5.gameObject.GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
                WorldJeratoO95_99024RandomSite5.gameObject.GetComponent<EnemyGet>().enabled = true;
                WorldJeratoO95_99024RandomSite5.gameObject.GetComponent<OurForceGet>().enabled = true;
            }
        }
    }

    //우주맵 시작 애니메이션
    IEnumerator UniverseMapOnAnime()
    {
        PlayerShipMapping();
        CalculateMapRatio();
        PlayerPositionSetting();

        if (Tutorial == true)
        {
            TutorialSystem.ViewerPrefab.SetActive(false);
        }

        //함대전 UI버튼 끄기
        CameraFollow.WarpLiveLogs.SetActive(false);
        UniverseMapButtonImage.raycastTarget = false;
        UIControlSystem.MenuUIImage.raycastTarget = false;
        UIControlSystem.ShipModeUIImage.raycastTarget = false;
        UIControlSystem.BehaviorModeUIImage.raycastTarget = false;
        UIControlSystem.SelectButtenImage.raycastTarget = false;
        CameraZoom.CameraImage.raycastTarget = false;
        MultiFlagshipSystem.FlagshipListButtonImage.raycastTarget = false;
        HurricaneOperationMenu.HurricaneOperationButtonImage.raycastTarget = false;
        if (MultiFlagshipSystem.FlagshipListMode == true)
            MultiFlagshipSystem.FlagshipListButtonClick();
        if (HurricaneOperationMenu.MenuStep > 0)
            HurricaneOperationMenu.HurricaneOperationButtonClick();

        CameraZoom.UniverseMapOnline();
        CameraZoom.CameraPrefab.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
        WordPrintSystem.UCCISBootingPrint(1);
        UniverseMapCompleteOff = false;
        StartToGetIconColor();

        yield return new WaitForSecondsRealtime(0.5f);
        //화면 전환이 시작된다.
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().Direction = 0;
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveSetting = true;
        if (WallEffectUCCIS.GetComponent<Animator>().updateMode != AnimatorUpdateMode.UnscaledTime)
            WallEffectUCCIS.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        MainMenuButtonSystem.CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 100);
        LiveCommunication.SetActive(false);

        yield return new WaitForSecondsRealtime(0.05f);
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveStart = true;
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveSetting = false;
        MenuBackground.GetComponent<Animator>().SetBool("Map online, Map background active", true);
        WallEffectUCCIS.GetComponent<Animator>().SetFloat("Menu wall effect1, Menu wall", 1);
        UIControlSystem.MenuUI.GetComponent<Animator>().SetBool("Menu booting, Main menu", true);
        UIControlSystem.ShipModeUI.GetComponent<Animator>().SetBool("Menu booting, Ship Mode Butten", true);
        UIControlSystem.BehaviorModeUI.GetComponent<Animator>().SetBool("Menu booting, Behavior Butten", true);
        UIControlSystem.SelectButtenUI.GetComponent<Animator>().SetBool("Menu booting, Select Butten", true);
        UIControlSystem.UniverseFrame.GetComponent<Animator>().SetBool("Menu booting, Universe Frame", true);
        CameraZoom.CameraUI.GetComponent<Animator>().SetBool("Menu booting, Camera", true);
        MultiFlagshipSystem.FlagshipListButton.GetComponent<Animator>().SetBool("Menu booting, Flagship list", true);
        if (HurricaneOperationMenu.HurricaneOperationButtonPrefab.GetComponent<Animator>().GetFloat("Online, Hurricane operation") == 2)
            HurricaneOperationMenu.HurricaneOperationButtonPrefab.GetComponent<Animator>().SetFloat("Online, Hurricane operation", 0);
        HurricaneOperationMenu.HurricaneOperationButtonPrefab.GetComponent<Animator>().SetBool("Menu booting, Hurricane operation", true);
        if (HurricaneOperationMenu.PlanetAnime.GetComponent<Animator>().GetFloat("Active, Hurricane operation planet") < 2)
            HurricaneOperationMenu.PlanetAnime.SetActive(false);
        HurricaneOperationMenu.HurricaneAnimePrefab.GetComponent<SpriteMask>().enabled = false;

        MenuBooting.GetComponent<Animator>().SetFloat("Menu booting, UCCIS mark", 0);
        UniverseProgressBarUIActive.SetActive(true);
        UniverseProgressBarButtonActive.SetActive(true);
        WordPrintSystem.PrintNumber = 1;
        WordPrintSystem.PrintAreaSelectText();
        CameraZoom.CameraPrefab.GetComponent<Animator>().SetBool("Universe map online, Camera", true);

        yield return new WaitForSecondsRealtime(0.5f);
        UniverseMapPrefab.GetComponent<Animator>().SetBool("Icons online, Universe map", true);
        UniverseProgressBarUI.GetComponent<Animator>().SetBool("Open, Universe Map Progress", true);
        UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step, Progress Button", 1);

        yield return new WaitForSecondsRealtime(0.17f);
        CameraZoom.CameraPrefab.GetComponent<Animator>().SetBool("Universe map online, Camera", false);
        CameraZoom.CameraPrefab.GetComponent<Animator>().cullingMode = AnimatorCullingMode.CullCompletely;

        yield return new WaitForSecondsRealtime(0.33f);
        UniverseMapUI.GetComponent<Animator>().SetFloat("Open, Universe Map Butten", 1);
        UniverseMapUI.GetComponent<Animator>().SetFloat("Active ship, Universe Map Butten", 1);
        UniverseMapUI.GetComponent<Animator>().SetFloat("Star move, Universe Map Butten", 1);
        WordPrintSystem.PrintNumber = 3;
        WordPrintSystem.UniverseConfirmPrintText();
        WordPrintSystem.PrintNumber = 1;
        WordPrintSystem.UniverseMapCancelPrintText();
        WordPrintSystem.HidePlayerShipName();
        WordPrintSystem.PrintAreaName();
        UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step, Progress Button", 2);
        UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step cancel, Progress Button", 1);
        UniverseMapButtonImage.raycastTarget = true;
        yield return new WaitForSecondsRealtime(0.25f);
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveStart = false;
    }

    //우주맵 종료 애니메이션
    IEnumerator UniverseMapOffAnime()
    {
        if (WarpStep == 4)
        {
            WorldPlayer1.gameObject.GetComponent<FlagshipSystemNumber>().SystemDestinationNumber = SystemDestinationNumber;
            if (WorldPlayer2 != null)
                WorldPlayer2.gameObject.GetComponent<FlagshipSystemNumber>().SystemDestinationNumber = SystemDestinationNumber;
            if (WorldPlayer3 != null)
                WorldPlayer3.gameObject.GetComponent<FlagshipSystemNumber>().SystemDestinationNumber = SystemDestinationNumber;
            if (WorldPlayer4 != null)
                WorldPlayer4.gameObject.GetComponent<FlagshipSystemNumber>().SystemDestinationNumber = SystemDestinationNumber;
            if (WorldPlayer5 != null)
                WorldPlayer5.gameObject.GetComponent<FlagshipSystemNumber>().SystemDestinationNumber = SystemDestinationNumber;

            UIEffect.GetComponent<Animator>().SetBool("Ship selected, Tele effects", false);
            WordPrintSystem.WarpReadyPrint(0, 0);
            TeleEffect2.SetActive(false);
            TeleEffect3.SetActive(true);
            UniverseProgressBarUI.GetComponent<Animator>().SetBool("Open, Universe Map Progress", false);
            UniverseProgressBarUI.GetComponent<Animator>().SetFloat("Warp step, Universe Map Progress", 2);
            yield return new WaitForSecondsRealtime(0.416f);
            StartCoroutine(WarpLiveLogPrint());
        }

        CancelSelect(); //우주맵의 주요 시스템 종료
        yield return new WaitForSecondsRealtime(0.1f);
        //함대전 UI로 복귀
        UniverseMapButtonImage.raycastTarget = false;
        UIEffect.GetComponent<Animator>().SetBool("Active, Tele effects", false);
        MenuBooting.GetComponent<Animator>().SetFloat("Menu booting, UCCIS mark", 2);
        WordPrintSystem.UCCISExitingPrint();
        MenuBackground.GetComponent<Animator>().SetBool("Map online, Map background active", false);
        UniverseProgressBarUI.GetComponent<Animator>().SetBool("Open, Universe Map Progress", false);
        UniverseProgressBarUIActive.SetActive(false);
        UniverseProgressBarButtonActive.SetActive(false);
        UniverseMapPrefab.GetComponent<Animator>().SetBool("Icons online, Universe map", false);
        UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step, Progress Button", 0);
        UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step cancel, Progress Button", 0);
        LiveCommunication.SetActive(true);

        if (WorldPlayer1 != null && WorldPlayer1.gameObject.GetComponent<MoveVelocity>().WarpDriveActive == false && WorldPlayer1.gameObject.GetComponent<MoveVelocity>().WarpDriveReady == false)
            Player1Prefab.GetComponent<MapRouter>().DeleteLine();
        if (WorldPlayer2 != null && WorldPlayer2.gameObject.GetComponent<MoveVelocity>().WarpDriveActive == false && WorldPlayer2.gameObject.GetComponent<MoveVelocity>().WarpDriveReady == false)
            Player2Prefab.GetComponent<MapRouter>().DeleteLine();
        if (WorldPlayer3 != null && WorldPlayer3.gameObject.GetComponent<MoveVelocity>().WarpDriveActive == false && WorldPlayer3.gameObject.GetComponent<MoveVelocity>().WarpDriveReady == false)
            Player3Prefab.GetComponent<MapRouter>().DeleteLine();
        if (WorldPlayer4 != null && WorldPlayer4.gameObject.GetComponent<MoveVelocity>().WarpDriveActive == false && WorldPlayer4.gameObject.GetComponent<MoveVelocity>().WarpDriveReady == false)
            Player4Prefab.GetComponent<MapRouter>().DeleteLine();
        if (WorldPlayer5 != null && WorldPlayer5.gameObject.GetComponent<MoveVelocity>().WarpDriveActive == false && WorldPlayer5.gameObject.GetComponent<MoveVelocity>().WarpDriveReady == false)
            Player5Prefab.GetComponent<MapRouter>().DeleteLine();

        yield return new WaitForSecondsRealtime(0.25f);

        if (Tutorial == true) //첫 튜토리얼에서 처음 메시지만 출력하기 위한 목적
        {
            TutorialOnce = false;
            TutorialSystem.ViewerPrefab.SetActive(true);
        }

        //카메라가 함대전으로 전환되는 구간, 함대전 버튼이 사용가능해진다.
        MainMenuButtonSystem.CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 0);
        CameraZoom.UniverseMapOffline();
        UniverseMapCompleteOff = true;
        UniverseMapButtonImage.raycastTarget = true;
        UIControlSystem.MenuUIImage.raycastTarget = true;
        UIControlSystem.ShipModeUIImage.raycastTarget = true;
        UIControlSystem.BehaviorModeUIImage.raycastTarget = true;
        UIControlSystem.SelectButtenImage.raycastTarget = true;
        CameraZoom.CameraImage.raycastTarget = true;
        MultiFlagshipSystem.FlagshipListButtonImage.raycastTarget = true;
        HurricaneOperationMenu.HurricaneOperationButtonImage.raycastTarget = true;

        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().Direction = 1;
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveSetting = true;

        yield return new WaitForSecondsRealtime(0.05f);
        CameraZoom.TurnCameraDamping(); //카메라 댐핑 복원
        CameraFollow.WarpLiveLogs.SetActive(true);
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveStart = true;
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveSetting = false;
        WallEffectUCCIS.GetComponent<Animator>().SetFloat("Menu wall effect1, Menu wall", 2);
        UIControlSystem.MenuUI.GetComponent<Animator>().SetBool("Menu booting, Main menu", false);
        UIControlSystem.ShipModeUI.GetComponent<Animator>().SetBool("Menu booting, Ship Mode Butten", false);
        UIControlSystem.BehaviorModeUI.GetComponent<Animator>().SetBool("Menu booting, Behavior Butten", false);
        UIControlSystem.SelectButtenUI.GetComponent<Animator>().SetBool("Menu booting, Select Butten", false);
        UIControlSystem.UniverseFrame.GetComponent<Animator>().SetBool("Menu booting, Universe Frame", false);
        CameraZoom.CameraUI.GetComponent<Animator>().SetBool("Menu booting, Camera", false);
        MultiFlagshipSystem.FlagshipListButton.GetComponent<Animator>().SetBool("Menu booting, Flagship list", false);
        HurricaneOperationMenu.HurricaneOperationButtonPrefab.GetComponent<Animator>().SetBool("Menu booting, Hurricane operation", false);
        HurricaneOperationMenu.PlanetAnime.SetActive(true);
        HurricaneOperationMenu.HurricaneAnimePrefab.GetComponent<SpriteMask>().enabled = true;
        UniverseMapEnabled = false;

        yield return new WaitForSecondsRealtime(0.35f);
        UniverseMapUI.GetComponent<Animator>().SetFloat("Open, Universe Map Butten", 2);
        UniverseMapUI.GetComponent<Animator>().SetFloat("Active ship, Universe Map Butten", 2);
        UniverseMapUI.GetComponent<Animator>().SetFloat("Star move, Universe Map Butten", 0);
        MenuBooting.GetComponent<Animator>().SetFloat("Menu booting, UCCIS mark", 0);
        UniverseMapButtonImage.raycastTarget = true;
        TeleEffect3.SetActive(false);
        WordPrintSystem.PrintNumber = 1;
        WordPrintSystem.PrintAreaSelectText();

        yield return new WaitForSecondsRealtime(1.25f);
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveStart = false;
        WallEffectUCCIS.GetComponent<Animator>().SetFloat("Menu wall effect1, Menu wall", 0);
        UniverseMapUI.GetComponent<Animator>().SetFloat("Open, Universe Map Butten", 0);
        UniverseMapUI.GetComponent<Animator>().SetFloat("Active ship, Universe Map Butten", 0);
    }

    //워프 실시간 로그 출력
    IEnumerator WarpLiveLogPrint()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        WordPrintSystem.WarpStartLiveLog(AreaNumber);
    }

    //우주지도 축적 계산
    void CalculateMapRatio()
    {
        Vector3 DistanceWorldVector = WorldMapPoint1.position - WorldMapPoint2.position;
        DistanceWorldVector.y = 0f;
        float DistanceWorld = DistanceWorldVector.magnitude;

        float DistanceMiniMap = Mathf.Sqrt(Mathf.Pow((MiniMapPoint1.anchoredPosition.x - MiniMapPoint2.anchoredPosition.x), 2) + Mathf.Pow((MiniMapPoint1.anchoredPosition.y - MiniMapPoint2.anchoredPosition.y), 2));

        MinimapRatio = (DistanceMiniMap / DistanceWorld);

        StarPositionSetting();
        PlanetPositionSetting();
        RandomSitePositionSetting();
    }

    //랜덤 사이트 위치 표시
    void RandomSitePositionSetting()
    {
        if (ToropioRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            if (TutorialMapStep != 2)
                ToropioRandomSite1.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldToropioRandomSite1.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldToropioRandomSite1.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            if (Tutorial == false)
                ToropioRandomSite2.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldToropioRandomSite2.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldToropioRandomSite2.position.y - WorldMapPoint1.position.y) * MinimapRatio);

            if (Tutorial == true)
            {
                TutorialStar.SetActive(false);
                TutorialPlanet.SetActive(false);
                if (FirstBattle == false)
                    TutorialViewer.gameObject.SetActive(true);
                else
                    TutorialViewer.gameObject.SetActive(false);
                if (TutorialMapStep != 2 && TutorialOnce == true)
                    StartCoroutine(TutorialSystem.TutorialWindowOpen(2));
            }
            if (TutorialMapStep > 0)
            {
                TutorialSystem.UniverseMapViewGuidePrefab.SetActive(false);

                if (BattleSave.Save1.LanguageType == 1)
                {
                    TutorialText.text = string.Format("Click here to select destination.");
                    TutorialText2.text = string.Format("Click here to select destination.");
                }
                else if (BattleSave.Save1.LanguageType == 2)
                {
                    TutorialText.text = string.Format("목적지를 선택하려면 여기를 클릭하십시오.");
                    TutorialText2.text = string.Format("목적지를 선택하려면 여기를 클릭하십시오.");
                }
                if (TutorialMapStep == 1)
                {
                    SatariusGlessiaTutorial.SetActive(false);
                    TutorialViewer.anchoredPosition = ToropioRandomSite1.anchoredPosition;
                }
                else if (TutorialMapStep == 2)
                {
                    SatariusGlessiaTutorial.SetActive(true);
                    TutorialViewer.anchoredPosition = SatariusGlessia.anchoredPosition;
                }
            }
        }
        if (RoroRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            RoroRandomSite1.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldRoroRandomSite1.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldRoroRandomSite1.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            RoroRandomSite2.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldRoroRandomSite2.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldRoroRandomSite2.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            RoroRandomSite3.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldRoroRandomSite3.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldRoroRandomSite3.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        }
        if (SarisiRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            SarisiRandomSite1.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldSarisiRandomSite1.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldSarisiRandomSite1.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            SarisiRandomSite2.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldSarisiRandomSite2.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldSarisiRandomSite2.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            SarisiRandomSite3.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldSarisiRandomSite3.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldSarisiRandomSite3.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        }
        if (GarixRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            GarixRandomSite1.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldGarixRandomSite1.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldGarixRandomSite1.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            GarixRandomSite2.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldGarixRandomSite2.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldGarixRandomSite2.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            GarixRandomSite3.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldGarixRandomSite3.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldGarixRandomSite3.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        }
        if (OctoKrasisPatoroRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            OctoKrasisPatoroRandomSite1.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldOctoKrasisPatoroRandomSite1.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldOctoKrasisPatoroRandomSite1.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            OctoKrasisPatoroRandomSite2.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldOctoKrasisPatoroRandomSite2.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldOctoKrasisPatoroRandomSite2.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            OctoKrasisPatoroRandomSite3.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldOctoKrasisPatoroRandomSite3.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldOctoKrasisPatoroRandomSite3.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            OctoKrasisPatoroRandomSite4.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldOctoKrasisPatoroRandomSite4.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldOctoKrasisPatoroRandomSite4.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        }
        if (DeltaD31_402054RandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            DeltaD31_402054RandomSite1.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldDeltaD31_402054RandomSite1.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldDeltaD31_402054RandomSite1.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            DeltaD31_402054RandomSite2.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldDeltaD31_402054RandomSite2.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldDeltaD31_402054RandomSite2.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            DeltaD31_402054RandomSite3.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldDeltaD31_402054RandomSite3.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldDeltaD31_402054RandomSite3.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            DeltaD31_402054RandomSite4.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldDeltaD31_402054RandomSite4.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldDeltaD31_402054RandomSite4.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            DeltaD31_402054RandomSite5.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldDeltaD31_402054RandomSite5.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldDeltaD31_402054RandomSite5.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        }
        if (JeratoO95_99024RandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            JeratoO95_99024RandomSite1.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_99024RandomSite1.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_99024RandomSite1.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            JeratoO95_99024RandomSite2.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_99024RandomSite2.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_99024RandomSite2.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            JeratoO95_99024RandomSite3.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_99024RandomSite3.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_99024RandomSite3.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            JeratoO95_99024RandomSite4.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_99024RandomSite4.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_99024RandomSite4.position.y - WorldMapPoint1.position.y) * MinimapRatio);
            JeratoO95_99024RandomSite5.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_99024RandomSite5.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_99024RandomSite5.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        }
    }

    //항성 위치 표시
    void StarPositionSetting()
    {
        ToropioStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldToropioStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldToropioStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Roro1Star.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldRoro1Star.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldRoro1Star.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Roro2Star.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldRoro2Star.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldRoro2Star.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        SarisiStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldSarisiStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldSarisiStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        GarixStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldGarixStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldGarixStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        OctoKrasisPatoroSystemOrbit.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldOctoKrasisPatoroSystemOrbit.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldOctoKrasisPatoroSystemOrbit.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        SecrosStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldSecrosStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldSecrosStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        TeretosStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldTeretosStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldTeretosStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        MiniPopoStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldMiniPopoStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldMiniPopoStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        DeltaD31_402054SystemOrbit.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldDeltaD31_402054SystemOrbit.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldDeltaD31_402054SystemOrbit.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        DeltaD31_4AStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldDeltaD31_4AStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldDeltaD31_4AStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        DeltaD31_4BStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldDeltaD31_4BStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldDeltaD31_4BStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        JeratoO95_99024SystemOrbit.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_99024SystemOrbit.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_99024SystemOrbit.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        JeratoO95_7Orbit.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_7Orbit.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_7Orbit.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        JeratoO95_14Orbit.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_14Orbit.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_14Orbit.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        JeratoO95_7AStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_7AStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_7AStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        JeratoO95_7BStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_7BStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_7BStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        JeratoO95_14CStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_14CStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_14CStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        JeratoO95_14DStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_14DStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_14DStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        JeratoO95_OmegaStar.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_OmegaStar.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_OmegaStar.position.y - WorldMapPoint1.position.y) * MinimapRatio);
    }

    //행성 위치 표시
    void PlanetPositionSetting()
    {
        SatariusGlessia.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldSatariusGlessia.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldSatariusGlessia.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Aposis.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldAposis.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldAposis.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Torono.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldTorono.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldTorono.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Plopa2.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldPlopa2.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldPlopa2.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Vedes4.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldVedes4.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldVedes4.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        AronPeri.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldAronPeri.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldAronPeri.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Papatus2.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldPapatus2.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldPapatus2.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Papatus3.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldPapatus3.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldPapatus3.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Kyepotoros.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldKyepotoros.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldKyepotoros.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Tratos.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldTratos.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldTratos.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Oclasis.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldOclasis.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldOclasis.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        DeriousHeri.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldDeriousHeri.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldDeriousHeri.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Veltrorexy.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldVeltrorexy.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldVeltrorexy.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        ErixJeoqeta.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldErixJeoqeta.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldErixJeoqeta.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Qeepo.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldQeepo.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldQeepo.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        CrownYosere.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldCrownYosere.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldCrownYosere.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Oros.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldOros.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldOros.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        JapetAgrone.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJapetAgrone.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJapetAgrone.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        Xacro042351.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldXacro042351.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldXacro042351.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        DeltaD31_2208.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldDeltaD31_2208.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldDeltaD31_2208.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        DeltaD31_9523.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldDeltaD31_9523.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldDeltaD31_9523.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        DeltaD31_12721.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldDeltaD31_12721.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldDeltaD31_12721.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        JeratoO95_1125.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_1125.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_1125.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        JeratoO95_2252.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_2252.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_2252.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        JeratoO95_8510.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldJeratoO95_8510.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldJeratoO95_8510.position.y - WorldMapPoint1.position.y) * MinimapRatio);
    }

    //플레이어 함대 위치 표시
    void PlayerPositionSetting()
    {
        Player1.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldPlayer1.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldPlayer1.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        if (WorldPlayer2 != null)
            Player2.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldPlayer2.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldPlayer2.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        if (WorldPlayer3 != null)
            Player3.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldPlayer3.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldPlayer3.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        if (WorldPlayer4 != null)
            Player4.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldPlayer4.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldPlayer4.position.y - WorldMapPoint1.position.y) * MinimapRatio);
        if (WorldPlayer5 != null)
            Player5.anchoredPosition = MiniMapPoint1.anchoredPosition + new Vector2((WorldPlayer5.position.x - WorldMapPoint1.position.x) * MinimapRatio, (WorldPlayer5.position.y - WorldMapPoint1.position.y) * MinimapRatio);
    }

    //플레이어 함대 가져오기
    public void PlayerShipMapping()
    {
        WorldPlayer1 = ShipManager.instance.FlagShipList[0].transform;
        if (ShipManager.instance.FlagShipList.Count > 1)
            WorldPlayer2 = ShipManager.instance.FlagShipList[1].transform;
        if (ShipManager.instance.FlagShipList.Count > 2)
            WorldPlayer3 = ShipManager.instance.FlagShipList[2].transform;
        if (ShipManager.instance.FlagShipList.Count > 3)
            WorldPlayer4 = ShipManager.instance.FlagShipList[3].transform;
        if (ShipManager.instance.FlagShipList.Count > 4)
            WorldPlayer5 = ShipManager.instance.FlagShipList[4].transform;
    }

    void Update()
    {
        if (StartMapping != true)
        {
            //Transform(Quaternion)회전 정보를 RectTransform(eulerAngles)로 전환
            if (WorldPlayer1 != null)
            {
                Quaternion rotation = WorldPlayer1.rotation;
                Vector3 euler = rotation.eulerAngles;
                Player1.eulerAngles = new Vector3(0, 0, euler.z);
            }
            if (WorldPlayer2 != null)
            {
                Quaternion rotation = WorldPlayer2.rotation;
                Vector3 euler = rotation.eulerAngles;
                Player2.eulerAngles = new Vector3(0, 0, euler.z);
            }
            if (WorldPlayer3 != null)
            {
                Quaternion rotation = WorldPlayer3.rotation;
                Vector3 euler = rotation.eulerAngles;
                Player3.eulerAngles = new Vector3(0, 0, euler.z);
            }
            if (WorldPlayer4 != null)
            {
                Quaternion rotation = WorldPlayer4.rotation;
                Vector3 euler = rotation.eulerAngles;
                Player4.eulerAngles = new Vector3(0, 0, euler.z);
            }
            if (WorldPlayer5 != null)
            {
                Quaternion rotation = WorldPlayer5.rotation;
                Vector3 euler = rotation.eulerAngles;
                Player5.eulerAngles = new Vector3(0, 0, euler.z);
            }
        }
    }

    //워프 선택
    public void WarpSelectClick()
    {
        if (WarpStep == 1) //워프 지역 선택 승인
        {
            WarpStep = 2;
            WordPrintSystem.HideAreaName(AreaNumber);
            WordPrintSystem.PrintPlayerShipName();
            UniverseProgressBarUI.GetComponent<Animator>().SetFloat("Warp step, Universe Map Progress", 1);
            SelectAreaOrShip();
            ShowUI.SetActive(false);
            TouchAreaUI.SetActive(false);
            StarImageOff();
            WordPrintSystem.PrintNumber = 3;
            WordPrintSystem.PrintAreaSelectText();
            WordPrintSystem.PrintNumber = 2;
            WordPrintSystem.UniverseMapCancelPrintText();
            UIEffect.GetComponent<Animator>().SetBool("Active, Tele effects", true);
            TeleEffect1.SetActive(true);
            if (TutorialMapStep > 0)
            {
                if (BattleSave.Save1.LanguageType == 1)
                {
                    TutorialText.text = string.Format("Click here to select flagship that want to warp.");
                    TutorialText2.text = string.Format("Click here to select flagship that want to warp.");
                }
                else if (BattleSave.Save1.LanguageType == 2)
                {
                    TutorialText.text = string.Format("워프하려는 기함을 선택하려면 여기를 클릭하십시오.");
                    TutorialText2.text = string.Format("워프하려는 기함을 선택하려면 여기를 클릭하십시오.");
                }
                TutorialViewer.anchoredPosition = Player1.anchoredPosition;
            }
        }
        else if (WarpStep == 3)  //워프 함대 선택 승인
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", WarpStartUIAudio);
            if (AccountOfShip > 1)
                FirstWarpFleet = true;
            WarpStep = 4;
            WordPrintSystem.PlayerWarpPrintText(-1, 0);
            WordPrintSystem.PrintNumber = 4;
            WordPrintSystem.PrintAreaSelectText();

            if (Player1Selet == true)
            {
                Player1Selet = false;
                if (WarpToPlayer != 1)
                {
                    if (AccountOfShip == 1)
                        GetWarpLocation(WorldPlayer1.position, WorldPlayer1.gameObject); //워프 실시
                    else
                    {
                        if (FirstWarpFleet == true)
                        {
                            FirstWarpFleet = false;
                            GetWarpLocation(WorldPlayer1.position, WorldPlayer1.gameObject);
                        }
                        else
                            GetWarpLocationFollow(WorldPlayer1.position, WorldPlayer1.gameObject);
                    }
                }
            }
            if (Player2Selet == true)
            {
                Player2Selet = false;
                if (WarpToPlayer != 2)
                {
                    if (AccountOfShip == 1)
                        GetWarpLocation(WorldPlayer2.position, WorldPlayer2.gameObject);
                    else
                    {
                        if (FirstWarpFleet == true)
                        {
                            FirstWarpFleet = false;
                            GetWarpLocation(WorldPlayer2.position, WorldPlayer2.gameObject);
                        }
                        else
                            GetWarpLocationFollow(WorldPlayer2.position, WorldPlayer2.gameObject);
                    }
                }
            }
            if (Player3Selet == true)
            {
                Player3Selet = false;
                if (WarpToPlayer != 3)
                {
                    if (AccountOfShip == 1)
                        GetWarpLocation(WorldPlayer3.position, WorldPlayer3.gameObject);
                    else
                    {
                        if (FirstWarpFleet == true)
                        {
                            FirstWarpFleet = false;
                            GetWarpLocation(WorldPlayer3.position, WorldPlayer3.gameObject);
                        }
                        else
                            GetWarpLocationFollow(WorldPlayer3.position, WorldPlayer3.gameObject);
                    }
                }
            }
            if (Player4Selet == true)
            {
                Player4Selet = false;
                if (WarpToPlayer != 4)
                {
                    if (AccountOfShip == 1)
                        GetWarpLocation(WorldPlayer4.position, WorldPlayer4.gameObject);
                    else
                    {
                        if (FirstWarpFleet == true)
                        {
                            FirstWarpFleet = false;
                            GetWarpLocation(WorldPlayer4.position, WorldPlayer4.gameObject);
                        }
                        else
                            GetWarpLocationFollow(WorldPlayer4.position, WorldPlayer4.gameObject);
                    }
                }
            }
            if (Player5Selet == true)
            {
                Player5Selet = false;
                if (WarpToPlayer != 5)
                {
                    if (AccountOfShip == 1)
                        GetWarpLocation(WorldPlayer5.position, WorldPlayer5.gameObject);
                    else
                    {
                        if (FirstWarpFleet == true)
                        {
                            FirstWarpFleet = false;
                            GetWarpLocation(WorldPlayer5.position, WorldPlayer5.gameObject);
                        }
                        else
                            GetWarpLocationFollow(WorldPlayer5.position, WorldPlayer5.gameObject);
                    }
                }
            }
            UniverseMapClick();
        }
    }
    public void WarpSelectDown()
    {
        ClickUniverseProgressBarButton = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", SelectButtonAudio);
        UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Click confirm, Progress Button", 1);
    }
    public void WarpSelectUp()
    {
        if (ClickUniverseProgressBarButton == true)
        {
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Click confirm, Progress Button", 2);
            StartCoroutine(ProgressButtonConfirmOff());
        }
        ClickUniverseProgressBarButton = false;
    }
    public void WarpSelectEnter()
    {
        if (ClickUniverseProgressBarButton == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", SelectButtonAudio);
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Click confirm, Progress Button", 1);
        }
    }
    public void WarpSelectExit()
    {
        if (ClickUniverseProgressBarButton == true)
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Click confirm, Progress Button", 0);
    }
    IEnumerator ProgressButtonConfirmOff()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Click confirm, Progress Button", 0);
    }
    IEnumerator ProgressButtonCancelOff()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Click cancel, Progress Button", 0);
    }

    //취소 버튼
    public void CancelSelect()
    {
        if (WarpStep == 0 || WarpStep == 1 || WarpStep == 4) //지역 클릭 및 워프 실시에만 작동, 완전히 모두 초기화(우주맵 버튼을 눌러서 함대전으로 돌아가거나, 지역 선택을 취소하거나 워프를 실시할 때에만 사용)
        {
            if (WarpStep == 1)
            {
                WordPrintSystem.PrintNumber = 1;
                WordPrintSystem.PrintAreaSelectText();
                WarpToPlayer = 0;
            }
            if (WarpStep != 1)
            {
                WordPrintSystem.PrintNumber = 2;
                WordPrintSystem.PrintAreaSelectText();
            }
            WarpStep = 0;
            AreaNumber = 0;
            AccountOfShip = 0;
            FleetSelectCancel(); //플레이어의 모든 선택된 함대 터치 bool 끄기
            PlayerFleetImageOff(); //플레이어 함대 터치 이미지 끄기
            StarImageOn(); //지역 터치 이미지 켜기
            ProgressAreaPrefabOff(); //우주맵 진행 바의 중앙의 지역 아이콘 프리팹 끄기 및 지도의 지역 아이콘 색상을 기존 상태로 되돌리기
            UniverseProgressBarUI.GetComponent<Animator>().SetFloat("Warp step, Universe Map Progress", 0); //진행바 애니메이션 끄기
            UIEffect.GetComponent<Animator>().SetBool("Active, Tele effects", false); //행성 선택시, 통신 도메인 위치설정 애니메이션 끄기
            UIEffect.GetComponent<Animator>().SetBool("Ship selected, Tele effects", false); //함선 선택시, 통신 도메인 활성 이펙트 애니메이션 끄기
            DeselectAreaOrShip(); //우주맵 진행바의 선택 활성화 애니메이션 끄기
            TouchAreaUI.SetActive(false); //터치 UI 끄기
            ShowUI.SetActive(false); //지역 설명창 끄기
            TeleEffect1.SetActive(false); //지역으로부터 통신 주파수 이펙트(지역 선택시) 끄기
            TeleEffect2.SetActive(false); //지역으로부터 통신 주파수 이펙트(함선 선택시) 끄기
            ShipSelectedIcon.GetComponent<Image>().enabled = false; //함선 선택시, 아이콘 활성화 끄기
            UniverseProgressBarShipPrefab.SetActive(false); //진행 바의 함선 아이콘 프리팹 끄기
            WordPrintSystem.HidePlayerShipName();
            WordPrintSystem.PrintAreaName();
            Player1Prefab.GetComponent<MapRouter>().DeleteLine(); //함선과 지역간 경로 라인 끄기
            if (WorldPlayer2 != null)
                Player1Prefab.GetComponent<MapRouter>().DeleteLine();
            if (WorldPlayer3 != null)
                Player1Prefab.GetComponent<MapRouter>().DeleteLine();
            if (WorldPlayer4 != null)
                Player1Prefab.GetComponent<MapRouter>().DeleteLine();
            if (WorldPlayer5 != null)
                Player1Prefab.GetComponent<MapRouter>().DeleteLine();

            if (TutorialMapStep > 0)
            {
                if (BattleSave.Save1.LanguageType == 1)
                {
                    TutorialText.text = string.Format("Click here to select destination.");
                    TutorialText2.text = string.Format("Click here to select destination.");
                }
                else if (BattleSave.Save1.LanguageType == 2)
                {
                    TutorialText.text = string.Format("목적지를 선택하려면 여기를 클릭하십시오.");
                    TutorialText2.text = string.Format("목적지를 선택하려면 여기를 클릭하십시오.");
                }
                if (TutorialMapStep == 1)
                    TutorialViewer.anchoredPosition = ToropioRandomSite1.anchoredPosition;
                else if (TutorialMapStep == 2)
                    TutorialViewer.anchoredPosition = SatariusGlessia.anchoredPosition;
            }
        }
        else if (WarpStep == 2) //지역 선택 이후에만 작동(다시 지역 선택으로 되돌리기)
        {
            WarpStep = 1;
            StarImageOn(); //지역 터치 이미지 켜기
            SelectAreaOrShip();
            TouchAreaUI.SetActive(true);
            TeleEffect1.SetActive(false); //지역으로부터 통신 주파수 이펙트(지역 선택시) 끄기
            WordPrintSystem.AreaExplainWindow(AreaNumber);
            AreaSelectAction();
            UniverseProgressBarUI.GetComponent<Animator>().SetFloat("Warp step, Universe Map Progress", 0); //진행바 애니메이션 끄기
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step, Progress Button", 3);
            WordPrintSystem.HidePlayerShipName();
            WordPrintSystem.PrintAreaName();

            if (TutorialMapStep > 0)
            {
                if (BattleSave.Save1.LanguageType == 1)
                {
                    TutorialText.text = string.Format("Click here to select destination.");
                    TutorialText2.text = string.Format("Click here to select destination.");
                }
                else if (BattleSave.Save1.LanguageType == 2)
                {
                    TutorialText.text = string.Format("목적지를 선택하려면 여기를 클릭하십시오.");
                    TutorialText2.text = string.Format("목적지를 선택하려면 여기를 클릭하십시오.");
                }
                if (TutorialMapStep == 1)
                    TutorialViewer.anchoredPosition = ToropioRandomSite1.anchoredPosition;
                else if (TutorialMapStep == 2)
                    TutorialViewer.anchoredPosition = SatariusGlessia.anchoredPosition;
            }
        }
        else if (WarpStep == 3) //함선 선택 이후에만 작동(함선 선택을 완전 취소)
        {
            AccountOfShip = 0;
            WarpStep = 2;
            if (AccountOfShip == 0)
                UniverseProgressBarShipPrefab.SetActive(false);
            DeselectAreaOrShip();
            FleetSelectCancel();
            PlayerFleetImageOff(); //플레이어 함대 터치 이미지 끄기
            ShipSelectedIcon.GetComponent<Image>().enabled = false;
            TouchAreaUI.SetActive(false);
            TeleEffect1.SetActive(true);
            TeleEffect2.SetActive(false);
            UIEffect.GetComponent<Animator>().SetBool("Ship selected, Tele effects", false);
            WordPrintSystem.PlayerWarpPrintText(-1, 0);
            WordPrintSystem.PrintNumber = 2;
            WordPrintSystem.UniverseMapCancelPrintText();

            if (WorldPlayer1 != null)
                Player1Prefab.GetComponent<MapRouter>().DeleteLine();
            if (WorldPlayer2 != null)
                Player2Prefab.GetComponent<MapRouter>().DeleteLine();
            if (WorldPlayer3 != null)
                Player3Prefab.GetComponent<MapRouter>().DeleteLine();
            if (WorldPlayer4 != null)
                Player4Prefab.GetComponent<MapRouter>().DeleteLine();
            if (WorldPlayer5 != null)
                Player5Prefab.GetComponent<MapRouter>().DeleteLine();
        }
    }
    public void WarpCancelDown()
    {
        ClickUniverseProgressBarButton = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", SelectButtonAudio);
        UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Click cancel, Progress Button", 1);
    }
    public void WarpCancelUp()
    {
        if (ClickUniverseProgressBarButton == true)
        {
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Click cancel, Progress Button", 2);
            StartCoroutine(ProgressButtonCancelOff());
        }
        ClickUniverseProgressBarButton = false;
    }
    public void WarpCancelEnter()
    {
        if (ClickUniverseProgressBarButton == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", SelectButtonAudio);
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Click cancel, Progress Button", 1);
        }
    }
    public void WarpCancelExit()
    {
        if (ClickUniverseProgressBarButton == true)
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Click cancel, Progress Button", 0);
    }

    //선택 활성화 애니메이션
    void SelectAreaOrShip()
    {
        AreaSelectGet(); //지역 좌표

        if (WarpStep == 1)
        {
            WordPrintSystem.PrintNumber = 1;
            WordPrintSystem.UniverseConfirmPrintText();
            WordPrintSystem.PrintNumber = 1;
            WordPrintSystem.UniverseMapCancelPrintText();
            WordPrintSystem.PrintNumber = 1;
            WordPrintSystem.PrintAreaSelectText();

            UniverseProgressBarUI.GetComponent<Animator>().SetFloat("Selected, Universe Map Progress", 0);
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step, Progress Button", 3);
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step cancel, Progress Button", 2); //선택 버튼 초기화
        }
        else if (WarpStep == 2)
        {
            WordPrintSystem.PrintNumber = 3;
            WordPrintSystem.UniverseConfirmPrintText();
            WordPrintSystem.PrintNumber = 2;
            WordPrintSystem.UniverseMapCancelPrintText();
            WordPrintSystem.PrintNumber = 3;
            WordPrintSystem.PrintAreaSelectText(); //선택 요청문 출력

            UniverseProgressBarUI.GetComponent<Animator>().SetFloat("Selected, Universe Map Progress", 0);
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step, Progress Button", 2);
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step cancel, Progress Button", 2);
        }
        else if (WarpStep == 3)
        {
            if (AccountOfShip > 0)
            {
                if (Player1Selet == true)
                {
                    JourneyTime(WorldPlayer1.gameObject.GetComponent<MoveVelocity>().WarpSpeed, WorldPlayer1.position, WorldPlayer1, 1); //워프 항해 소요 시간 계산
                    WordPrintSystem.PrintNumber = 1;
                    WordPrintSystem.PlayerWarpPrintText(timeTaken, 1);
                }
                if (Player2Selet == true)
                {
                    JourneyTime(WorldPlayer2.gameObject.GetComponent<MoveVelocity>().WarpSpeed, WorldPlayer2.position, WorldPlayer2, 2);
                    WordPrintSystem.PrintNumber = 1;
                    WordPrintSystem.PlayerWarpPrintText(timeTaken, 2);
                }
                if (Player3Selet == true)
                {
                    JourneyTime(WorldPlayer3.gameObject.GetComponent<MoveVelocity>().WarpSpeed, WorldPlayer3.position, WorldPlayer3, 3);
                    WordPrintSystem.PrintNumber = 1;
                    WordPrintSystem.PlayerWarpPrintText(timeTaken, 3);
                }
                if (Player4Selet == true)
                {
                    JourneyTime(WorldPlayer4.gameObject.GetComponent<MoveVelocity>().WarpSpeed, WorldPlayer4.position, WorldPlayer4, 4);
                    WordPrintSystem.PrintNumber = 1;
                    WordPrintSystem.PlayerWarpPrintText(timeTaken, 4);
                }
                if (Player5Selet == true)
                {
                    JourneyTime(WorldPlayer5.gameObject.GetComponent<MoveVelocity>().WarpSpeed, WorldPlayer5.position, WorldPlayer5, 5);
                    WordPrintSystem.PrintNumber = 1;
                    WordPrintSystem.PlayerWarpPrintText(timeTaken, 5);
                }
                WordPrintSystem.WarpReadyPrint(AreaNumber, WarpToPlayer);
                WordPrintSystem.PrintTypeNumber = 0;
                WordPrintSystem.PrintNumber = 3;
                WordPrintSystem.UniverseMapCancelPrintText();
                WordPrintSystem.PrintNumber = 2;
                WordPrintSystem.UniverseConfirmPrintText();
                WordPrintSystem.PrintNumber = 5;
                WordPrintSystem.PrintAreaSelectText();

                UniverseProgressBarUI.GetComponent<Animator>().SetFloat("Selected, Universe Map Progress", 1);
                UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step, Progress Button", 3);
                UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step cancel, Progress Button", 2);
            }
        }
    }
    void DeselectAreaOrShip()
    {
        if (WarpStep == 3) //함선이 선택된 상태
        {
            //선택된 함선만 메시지 해제
            if (Player1Selet == false)
                WordPrintSystem.PlayerWarpPrintText(0, 1);
            if (Player2Selet == false)
                WordPrintSystem.PlayerWarpPrintText(0, 2);
            if (Player3Selet == false)
                WordPrintSystem.PlayerWarpPrintText(0, 3);
            if (Player4Selet == false)
                WordPrintSystem.PlayerWarpPrintText(0, 4);
            if (Player5Selet == false)
                WordPrintSystem.PlayerWarpPrintText(0, 5);

            if (AccountOfShip > 0) //선택된 함대가 1대 이상 존재할 경우, 버튼과 중앙 상단 메시지 유지
            {
                UniverseProgressBarUI.GetComponent<Animator>().SetFloat("Selected, Universe Map Progress", 1); //중앙 상단 선택 상태 유지
                UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step, Progress Button", 3); //워프 가능 버튼 상태를 유지
                WordPrintSystem.PrintNumber = 3;
                WordPrintSystem.UniverseMapCancelPrintText();
            }
        }
        else if (WarpStep == 2) //함선 선택
        {
            WordPrintSystem.PlayerWarpPrintText(-1, 0);
            WordPrintSystem.WarpReadyPrint(0, 0);

            WordPrintSystem.PrintNumber = 3;
            WordPrintSystem.UniverseConfirmPrintText();
            WordPrintSystem.PrintNumber = 2;
            WordPrintSystem.UniverseMapCancelPrintText();
            WordPrintSystem.PrintNumber = 3;
            WordPrintSystem.PrintAreaSelectText();
            UniverseProgressBarUI.GetComponent<Animator>().SetFloat("Selected, Universe Map Progress", 0); //중앙 상단 선택 상태 중단
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step, Progress Button", 2);
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step cancel, Progress Button", 2);
        }
        else if (WarpStep == 1) //지역까지 모두 취소된 상태
        {
            WordPrintSystem.PrintNumber = 1;
            WordPrintSystem.UniverseConfirmPrintText();
            WordPrintSystem.PrintNumber = 1;
            WordPrintSystem.UniverseMapCancelPrintText();
            WordPrintSystem.PrintNumber = 1;
            WordPrintSystem.PrintAreaSelectText();
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step, Progress Button", 3);
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step cancel, Progress Button", 2); //선택 버튼 초기화
        }
        else if (WarpStep == 0) //완전 취소
        {
            WordPrintSystem.PrintNumber = 3;
            WordPrintSystem.UniverseConfirmPrintText();
            WordPrintSystem.PrintNumber = 1;
            WordPrintSystem.UniverseMapCancelPrintText();
            WordPrintSystem.PrintNumber = 1;
            WordPrintSystem.PrintAreaSelectText();
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step, Progress Button", 2);
            UniverseProgressBarButton.GetComponent<Animator>().SetFloat("Step cancel, Progress Button", 1); //선택 버튼 초기화
            WarpToPlayerArea = new Vector3(0, 0, 0);
        }
    }

    //천체 버튼 활성화
    void StarImageOn()
    {
        //랜덤 사이트
        if (ToropioRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            ToropioRandomSite1Image.raycastTarget = true;
            ToropioRandomSite2Image.raycastTarget = true;
        }
        if (RoroRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            RoroRandomSite1Image.raycastTarget = true;
            RoroRandomSite2Image.raycastTarget = true;
            RoroRandomSite3Image.raycastTarget = true;
        }
        if (SarisiRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            SarisiRandomSite1Image.raycastTarget = true;
            SarisiRandomSite2Image.raycastTarget = true;
            SarisiRandomSite3Image.raycastTarget = true;
        }
        if (GarixRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            GarixRandomSite1Image.raycastTarget = true;
            GarixRandomSite2Image.raycastTarget = true;
            GarixRandomSite3Image.raycastTarget = true;
        }
        if (OctoKrasisPatoroRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            OctoKrasisPatoroRandomSite1Image.raycastTarget = true;
            OctoKrasisPatoroRandomSite2Image.raycastTarget = true;
            OctoKrasisPatoroRandomSite3Image.raycastTarget = true;
            OctoKrasisPatoroRandomSite4Image.raycastTarget = true;
        }
        if (DeltaD31_402054RandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            DeltaD31_402054RandomSite1Image.raycastTarget = true;
            DeltaD31_402054RandomSite2Image.raycastTarget = true;
            DeltaD31_402054RandomSite3Image.raycastTarget = true;
            DeltaD31_402054RandomSite4Image.raycastTarget = true;
            DeltaD31_402054RandomSite5Image.raycastTarget = true;
        }
        if (JeratoO95_99024RandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            JeratoO95_99024RandomSite1Image.raycastTarget = true;
            JeratoO95_99024RandomSite2Image.raycastTarget = true;
            JeratoO95_99024RandomSite3Image.raycastTarget = true;
            JeratoO95_99024RandomSite4Image.raycastTarget = true;
            JeratoO95_99024RandomSite5Image.raycastTarget = true;
        }

        //항성
        ToropioStarImage.raycastTarget = true;
        Roro1StarImage.raycastTarget = true;
        Roro2StarImage.raycastTarget = true;
        SarisiStarImage.raycastTarget = true;
        GarixStarImage.raycastTarget = true;
        SecrosStarImage.raycastTarget = true;
        TeretosStarImage.raycastTarget = true;
        MiniPopoStarImage.raycastTarget = true;
        DeltaD31_4AStarImage.raycastTarget = true;
        DeltaD31_4BStarImage.raycastTarget = true;
        JeratoO95_7AStarImage.raycastTarget = true;
        JeratoO95_7BStarImage.raycastTarget = true;
        JeratoO95_14CStarImage.raycastTarget = true;
        JeratoO95_14DStarImage.raycastTarget = true;
        JeratoO95_OmegaStarImage.raycastTarget = true;

        //행성
        //토로피오 항성계
        SatariusGlessiaImage.raycastTarget = true;
        AposisImage.raycastTarget = true;
        ToronoImage.raycastTarget = true;
        Plopa2Image.raycastTarget = true;
        Vedes4Image.raycastTarget = true;

        //로로 항성계
        AronPeriImage.raycastTarget = true;
        Papatus2Image.raycastTarget = true;
        Papatus3Image.raycastTarget = true;
        KyepotorosImage.raycastTarget = true;

        //사리시 항성계
        TratosImage.raycastTarget = true;
        OclasisImage.raycastTarget = true;
        DeriousHeriImage.raycastTarget = true;

        //가릭스 항성계
        VeltrorexyImage.raycastTarget = true;
        ErixJeoqetaImage.raycastTarget = true;
        QeepoImage.raycastTarget = true;
        CrownYosereImage.raycastTarget = true;

        //옥토크라시스 파토로 항성계
        OrosImage.raycastTarget = true;
        JapetAgroneImage.raycastTarget = true;
        Xacro042351Image.raycastTarget = true;

        //델타 D31-402054 항성계
        DeltaD31_2208Image.raycastTarget = true;
        DeltaD31_9523Image.raycastTarget = true;
        DeltaD31_12721Image.raycastTarget = true;

        //제라토 O95-99024 항성계
        JeratoO95_1125Image.raycastTarget = true;
        JeratoO95_2252Image.raycastTarget = true;
        JeratoO95_8510Image.raycastTarget = true;
    }
    void StarImageOff()
    {
        //랜덤 사이트
        if (ToropioRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            ToropioRandomSite1Image.raycastTarget = false;
            ToropioRandomSite2Image.raycastTarget = false;
        }
        if (RoroRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            RoroRandomSite1Image.raycastTarget = false;
            RoroRandomSite2Image.raycastTarget = false;
            RoroRandomSite3Image.raycastTarget = false;
        }
        if (SarisiRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            SarisiRandomSite1Image.raycastTarget = false;
            SarisiRandomSite2Image.raycastTarget = false;
            SarisiRandomSite3Image.raycastTarget = false;
        }
        if (GarixRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            GarixRandomSite1Image.raycastTarget = false;
            GarixRandomSite2Image.raycastTarget = false;
            GarixRandomSite3Image.raycastTarget = false;
        }
        if (OctoKrasisPatoroRandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            OctoKrasisPatoroRandomSite1Image.raycastTarget = false;
            OctoKrasisPatoroRandomSite2Image.raycastTarget = false;
            OctoKrasisPatoroRandomSite3Image.raycastTarget = false;
            OctoKrasisPatoroRandomSite4Image.raycastTarget = false;
        }
        if (DeltaD31_402054RandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            DeltaD31_402054RandomSite1Image.raycastTarget = false;
            DeltaD31_402054RandomSite2Image.raycastTarget = false;
            DeltaD31_402054RandomSite3Image.raycastTarget = false;
            DeltaD31_402054RandomSite4Image.raycastTarget = false;
            DeltaD31_402054RandomSite5Image.raycastTarget = false;
        }
        if (JeratoO95_99024RandomSitePrefab.GetComponent<RandomSiteManager>().isFlagship == true)
        {
            JeratoO95_99024RandomSite1Image.raycastTarget = false;
            JeratoO95_99024RandomSite2Image.raycastTarget = false;
            JeratoO95_99024RandomSite3Image.raycastTarget = false;
            JeratoO95_99024RandomSite4Image.raycastTarget = false;
            JeratoO95_99024RandomSite5Image.raycastTarget = false;
        }

        //항성
        ToropioStarImage.raycastTarget = false;
        Roro1StarImage.raycastTarget = false;
        Roro2StarImage.raycastTarget = false;
        SarisiStarImage.raycastTarget = false;
        GarixStarImage.raycastTarget = false;
        SecrosStarImage.raycastTarget = false;
        TeretosStarImage.raycastTarget = false;
        MiniPopoStarImage.raycastTarget = false;
        DeltaD31_4AStarImage.raycastTarget = false;
        DeltaD31_4BStarImage.raycastTarget = false;
        JeratoO95_7AStarImage.raycastTarget = false;
        JeratoO95_7BStarImage.raycastTarget = false;
        JeratoO95_14CStarImage.raycastTarget = false;
        JeratoO95_14DStarImage.raycastTarget = false;
        JeratoO95_OmegaStarImage.raycastTarget = false;

        //행성
        //토로피오 항성계
        SatariusGlessiaImage.raycastTarget = false;
        AposisImage.raycastTarget = false;
        ToronoImage.raycastTarget = false;
        Plopa2Image.raycastTarget = false;
        Vedes4Image.raycastTarget = false;

        //로로 항성계
        AronPeriImage.raycastTarget = false;
        Papatus2Image.raycastTarget = false;
        Papatus3Image.raycastTarget = false;
        KyepotorosImage.raycastTarget = false;

        //사리시 항성계
        TratosImage.raycastTarget = false;
        OclasisImage.raycastTarget = false;
        DeriousHeriImage.raycastTarget = false;

        //가릭스 항성계
        VeltrorexyImage.raycastTarget = false;
        ErixJeoqetaImage.raycastTarget = false;
        QeepoImage.raycastTarget = false;
        CrownYosereImage.raycastTarget = false;

        //옥토크라시스 파토로 항성계
        OrosImage.raycastTarget = false;
        JapetAgroneImage.raycastTarget = false;
        Xacro042351Image.raycastTarget = false;

        //델타 D31-402054 항성계
        DeltaD31_2208Image.raycastTarget = false;
        DeltaD31_9523Image.raycastTarget = false;
        DeltaD31_12721Image.raycastTarget = false;

        //제라토 O95-99024 항성계
        JeratoO95_1125Image.raycastTarget = false;
        JeratoO95_2252Image.raycastTarget = false;
        JeratoO95_8510Image.raycastTarget = false;
    }

    //플레이어 함대 버튼 색상 비활성화
    void PlayerFleetImageOff()
    {
        if (WorldPlayer1 != null && WarpToPlayer != 1)
        {
            Player1Number = 0;
            Player1Prefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", false);
            Color ShipNormalColor = IconShipNormal;
            Player1Image.color = ShipNormalColor;
        }
        if (WorldPlayer2 != null && WarpToPlayer != 2)
        {
            Player2Number = 0;
            Player2Prefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", false);
            Color ShipNormalColor = IconShipNormal;
            Player2Image.color = ShipNormalColor;
        }
        if (WorldPlayer3 != null && WarpToPlayer != 3)
        {
            Player3Number = 0;
            Player3Prefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", false);
            Color ShipNormalColor = IconShipNormal;
            Player3Image.color = ShipNormalColor;
        }
        if (WorldPlayer4 != null && WarpToPlayer != 4)
        {
            Player4Number = 0;
            Player4Prefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", false);
            Color ShipNormalColor = IconShipNormal;
            Player4Image.color = ShipNormalColor;
        }
        if (WorldPlayer5 != null && WarpToPlayer != 5)
        {
            Player5Number = 0;
            Player5Prefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", false);
            Color ShipNormalColor = IconShipNormal;
            Player5Image.color = ShipNormalColor;
        }
    }

    //플레이어 터치 bool 끄기(함대 선택 취소, Back 버튼으로 취소, 우주맵 버튼 눌러서 함대전으로 돌아갈 때, 워프를 실시할 때 사용)
    void FleetSelectCancel()
    {
        Player1Selet = false;
        Player2Selet = false;
        Player3Selet = false;
        Player4Selet = false;
        Player5Selet = false;
        Player1Number = 0;
        Player2Number = 0;
        Player3Number = 0;
        Player4Number = 0;
        Player5Number = 0;
        WordPrintSystem.PlayerWarpPrintText(-1, 0);
    }

    //워프 실시
    public void GetWarpLocation(Vector3 Player, GameObject Flagship)
    {
        //플레이어 함대와 목적지 사이에 정해진 거리만큼 Vector3 생성
        float Distance = Vector3.Distance(Player, WarpDestination);
        Vector3 Direction = (WarpDestination - Player).normalized;
        float DistancePercent = Distance - MinusOfCenter;
        Vector3 Point = Player + Direction * DistancePercent;

        float LightYear = Distance / 200;
        float AstronomicalUnit = LightYear / 63241.07f;
        if (SystemDestinationNumber == Flagship.gameObject.GetComponent<FlagshipSystemNumber>().SystemNowNumber)
            ScoreManager.instance.TotalWarpDistance += AstronomicalUnit;
        else
            ScoreManager.instance.TotalWarpDistance += LightYear;

        //생성된 Vector3 주변으로 랜덤 워프 도착지점 생성
        float RandomMovement1 = Random.Range(-5, 5);
        float RandomMovement2 = Random.Range(-5, 5);
        Destination = new Vector2(Point.x + RandomMovement1, Point.y + RandomMovement2);

        if (AreaNumber >= 1 && AreaNumber <= 15 || AreaNumber >= 1001 && AreaNumber <= 10024)
            BattleSiteEnemyGet(AreaNumber);

        //워프 돌입시, 꺼야할 부분들을 모두 종료
        if (Flagship.GetComponent<MoveVelocity>().ShipControlMode == false)
        {
            Flagship.GetComponent<MoveVelocity>().ShipControlMode = true;
            UIControlSystem.ShipControlModeOffWarp(Flagship);
        }
        if (Flagship.GetComponent<MoveVelocity>().ShipSelectionMode == true)
        {
            Flagship.GetComponent<MoveVelocity>().ShipSelectionMode = false;
            UIControlSystem.ShipFormationSettingModeOffWarp(Flagship);
        }
        Flagship.GetComponent<ShipRTS>().FastWarpLoactionGet(Destination);
    }

    //다른 함대의 목적지를 향해 워프
    public void GetWarpLocationFollow(Vector3 Player, GameObject Flagship)
    {
        //생성된 Vector3 주변으로 랜덤 워프 도착지점 생성
        float RandomMovement1 = Random.Range(-30, 30);
        DestinationFollow = new Vector2(Destination.x + RandomMovement1, Destination.y + RandomMovement1);

        float Distance = Vector3.Distance(Player, WarpDestination);
        float LightYear = Distance / 200;
        float AstronomicalUnit = LightYear / 63241.07f;
        if (SystemDestinationNumber == Flagship.gameObject.GetComponent<FlagshipSystemNumber>().SystemNowNumber)
            ScoreManager.instance.TotalWarpDistance += AstronomicalUnit;
        else
            ScoreManager.instance.TotalWarpDistance += LightYear;

        //워프 돌입시, 꺼야할 부분들을 모두 종료
        if (Flagship.GetComponent<MoveVelocity>().ShipControlMode == false)
        {
            Flagship.GetComponent<MoveVelocity>().ShipControlMode = true;
            UIControlSystem.ShipControlModeOffWarp(Flagship);
        }
        if (Flagship.GetComponent<MoveVelocity>().ShipSelectionMode == true)
        {
            Flagship.GetComponent<MoveVelocity>().ShipSelectionMode = false;
            UIControlSystem.ShipFormationSettingModeOffWarp(Flagship);
        }
        Flagship.GetComponent<ShipRTS>().FastWarpLoactionGet(DestinationFollow);
    }

    //지정된 목표 지역으로 워프 장소로 선택
    void AreaSelectGet()
    {
        //랜덤 사이트
        if (AreaNumber == 10000)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldToropioRandomSite1.transform.position;
        }
        else if (AreaNumber == 10001)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldToropioRandomSite2.transform.position;
        }
        else if(AreaNumber == 10002)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldRoroRandomSite1.transform.position;
        }
        else if (AreaNumber == 10003)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldRoroRandomSite2.transform.position;
        }
        else if (AreaNumber == 10004)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldRoroRandomSite3.transform.position;
        }
        else if(AreaNumber == 10005)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldSarisiRandomSite1.transform.position;
        }
        else if (AreaNumber == 10006)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldSarisiRandomSite2.transform.position;
        }
        else if (AreaNumber == 10007)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldSarisiRandomSite3.transform.position;
        }
        else if(AreaNumber == 10008)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldGarixRandomSite1.transform.position;
        }
        else if (AreaNumber == 10009)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldGarixRandomSite2.transform.position;
        }
        else if (AreaNumber == 10010)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldGarixRandomSite3.transform.position;
        }
        else if(AreaNumber == 10011)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldOctoKrasisPatoroRandomSite1.transform.position;
        }
        else if (AreaNumber == 10012)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldOctoKrasisPatoroRandomSite2.transform.position;
        }
        else if (AreaNumber == 10013)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldOctoKrasisPatoroRandomSite3.transform.position;
        }
        else if (AreaNumber == 10014)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldOctoKrasisPatoroRandomSite4.transform.position;
        }
        else if(AreaNumber == 10015)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldDeltaD31_402054RandomSite1.transform.position;
        }
        else if (AreaNumber == 10016)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldDeltaD31_402054RandomSite2.transform.position;
        }
        else if (AreaNumber == 10017)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldDeltaD31_402054RandomSite3.transform.position;
        }
        else if (AreaNumber == 10018)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldDeltaD31_402054RandomSite4.transform.position;
        }
        else if (AreaNumber == 10019)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldDeltaD31_402054RandomSite5.transform.position;
        }
        else if(AreaNumber == 10020)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldJeratoO95_99024RandomSite1.transform.position;
        }
        else if (AreaNumber == 10021)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldJeratoO95_99024RandomSite2.transform.position;
        }
        else if (AreaNumber == 10022)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldJeratoO95_99024RandomSite3.transform.position;
        }
        else if (AreaNumber == 10023)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldJeratoO95_99024RandomSite4.transform.position;
        }
        else if (AreaNumber == 10024)
        {
            MinusOfCenter = 40;
            WarpDestination = WorldJeratoO95_99024RandomSite5.transform.position;
        }

        //항성
        if (AreaNumber == 1)
        {
            MinusOfCenter = 110;
            WarpDestination = WorldToropioStar.transform.position;
        }
        else if (AreaNumber == 2)
        {
            MinusOfCenter = 94;
            WarpDestination = WorldRoro1Star.transform.position;
        }
        else if (AreaNumber == 3)
        {
            MinusOfCenter = 110;
            WarpDestination = WorldRoro2Star.transform.position;
        }
        else if (AreaNumber == 4)
        {
            MinusOfCenter = 110;
            WarpDestination = WorldSarisiStar.transform.position;
        }
        else if (AreaNumber == 5)
        {
            MinusOfCenter = 110;
            WarpDestination = WorldGarixStar.transform.position;
        }
        else if (AreaNumber == 6)
        {
            MinusOfCenter = 110;
            WarpDestination = WorldSecrosStar.transform.position;
        }
        else if (AreaNumber == 7)
        {
            MinusOfCenter = 93;
            WarpDestination = WorldTeretosStar.transform.position;
        }
        else if (AreaNumber == 8)
        {
            MinusOfCenter = 80;
            WarpDestination = WorldMiniPopoStar.transform.position;
        }
        else if (AreaNumber == 9)
        {
            MinusOfCenter = 93;
            WarpDestination = WorldDeltaD31_4AStar.transform.position;
        }
        else if (AreaNumber == 10)
        {
            MinusOfCenter = 103;
            WarpDestination = WorldDeltaD31_4BStar.transform.position;
        }
        else if (AreaNumber == 11)
        {
            MinusOfCenter = 85;
            WarpDestination = WorldJeratoO95_7AStar.transform.position;
        }
        else if (AreaNumber == 12)
        {
            MinusOfCenter = 75;
            WarpDestination = WorldJeratoO95_7BStar.transform.position;
        }
        else if (AreaNumber == 13)
        {
            MinusOfCenter = 75;
            WarpDestination = WorldJeratoO95_14CStar.transform.position;
        }
        else if (AreaNumber == 14)
        {
            MinusOfCenter = 85;
            WarpDestination = WorldJeratoO95_14DStar.transform.position;
        }
        else if (AreaNumber == 15)
        {
            MinusOfCenter = 140;
            WarpDestination = WorldJeratoO95_OmegaStar.transform.position;
        }

        //행성
        //토로피오 항성계
        else if (AreaNumber == 1001)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldSatariusGlessia.transform.position;
        }
        else if (AreaNumber == 1002)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldAposis.transform.position;
        }
        else if (AreaNumber == 1003)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldTorono.transform.position;
        }
        else if (AreaNumber == 1004)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldPlopa2.transform.position;
        }
        else if (AreaNumber == 1005)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldVedes4.transform.position;
        }

        //로로 항성계
        else if (AreaNumber == 1006)
        {
            MinusOfCenter = 54;
            WarpDestination = WorldAronPeri.transform.position;
        }
        else if (AreaNumber == 1007)
        {
            MinusOfCenter = 90;
            WarpDestination = WorldPapatus2.transform.position;
        }
        else if (AreaNumber == 1008)
        {
            MinusOfCenter = 65;
            WarpDestination = WorldPapatus3.transform.position;
        }
        else if (AreaNumber == 1009)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldKyepotoros.transform.position;
        }

        //사리시 항성계
        else if (AreaNumber == 1010)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldTratos.transform.position;
        }
        else if (AreaNumber == 1011)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldOclasis.transform.position;
        }
        else if (AreaNumber == 1012)
        {
            MinusOfCenter = 65;
            WarpDestination = WorldDeriousHeri.transform.position;
        }

        //가릭스 항성계
        else if (AreaNumber == 1013)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldVeltrorexy.transform.position;
        }
        else if (AreaNumber == 1014)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldErixJeoqeta.transform.position;
        }
        else if (AreaNumber == 1015)
        {
            MinusOfCenter = 78;
            WarpDestination = WorldQeepo.transform.position;
        }
        else if (AreaNumber == 1016)
        {
            MinusOfCenter = 65;
            WarpDestination = WorldCrownYosere.transform.position;
        }

        //옥토크라시스 파토로 항성계
        else if (AreaNumber == 1017)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldOros.transform.position;
        }
        else if (AreaNumber == 1018)
        {
            MinusOfCenter = 49;
            WarpDestination = WorldJapetAgrone.transform.position;
        }
        else if (AreaNumber == 1019)
        {
            MinusOfCenter = 63;
            WarpDestination = WorldXacro042351.transform.position;
        }

        //델타 D31-402054 항성계
        else if (AreaNumber == 1020)
        {
            MinusOfCenter = 31;
            WarpDestination = WorldDeltaD31_2208.transform.position;
        }
        else if (AreaNumber == 1021)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldDeltaD31_9523.transform.position;
        }
        else if (AreaNumber == 1022)
        {
            MinusOfCenter = 90;
            WarpDestination = WorldDeltaD31_12721.transform.position;
        }

        //제라토 O95-99024 항성계
        else if (AreaNumber == 1023)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldJeratoO95_1125.transform.position;
        }
        else if (AreaNumber == 1024)
        {
            MinusOfCenter = 45;
            WarpDestination = WorldJeratoO95_2252.transform.position;
        }
        else if (AreaNumber == 1025)
        {
            MinusOfCenter = 76;
            WarpDestination = WorldJeratoO95_8510.transform.position;
        }
    }

    //AreaSelectAction메서드에서 활성화
    void AreaSelectProgress(int SystemDestination, int AreaNumber, GameObject ProGressPrefab, GameObject IconClickPrefab, RectTransform RectPosition, RectTransform LocationLine)
    {
        SystemDestinationNumber = SystemDestination;
        ProGressPrefab.SetActive(true);
        TouchAreaUI.transform.position = RectPosition.transform.position;
        MapDestination = LocationLine;
        WordPrintSystem.PrintTypeNumber = AreaNumber;
        WordPrintSystem.PrintAreaSelectText();
        WordPrintSystem.PrintNumber = 1;
        WordPrintSystem.UniverseMapCancelPrintText();
        IconClickPrefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", true);
    }

    //지역 선택시 해당 지역 아이콘 활성화
    void AreaSelectAction()
    {
        WordPrintSystem.PrintTypeNumber = 0;
        WordPrintSystem.PrintNumber = 1;
        WordPrintSystem.UniverseConfirmPrintText();

        //랜덤 사이트
        if (AreaNumber == 10000)
        {
            AreaSelectProgress(1, AreaNumber, ToropioRandomSite1ProgressPrefab, ToropioRandomSite1Prefab, ToropioRandomSite1, ToropioRandomSite1Line);
        }
        else if (AreaNumber == 10001)
        {
            AreaSelectProgress(1, AreaNumber, ToropioRandomSite2ProgressPrefab, ToropioRandomSite2Prefab, ToropioRandomSite2, ToropioRandomSite2Line);
        }
        else if(AreaNumber == 10002)
        {
            AreaSelectProgress(2, AreaNumber, RoroRandomSite1ProgressPrefab, RoroRandomSite1Prefab, RoroRandomSite1, RoroRandomSite1Line);
        }
        else if (AreaNumber == 10003)
        {
            AreaSelectProgress(2, AreaNumber, RoroRandomSite2ProgressPrefab, RoroRandomSite2Prefab, RoroRandomSite2, RoroRandomSite2Line);
        }
        else if (AreaNumber == 10004)
        {
            AreaSelectProgress(2, AreaNumber, RoroRandomSite3ProgressPrefab, RoroRandomSite3Prefab, RoroRandomSite3, RoroRandomSite3Line);
        }
        else if(AreaNumber == 10005)
        {
            AreaSelectProgress(3, AreaNumber, SarisiRandomSite1ProgressPrefab, SarisiRandomSite1Prefab, SarisiRandomSite1, SarisiRandomSite1Line);
        }
        else if (AreaNumber == 10006)
        {
            AreaSelectProgress(3, AreaNumber, SarisiRandomSite2ProgressPrefab, SarisiRandomSite2Prefab, SarisiRandomSite2, SarisiRandomSite2Line);
        }
        else if (AreaNumber == 10007)
        {
            AreaSelectProgress(3, AreaNumber, SarisiRandomSite3ProgressPrefab, SarisiRandomSite3Prefab, SarisiRandomSite3, SarisiRandomSite3Line);
        }
        else if(AreaNumber == 10008)
        {
            AreaSelectProgress(4, AreaNumber, GarixRandomSite1ProgressPrefab, GarixRandomSite1Prefab, GarixRandomSite1, GarixRandomSite1Line);
        }
        else if (AreaNumber == 10009)
        {
            AreaSelectProgress(4, AreaNumber, GarixRandomSite2ProgressPrefab, GarixRandomSite2Prefab, GarixRandomSite2, GarixRandomSite2Line);
        }
        else if (AreaNumber == 10010)
        {
            AreaSelectProgress(4, AreaNumber, GarixRandomSite3ProgressPrefab, GarixRandomSite3Prefab, GarixRandomSite3, GarixRandomSite3Line);
        }
        else if(AreaNumber == 10011)
        {
            AreaSelectProgress(5, AreaNumber, OctoKrasisPatoroRandomSite1ProgressPrefab, OctoKrasisPatoroRandomSite1Prefab, OctoKrasisPatoroRandomSite1, OctoKrasisPatoroRandomSite1Line);
        }
        else if (AreaNumber == 10012)
        {
            AreaSelectProgress(5, AreaNumber, OctoKrasisPatoroRandomSite2ProgressPrefab, OctoKrasisPatoroRandomSite2Prefab, OctoKrasisPatoroRandomSite2, OctoKrasisPatoroRandomSite2Line);
        }
        else if (AreaNumber == 10013)
        {
            AreaSelectProgress(5, AreaNumber, OctoKrasisPatoroRandomSite3ProgressPrefab, OctoKrasisPatoroRandomSite3Prefab, OctoKrasisPatoroRandomSite3, OctoKrasisPatoroRandomSite3Line);
        }
        else if (AreaNumber == 10014)
        {
            AreaSelectProgress(5, AreaNumber, OctoKrasisPatoroRandomSite4ProgressPrefab, OctoKrasisPatoroRandomSite4Prefab, OctoKrasisPatoroRandomSite4, OctoKrasisPatoroRandomSite4Line);
        }
        else if(AreaNumber == 10015)
        {
            AreaSelectProgress(6, AreaNumber, DeltaD31_402054RandomSite1ProgressPrefab, DeltaD31_402054RandomSite1Prefab, DeltaD31_402054RandomSite1, DeltaD31_402054RandomSite1Line);
        }
        else if (AreaNumber == 10016)
        {
            AreaSelectProgress(6, AreaNumber, DeltaD31_402054RandomSite2ProgressPrefab, DeltaD31_402054RandomSite2Prefab, DeltaD31_402054RandomSite2, DeltaD31_402054RandomSite2Line);
        }
        else if (AreaNumber == 10017)
        {
            AreaSelectProgress(6, AreaNumber, DeltaD31_402054RandomSite3ProgressPrefab, DeltaD31_402054RandomSite3Prefab, DeltaD31_402054RandomSite3, DeltaD31_402054RandomSite3Line);
        }
        else if (AreaNumber == 10018)
        {
            AreaSelectProgress(6, AreaNumber, DeltaD31_402054RandomSite4ProgressPrefab, DeltaD31_402054RandomSite4Prefab, DeltaD31_402054RandomSite4, DeltaD31_402054RandomSite4Line);
        }
        else if (AreaNumber == 10019)
        {
            AreaSelectProgress(6, AreaNumber, DeltaD31_402054RandomSite5ProgressPrefab, DeltaD31_402054RandomSite5Prefab, DeltaD31_402054RandomSite5, DeltaD31_402054RandomSite5Line);
        }
        else if(AreaNumber == 10020)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_99024RandomSite1ProgressPrefab, JeratoO95_99024RandomSite1Prefab, JeratoO95_99024RandomSite1, JeratoO95_99024RandomSite1Line);
        }
        else if (AreaNumber == 10021)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_99024RandomSite2ProgressPrefab, JeratoO95_99024RandomSite2Prefab, JeratoO95_99024RandomSite2, JeratoO95_99024RandomSite2Line);
        }
        else if (AreaNumber == 10022)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_99024RandomSite3ProgressPrefab, JeratoO95_99024RandomSite3Prefab, JeratoO95_99024RandomSite3, JeratoO95_99024RandomSite3Line);
        }
        else if (AreaNumber == 10023)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_99024RandomSite4ProgressPrefab, JeratoO95_99024RandomSite4Prefab, JeratoO95_99024RandomSite4, JeratoO95_99024RandomSite4Line);
        }
        else if (AreaNumber == 10024)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_99024RandomSite5ProgressPrefab, JeratoO95_99024RandomSite5Prefab, JeratoO95_99024RandomSite5, JeratoO95_99024RandomSite5Line);
        }

        //항성
        if (AreaNumber == 1)
        {
            AreaSelectProgress(1, AreaNumber, ToropioProgressPrefab, ToropioStarPrefab, ToropioStar, ToropioStarLine);
        }
        else if (AreaNumber == 2)
        {
            AreaSelectProgress(2, AreaNumber, Roro1ProgressPrefab, Roro1StarPrefab, Roro1Star, Roro1StarLine);
        }
        else if (AreaNumber == 3)
        {
            AreaSelectProgress(2, AreaNumber, Roro2ProgressPrefab, Roro2StarPrefab, Roro2Star, Roro2StarLine);
        }
        else if (AreaNumber == 4)
        {
            AreaSelectProgress(3, AreaNumber, SarisiProgressPrefab, SarisiStarPrefab, SarisiStar, SarisiStarLine);
        }
        else if (AreaNumber == 5)
        {
            AreaSelectProgress(4, AreaNumber, GarixProgressPrefab, GarixStarPrefab, GarixStar, GarixStarLine);
        }
        else if (AreaNumber == 6)
        {
            AreaSelectProgress(5, AreaNumber, SecrosProgressPrefab, SecrosStarPrefab, SecrosStar, SecrosStarLine);
        }
        else if (AreaNumber == 7)
        {
            AreaSelectProgress(5, AreaNumber, TeretosProgressPrefab, TeretosStarPrefab, TeretosStar, TeretosStarLine);
        }
        else if (AreaNumber == 8)
        {
            AreaSelectProgress(5, AreaNumber, MiniPopoProgressPrefab, MiniPopoStarPrefab, MiniPopoStar, MiniPopoStarLine);
        }
        else if (AreaNumber == 9)
        {
            AreaSelectProgress(6, AreaNumber, DeltaD31_4AProgressPrefab, DeltaD31_4AStarPrefab, DeltaD31_4AStar, DeltaD31_4AStarLine);
        }
        else if (AreaNumber == 10)
        {
            AreaSelectProgress(6, AreaNumber, DeltaD31_4BProgressPrefab, DeltaD31_4BStarPrefab, DeltaD31_4BStar, DeltaD31_4BStarLine);
        }
        else if (AreaNumber == 11)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_7AProgressPrefab, JeratoO95_7AStarPrefab, JeratoO95_7AStar, JeratoO95_7AStarLine);
        }
        else if (AreaNumber == 12)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_7BProgressPrefab, JeratoO95_7BStarPrefab, JeratoO95_7BStar, JeratoO95_7BStarLine);
        }
        else if (AreaNumber == 13)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_14CProgressPrefab, JeratoO95_14CStarPrefab, JeratoO95_14CStar, JeratoO95_14CStarLine);
        }
        else if (AreaNumber == 14)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_14DProgressPrefab, JeratoO95_14DStarPrefab, JeratoO95_14DStar, JeratoO95_14DStarLine);
        }
        else if (AreaNumber == 15)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_OmegaProgressPrefab, JeratoO95_OmegaStarPrefab, JeratoO95_OmegaStar, JeratoO95_OmegaStarLine);
        }

        //행성
        //토로피오 항성계
        else if (AreaNumber == 1001)
        {
            AreaSelectProgress(1, AreaNumber, SatariusGlessiaProgressPrefab, SatariusGlessiaPrefab, SatariusGlessia, SatariusGlessiaLine);
        }
        else if (AreaNumber == 1002)
        {
            AreaSelectProgress(1, AreaNumber, AposisProgressPrefab, AposisPrefab, Aposis, AposisLine);
        }
        else if (AreaNumber == 1003)
        {
            AreaSelectProgress(1, AreaNumber, ToronoProgressPrefab, ToronoPrefab, Torono, ToronoLine);
        }
        else if (AreaNumber == 1004)
        {
            AreaSelectProgress(1, AreaNumber, Plopa2ProgressPrefab, Plopa2Prefab, Plopa2, Plopa2Line);
        }
        else if (AreaNumber == 1005)
        {
            AreaSelectProgress(1, AreaNumber, Vedes4ProgressPrefab, Vedes4Prefab, Vedes4, Vedes4Line);
        }

        //로로 항성계
        else if (AreaNumber == 1006)
        {
            AreaSelectProgress(2, AreaNumber, AronPeriProgressPrefab, AronPeriPrefab, AronPeri, AronPeriLine);
        }
        else if (AreaNumber == 1007)
        {
            AreaSelectProgress(2, AreaNumber, Papatus2ProgressPrefab, Papatus2Prefab, Papatus2, Papatus2Line);
        }
        else if (AreaNumber == 1008)
        {
            AreaSelectProgress(2, AreaNumber, Papatus3ProgressPrefab, Papatus3Prefab, Papatus3, Papatus3Line);
        }
        else if (AreaNumber == 1009)
        {
            AreaSelectProgress(2, AreaNumber, KyepotorosProgressPrefab, KyepotorosPrefab, Kyepotoros, KyepotorosLine);
        }

        //사리시 항성계
        else if (AreaNumber == 1010)
        {
            AreaSelectProgress(3, AreaNumber, TratosProgressPrefab, TratosPrefab, Tratos, TratosLine);
        }
        else if (AreaNumber == 1011)
        {
            AreaSelectProgress(3, AreaNumber, OclasisProgressPrefab, OclasisPrefab, Oclasis, OclasisLine);
        }
        else if (AreaNumber == 1012)
        {
            AreaSelectProgress(3, AreaNumber, DeriousHeriProgressPrefab, DeriousHeriPrefab, DeriousHeri, DeriousHeriLine);
        }

        //가릭스 항성계
        else if (AreaNumber == 1013)
        {
            AreaSelectProgress(4, AreaNumber, VeltrorexyProgressPrefab, VeltrorexyPrefab, Veltrorexy, VeltrorexyLine);
        }
        else if (AreaNumber == 1014)
        {
            AreaSelectProgress(4, AreaNumber, ErixJeoqetaProgressPrefab, ErixJeoqetaPrefab, ErixJeoqeta, ErixJeoqetaLine);
        }
        else if (AreaNumber == 1015)
        {
            AreaSelectProgress(4, AreaNumber, QeepoProgressPrefab, QeepoPrefab, Qeepo, QeepoLine);
        }
        else if (AreaNumber == 1016)
        {
            AreaSelectProgress(4, AreaNumber, CrownYosereProgressPrefab, CrownYoserePrefab, CrownYosere, CrownYosereLine);
        }

        //옥토크라시스 파토로 항성계
        else if (AreaNumber == 1017)
        {
            AreaSelectProgress(5, AreaNumber, OrosProgressPrefab, OrosPrefab, Oros, OrosLine);
        }
        else if (AreaNumber == 1018)
        {
            AreaSelectProgress(5, AreaNumber, JapetAgroneProgressPrefab, JapetAgronePrefab, JapetAgrone, JapetAgroneLine);
        }
        else if (AreaNumber == 1019)
        {
            AreaSelectProgress(5, AreaNumber, Xacro042351ProgressPrefab, Xacro042351Prefab, Xacro042351, Xacro042351Line);
        }

        //델타 D31-402054 항성계
        else if (AreaNumber == 1020)
        {
            AreaSelectProgress(6, AreaNumber, DeltaD31_2208ProgressPrefab, DeltaD31_2208Prefab, DeltaD31_2208, DeltaD31_2208Line);
        }
        else if (AreaNumber == 1021)
        {
            AreaSelectProgress(6, AreaNumber, DeltaD31_9523ProgressPrefab, DeltaD31_9523Prefab, DeltaD31_9523, DeltaD31_9523Line);
        }
        else if (AreaNumber == 1022)
        {
            AreaSelectProgress(6, AreaNumber, DeltaD31_12721ProgressPrefab, DeltaD31_12721Prefab, DeltaD31_12721, DeltaD31_12721Line);
        }

        //제라토 O95-99024 항성계
        else if (AreaNumber == 1023)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_1125ProgressPrefab, JeratoO95_1125Prefab, JeratoO95_1125, JeratoO95_1125Line);
        }
        else if (AreaNumber == 1024)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_2252ProgressPrefab, JeratoO95_2252Prefab, JeratoO95_2252, JeratoO95_2252Line);
        }
        else if (AreaNumber == 1025)
        {
            AreaSelectProgress(7, AreaNumber, JeratoO95_8510ProgressPrefab, JeratoO95_8510Prefab, JeratoO95_8510, JeratoO95_8510Line);
        }
    }

    //ProgressAreaPrefabOff 메서드에서 진행
    void ProgressTurnOff(int State, GameObject ProGressPrefab, GameObject IconClickPrefab, Image Color)
    {
        ProGressPrefab.SetActive(false);
        if (State == 1 || State == 0)
        {
            IconClickPrefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", false);
            Color NormalColor = IconNormal;
            Color.color = NormalColor;
        }
        else if (State == 2)
        {
            IconClickPrefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", false);
            Color BattleColor = AreaInBattle;
            Color.color = BattleColor;
        }
        else if (State == 3)
        {
            IconClickPrefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", false);
            Color OccupationColor = AreaInOccupation;
            Color.color = OccupationColor;
        }
        else if (State == 4)
        {
            IconClickPrefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", false);
            Color InfectionColor = AreaInInfection;
            Color.color = InfectionColor;
        }

        StateNumber = 0;
    }

    //지역 선택 후, 우주맵 진행바에 있는 다른 선택된 지역 아이콘 삭제
    void ProgressAreaPrefabOff()
    {
        AreaStatement.BringState(AreaNumber);

        //랜덤 사이트
        if (ToropioRandomSite1ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, ToropioRandomSite1ProgressPrefab, ToropioRandomSite1Prefab, ToropioRandomSite1Image);
        }
        else if(ToropioRandomSite2ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, ToropioRandomSite2ProgressPrefab, ToropioRandomSite2Prefab, ToropioRandomSite2Image);
        }
        else if (RoroRandomSite1ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, RoroRandomSite1ProgressPrefab, RoroRandomSite1Prefab, RoroRandomSite1Image);
        }
        else if (RoroRandomSite2ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, RoroRandomSite2ProgressPrefab, RoroRandomSite2Prefab, RoroRandomSite2Image);
        }
        else if (RoroRandomSite3ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, RoroRandomSite3ProgressPrefab, RoroRandomSite3Prefab, RoroRandomSite3Image);
        }
        else if (SarisiRandomSite1ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, SarisiRandomSite1ProgressPrefab, SarisiRandomSite1Prefab, SarisiRandomSite1Image);
        }
        else if (SarisiRandomSite2ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, SarisiRandomSite2ProgressPrefab, SarisiRandomSite2Prefab, SarisiRandomSite2Image);
        }
        else if (SarisiRandomSite3ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, SarisiRandomSite3ProgressPrefab, SarisiRandomSite3Prefab, SarisiRandomSite3Image);
        }
        else if (GarixRandomSite1ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, GarixRandomSite1ProgressPrefab, GarixRandomSite1Prefab, GarixRandomSite1Image);
        }
        else if (GarixRandomSite2ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, GarixRandomSite2ProgressPrefab, GarixRandomSite2Prefab, GarixRandomSite2Image);
        }
        else if (GarixRandomSite3ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, GarixRandomSite3ProgressPrefab, GarixRandomSite3Prefab, GarixRandomSite3Image);
        }
        else if (OctoKrasisPatoroRandomSite1ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, OctoKrasisPatoroRandomSite1ProgressPrefab, OctoKrasisPatoroRandomSite1Prefab, OctoKrasisPatoroRandomSite1Image);
        }
        else if (OctoKrasisPatoroRandomSite2ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, OctoKrasisPatoroRandomSite2ProgressPrefab, OctoKrasisPatoroRandomSite2Prefab, OctoKrasisPatoroRandomSite2Image);
        }
        else if (OctoKrasisPatoroRandomSite3ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, OctoKrasisPatoroRandomSite3ProgressPrefab, OctoKrasisPatoroRandomSite3Prefab, OctoKrasisPatoroRandomSite3Image);
        }
        else if (OctoKrasisPatoroRandomSite4ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, OctoKrasisPatoroRandomSite4ProgressPrefab, OctoKrasisPatoroRandomSite4Prefab, OctoKrasisPatoroRandomSite4Image);
        }
        else if (DeltaD31_402054RandomSite1ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, DeltaD31_402054RandomSite1ProgressPrefab, DeltaD31_402054RandomSite1Prefab, DeltaD31_402054RandomSite1Image);
        }
        else if (DeltaD31_402054RandomSite2ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, DeltaD31_402054RandomSite2ProgressPrefab, DeltaD31_402054RandomSite2Prefab, DeltaD31_402054RandomSite2Image);
        }
        else if (DeltaD31_402054RandomSite3ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, DeltaD31_402054RandomSite3ProgressPrefab, DeltaD31_402054RandomSite3Prefab, DeltaD31_402054RandomSite3Image);
        }
        else if (DeltaD31_402054RandomSite4ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, DeltaD31_402054RandomSite4ProgressPrefab, DeltaD31_402054RandomSite4Prefab, DeltaD31_402054RandomSite4Image);
        }
        else if (DeltaD31_402054RandomSite5ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, DeltaD31_402054RandomSite5ProgressPrefab, DeltaD31_402054RandomSite5Prefab, DeltaD31_402054RandomSite5Image);
        }
        else if (JeratoO95_99024RandomSite1ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, JeratoO95_99024RandomSite1ProgressPrefab, JeratoO95_99024RandomSite1Prefab, JeratoO95_99024RandomSite1Image);
        }
        else if (JeratoO95_99024RandomSite2ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, JeratoO95_99024RandomSite2ProgressPrefab, JeratoO95_99024RandomSite2Prefab, JeratoO95_99024RandomSite2Image);
        }
        else if (JeratoO95_99024RandomSite3ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, JeratoO95_99024RandomSite3ProgressPrefab, JeratoO95_99024RandomSite3Prefab, JeratoO95_99024RandomSite3Image);
        }
        else if (JeratoO95_99024RandomSite4ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, JeratoO95_99024RandomSite4ProgressPrefab, JeratoO95_99024RandomSite4Prefab, JeratoO95_99024RandomSite4Image);
        }
        else if (JeratoO95_99024RandomSite5ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(2, JeratoO95_99024RandomSite5ProgressPrefab, JeratoO95_99024RandomSite5Prefab, JeratoO95_99024RandomSite5Image);
        }

        //항성
        if (ToropioProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, ToropioProgressPrefab, ToropioStarPrefab, ToropioStarImage);
        }
        else if (Roro1ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, Roro1ProgressPrefab, Roro1StarPrefab, Roro1StarImage);
        }
        else if (Roro2ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, Roro2ProgressPrefab, Roro2StarPrefab, Roro2StarImage);
        }
        else if (SarisiProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, SarisiProgressPrefab, SarisiStarPrefab, SarisiStarImage);
        }
        else if (GarixProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, GarixProgressPrefab, GarixStarPrefab, GarixStarImage);
        }
        else if (SecrosProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, SecrosProgressPrefab, SecrosStarPrefab, SecrosStarImage);
        }
        else if (TeretosProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, TeretosProgressPrefab, TeretosStarPrefab, TeretosStarImage);
        }
        else if (MiniPopoProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, MiniPopoProgressPrefab, MiniPopoStarPrefab, MiniPopoStarImage);
        }
        else if (DeltaD31_4AProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, DeltaD31_4AProgressPrefab, DeltaD31_4AStarPrefab, DeltaD31_4AStarImage);
        }
        else if (DeltaD31_4BProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, DeltaD31_4BProgressPrefab, DeltaD31_4BStarPrefab, DeltaD31_4BStarImage);
        }
        else if (JeratoO95_7AProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, JeratoO95_7AProgressPrefab, JeratoO95_7AStarPrefab, JeratoO95_7AStarImage);
        }
        else if (JeratoO95_7BProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, JeratoO95_7BProgressPrefab, JeratoO95_7BStarPrefab, JeratoO95_7BStarImage);
        }
        else if (JeratoO95_14CProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, JeratoO95_14CProgressPrefab, JeratoO95_14CStarPrefab, JeratoO95_14CStarImage);
        }
        else if (JeratoO95_14DProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, JeratoO95_14DProgressPrefab, JeratoO95_14DStarPrefab, JeratoO95_14DStarImage);
        }
        else if (JeratoO95_OmegaProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, JeratoO95_OmegaProgressPrefab, JeratoO95_OmegaStarPrefab, JeratoO95_OmegaStarImage);
        }

        //행성
        //토로피오 항성계
        else if (SatariusGlessiaProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, SatariusGlessiaProgressPrefab, SatariusGlessiaPrefab, SatariusGlessiaImage);
        }
        else if (AposisProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, AposisProgressPrefab, AposisPrefab, AposisImage);
        }
        else if (ToronoProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, ToronoProgressPrefab, ToronoPrefab, ToronoImage);
        }
        else if (Plopa2ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, Plopa2ProgressPrefab, Plopa2Prefab, Plopa2Image);
        }
        else if (Vedes4ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, Vedes4ProgressPrefab, Vedes4Prefab, Vedes4Image);
        }

        //로로 항성계
        else if (AronPeriProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, AronPeriProgressPrefab, AronPeriPrefab, AronPeriImage);
        }
        else if (Papatus2ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, Papatus2ProgressPrefab, Papatus2Prefab, Papatus2Image);
        }
        else if (Papatus3ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, Papatus3ProgressPrefab, Papatus3Prefab, Papatus3Image);
        }
        else if (KyepotorosProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, KyepotorosProgressPrefab, KyepotorosPrefab, KyepotorosImage);
        }

        //사리시 항성계
        else if (TratosProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, TratosProgressPrefab, TratosPrefab, TratosImage);
        }
        else if (OclasisProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, OclasisProgressPrefab, OclasisPrefab, OclasisImage);
        }
        else if (DeriousHeriProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, DeriousHeriProgressPrefab, DeriousHeriPrefab, DeriousHeriImage);
        }

        //가릭스 항성계
        else if (VeltrorexyProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, VeltrorexyProgressPrefab, VeltrorexyPrefab, VeltrorexyImage);
        }
        else if (ErixJeoqetaProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, ErixJeoqetaProgressPrefab, ErixJeoqetaPrefab, ErixJeoqetaImage);
        }
        else if (QeepoProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, QeepoProgressPrefab, QeepoPrefab, QeepoImage);
        }
        else if (CrownYosereProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, CrownYosereProgressPrefab, CrownYoserePrefab, CrownYosereImage);
        }

        //옥토크라시스 파토로 항성계
        else if (OrosProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, OrosProgressPrefab, OrosPrefab, OrosImage);
        }
        else if (JapetAgroneProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, JapetAgroneProgressPrefab, JapetAgronePrefab, JapetAgroneImage);
        }
        else if (Xacro042351ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, Xacro042351ProgressPrefab, Xacro042351Prefab, Xacro042351Image);
        }

        //델타 D31-402054 항성계
        else if (DeltaD31_2208ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, DeltaD31_2208ProgressPrefab, DeltaD31_2208Prefab, DeltaD31_2208Image);
        }
        else if (DeltaD31_9523ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, DeltaD31_9523ProgressPrefab, DeltaD31_9523Prefab, DeltaD31_9523Image);
        }
        else if (DeltaD31_12721ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, DeltaD31_12721ProgressPrefab, DeltaD31_12721Prefab, DeltaD31_12721Image);
        }

        //제라토 O95-99024 항성계
        else if (JeratoO95_1125ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, JeratoO95_1125ProgressPrefab, JeratoO95_1125Prefab, JeratoO95_1125Image);
        }
        else if (JeratoO95_2252ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, JeratoO95_2252ProgressPrefab, JeratoO95_2252Prefab, JeratoO95_2252Image);
        }
        else if (JeratoO95_8510ProgressPrefab.activeSelf == true)
        {
            ProgressTurnOff(StateNumber, JeratoO95_8510ProgressPrefab, JeratoO95_8510Prefab, JeratoO95_8510Image);
        }

        if (FlagshipProgressPrefab.activeSelf == true)
            FlagshipProgressPrefab.SetActive(false);
    }

    //기함 선택시 해당 기함 아이콘 활성화
    void ShipSelectAction()
    {
        WordPrintSystem.PrintTypeNumber = 0;
        WordPrintSystem.PrintNumber = 1;
        WordPrintSystem.UniverseConfirmPrintText();
        PlayerFleetImageOff();
        MinusOfCenter = Random.Range(5, 10);

        if (WarpToPlayer == 1)
        {
            WarpDestination = WorldPlayer1.transform.position;
            SystemDestinationNumber = WorldPlayer1.gameObject.GetComponent<FlagshipSystemNumber>().SystemNowNumber;
            FlagshipProgressPrefab.SetActive(true);
            MapDestination = Player1;
            WordPrintSystem.PrintNumber = 2;
            WordPrintSystem.UniverseMapCancelPrintText();
            Player1Prefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", true);
        }
        else if (WarpToPlayer == 2)
        {
            WarpDestination = WorldPlayer2.transform.position;
            SystemDestinationNumber = WorldPlayer2.gameObject.GetComponent<FlagshipSystemNumber>().SystemNowNumber;
            FlagshipProgressPrefab.SetActive(true);
            MapDestination = Player2;
            WordPrintSystem.PrintNumber = 2;
            WordPrintSystem.UniverseMapCancelPrintText();
            Player2Prefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", true);
        }
        else if (WarpToPlayer == 3)
        {
            WarpDestination = WorldPlayer3.transform.position;
            SystemDestinationNumber = WorldPlayer3.gameObject.GetComponent<FlagshipSystemNumber>().SystemNowNumber;
            FlagshipProgressPrefab.SetActive(true);
            MapDestination = Player3;
            WordPrintSystem.PrintNumber = 2;
            WordPrintSystem.UniverseMapCancelPrintText();
            Player3Prefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", true);
        }
        else if (WarpToPlayer == 4)
        {
            WarpDestination = WorldPlayer4.transform.position;
            SystemDestinationNumber = WorldPlayer4.gameObject.GetComponent<FlagshipSystemNumber>().SystemNowNumber;
            FlagshipProgressPrefab.SetActive(true);
            MapDestination = Player4;
            WordPrintSystem.PrintNumber = 2;
            WordPrintSystem.UniverseMapCancelPrintText();
            Player4Prefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", true);
        }
        else if (WarpToPlayer == 5)
        {
            WarpDestination = WorldPlayer5.transform.position;
            SystemDestinationNumber = WorldPlayer5.gameObject.GetComponent<FlagshipSystemNumber>().SystemNowNumber;
            FlagshipProgressPrefab.SetActive(true);
            MapDestination = Player5;
            WordPrintSystem.PrintNumber = 2;
            WordPrintSystem.UniverseMapCancelPrintText();
            Player5Prefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", true);
        }
    }

    //워프 항해 소요 시간 계산
    void JourneyTime(float Speed, Vector3 Player, Transform WorldPlayer, int PlayerNumber)
    {
        float Distance = Vector3.Distance(Player, WarpDestination);
        timeTaken = Distance / Speed;
        WordPrintSystem.WarpDistanceCalculate(Distance, SystemDestinationNumber, WorldPlayer.gameObject.GetComponent<FlagshipSystemNumber>().SystemNowNumber, PlayerNumber);
    }

    //플레이어 함선 선택
    void PlayerFleetClick(Transform WorldPlayer, GameObject PlayerPrefab, RectTransform PlayerRect, bool PlayerSelet, int WarpToPlayerNumber, int PlayerArrivalNumber, Image PlayerImage)
    {
        if (WorldPlayer.gameObject.GetComponent<MoveVelocity>().WarpDriveActive == false && WorldPlayer.gameObject.GetComponent<MoveVelocity>().WarpDriveReady == false)
        {
            if (WarpStep < 4 && WarpStep > 1 && WarpToPlayer != WarpToPlayerNumber && WorldPlayer.gameObject.GetComponent<FlagshipSystemNumber>().PlayerNumber != AreaNumber) //지역으로 워프할 때 사용
            {
                if (WarpToPlayer > 0 && Vector3.Distance(WarpToPlayerArea, WorldPlayer.position) > 50 || AreaNumber > 0)
                {
                    UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", FinalFleetSelectAudio);

                    bool SelectPlayer = PlayerSelet;

                    if (!SelectPlayer)
                    {
                        SelectPlayer = true;

                        if (WarpToPlayerNumber == 1)
                            Player1Selet = SelectPlayer;
                        else if (WarpToPlayerNumber == 2)
                            Player2Selet = SelectPlayer;
                        else if (WarpToPlayerNumber == 3)
                            Player3Selet = SelectPlayer;
                        else if (WarpToPlayerNumber == 4)
                            Player4Selet = SelectPlayer;
                        else if (WarpToPlayerNumber == 5)
                            Player5Selet = SelectPlayer;

                        WarpStep = 3;
                        AccountOfShip++;
                        if (AccountOfShip > 0)
                        {
                            SelectAreaOrShip();
                            UniverseProgressBarShipPrefab.SetActive(true);
                            ShipSelectedIcon.GetComponent<Image>().enabled = true;
                            TeleEffect1.SetActive(false);
                            TeleEffect2.SetActive(true);
                        }
                        TouchAreaUI.transform.position = PlayerRect.transform.position;
                        TouchAreaUI.SetActive(true);
                        UIEffect.GetComponent<Animator>().SetBool("Ship selected, Tele effects", true);
                        PlayerPrefab.GetComponent<MapRouter>().CreateLine(PlayerRect, MapDestination); //라인에게 길이와 좌표 전송
                        PlayerPrefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", true);
                    }
                    else
                    {
                        SelectPlayer = false;

                        if (WarpToPlayerNumber == 1)
                            Player1Selet = SelectPlayer;
                        else if (WarpToPlayerNumber == 2)
                            Player2Selet = SelectPlayer;
                        else if (WarpToPlayerNumber == 3)
                            Player3Selet = SelectPlayer;
                        else if (WarpToPlayerNumber == 4)
                            Player4Selet = SelectPlayer;
                        else if (WarpToPlayerNumber == 5)
                            Player5Selet = SelectPlayer;

                        AccountOfShip--;

                        if (AccountOfShip <= 0)
                        {
                            WarpStep = 2;
                            UniverseProgressBarShipPrefab.SetActive(false);
                            ShipSelectedIcon.GetComponent<Image>().enabled = false;
                            TeleEffect1.SetActive(true);
                            TeleEffect2.SetActive(false);
                        }

                        DeselectAreaOrShip();
                        TouchAreaUI.SetActive(false);
                        UIEffect.GetComponent<Animator>().SetBool("Ship selected, Tele effects", false);
                        PlayerPrefab.GetComponent<MapRouter>().DeleteLine();
                        PlayerPrefab.GetComponent<Animator>().SetBool("Color change, Universe map icon", false);
                        Color ShipNormalColor = IconShipNormal;
                        PlayerImage.color = ShipNormalColor;
                    }
                }
                else if (WarpToPlayer > 0 && AreaNumber == 0 && Vector3.Distance(WarpToPlayerArea, WorldPlayer.position) <= 50)
                {
                    UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", WarningFleetSelectAudio);
                    StartCoroutine(WordPrintSystem.WarpWarnningPrint(3));
                }
            }
            else if (WarpStep < 2 && WorldPlayer.gameObject.GetComponent<FlagshipSystemNumber>().PlayerNumber != AreaNumber) //아군 함선에게 워프 도약할 때 사용
            {
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", DestinationAudio);
                if (PlayerArrivalNumber != WorldPlayer.gameObject.GetComponent<FlagshipSystemNumber>().PlayerNumber)
                {
                    WarpToPlayerArea = WorldPlayer.position;
                    WarpToPlayer = WarpToPlayerNumber;
                    WarpStep = 1;

                    if (WarpToPlayerNumber == 1)
                        Player1Number = WorldPlayer.gameObject.GetComponent<FlagshipSystemNumber>().PlayerNumber;
                    else if (WarpToPlayerNumber == 2)
                        Player2Number = WorldPlayer.gameObject.GetComponent<FlagshipSystemNumber>().PlayerNumber;
                    else if (WarpToPlayerNumber == 3)
                        Player3Number = WorldPlayer.gameObject.GetComponent<FlagshipSystemNumber>().PlayerNumber;
                    else if (WarpToPlayerNumber == 4)
                        Player4Number = WorldPlayer.gameObject.GetComponent<FlagshipSystemNumber>().PlayerNumber;
                    else if (WarpToPlayerNumber == 5)
                        Player5Number = WorldPlayer.gameObject.GetComponent<FlagshipSystemNumber>().PlayerNumber;

                    ProgressAreaPrefabOff();
                    ShipSelectAction();
                    SelectAreaOrShip();
                }
                else if (PlayerArrivalNumber == WorldPlayer.gameObject.GetComponent<FlagshipSystemNumber>().PlayerNumber)
                {
                    if (WarpToPlayerNumber == 1)
                        Player1Number = 0;
                    else if (WarpToPlayerNumber == 2)
                        Player2Number = 0;
                    else if (WarpToPlayerNumber == 3)
                        Player3Number = 0;
                    else if (WarpToPlayerNumber == 4)
                        Player4Number = 0;
                    else if (WarpToPlayerNumber == 5)
                        Player5Number = 0;

                    WarpToPlayer = 0;
                    WarpStep = 0;
                    ProgressAreaPrefabOff();
                    DeselectAreaOrShip();
                    Destination = new Vector3(0, 0, 0);
                }
            }
        }
        else if (WorldPlayer.gameObject.GetComponent<MoveVelocity>().WarpDriveActive == true || WorldPlayer.gameObject.GetComponent<MoveVelocity>().WarpDriveReady == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", WarningFleetSelectAudio);
            StartCoroutine(WordPrintSystem.WarpWarnningPrint(1));
        }
        if (WorldPlayer.gameObject.GetComponent<FlagshipSystemNumber>().PlayerNumber == AreaNumber)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", WarningFleetSelectAudio);
            StartCoroutine(WordPrintSystem.WarpWarnningPrint(2));
        }
    }

    //플레이어 함대 UI 목록
    public void Player1FleetClick()
    {
        PlayerFleetClick(WorldPlayer1, Player1Prefab, Player1, Player1Selet, 1, Player1Number, Player1Image);
    }
    public void Player2FleetClick()
    {
        PlayerFleetClick(WorldPlayer2, Player2Prefab, Player2, Player2Selet, 2, Player2Number, Player2Image);
    }
    public void Player3FleetClick()
    {
        PlayerFleetClick(WorldPlayer3, Player3Prefab, Player3, Player3Selet, 3, Player3Number, Player3Image);
    }
    public void Player4FleetClick()
    {
        PlayerFleetClick(WorldPlayer4, Player4Prefab, Player4, Player4Selet, 4, Player4Number, Player4Image);
    }
    public void Player5FleetClick()
    {
        PlayerFleetClick(WorldPlayer5, Player5Prefab, Player5, Player5Selet, 5, Player5Number, Player5Image);
    }

    //천체 목적지로 좌표 전송
    void CelestialBodiesDestination(int Area, GameObject Progressprefab)
    {
        if (WarpStep < 2)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", DestinationAudio);
            if (AreaNumber != Area)
            {
                WarpStep = 1;
                AreaNumber = Area;
                ProgressAreaPrefabOff();
                AreaSelectAction();
                SelectAreaOrShip();
                TouchAreaUI.SetActive(true);
                WordPrintSystem.AreaExplainWindow(AreaNumber);
            }
            else if (AreaNumber == Area)
            {
                WarpStep = 0;
                AreaNumber = 0;
                ProgressAreaPrefabOff();
                DeselectAreaOrShip();
                Progressprefab.SetActive(false);
                ShowUI.SetActive(false);
                Destination = new Vector3(0, 0, 0);
                TouchAreaUI.SetActive(false);
            }
        }
    }

    //랜덤 사이트 UI 목록
    public void ToropioRandomSite1Click()
    {
        CelestialBodiesDestination(10000, ToropioRandomSite1ProgressPrefab);
    }
    public void ToropioRandomSite2Click()
    {
        CelestialBodiesDestination(10001, ToropioRandomSite2ProgressPrefab);
    }
    public void RoroRandomSite1Click()
    {
        CelestialBodiesDestination(10002, RoroRandomSite1ProgressPrefab);
    }
    public void RoroRandomSite2Click()
    {
        CelestialBodiesDestination(10003, RoroRandomSite2ProgressPrefab);
    }
    public void RoroRandomSite3Click()
    {
        CelestialBodiesDestination(10004, RoroRandomSite3ProgressPrefab);
    }
    public void SarisiRandomSite1Click()
    {
        CelestialBodiesDestination(10005, SarisiRandomSite1ProgressPrefab);
    }
    public void SarisiRandomSite2Click()
    {
        CelestialBodiesDestination(10006, SarisiRandomSite2ProgressPrefab);
    }
    public void SarisiRandomSite3Click()
    {
        CelestialBodiesDestination(10007, SarisiRandomSite3ProgressPrefab);
    }
    public void GarixRandomSite1Click()
    {
        CelestialBodiesDestination(10008, GarixRandomSite1ProgressPrefab);
    }
    public void GarixRandomSite2Click()
    {
        CelestialBodiesDestination(10009, GarixRandomSite2ProgressPrefab);
    }
    public void GarixRandomSite3Click()
    {
        CelestialBodiesDestination(10010, GarixRandomSite3ProgressPrefab);
    }
    public void OctoKrasisPatoroRandomSite1Click()
    {
        CelestialBodiesDestination(10011, OctoKrasisPatoroRandomSite1ProgressPrefab);
    }
    public void OctoKrasisPatoroRandomSite2Click()
    {
        CelestialBodiesDestination(10012, OctoKrasisPatoroRandomSite2ProgressPrefab);
    }
    public void OctoKrasisPatoroRandomSite3Click()
    {
        CelestialBodiesDestination(10013, OctoKrasisPatoroRandomSite3ProgressPrefab);
    }
    public void OctoKrasisPatoroRandomSite4Click()
    {
        CelestialBodiesDestination(10014, OctoKrasisPatoroRandomSite4ProgressPrefab);
    }
    public void DeltaD31_402054RandomSite1Click()
    {
        CelestialBodiesDestination(10015, DeltaD31_402054RandomSite1ProgressPrefab);
    }
    public void DeltaD31_402054RandomSite2Click()
    {
        CelestialBodiesDestination(10016, DeltaD31_402054RandomSite2ProgressPrefab);
    }
    public void DeltaD31_402054RandomSite3Click()
    {
        CelestialBodiesDestination(10017, DeltaD31_402054RandomSite3ProgressPrefab);
    }
    public void DeltaD31_402054RandomSite4Click()
    {
        CelestialBodiesDestination(10018, DeltaD31_402054RandomSite4ProgressPrefab);
    }
    public void DeltaD31_402054RandomSite5Click()
    {
        CelestialBodiesDestination(10019, DeltaD31_402054RandomSite5ProgressPrefab);
    }
    public void JeratoO95_99024RandomSite1Click()
    {
        CelestialBodiesDestination(10020, JeratoO95_99024RandomSite1ProgressPrefab);
    }
    public void JeratoO95_99024RandomSite2Click()
    {
        CelestialBodiesDestination(10021, JeratoO95_99024RandomSite2ProgressPrefab);
    }
    public void JeratoO95_99024RandomSite3Click()
    {
        CelestialBodiesDestination(10022, JeratoO95_99024RandomSite3ProgressPrefab);
    }
    public void JeratoO95_99024RandomSite4Click()
    {
        CelestialBodiesDestination(10023, JeratoO95_99024RandomSite4ProgressPrefab);
    }
    public void JeratoO95_99024RandomSite5Click()
    {
        CelestialBodiesDestination(10024, JeratoO95_99024RandomSite5ProgressPrefab);
    }

    //항성 UI 목록
    public void StarToropioClick()
    {
        CelestialBodiesDestination(1, ToropioProgressPrefab);
    }
    public void StarRoro1Click()
    {
        CelestialBodiesDestination(2, Roro1ProgressPrefab);
    }
    public void StarRoro2Click()
    {
        CelestialBodiesDestination(3, Roro2ProgressPrefab);
    }
    public void StarSarisiClick()
    {
        CelestialBodiesDestination(4, SarisiProgressPrefab);
    }
    public void StarGarixClick()
    {
        CelestialBodiesDestination(5, GarixProgressPrefab);
    }
    public void StarSecrosClick()
    {
        CelestialBodiesDestination(6, SecrosProgressPrefab);
    }
    public void StarTeretosClick()
    {
        CelestialBodiesDestination(7, TeretosProgressPrefab);
    }
    public void StarMiniPopoClick()
    {
        CelestialBodiesDestination(8, MiniPopoProgressPrefab);
    }
    public void StarDeltaD31_4AClick()
    {
        CelestialBodiesDestination(9, DeltaD31_4AProgressPrefab);
    }
    public void StarDeltaD31_4BClick()
    {
        CelestialBodiesDestination(10, DeltaD31_4BProgressPrefab);
    }
    public void StarJeratoO95_7AClick()
    {
        CelestialBodiesDestination(11, JeratoO95_7AProgressPrefab);
    }
    public void StarJeratoO95_7BClick()
    {
        CelestialBodiesDestination(12, JeratoO95_7BProgressPrefab);
    }
    public void StarJeratoO95_14CClick()
    {
        CelestialBodiesDestination(13, JeratoO95_14CProgressPrefab);
    }
    public void StarJeratoO95_14DClick()
    {
        CelestialBodiesDestination(14, JeratoO95_14DProgressPrefab);
    }
    public void StarJeratoO95_OmegaClick()
    {
        CelestialBodiesDestination(15, JeratoO95_OmegaProgressPrefab);
    }

    //행성 UI 목록
    //토로피오 항성계
    public void PlanetSatariusGlessiaClick()
    {
        CelestialBodiesDestination(1001, SatariusGlessiaProgressPrefab);
    }
    public void PlanetAposisClick()
    {
        CelestialBodiesDestination(1002, AposisProgressPrefab);
    }
    public void PlanetToronoClick()
    {
        CelestialBodiesDestination(1003, ToronoProgressPrefab);
    }
    public void PlanetPlopa2Click()
    {
        CelestialBodiesDestination(1004, Plopa2ProgressPrefab);
    }
    public void PlanetVedes4Click()
    {
        CelestialBodiesDestination(1005, Vedes4ProgressPrefab);
    }

    //로로 항성계
    public void PlanetAronPeriClick()
    {
        CelestialBodiesDestination(1006, AronPeriProgressPrefab);
    }
    public void PlanetPapatus2Click()
    {
        CelestialBodiesDestination(1007, Papatus2ProgressPrefab);
    }
    public void PlanetPapatus3Click()
    {
        CelestialBodiesDestination(1008, Papatus3ProgressPrefab);
    }
    public void PlanetKyepotorosClick()
    {
        CelestialBodiesDestination(1009, KyepotorosProgressPrefab);
    }

    //사리시 항성계
    public void PlanetTratosClick()
    {
        CelestialBodiesDestination(1010, TratosProgressPrefab);
    }
    public void PlanetOclasisClick()
    {
        CelestialBodiesDestination(1011, TratosProgressPrefab);
    }
    public void PlanetDeriousHeriClick()
    {
        CelestialBodiesDestination(1012, DeriousHeriProgressPrefab);
    }

    //가릭스 항성계
    public void PlanetVeltrorexyClick()
    {
        CelestialBodiesDestination(1013, VeltrorexyProgressPrefab);
    }
    public void PlanetErixJeoqetaClick()
    {
        CelestialBodiesDestination(1014, ErixJeoqetaProgressPrefab);
    }
    public void PlanetQeepoClick()
    {
        CelestialBodiesDestination(1015, QeepoProgressPrefab);
    }
    public void PlanetCrownYosereClick()
    {
        CelestialBodiesDestination(1016, CrownYosereProgressPrefab);
    }

    //옥토크라시스 파토로 항성계
    public void PlanetOrosClick()
    {
        CelestialBodiesDestination(1017, OrosProgressPrefab);
    }
    public void PlanetJapetAgroneClick()
    {
        CelestialBodiesDestination(1018, JapetAgroneProgressPrefab);
    }
    public void PlanetXacro042351Click()
    {
        CelestialBodiesDestination(1019, Xacro042351ProgressPrefab);
    }

    //델타 D31-402054 항성계
    public void PlanetDeltaD31_2208Click()
    {
        CelestialBodiesDestination(1020, DeltaD31_2208ProgressPrefab);
    }
    public void PlanetDeltaD31_9523Click()
    {
        CelestialBodiesDestination(1021, DeltaD31_9523ProgressPrefab);
    }
    public void PlanetDeltaD31_12721Click()
    {
        CelestialBodiesDestination(1022, DeltaD31_12721ProgressPrefab);
    }

    //제라토 O95-99024 항성계
    public void PlanetJeratoO95_1125Click()
    {
        CelestialBodiesDestination(1023, JeratoO95_1125ProgressPrefab);
    }
    public void PlanetJeratoO95_2252Click()
    {
        CelestialBodiesDestination(1024, JeratoO95_2252ProgressPrefab);
    }
    public void PlanetJeratoO95_8510Click()
    {
        CelestialBodiesDestination(1025, JeratoO95_8510ProgressPrefab);
    }
}