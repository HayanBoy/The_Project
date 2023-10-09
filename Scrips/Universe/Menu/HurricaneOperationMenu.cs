using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HurricaneOperationMenu : MonoBehaviour
{
    [Header("스크립트")]
    public SystemMessages SystemMessages;
    public WordPrintMenu WordPrintMenu;
    public WordPrintSystem WordPrintSystem;
    public UniverseMapSystem UniverseMapSystem;
    public HurricaneMissionManager HurricaneMissionManager;
    public MultiFlagshipSystem MultiFlagshipSystem;
    public CashResourceSystem CashResourceSystem;
    public MissionWindowPlanetName MissionWindowPlanetName;
    public MissionWindowPlanetName MissionWindowPlanetName2;
    public TutorialSystem TutorialSystem;

    Coroutine hurricaneButtonAnimation;

    [Header("절차 및 유형")]
    public int TypeOfPlanetBattle; //행성전 유형 번호
    public int MenuStep; //메뉴 절차 번호

    [Header("창 및 메뉴")]
    public Text NameText; //테이블 상단의 천체 이름
    public GameObject StartBattleScenePrefab; //허리케인 전투 메뉴
    public GameObject HurricaneMenuPrefab; //허리케인 작전 메뉴

    [Header("탭")]
    public GameObject PlanetBattleTabPrefab;
    public GameObject ShipBattleTabPrefab;
    public GameObject PlanetManagementTabPrefab;
    public GameObject PlanetInformationTabPrefab;
    public GameObject PlanetBattleTabClick;
    public GameObject ShipBattleTabClick;
    public GameObject PlanetManagementTabClick;
    public GameObject PlanetInformationTabClick;
    public GameObject PlanetBattleTabClicked;
    public GameObject ShipBattleTabClicked;
    public GameObject PlanetManagementTabClicked;
    public GameObject PlanetInformationTabClicked;
    public GameObject TableList; //테이블 정보(탭1, 2, 4)
    public GameObject ScrollList; //스크롤 조절 정보(탭3)
    private bool TabClick;
    private int TabNumber;

    [Header("버튼들")]
    private bool ButtonClick = false;
    public GameObject HurricaneOperationButtonPrefab;
    public GameObject OperationStartButtonPrefab;
    public Image HurricaneOperationButtonImage;

    [Header("미션 테이블")]
    public List<GameObject> EnemyFlagship = new List<GameObject>(); //허리케인 작전용 적 기함 리스트
    public GameObject SelectedEnemyFlagship; //선택된 적 기함
    public MissionTable[] missionTableList;

    //테이블 별 미션
    [System.Serializable]
    public struct MissionTable
    {
        public GameObject MissionTablePrefab;
        public GameObject MissionTableClickPrefab;
        public Text AreaName;
        public Text MissionName;
        public int FinishSpawnNumber;

        //미션 종류
        public enum ShipMissionType
        {
            DestroySloriusFlagship,
            DestroyKantakriFlagship
        }
        public ShipMissionType shipMissionType;
    }

    [Header("미션 테이블 데이터")]
    public int MissionLevel;
    public int MissionArea;
    public int MissionType;
    public int FinishSpawnNumber;
    public int SelectedMissionTableNumber;

    [Header("버튼 상호작용")]
    public bool Operation1; //기함 침투전
    public bool Operation2; //행성전
    public bool Operation3; //행성 관리
    public int OperationOnline; //활성화 여부. 1 = 기함 침투전, 2 = 행성전, 3 = 행성 관리
    public GameObject HurricaneAnimePrefab;
    public GameObject HurricaneAnime;
    public GameObject PlanetAnime;
    private float AnimOnce1;
    private float AnimOnce2;
    private float AnimOnce3;
    private bool SubMachineGunOnline;

    public Text MissionName;
    public Text MissionExplainText; //행성 및 함선 침투전의 설명 부분

    [Header("튜토리얼")]
    public bool Tutorial = false;
    public bool FirstStart = false;

    [Header("사운드")]
    public AudioClip ButtonUIAudio;
    public AudioClip OKButtonAudio;

    //미션 테이블 클릭
    public void MissionTable1Click()
    {
        MissionTableClickRemove();
        missionTableList[0].MissionTableClickPrefab.SetActive(true);
        if (TabNumber == 2)
        {
            if (EnemyFlagship.Count > 0)
                SelectedEnemyFlagship = EnemyFlagship[0];
        }

        MissionLevel = missionTableList[0].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel;
        MissionArea = missionTableList[0].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionArea;
        MissionType = missionTableList[0].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType;
        FinishSpawnNumber = missionTableList[0].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber;
        SelectedMissionTableNumber = 0;
        OperationStartButtonPrefab.SetActive(true);
    }
    public void MissionTable2Click()
    {
        MissionTableClickRemove();
        missionTableList[1].MissionTableClickPrefab.SetActive(true);
        if (TabNumber == 2)
        {
            if (EnemyFlagship.Count > 0)
                SelectedEnemyFlagship = EnemyFlagship[1];
        }

        MissionLevel = missionTableList[1].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel;
        MissionArea = missionTableList[1].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionArea;
        MissionType = missionTableList[1].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType;
        FinishSpawnNumber = missionTableList[1].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber;
        SelectedMissionTableNumber = 1;
        OperationStartButtonPrefab.SetActive(true);
    }
    public void MissionTable3Click()
    {
        MissionTableClickRemove();
        missionTableList[2].MissionTableClickPrefab.SetActive(true);
        if (TabNumber == 2)
        {
            if (EnemyFlagship.Count > 0)
                SelectedEnemyFlagship = EnemyFlagship[2];
        }

        MissionLevel = missionTableList[2].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel;
        MissionArea = missionTableList[2].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionArea;
        MissionType = missionTableList[2].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType;
        FinishSpawnNumber = missionTableList[2].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber;
        SelectedMissionTableNumber = 2;
        OperationStartButtonPrefab.SetActive(true);
    }
    public void MissionTable4Click()
    {
        MissionTableClickRemove();
        missionTableList[3].MissionTableClickPrefab.SetActive(true);
        if (TabNumber == 2)
        {
            if (EnemyFlagship.Count > 0)
                SelectedEnemyFlagship = EnemyFlagship[3];
        }

        MissionLevel = missionTableList[3].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel;
        MissionArea = missionTableList[3].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionArea;
        MissionType = missionTableList[3].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType;
        FinishSpawnNumber = missionTableList[3].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber;
        SelectedMissionTableNumber = 3;
        OperationStartButtonPrefab.SetActive(true);
    }
    public void MissionTable5Click()
    {
        MissionTableClickRemove();
        missionTableList[4].MissionTableClickPrefab.SetActive(true);
        if (TabNumber == 2)
        {
            if (EnemyFlagship.Count > 0)
                SelectedEnemyFlagship = EnemyFlagship[4];
        }

        MissionLevel = missionTableList[4].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel;
        MissionArea = missionTableList[4].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionArea;
        MissionType = missionTableList[4].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType;
        FinishSpawnNumber = missionTableList[4].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber;
        SelectedMissionTableNumber = 4;
        OperationStartButtonPrefab.SetActive(true);
    }
    public void MissionTable6Click()
    {
        MissionTableClickRemove();
        missionTableList[5].MissionTableClickPrefab.SetActive(true);
        if (TabNumber == 2)
        {
            if (EnemyFlagship.Count > 0)
                SelectedEnemyFlagship = EnemyFlagship[5];
        }

        MissionLevel = missionTableList[5].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel;
        MissionArea = missionTableList[5].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionArea;
        MissionType = missionTableList[5].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType;
        FinishSpawnNumber = missionTableList[5].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber;
        SelectedMissionTableNumber = 5;
        OperationStartButtonPrefab.SetActive(true);
    }

    //미션 테이블 클릭 초기화
    void MissionTableClickRemove()
    {
        if (missionTableList[0].MissionTableClickPrefab.activeSelf == true)
            missionTableList[0].MissionTableClickPrefab.SetActive(false);
        else if (missionTableList[1].MissionTableClickPrefab.activeSelf == true)
            missionTableList[1].MissionTableClickPrefab.SetActive(false);
        else if (missionTableList[2].MissionTableClickPrefab.activeSelf == true)
            missionTableList[2].MissionTableClickPrefab.SetActive(false);
        else if (missionTableList[3].MissionTableClickPrefab.activeSelf == true)
            missionTableList[3].MissionTableClickPrefab.SetActive(false);
        else if (missionTableList[4].MissionTableClickPrefab.activeSelf == true)
            missionTableList[4].MissionTableClickPrefab.SetActive(false);
        else if (missionTableList[5].MissionTableClickPrefab.activeSelf == true)
            missionTableList[5].MissionTableClickPrefab.SetActive(false);
    }

    //미션 테이블 초기화
    void MissionTableRemove()
    {
        if (missionTableList[0].MissionTablePrefab.activeSelf == true)
            missionTableList[0].MissionTablePrefab.SetActive(false);
        if (missionTableList[1].MissionTablePrefab.activeSelf == true)
            missionTableList[1].MissionTablePrefab.SetActive(false);
        if (missionTableList[2].MissionTablePrefab.activeSelf == true)
            missionTableList[2].MissionTablePrefab.SetActive(false);
        if (missionTableList[3].MissionTablePrefab.activeSelf == true)
            missionTableList[3].MissionTablePrefab.SetActive(false);
        if (missionTableList[4].MissionTablePrefab.activeSelf == true)
            missionTableList[4].MissionTablePrefab.SetActive(false);
        if (missionTableList[5].MissionTablePrefab.activeSelf == true)
            missionTableList[5].MissionTablePrefab.SetActive(false);
    }

    //허리케인 작전 및 행성 관리 모드
    public void HurricaneOperationButtonClick()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (MenuStep == 0) //메뉴를 킨다.
            {
                if (Tutorial == true)
                {
                    Tutorial = false;
                    FirstStart = true;
                    StartCoroutine(TutorialSystem.TutorialWindowOpen(4));
                }
                MissionWindowPlanetName.AreaNamePrint(ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber);
                MissionWindowPlanetName2.AreaNamePrint(ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber);
                if (MultiFlagshipSystem.FlagshipListMode == true)
                    MultiFlagshipSystem.FlagshipListButtonClick();

                if (SubMachineGunOnline == false)
                    HurricaneAnime.GetComponent<Animator>().SetBool("isWalking", false);
                else
                    HurricaneAnime.GetComponent<Animator>().SetBool("subMachineGun idle", false);
                HurricaneAnime.GetComponent<Animator>().SetFloat("Move Type", 1);
                HurricaneAnime.GetComponent<Animator>().SetFloat("Move speed", 1.2f);
                HurricaneOperationButtonPrefab.GetComponent<Animator>().SetBool("Active, Hurricane operation", true);

                MenuStep = 1;
                HurricaneMenuPrefab.SetActive(true);
                OperationStartButtonPrefab.SetActive(false);

                if (OperationOnline == 1) //기함 침투전
                {
                    TabNumber = 2;
                    TabSelect();
                    HurricaneMissionManager.SearchEnemyFlagshipMissionList();
                }
                else if(OperationOnline == 2) //행성전
                {
                    MissionTableClickRemove();
                    TabNumber = 1;
                    TabSelect();
                    HurricaneMissionManager.SearchMissionListAtPlanet();
                }
                else if (OperationOnline == 3) //행성 관리
                {
                    TabNumber = 3;
                    TabSelect();
                    CashResourceSystem.SliderOn = true;
                    CashResourceSystem.PlanetResourceBring(ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber);
                }
                MissionExplainWindow();
            }
            else if (MenuStep > 0) //메뉴를 끈다.
            {
                OperationStartButtonPrefab.SetActive(false);
                if (SubMachineGunOnline == false)
                    HurricaneAnime.GetComponent<Animator>().SetBool("isWalking", true);
                else
                    HurricaneAnime.GetComponent<Animator>().SetBool("subMachineGun idle", true);
                HurricaneAnime.GetComponent<Animator>().SetFloat("Move Type", 0);
                HurricaneOperationButtonPrefab.GetComponent<Animator>().SetBool("Active, Hurricane operation", false);

                TabNumber = 0;
                PlanetBattleTabClicked.SetActive(false);
                ShipBattleTabClicked.SetActive(false);
                PlanetManagementTabClicked.SetActive(false);
                PlanetInformationTabClicked.SetActive(false);
                CashResourceSystem.SliderOn = false;

                MenuStep = 0;
                MissionTableRemove();
                MissionTableClickRemove();
                EnemyFlagship.Clear();
                HurricaneMenuPrefab.SetActive(false);
            }
        }
    }
    public void HurricaneOperationButtonDown()
    {
        ButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        HurricaneOperationButtonPrefab.GetComponent<Animator>().SetBool("Click, Hurricane operation", true);
    }
    public void HurricaneOperationButtonUp()
    {
        if (ButtonClick == true)
        {
            HurricaneOperationButtonPrefab.GetComponent<Animator>().SetBool("Click, Hurricane operation", false);
        }
        ButtonClick = false;
    }
    public void HurricaneOperationButtonEnter()
    {
        if (ButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            HurricaneOperationButtonPrefab.GetComponent<Animator>().SetBool("Click, Hurricane operation", true);
        }
    }
    public void HurricaneOperationButtonExit()
    {
        if (ButtonClick == true)
        {
            HurricaneOperationButtonPrefab.GetComponent<Animator>().SetBool("Click, Hurricane operation", false);
        }
    }

    //허리케인 작전 시작 버튼(보병전으로 넘어가는 버튼)
    public void OperationStartButtonClick()
    {
        MenuStep = 2;
        WordPrintMenu.HurricaneStart(1);
        StartCoroutine(SystemMessages.MessageOn(2, 0));
    }
    public void OperationStartButtonDown()
    {
        ButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
        OperationStartButtonPrefab.GetComponent<Animator>().SetFloat("Click confirm, Progress Button", 1);
    }
    public void OperationStartButtonUp()
    {
        if (ButtonClick == true)
        {
            OperationStartButtonPrefab.GetComponent<Animator>().SetFloat("Click confirm, Progress Button", 2);
        }
        ButtonClick = false;
    }
    public void OperationStartButtonEnter()
    {
        if (ButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
            OperationStartButtonPrefab.GetComponent<Animator>().SetFloat("Click confirm, Progress Button", 1);
        }
    }
    public void OperationStartButtonExit()
    {
        if (ButtonClick == true)
        {
            OperationStartButtonPrefab.GetComponent<Animator>().SetFloat("Click confirm, Progress Button", 0);
        }
    }

    //허리케인 무기 선택 메뉴 씬으로 이동
    public IEnumerator StartHurricaneOperation()
    {
        MenuStep = 0;
        if (FirstStart == true) //첫 전투 투입에만 적용
        {
            BattleSave.Save1.Tutorial = 1;
            BattleSave.Save1.PlanetTutorial = 1;
        }
        if (WeaponUnlockManager.instance.MBCA79IronHurricaneUnlock == true && BattleSave.Save1.MBCA79Tutorial == true) //MBCA-79를 잠금 해제한 뒤, 처음 사용했을 때에만 적용
            BattleSave.Save1.Tutorial = 2;
        BattleSave.Save1.MissionLevel = MissionLevel;
        BattleSave.Save1.MissionArea = MissionArea;
        BattleSave.Save1.MissionType = MissionType;
        BattleSave.Save1.FinishSpawnNumber = FinishSpawnNumber;
        BattleSave.Save1.SelectedMissionTableNumber = SelectedMissionTableNumber;
        BattleSave.Save1.MissionPlanetNumber = ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber;
        if (SelectedEnemyFlagship != null)
            SelectedEnemyFlagship.GetComponent<EnemyShipLevelInformation>().Selected = true;

        WordPrintSystem.UCCISBootingPrint(100);
        UniverseMapSystem.MenuBooting.GetComponent<Animator>().SetFloat("Delta Strike Group Booting, UCCIS mark", 1);
        yield return new WaitForSeconds(0.25f);
        HurricaneAnime.GetComponent<Animator>().SetFloat("Move Type", 0);
        HurricaneAnime.GetComponent<Animator>().SetBool("Player Turn off", false);
        HurricaneAnime.GetComponent<Animator>().SetBool("Player landing", false);
        yield return new WaitForSeconds(0.25f);
        StartBattleScenePrefab.GetComponent<SceneSaveStart>().enabled = true;
    }

    //탭 선택
    public void TabSelect()
    {
        PlanetBattleTabClicked.SetActive(false);
        ShipBattleTabClicked.SetActive(false);
        PlanetManagementTabClicked.SetActive(false);
        PlanetInformationTabClicked.SetActive(false);

        TableList.SetActive(false);
        ScrollList.SetActive(false);
        CashResourceSystem.SliderOn = false;

        if (TabNumber == 1)
        {
            PlanetBattleTabClicked.SetActive(true);
            TableList.SetActive(true);
            HurricaneMissionManager.SearchMissionListAtPlanet();
        }
        else if (TabNumber == 2)
        {
            ShipBattleTabClicked.SetActive(true);
            TableList.SetActive(true);
            HurricaneMissionManager.SearchEnemyFlagshipMissionList();
        }
        else if (TabNumber == 3)
        {
            PlanetManagementTabClicked.SetActive(true);
            ScrollList.SetActive(true);
            CashResourceSystem.SliderOn = true;
            CashResourceSystem.PlanetResourceBring(ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber);
        }
        else if (TabNumber == 4)
        {
            PlanetInformationTabClicked.SetActive(true);
            TableList.SetActive(true);
        }
    }
    private void TabDown()
    {
        TabClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        if (TabNumber == 1)
            PlanetBattleTabClick.SetActive(true);
        else if (TabNumber == 2)
            ShipBattleTabClick.SetActive(true);
        else if (TabNumber == 3)
            PlanetManagementTabClick.SetActive(true);
        else if (TabNumber == 4)
            PlanetInformationTabClick.SetActive(true);
    }
    public void PlanetBattleTabDown()
    {
        TabNumber = 1;
        TabDown();
    }
    public void ShipBattleTabDown()
    {
        TabNumber = 2;
        TabDown();
    }
    public void PlanetManagementTabDown()
    {
        TabNumber = 3;
        TabDown();
    }
    public void PlanetInformationTabDown()
    {
        TabNumber = 4;
        TabDown();
    }
    public void TabUp()
    {
        if (TabClick == true)
        {
            if (TabNumber == 1)
                PlanetBattleTabClick.SetActive(false);
            else if (TabNumber == 2)
                ShipBattleTabClick.SetActive(false);
            else if (TabNumber == 3)
                PlanetManagementTabClick.SetActive(false);
            else if (TabNumber == 4)
                PlanetInformationTabClick.SetActive(false);
        }
        TabClick = false;
    }
    public void TabEnter()
    {
        if (TabClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            if (TabNumber == 1)
                PlanetBattleTabClick.SetActive(true);
            else if (TabNumber == 2)
                ShipBattleTabClick.SetActive(true);
            else if (TabNumber == 3)
                PlanetManagementTabClick.SetActive(true);
            else if (TabNumber == 4)
                PlanetInformationTabClick.SetActive(true);
        }
    }
    public void TabExit()
    {
        if (TabClick == true)
        {
            if (TabNumber == 1)
                PlanetBattleTabClick.SetActive(false);
            else if (TabNumber == 2)
                ShipBattleTabClick.SetActive(false);
            else if (TabNumber == 3)
                PlanetManagementTabClick.SetActive(false);
            else if (TabNumber == 4)
                PlanetInformationTabClick.SetActive(false);
        }
    }

    void Start()
    {
        HurricaneMainWeaponActive();
    }

    void Update()
    {
        if (ShipManager.instance.SelectedFlagShip != null && ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null) //허리케인 모드 설정
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<HurricaneOperationForFlagship>().EnemyFlagship.Count > 0) //적 기함 포착시, 허리케인 버튼 활성화
            {
                OperationOnline = 1;
                Operation1 = true;
                ShipBattleTabPrefab.SetActive(true);
            }
            else
            {
                Operation1 = false;
                ShipBattleTabPrefab.SetActive(false);
            }
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber > 1000)
            {
                //미 해방된 행성 및 감염된 행성에서 허리케인 버튼 활성화
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().StatePlanet == 3 || ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().StatePlanet == 4)
                {
                    OperationOnline = 2;
                    Operation2 = true;
                    PlanetBattleTabPrefab.SetActive(true);
                }
                else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().StatePlanet != 3 && ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().StatePlanet != 4)
                {
                    Operation2 = false;
                    PlanetBattleTabPrefab.SetActive(false);
                }
                //해방된 행성에서 행성 버튼 활성화
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlanetType == 1)
                {
                    if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().StatePlanet == 1 || ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().StatePlanet == 2)
                    {
                        OperationOnline = 3;
                        Operation3 = true;
                        PlanetManagementTabPrefab.SetActive(true);
                    }
                    else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().StatePlanet != 1 && ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().StatePlanet != 2)
                    {
                        Operation3 = false;
                        PlanetManagementTabPrefab.SetActive(false);
                    }
                }
                else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlanetType != 1)
                {
                    Operation3 = false;
                    PlanetManagementTabPrefab.SetActive(false);
                }
            }
            //적 기함 및 행성이 없는 우주 공간(항성 포함)에 있을 시 허리케인 버튼 비활성화
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<HurricaneOperationForFlagship>().EnemyFlagship.Count <= 0 &&
                ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber < 1000)
            {
                OperationOnline = 0;
                Operation1 = false;
                Operation2 = false;
                Operation3 = false;
            }
        }

        if (Operation1 == true || Operation2 == true) //허리케인 버튼 활성화
        {
            if (AnimOnce1 == 0)
            {
                AnimOnce1 += Time.deltaTime;
                AnimOnce3 = 0;
                HurricaneOperationButtonImage.raycastTarget = true;
                hurricaneButtonAnimation = StartCoroutine(HurricaneButtonAnimation());
            }
        }
        else if (Operation1 == false && Operation2 == false)  //허리케인 버튼 비활성화
        {
            HurricaneAnime.GetComponent<Animator>().SetFloat("Move Type", 0);
            HurricaneAnime.GetComponent<Animator>().SetBool("Player Turn off", true);
            HurricaneAnime.GetComponent<Animator>().SetBool("Player landing", false);
            AnimOnce1 = 0;
        }
        if (Operation3 == true) //행성 모드 버튼 활성화
        {
            if (AnimOnce2 == 0)
            {
                AnimOnce2 += Time.deltaTime;
                AnimOnce3 = 0;
                HurricaneOperationButtonImage.raycastTarget = true;
                PlanetAnime.GetComponent<Animator>().SetFloat("Active, Hurricane operation planet", 1);
                if (HurricaneOperationButtonPrefab.GetComponent<Animator>().GetFloat("Online, Hurricane operation") != 2)
                    HurricaneOperationButtonPrefab.GetComponent<Animator>().SetFloat("Online, Hurricane operation", 2);
                else if(UniverseMapSystem.UniverseMapEnabled == true)
                    HurricaneOperationButtonPrefab.GetComponent<Animator>().SetFloat("Online, Hurricane operation", 0);
                StartCoroutine(HurricaneButtonAnimationOff());
            }
        }
        else //행성 모드 버튼 비활성화
        {
            PlanetAnime.GetComponent<Animator>().SetFloat("Active, Hurricane operation planet", 2);
            AnimOnce2 = 0;
        }
        if (Operation1 == false && Operation2 == false && Operation3 == false) //허리케인 버튼 비활성화
        {
            if (AnimOnce3 == 0)
            {
                AnimOnce3 += Time.deltaTime;
                if (hurricaneButtonAnimation != null)
                    StopCoroutine(hurricaneButtonAnimation);
                if (MenuStep > 0)
                    HurricaneOperationButtonClick();
            }
            HurricaneOperationButtonImage.raycastTarget = false;
            HurricaneOperationButtonPrefab.GetComponent<Animator>().SetFloat("Online, Hurricane operation", 1);
        }

        //테이블의 적 기함 갯수와 선택된 기함에서의 적 기함 갯수가 다를 경우, 다시 재설정을 한다.
        if (MenuStep > 0)
        {
            if (EnemyFlagship.Count != ShipManager.instance.SelectedFlagShip[0].GetComponent<HurricaneOperationForFlagship>().EnemyFlagship.Count)
            {
                MissionTableRemove();
                EnemyFlagship.Clear();
                HurricaneMissionManager.SearchEnemyFlagshipMissionList();
            }
        }
    }

    //허리케인 버튼 활성화
    IEnumerator HurricaneButtonAnimation()
    {
        HurricaneAnime.GetComponent<Animator>().SetBool("Player Turn off", false);
        HurricaneOperationButtonPrefab.GetComponent<Animator>().SetFloat("Online, Hurricane operation", 2);
        HurricaneAnime.GetComponent<Animator>().SetBool("Player landing", true);
        yield return new WaitForSeconds(0.25f);
        HurricaneOperationButtonPrefab.GetComponent<Animator>().SetFloat("Online, Hurricane operation", 0);
        yield return new WaitForSeconds(0.35f);
        HurricaneAnime.GetComponent<Animator>().SetBool("Player landing", false);
    }

    IEnumerator HurricaneButtonAnimationOff()
    {
        yield return new WaitForSeconds(0.25f);
        HurricaneOperationButtonPrefab.GetComponent<Animator>().SetFloat("Online, Hurricane operation", 0);
    }

    //허리케인 버튼의 선택된 무기 활성화
    public void HurricaneMainWeaponActive()
    {
        if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 1)
        {
            SubMachineGunOnline = false;
            HurricaneAnime.GetComponent<Animator>().SetFloat("subGun active", 0);
            HurricaneAnime.GetComponent<Animator>().SetFloat("subGun active2", 0);
            HurricaneAnime.GetComponent<Animator>().SetFloat("Gun active", 1);
        }
        else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 1000)
        {
            SubMachineGunOnline = false;
            HurricaneAnime.GetComponent<Animator>().SetFloat("subGun active", 0);
            HurricaneAnime.GetComponent<Animator>().SetFloat("subGun active2", 0);
            HurricaneAnime.GetComponent<Animator>().SetFloat("Gun active", 1000);
        }
        else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 2000)
        {
            SubMachineGunOnline = false;
            HurricaneAnime.GetComponent<Animator>().SetFloat("subGun active", 0);
            HurricaneAnime.GetComponent<Animator>().SetFloat("subGun active2", 0);
            HurricaneAnime.GetComponent<Animator>().SetFloat("Gun active", 2000);
        }
        else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 0)
        {
            SubMachineGunOnline = true;
            HurricaneAnime.GetComponent<Animator>().SetFloat("Gun active", 0);
            HurricaneAnime.GetComponent<Animator>().SetFloat("subGun active", 1);
            HurricaneAnime.GetComponent<Animator>().SetFloat("subGun active2", 1);
        }
    }

    //미션 설명창
    void MissionExplainWindow()
    {
        if (BattleSave.Save1.LanguageType == 1)
        {
            //자원 행성 해방
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1001)
            {
                MissionName.text = string.Format("Satarius Glessia Commercial Planet");
                MissionExplainText.text = string.Format("Following resources will be gained if you liberate this planet. \n\nMax 12 Glopa per 5 seconds\nMax 4 Construction Resource per 5 seconds\n\nGlopaoros limit accpet : 10000\nConstruction Resource limit accpet : 10000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1003)
            {
                MissionName.text = string.Format("Torono Mining Planet");
                MissionExplainText.text = string.Format("Following resources will be gained if you liberate this planet. \n\nMax 4 Glopa per 5 seconds\nMax 12 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1006)
            {
                MissionName.text = string.Format("Aron Peri Commercial Planet");
                MissionExplainText.text = string.Format("Following resources will be gained if you liberate this planet. \n\nMax 14 Glopa per 5 seconds\nMax 4 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1007)
            {
                MissionName.text = string.Format("Papatus II Mining Planet");
                MissionExplainText.text = string.Format("Following resources will be gained if you liberate this planet. \n\nMax 4 Glopa per 5 seconds\nMax 16 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1011)
            {
                MissionName.text = string.Format("Oclasis Mining Planet");
                MissionExplainText.text = string.Format("Following resources will be gained if you liberate this planet. \n\nMax 4 Glopa per 5 seconds\nMax 18 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1013)
            {
                MissionName.text = string.Format("Veltrorexy Commercial Planet");
                MissionExplainText.text = string.Format("Following resources will be gained if you liberate this planet. \n\nMax 20 Glopa per 5 seconds\nMax 4 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1014)
            {
                MissionName.text = string.Format("Erix Jeoqeta Commercial Planet");
                MissionExplainText.text = string.Format("Following resources will be gained if you liberate this planet. \n\nMax 22 Glopa per 5 seconds\nMax 4 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1015)
            {
                MissionName.text = string.Format("Qeepo Mining Planet");
                MissionExplainText.text = string.Format("Following resources will be gained if you liberate this planet. \n\nMax 4 Glopa per 5 seconds\nMax 24 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1017)
            {
                MissionName.text = string.Format("Oros Commercial Planet");
                MissionExplainText.text = string.Format("Following resources will be gained if you liberate this planet. \n\nMax 26 Glopa per 5 seconds\nMax 4 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1019)
            {
                MissionName.text = string.Format("Xacro 042351 Mining Planet");
                MissionExplainText.text = string.Format("Following resources will be gained if you liberate this planet. \n\nMax 4 Glopa per 5 seconds\nMax 28 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
            }

            //연구 행성 해방
            else if(ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1002)
            {
                MissionName.text = string.Format("Aposis Research Planet");
                MissionExplainText.text = string.Format("Following weapons will unlock if you liberate this planet. \n\nShip : Shield ship\nDelta Hurricane Weapon : Change heavy weapon(Hydra-56 Armor Piercing Discarding Sabot)\nDelta Hurricane Weapon : Sub gea(OSEH-302 Widow Hire guided missile)\n\nResearch : Logistics support class 1");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1005)
            {
                MissionName.text = string.Format("Vedes VI Research Planet");
                MissionExplainText.text = string.Format("Following weapons will unlock if you liberate this planet. \n\nDelta Hurricane Weapon : Change heavy weapon(MEAG railgun)\nDelta Hurricane Weapon : Main weapon(CGD-27 Pillishion Submachine gun)\nDelta Hurricane Weapon : Grenade(VM-5 AEG)\n\nResearch : Delta Hurricane Hit point class 1");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1008)
            {
                MissionName.text = string.Format("Papatus III Research Planet");
                MissionExplainText.text = string.Format("Following weapons will unlock if you liberate this planet. \n\nShip weapon : Over jump\nFleet support slot : First slot\nDelta Hurricane Weapon : Change heavy weapon(Arthes L-775 Charge laser)\nDelta Hurricane Weapon : Main weapon(DP-9007 Sniper rifle)\nShip support : Heavy weapon(M3078 Mini gun)\n\nResearch : Sub gear type class 1\nResearch : Grenade type class 1");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1012)
            {
                MissionName.text = string.Format("Derious Heri Research Planet");
                MissionExplainText.text = string.Format("Following weapons will unlock if you liberate this planet. \n\nShip weapon : Delta Needle-42 Halist multi missile\nDelta Hurricane Weapon : Change heavy weapon(UGG 98 Gravity cannon)\nDelta Hurricane Weapon : Main weapon(DS-65 Shotgun)\nShip support : Bombardment(PGM 1036 Scalet Hawk cruise missile)\n\nResearch : Flagship armor system class 1\nResearch : Formation ship armor system class 1\nResearch : Tactical ship armor system class 1\nResearch : Cannon type class 1\nResearch : Missile type class 1\nResearch : Carrier-based aircraft type class 1\nResearch : Change heavy weapon class 1\nResearch : Logistics support class 2\nResearch : Bombardment support class 1");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1016)
            {
                MissionName.text = string.Format("Crown Yosere Research Planet");
                MissionExplainText.text = string.Format("Following weapons will unlock if you liberate this planet. \n\nShip : Carrier\nShip support : Heavy weapon(ASC 365 flamethrower)\n\nResearch : Hurricane Hit point class 2\nResearch : Assault rifle type class 1\nResearch : Shotgun type class 1\nResearch : Sniper rifle type class 1\nResearch : Submachine gun type class 1\nResearch : Grenade type class 2\nResearch : Heavy weapon support class 1");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1018)
            {
                MissionName.text = string.Format("Japet Agrone Research Planet");
                MissionExplainText.text = string.Format("Following weapons will unlock if you liberate this planet. \n\nFleet support slot : Second slot\nShip support : Vehicle(MBCA-79 Iron Hurricane)\n\nResearch : Flagship strike class 1\nResearch : Fleet strike class 1\nResearch : Sub gear type class 2\nResearch : Vehicle support class 1\nResearch : Bombardment support class 2");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1020)
            {
                MissionName.text = string.Format("Delta D31-2208 Military Research Planet");
                MissionExplainText.text = string.Format("Following weapons will unlock if you liberate this planet. \n\nResearch : Flagship armor system class 2\nResearch : Formation ship armor system class 2\nResearch : Tactical ship armor system class 2\nResearch : Cannon type class 2\nResearch : Missile type class 2\nResearch : Carrier-based aircraft type class 2\nResearch : Grenade type class 3\nResearch : Change heavy weapon class 2\nResearch : Heavy weapon support class 2");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1022)
            {
                MissionName.text = string.Format("Delta D31-12721 Military Research Planet");
                MissionExplainText.text = string.Format("Following weapons will unlock if you liberate this planet. \n\nResearch : Flagship strike class 2\nResearch : Fleet strike class 2\nResearch : Hurricane Hit point class 3\nResearch : Assault rifle type class 2\nResearch : Shotgun type class 2\nResearch : Sniper rifle type class 2\nResearch : Submachine gun type class 2\nResearch : Logistics support class 3\nResearch : Vehicle support class 2");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1024)
            {
                MissionName.text = string.Format("Jerato O95-2252 Military Research Planet");
                MissionExplainText.text = string.Format("Following weapons will unlock if you liberate this planet. \n\nFleet support slot : Third slot\n\nResearch : Flagship armor system class 3\nResearch : Formation ship armor system class 3\nResearch : Tactical ship armor system class 3\nResearch : Cannon type class 3\nResearch : Missile type class 3\nResearch : Carrier-based aircraft type class 3\nResearch : Sub gear type class 3\nResearch : Change heavy weapon class 3\nResearch : Bombardment support class 3");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1025)
            {
                MissionName.text = string.Format("Jerato O95-8510 Military Research Planet");
                MissionExplainText.text = string.Format("Following weapons will unlock if you liberate this planet. \n\nResearch : Flagship strike class 3\nResearch : Fleet strike class 3\nResearch : Assault rifle type class 3\nResearch : Shotgun type class 3\nResearch : Sniper rifle type class 3\nResearch : Submachine gun type class 3\nResearch : Heavy weapon support class 3\nResearch : Vehicle support class 3");
            }

            //거주 행성 해방
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1004)
            {
                MissionName.text = string.Format("Plopa II Residence Planet");
                MissionExplainText.text = string.Format("You can check missions that can help your operation if you liberate this planet.(Currently unavailable, available in upcoming update)");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1009)
            {
                MissionName.text = string.Format("Kyepotoros Residence Planet");
                MissionExplainText.text = string.Format("You can check missions that can help your operation if you liberate this planet.(Currently unavailable, available in upcoming update)");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1010)
            {
                MissionName.text = string.Format("Tratos Residence Planet");
                MissionExplainText.text = string.Format("You can check missions that can help your operation if you liberate this planet.(Currently unavailable, available in upcoming update)");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1021)
            {
                MissionName.text = string.Format("Delta D31-9523 Military Base Planet");
                MissionExplainText.text = string.Format("You can check missions that can help your operation if you liberate this planet.(Currently unavailable, available in upcoming update)");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1023)
            {
                MissionName.text = string.Format("Jerato O95-1125 Military Base Planet");
                MissionExplainText.text = string.Format("You can check missions that can help your operation if you liberate this planet.(Currently unavailable, available in upcoming update)");
            }
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            //자원 행성 해방
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1001)
            {
                MissionName.text = string.Format("사타리우스 글래시아 상업 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 12 글로파\n5초당 최대 4 건설 재료\n\n글로파오로스 한도 승인 : 10000\n건설 재료 한도 승인 : 10000");
            }
            else if(ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1003)
            {
                MissionName.text = string.Format("토로노 채광 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 4 글로파\n5초당 최대 12 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1006)
            {
                MissionName.text = string.Format("아론 페리 상업 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 14 글로파\n5초당 최대 4 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1007)
            {
                MissionName.text = string.Format("파파투스 II 채광 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 4 글로파\n5초당 최대 16 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1011)
            {
                MissionName.text = string.Format("오클라시스 채광 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 4 글로파\n5초당 최대 18 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1013)
            {
                MissionName.text = string.Format("벨트로렉시 상업 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 20 글로파\n5초당 최대 4 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1014)
            {
                MissionName.text = string.Format("에릭스 제퀘타 상업 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 22 글로파\n\n5초당 최대 4 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1015)
            {
                MissionName.text = string.Format("퀴이포 채광 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 4 글로파\n5초당 최대 24 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1017)
            {
                MissionName.text = string.Format("오로스 상업 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 26 글로파\n5초당 최대 4 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1019)
            {
                MissionName.text = string.Format("자크로 042351 채광 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 4 글로파\n5초당 최대 28 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
            }

            //연구 행성 해방
            else if(ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1002)
            {
                MissionName.text = string.Format("아포시스 연구 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n함선 : 방패함\n델타 허리케인 무기 : 체인지 중화기(히드라-56 분리 철갑포)\n델타 허리케인 무기 : 보조 장비(OSEH-302 위도우 하이어 추적 미사일)\n\n연구 : 보급 지원 1 등급");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1005)
            {
                MissionName.text = string.Format("베데스 VI 연구 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n델타 허리케인 무기 : 체인지 중화기(MEAG 레일건)\n델타 허리케인 무기 : 주무기(CGD-27 필리시온 기관단총)\n델타 허리케인 무기 : 수류탄(VM-5 AEG)\n\n연구 : 델타 허리케인 체력 1 등급");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1008)
            {
                MissionName.text = string.Format("파파투스 III 연구 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n함선 무기 : 초과 점프\n함대 지원 슬롯 : 첫 번째 슬롯\n델타 허리케인 무기 : 체인지 중화기(아레스 L-775 충전 레이져)\n델타 허리케인 무기 : 주무기(DP-9007 저격총)\n함선 지원 : 중화기(M3078 미니건)\n\n연구 : 보조 장비 타입 1 등급\n연구 : 수류탄 타입 1 등급");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1012)
            {
                MissionName.text = string.Format("데리우스 헤리 연구 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n함선 무기 : 델타 니들-42 할리스트 멀티 미사일\n델타 허리케인 무기 : 체인지 중화기(UGG 98 중력포)\n델타 허리케인 무기 : 주무기(DS-65 샷건)\n함선 지원 : 폭격(PGM 1036 스칼렛 호크 순항 미사일)\n\n연구 : 기함 장갑 시스템 1 등급\n연구 : 편대함 장갑 시스템 1 등급\n연구 : 전략함 장갑 시스템 1 등급\n연구 : 주포 타입 1 등급\n연구 : 마사일 타입 1 등급\n연구 : 함재기 타입 1 등급\n연구 : 체인지 중화기 1 등급\n연구 : 보급 지원 2 등급\n연구 : 폭격 지원 1 등급");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1016)
            {
                MissionName.text = string.Format("크라운 요세레 연구 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n함선 : 우주모함\n함선 지원 : 중화기(ASC 365 화염방사기)\n\n연구 : 델타 허리케인 체력 2 등급\n연구 : 돌격 소총 타입 1 등급\n연구 : 샷건 타입 1 등급\n연구 : 저격총 타입 1 등급\n연구 : 기관단총 타입 1 등급\n연구 : 수류탄 타입 2 등급\n연구 : 중화기 지원 1 등급");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1018)
            {
                MissionName.text = string.Format("자펫 아그로네 연구 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n함대 지원 슬롯 : 두 번째 슬롯\n함선 지원 : 탑승 차량(MBCA-79 아이언 허리케인)\n\n연구 : 기함 공격 1 등급\n연구 : 함대 공격 1 등급\n연구 : 보조 장비 타입 2 등급\n연구 : 탑승 차량 지원 1 등급\n연구 : 폭격 지원 2 등급");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1020)
            {
                MissionName.text = string.Format("델타 D31-2208 군사 연구 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n연구 : 기함 장갑 시스템 2 등급\n연구 : 편대함 장갑 시스템 2 등급\n연구 : 전략함 장갑 시스템 2 등급\n연구 : 주포 타입 2 등급\n연구 : 마사일 타입 2 등급\n연구 : 함재기 타입 2 등급\n연구 : 수류탄 타입 3 등급\n연구 : 체인지 중화기 2 등급\n연구 : 중화기 지원 2 등급");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1022)
            {
                MissionName.text = string.Format("델타 D31-12721 군사 연구 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n연구 : 기함 공격 2 등급\n연구 : 함대 공격 2 등급\n연구 : 델타 허리케인 체력 3 등급\n연구 : 돌격 소총 타입 2 등급\n연구 : 샷건 타입 2 등급\n연구 : 저격총 타입 2 등급\n연구 : 기관단총 타입 2 등급\n연구 : 보급 지원 3 등급\n연구 : 탑승 차량 지원 2 등급");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1024)
            {
                MissionName.text = string.Format("제라토 O95-2252 군사 연구 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n함대 지원 슬롯 : 세 번째 슬롯\n\n연구 : 기함 장갑 시스템 3 등급\n연구 : 편대함 장갑 시스템 3 등급\n연구 : 전략함 장갑 시스템 3 등급\n연구 : 주포 타입 3 등급\n연구 : 마사일 타입 3 등급\n연구 : 함재기 타입 3 등급\n연구 : 보조 장비 타입 3 등급\n연구 : 체인지 중화기 3 등급\n연구 : 폭격 지원 3 등급");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1025)
            {
                MissionName.text = string.Format("제라토 O95-8510 군사 연구 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n연구 : 기함 공격 3 등급\n연구 : 함대 공격 3 등급\n연구 : 돌격 소총 타입 3 등급\n연구 : 샷건 타입 3 등급\n연구 : 저격총 타입 3 등급\n연구 : 기관단총 타입 3 등급\n연구 : 중화기 지원 3 등급\n연구 : 탑승 차량 지원 3 등급");
            }

            //거주 행성 해방
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1004)
            {
                MissionName.text = string.Format("플로파 II 거주 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 작전에 도움이 될 수 있는 임무를 확인할 수 있습니다.(현재 사용 불가능하며, 다음 업데이트에서 가능)");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1009)
            {
                MissionName.text = string.Format("키예포토로스 거주 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 작전에 도움이 될 수 있는 임무를 확인할 수 있습니다.(현재 사용 불가능하며, 다음 업데이트에서 가능)");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1010)
            {
                MissionName.text = string.Format("트라토스 거주 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 작전에 도움이 될 수 있는 임무를 확인할 수 있습니다.(현재 사용 불가능하며, 다음 업데이트에서 가능)");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1021)
            {
                MissionName.text = string.Format("델타 D31-9523 군사기지 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 작전에 도움이 될 수 있는 임무를 확인할 수 있습니다.(현재 사용 불가능하며, 다음 업데이트에서 가능)");
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1023)
            {
                MissionName.text = string.Format("제라토 O95-1125 군사기지 행성");
                MissionExplainText.text = string.Format("이 행성을 해방시, 작전에 도움이 될 수 있는 임무를 확인할 수 있습니다.(현재 사용 불가능하며, 다음 업데이트에서 가능)");
            }
        }
    }
}